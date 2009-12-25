using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

using Com.Martin.SMS.Data;

namespace Com.Martin.SMS.Common {

    public static class CommandProcessor {

        public static Com.Martin.SMS.Data.SMSOutgoing ProcessRequest(Com.Martin.SMS.Data.SMSIncoming Request) {
            
            //parsing message text split ;  array[0]=type
            // array[1]=name , array[2]...n = parameter list
            char[] cSplitter = {';'};
            String[] arrMSG = Request.MessageText.Split(cSplitter);
            
            //initialisasi sms config loader
            ConfigLoader cfgLoader = new ConfigLoader();
            
            //panggil configloader.CreateRequestCommand() = request
            Com.Martin.SMS.Command.AbstractRequest command = cfgLoader.CreateRequestCommand(arrMSG[0], arrMSG[1]);

            // set request.parameter = parameterlist
            for (int i = 2 ; i < arrMSG.Length ; i++)
            {
                command.Parameters.Add(arrMSG[i]);
            }

            // set request.incoming = smsincoming
            command.RequestSMS= Request;

            // smsoutgoing = request.execute
            SMSOutgoing outSMS = command.Execute();

            // helper.SaveOutgoingMessage
            SMSHelper.SaveOutgoingMessage(ref outSMS);

            return outSMS;
        }

        public static void ProcessBroadcast() {
           
            List<BroadcastScheduler> lstBroadCastSch = SMSHelper.GetBroadcastScheduler(DateTime.Now);
            for (int i = 0; i < lstBroadCastSch.Count; i++) 
            {
                ConfigLoader cfg = new ConfigLoader();
                Com.Martin.SMS.Command.AbstractBroadcast brc = cfg.CreateBroadcastCommand(lstBroadCastSch[i].RegisterType, lstBroadCastSch[i].RegisterName);
                List<SMSOutgoing> lstOut = brc.Execute();
                SMSHelper.SaveBroadcastMessage(lstOut);

                BroadcastScheduler schd = lstBroadCastSch[i];
                schd.CurrentLoop += 1;
                schd.LastExecuteTime = schd.NextExecuteTime;
                schd.NextExecuteTime = schd.NextExecuteTime.AddDays(schd.IntervalDays);

                SMSHelper.SaveBroadcastScheduler( schd);
            }
        }

    }

    public class SMSHelper {
        private static MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=rahasia;database=catering;Allow Zero Datetime=true");

        public static Com.Martin.SMS.Data.SMSIncoming SaveIncomingMessage(String Sender, String Receiver, String Message) {
            SMSIncoming incoming = new SMSIncoming();
            incoming.ID = CreateIdNumber(TypeSMS.Input);
            incoming.DateReceive = DateTime.Now;
            incoming.Sender = Sender;
            incoming.MessageText = Message;
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                //if (incoming.ID == "")
                //{
                    command.CommandText = "insert into SMS_INPUT (Id_Input, Tanggal_Terima,No_Pengirim,Pesan_Teks,Status) values ";
                    command.CommandText += "(?Id_Input, ?Tanggal_Terima,?No_Pengirim,?Pesan_Teks,?Status)";
                //}
                //else
                //{
                //    command.CommandText = "Update SMS_INPUT set ";
                //    command.CommandText += "Tanggal_Terima = ?Tanggal_Terima, ";
                //    command.CommandText += "No_Pengirim = ?No_Pengirim , ";
                //    command.CommandText += "Pesan_Teks = ?Pesan_Teks , ";
                //    command.CommandText += "Status = ?Status ";
                //    command.CommandText += "where ID_INPUT = ?ID_INPUT ";
                //}
                    
                command.Parameters.Clear();
                command.Parameters.AddWithValue("ID_INPUT", incoming.ID);
                command.Parameters.AddWithValue("Tanggal_Terima", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("No_Pengirim", incoming.Sender);
                command.Parameters.AddWithValue("Pesan_Teks", incoming.MessageText);
                command.Parameters.AddWithValue("Status", "OK");
                int rows = command.ExecuteNonQuery();

            }
            finally
            {
                conn.Close();
            }
            return incoming;
        }

        public static Com.Martin.SMS.Data.SMSIncoming GetIncomingMessage(String ID) {

            SMSIncoming inc = new SMSIncoming();
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT Id_Input, Tanggal_Terima, No_Pengirim, Pesan_Teks, Status FROM sms_input where ID_INPUT=?ID_INPUT";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("ID_INPUT", ID);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    
                    if (!reader.IsDBNull(0))
                        inc.ID = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        inc.DateReceive = reader.GetDateTime(1);
                    if (!reader.IsDBNull(2))
                        inc.Sender = reader.GetString(2);
                    if (!reader.IsDBNull(3))
                        inc.MessageText = reader.GetString(3);
                }
                reader.Close();
            }
            finally
            {                
                conn.Close();
            }

            return inc;
        }

        public static void SaveOutgoingMessage(ref Com.Martin.SMS.Data.SMSOutgoing Outgoing) {
            // update sms_output where id = outgoing.id;
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                int rows = 0;


                if (String.IsNullOrEmpty(Outgoing.ID))
                {
                    Outgoing.ID = CreateIdNumber(TypeSMS.Output);
                    command.CommandText = "INSERT INTO sms_output (Id_Output, Waktu_Diproses, Waktu_Kirim, No_Tujuan, Pesan_Teks, Status, Reg_Name, Reg_Type, Id_Input ) values ";
                    command.CommandText += "(?Id_Output, ?Waktu_Diproses, ?Waktu_Kirim, ?No_Tujuan, ?Pesan_Teks, ?Status, ?Reg_Name, ?Reg_Type, ?Id_Input ) ";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("Id_Output", Outgoing.ID);
                    command.Parameters.AddWithValue("Waktu_Diproses", Outgoing.DateProcess.ToString("yyyy-MM-dd hh:mm:ss"));
                    command.Parameters.AddWithValue("Waktu_Kirim", Outgoing.DateSent.ToString("yyyy-MM-dd hh:mm:ss"));
                    command.Parameters.AddWithValue("No_Tujuan", Outgoing.DestinationNo);
                    command.Parameters.AddWithValue("Pesan_Teks", Outgoing.MessageText);
                    command.Parameters.AddWithValue("Status", "NOK");
                    command.Parameters.AddWithValue("Reg_Name", Outgoing.RegisterName);
                    command.Parameters.AddWithValue("Reg_Type", Outgoing.RegisterType);
                    command.Parameters.AddWithValue("Id_Input", Outgoing.SMSRequest.ID);
                }
                else
                {
                    command.CommandText = "update sms_output set ";
                    command.CommandText += "Id_Output=?Id_Output, ";
                    command.CommandText += "Waktu_Diproses=?Waktu_Diproses, ";
                    command.CommandText += "Waktu_Kirim=?Waktu_Kirim, ";
                    command.CommandText += "No_Tujuan=?No_Tujuan, ";
                    command.CommandText += "Pesan_Teks=?Pesan_Teks, ";
                    command.CommandText += "Status=?Status, ";
                    command.CommandText += "Reg_Name=?Reg_Name, ";
                    command.CommandText += "Reg_Type=?Reg_Type, ";
                    command.CommandText += "Id_Input=?Id_Input ";
                    command.CommandText += "where Id_Output=?Id_Output";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("Id_Output", Outgoing.ID);
                    command.Parameters.AddWithValue("Waktu_Diproses", Outgoing.DateProcess.ToString("yyyy-MM-dd hh:mm:ss"));
                    command.Parameters.AddWithValue("Waktu_Kirim", Outgoing.DateSent.ToString("yyyy-MM-dd hh:mm:ss"));
                    command.Parameters.AddWithValue("No_Tujuan", Outgoing.DestinationNo);
                    command.Parameters.AddWithValue("Pesan_Teks", Outgoing.MessageText);
                    command.Parameters.AddWithValue("Status", "OK");
                    command.Parameters.AddWithValue("Reg_Name", Outgoing.RegisterName);
                    command.Parameters.AddWithValue("Reg_Type", Outgoing.RegisterType);
                    command.Parameters.AddWithValue("Id_Input", Outgoing.SMSRequest.ID);
                }
                rows = command.ExecuteNonQuery();
            }
            catch (System.Exception ex) { }
            finally
            {
                conn.Close();
            }
        }

        public static Com.Martin.SMS.Data.SMSOutgoing GetOutgoingMessage(String ID) {
            SMSOutgoing outp = new SMSOutgoing();
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT Id_Output, Waktu_Diproses, Waktu_Kirim, No_Tujuan, Pesan_Teks, Status, Reg_Name, Reg_Type, Id_Input FROM sms_output where ID_OUTPUT=?ID_OUTPUT";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("ID_OUTPUT", ID);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    if (!reader.IsDBNull(0))
                        outp.ID = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        outp.DateProcess = reader.GetDateTime(1);
                    if (!reader.IsDBNull(2))
                        outp.DateSent= reader.GetDateTime(2);
                    if (!reader.IsDBNull(3))
                        outp.DestinationNo = reader.GetString(3);
                    if (!reader.IsDBNull(4))
                        outp.MessageText = reader.GetString(4);
                    if (!reader.IsDBNull(6))
                        outp.RegisterName  = reader.GetString(6);
                    if (!reader.IsDBNull(7))
                        outp.RegisterType = reader.GetString(7);
                    if (!reader.IsDBNull(8))
                        outp.SMSRequest = GetIncomingMessage(reader.GetString(8));
                
                }
                reader.Close();
            }
            finally
            {
                conn.Close();
            }

            return outp;
        }

        public static void SaveBroadcastMessage(List<Com.Martin.SMS.Data.SMSOutgoing> OutgoingList) {

            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                int rows = 0;

                for (int i = 0; i < OutgoingList.Count; i++ )
                {
                    System.Diagnostics.Debug.WriteLine("SMSOutgoing index:" + i.ToString());
                    SMSOutgoing outPut = OutgoingList[i];
                    if (String.IsNullOrEmpty(outPut.ID))
                    {
                        outPut.ID = CreateIdNumber(TypeSMS.Output);
                        System.Diagnostics.Debug.WriteLine("Get ID:" + outPut.ID);
                        command.CommandText = "INSERT INTO sms_output (Id_Output, Waktu_Diproses, Waktu_Kirim, No_Tujuan, Pesan_Teks, Status, Reg_Name, Reg_Type, Id_Input ) values ";
                        command.CommandText += "(?Id_Output, ?Waktu_Diproses, ?Waktu_Kirim, ?No_Tujuan, ?Pesan_Teks, ?Status, ?Reg_Name, ?Reg_Type, ?Id_Input ) ";

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("Id_Output", outPut.ID);
                        command.Parameters.AddWithValue("Waktu_Diproses", outPut.DateProcess.ToString("yyyy-MM-dd hh:mm:ss"));
                        command.Parameters.AddWithValue("Waktu_Kirim", outPut.DateSent.ToString("yyyy-MM-dd hh:mm:ss"));
                        command.Parameters.AddWithValue("No_Tujuan", outPut.DestinationNo);
                        command.Parameters.AddWithValue("Pesan_Teks", outPut.MessageText);
                        command.Parameters.AddWithValue("Status", "NOK");
                        command.Parameters.AddWithValue("Reg_Name", outPut.RegisterName);
                        command.Parameters.AddWithValue("Reg_Type", outPut.RegisterType);
                        command.Parameters.AddWithValue("Id_Input", outPut.SMSRequest.ID);
                    }
                    else
                    {
                        command.CommandText = "update sms_output set ";
                        command.CommandText += "Id_Output=?Id_Output, ";
                        command.CommandText += "Waktu_Diproses=?Waktu_Diproses, ";
                        command.CommandText += "Waktu_Kirim=?Waktu_Kirim, ";
                        command.CommandText += "No_Tujuan=?No_Tujuan, ";
                        command.CommandText += "Pesan_Teks=?Pesan_Teks, ";
                        command.CommandText += "Status=?Status, ";
                        command.CommandText += "Reg_Name=?Reg_Name, ";
                        command.CommandText += "Reg_Type=?Reg_Type, ";
                        command.CommandText += "Id_Input=?Id_Input ";
                        command.CommandText += "where Id_Output=?Id_Output";

                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("Id_Output", outPut.ID);
                        command.Parameters.AddWithValue("Waktu_Diproses", outPut.DateProcess.ToString("yyyy-MM-dd hh:mm:ss"));
                        command.Parameters.AddWithValue("Waktu_Kirim", outPut.DateSent.ToString("yyyy-MM-dd hh:mm:ss"));
                        command.Parameters.AddWithValue("No_Tujuan", outPut.DestinationNo);
                        command.Parameters.AddWithValue("Pesan_Teks", outPut.MessageText);
                        command.Parameters.AddWithValue("Status", "NOK");
                        command.Parameters.AddWithValue("Reg_Name", outPut.RegisterName);
                        command.Parameters.AddWithValue("Reg_Type", outPut.RegisterType);
                        command.Parameters.AddWithValue("Id_Input", outPut.SMSRequest.ID);
                    }
                    rows = command.ExecuteNonQuery();
                }
            }
            catch (System.Exception ex) { }
            finally 
            {
                conn.Close();
            }

            
        }

        public static void SaveBroadcastScheduler(Com.Martin.SMS.Data.BroadcastScheduler brc)
        {
            //update jadwal_broadcast where id=brc.id

            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                int rows = 0;
                command.CommandText = "update jadwal_broadcast set ";
                command.CommandText += "Id_Jadwal=?Id_Jadwal, ";
                command.CommandText += "Pengulangan_Max=?Pengulangan_Max, ";
                command.CommandText += "Pengulangan_Hitung=?Pengulangan_Hitung, ";
                command.CommandText += "Pengulangan_Jeda_Hari=?Pengulangan_Jeda_Hari, ";
                command.CommandText += "Waktu_Eksekusi_Berikut=?Waktu_Eksekusi_Berikut, ";
                command.CommandText += "Waktu_Eksekusi_Terakhir=?Waktu_Eksekusi_Terakhir, ";
                command.CommandText += "Reg_Name=?Reg_Name, ";
                command.CommandText += "Reg_Type=?Reg_Type ";
                command.CommandText += "where Id_Jadwal=?Id_Jadwal";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("Id_Jadwal", brc.ID);
                command.Parameters.AddWithValue("Pengulangan_Max", brc.MaximumLoop);
                command.Parameters.AddWithValue("Pengulangan_Hitung", brc.CurrentLoop);
                command.Parameters.AddWithValue("Pengulangan_Jeda_Hari", brc.IntervalDays);
                command.Parameters.AddWithValue("Waktu_Eksekusi_Berikut", brc.NextExecuteTime.ToString("yyyy-MM-dd hh:mm:ss"));
                command.Parameters.AddWithValue("Waktu_Eksekusi_Terakhir", brc.LastExecuteTime.ToString("yyyy-MM-dd hh:mm:ss"));
                command.Parameters.AddWithValue("Reg_Name", brc.RegisterName);
                command.Parameters.AddWithValue("Reg_Type", brc.RegisterType);
                rows = command.ExecuteNonQuery();
                
            }
            catch (System.Exception ex) { }
            finally 
            {
                conn.Close();
            }
        }

        public static List<Com.Martin.SMS.Data.SMSOutgoing> GetOutgoingSMSList() {

            List<Com.Martin.SMS.Data.SMSOutgoing> lst = new List<SMSOutgoing>();

            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT Id_Output , Waktu_Diproses, Waktu_Kirim, No_Tujuan, Pesan_Teks, Reg_Name, Reg_Type, Id_Input FROM sms_output s where status='NOK' limit  5  offset 0";
                command.Parameters.Clear();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SMSOutgoing outSMS = new SMSOutgoing();
                    if (!reader.IsDBNull(0))
                        outSMS.ID = reader.GetString("Id_Output");
                    if (!reader.IsDBNull(1))
                        outSMS.DateProcess = reader.GetDateTime("Waktu_Diproses");
                    if (!reader.IsDBNull(2))
                        outSMS.DateSent = reader.GetDateTime("Waktu_Kirim");
                    if (!reader.IsDBNull(3))
                        outSMS.DestinationNo = reader.GetString("No_Tujuan");
                    if (!reader.IsDBNull(4))
                        outSMS.MessageText = reader.GetString("Pesan_Teks");
                    if (!reader.IsDBNull(5))
                        outSMS.RegisterName = reader.GetString("Reg_Name");
                    if (!reader.IsDBNull(6))
                        outSMS.RegisterType = reader.GetString("Reg_Type");
                    if (!reader.IsDBNull(7))
                        outSMS.SMSRequest.ID = reader.GetString("Id_Input");

                    lst.Add(outSMS);
                }
                reader.Close();
            }
            catch (System.Exception ex) { }
            finally
            {
                conn.Close();
            }
            return lst;
        }

        public static List<Com.Martin.SMS.Data.BroadcastScheduler> GetBroadcastScheduler(DateTime NextExecute) {
            // SELECT j.Id_Jadwal j.Pengulangan_Max j.Pengulangan_Hitung j.Pengulangan_Jeda_Hari j.Waktu_Eksekusi_Berikut 
            //  j.Waktu_Eksekusi_Terakhir j.Status j.Reg_Name j.Reg_Type FROM jadwal_broadcast j;
            // where current-loop<= maximum_loop and next_execute_time < now and status='enable'

            List<Com.Martin.SMS.Data.BroadcastScheduler> lst = new List<BroadcastScheduler>();
            try
            {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT Id_Jadwal, Pengulangan_Max , Pengulangan_Hitung , Pengulangan_Jeda_Hari , Waktu_Eksekusi_Berikut , ";
                command.CommandText += "Waktu_Eksekusi_Terakhir , Status , Reg_Name , Reg_Type FROM jadwal_broadcast ";
                command.CommandText += "where Pengulangan_Hitung <= Pengulangan_Max and Waktu_Eksekusi_Berikut <= CURDATE() and status='ACTIVE' ";

                command.Parameters.Clear();

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    BroadcastScheduler brcSchd = new BroadcastScheduler();
                    if (!reader.IsDBNull(0))
                        brcSchd.ID = reader.GetInt32("Id_Jadwal");
                    if (!reader.IsDBNull(1))
                        brcSchd.MaximumLoop = reader.GetInt32("Pengulangan_Max");
                    if (!reader.IsDBNull(2))
                        brcSchd.CurrentLoop = reader.GetInt32("Pengulangan_Hitung");
                    if (!reader.IsDBNull(3))
                        brcSchd.IntervalDays = reader.GetInt32("Pengulangan_Jeda_Hari");
                    if (!reader.IsDBNull(4))
                        brcSchd.NextExecuteTime = reader.GetDateTime("Waktu_Eksekusi_Berikut");
                    if (!reader.IsDBNull(5))
                        brcSchd.LastExecuteTime = reader.GetDateTime("Waktu_Eksekusi_Terakhir");
                    //if (!reader.IsDBNull(6))
                        //brcSchd.RegisterType = reader.GetString("Status");
                    if (!reader.IsDBNull(7))
                        brcSchd.RegisterName = reader.GetString("Reg_Name");
                    if (!reader.IsDBNull(7))
                        brcSchd.RegisterType = reader.GetString("Reg_Type");
                    lst.Add(brcSchd);
                }
                reader.Close();
            }
            catch (System.Exception ex) { }
            finally
            {
                conn.Close();
            }
            return lst;
        }

        private static String CreateIdNumber(TypeSMS type) {
            switch (type) {
                case TypeSMS.Broadcast:
                    return "b" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                case TypeSMS.Input:
                    return "i" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                case TypeSMS.Output:
                    return "o" + DateTime.Now.ToString("yyyyMMddHHmmssff");
                default:
                    return "u" + DateTime.Now.ToString("yyyyMMddHHmmssff");
            }
        }

        private enum TypeSMS {
            Broadcast,
            Input,
            Output
        }
    }

    class ConfigLoader {
        private MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=rahasia;database=catering;Allow Zero Datetime=true");

        public Com.Martin.SMS.Command.AbstractRequest CreateRequestCommand(String CommandType, String CommandName) {
            String nama_class = "";
            String nama_assembly = "";

            try {
                this.conn.Open();
                MySqlCommand command = this.conn.CreateCommand();
                command.CommandText = "select nama_class, nama_assembly from daftar_register where reg_type = ?type and reg_name = ?name";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("type", CommandType);
                command.Parameters.AddWithValue("name", CommandName);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {

                    if (!reader.IsDBNull(0))
                        nama_class = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        nama_assembly = reader.GetString(1);
                }

            } finally {
                this.conn.Close();
            }

            if ((nama_class.Length == 0) || (nama_assembly.Length == 0)) {
                throw new Com.Martin.SMS.Exception.SMSException("Register tidak ditemukan.");
            }

            //Com.Martin.SMS.Command.GetMenuSchedule mn = new Com.Martin.SMS.Command.GetMenuSchedule();
            //Type tp = mn.GetType();

            Com.Martin.SMS.Command.AbstractRequest request = Activator.CreateInstance(nama_assembly, nama_class).Unwrap() as Com.Martin.SMS.Command.AbstractRequest;
            return request;
        }

        public Com.Martin.SMS.Command.AbstractBroadcast CreateBroadcastCommand(String CommandType, String CommandName) {
            String nama_class = "";
            String nama_assembly = "";

            try {
                this.conn.Open();
                MySqlCommand command = this.conn.CreateCommand();
                command.CommandText = "select nama_class, nama_assembly from daftar_register where reg_type = ?type and reg_name = ?name";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("type", CommandType);
                command.Parameters.AddWithValue("name", CommandName);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {

                    if (!reader.IsDBNull(0))
                        nama_class = reader.GetString(0);
                    if (!reader.IsDBNull(1))
                        nama_assembly = reader.GetString(1);
                }

            } finally {
                this.conn.Close();
            }

            if ((nama_class.Length == 0) || (nama_assembly.Length == 0)) {
                throw new Com.Martin.SMS.Exception.SMSException("Register tidak ditemukan.");
            }

            Com.Martin.SMS.Command.AbstractBroadcast request = Activator.CreateInstance(nama_assembly, nama_class).Unwrap() as Com.Martin.SMS.Command.AbstractBroadcast;
            return request;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using MySql.Data;
using MySql.Data.MySqlClient;

using Com.Martin.SMS.Data;

namespace Com.Martin.SMS.Common {

    public static class CommandProcessor {

        public static Com.Martin.SMS.Data.SMSIncoming ProcessRequest(Com.Martin.SMS.Data.SMSIncoming Request) {
            
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
            SMSHelper.SaveOutgoingMessage(outSMS);

            return Request;
        }

        public static void ProcessBroadcast() {
           
            List<BroadcastScheduler> lstBroadCastSch = SMSHelper.GetBroadcastScheduler(DateTime.Now);
            for (int i = 0; i < lstBroadCastSch.Count; i++) 
            {
                ConfigLoader cfg = new ConfigLoader();
                Com.Martin.SMS.Command.AbstractBroadcast brc = cfg.CreateBroadcastCommand(lstBroadCastSch[i].RegisterType, lstBroadCastSch[i].RegisterName);
                List<SMSOutgoing> lstOut = brc.Execute();
                SMSHelper.SaveBroadcastMessage(lstOut);

                lstBroadCastSch[i].CurrentLoop += 1;
                lstBroadCastSch[i].LastExecuteTime = lstBroadCastSch[i].NextExecuteTime;
                lstBroadCastSch[i].NextExecuteTime = lstBroadCastSch[i].NextExecuteTime.AddDays(lstBroadCastSch[i].IntervalDays);

                SMSHelper.SaveBroadcastScheduler(lstBroadCastSch[i]);
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
                if (incoming.ID == "")
                {
                    command.CommandText = "insert into SMS_INPUT (Tanggal_Terima,No_Pengirim,Pesan_Teks,Status) values ()";
                    command.CommandText += "(?Tanggal_Terima,?No_Pengirim,?Pesan_Teks,?Status)";
                }
                else
                {
                    command.CommandText = "Update SMS_INPUT set ";
                    command.CommandText += "Tanggal_Terima = ?Tanggal_Terima, ";
                    command.CommandText += "No_Pengirim = ?No_Pengirim , ";
                    command.CommandText += "Pesan_Teks = ?Pesan_Teks , ";
                    command.CommandText += "Status = ?Status , ";
                    command.CommandText += "where ID_INPUT = ?ID_INPUT ";
                }
                    
                command.Parameters.Clear();
                command.Parameters.AddWithValue("Tanggal_Terima", DateTime.Now.ToString("yyyy-mm-dd"));
                command.Parameters.AddWithValue("No_Pengirim", incoming.Sender);
                command.Parameters.AddWithValue("Pesan_Teks", incoming.MessageText);
                command.Parameters.AddWithValue("Status", "OK");
                command.Parameters.AddWithValue("ID_INPUT",incoming.ID);
                int rows = command.ExecuteNonQuery();

            }
            finally
            {
                conn.Close();
            }
            return incoming;
        }

        public static Com.Martin.SMS.Data.SMSIncoming GetIncomingMessage(String ID) {
            return new SMS.Data.SMSIncoming();
        }

        public static void SaveOutgoingMessage(Com.Martin.SMS.Data.SMSOutgoing Outgoing) {
            // update sms_output where id = outgoing.id;
        }

        public static Com.Martin.SMS.Data.SMSOutgoing GetOutgoingMessage(String ID) {
            return new SMS.Data.SMSOutgoing();
        }

        public static void SaveBroadcastMessage(List<Com.Martin.SMS.Data.SMSOutgoing> OutgoingList) {
            //while list
            //  insert into sms_output
        }

        public static void SaveBroadcastScheduler(Com.Martin.SMS.Command.AbstractBroadcast brc)
        {
            //update jadwal_broadcast where id=brc.id
        }


        public static List<Com.Martin.SMS.Data.SMSOutgoing> GetOutgoingSMSList() {
            // SELECT * from sms_output where status = wait and limit = 5 offset 1

            return new List<Com.Martin.SMS.Data.SMSOutgoing>();
        }

        public static List<Com.Martin.SMS.Data.BroadcastScheduler> GetBroadcastScheduler(DateTime NextExecute) {
            // SELECT j.`Id_Jadwal`, j.`Pengulangan_Max`, j.`Pengulangan_Hitung`, j.`Pengulangan_Jeda_Hari`, j.`Waktu_Eksekusi_Berikut`, 
            //  j.`Waktu_Eksekusi_Terakhir`, j.`Status`, j.`Reg_Name`, j.`Reg_Type` FROM jadwal_broadcast j;
            // where current-loop<= maximum_loop and next_execute_time < now and status='enable'
            return new List<Com.Martin.SMS.Data.BroadcastScheduler>();
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

            Com.Martin.SMS.Command.GetMenuSchedule mn = new Com.Martin.SMS.Command.GetMenuSchedule();
            Type tp = mn.GetType();

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
                        nama_class = reader.GetString(1);
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

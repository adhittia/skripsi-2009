using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using MySql.Data.MySqlClient;

namespace Com.Martin.SMS.Command {

    #region Abstract Skeleon
    public abstract class AbstractRequest {
        private Com.Martin.SMS.Data.SMSIncoming request;

        private ParametersList param = new ParametersList();

        public ParametersList Parameters {
            get {
                return this.param;
            }
        }

        public Com.Martin.SMS.Data.SMSIncoming RequestSMS {
            set {
                this.request = value;
            }
            get {
                return this.request;
            }
        }

        public abstract String GetRegName();

        public abstract String GetRegType();

        public abstract Com.Martin.SMS.Data.SMSOutgoing Execute();
    }

    public abstract class AbstractBroadcast {
        public abstract String GetRegName();

        public abstract String GetRegType();

        public abstract List<Com.Martin.SMS.Data.SMSOutgoing> Execute();
    }

    public class ParametersList {
        private List<String> param = new List<String>();

        public void Add(String value) {
            this.param.Add(value);
        }

        public void Add(int index, String value) {
            this.param.Insert(index, value);
        }

        public void Clear() {
            this.param.Clear();
        }

        public String Get(int index) {
            return this.param[index];
        }

        public String[] GetAll() {
            return this.param.ToArray();
        }
    }

    public static class CommandHelper {
        private static string connString = "server=127.0.0.1;uid=root;pwd=rahasia;database=catering;Allow Zero Datetime=true";

        public static string ConnectionString {
            get {
                return connString;
            }
        }
    }
    #endregion

    #region Request Command
    public class MainChangeMenu : AbstractRequest {
        public override string GetRegName() {
            return "Change";
        }

        public override string GetRegType() {
            return "MAIN";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                conn.Open();
                // Get Parameters
                String custID = this.Parameters.Get(0);
                String orderDate = this.Parameters.Get(1);
                String menuSelect = this.Parameters.Get(2);
                decimal menuID = 0;
                decimal ScheduleID = 0;

                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "select Menu_Schedule_Id, Ms_Date, Ms_Menu_A, Ms_Menu_B, Ms_Menu_C from Menu_Schedule where Ms_Date = ?Ms_Date";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?Ms_Date", orderDate);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    ScheduleID = menuID = reader.GetDecimal(0);
                    switch (menuSelect) {
                        case "A":
                            menuID = reader.GetDecimal(2);
                            break;
                        case "B":
                            menuID = reader.GetDecimal(3);
                            break;
                        case "C":
                            menuID = reader.GetDecimal(4);
                            break;
                    }
                }
                reader.Close();

                command.CommandText = "update customer_order set Com_Selected = ?comSelected, Id_Input = ?input where menu_schedule_id = ?schedule and customer_id = ?customer";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?comSelected", menuID);
                command.Parameters.AddWithValue("?input", this.RequestSMS.ID);
                command.Parameters.AddWithValue("?schedule", ScheduleID);
                command.Parameters.AddWithValue("?customer", custID);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "Anda telah merubah menu tgl " + orderDate + " menjadi menu " + menuSelect;
            } finally {
                conn.Close();
            }
            return outgoing;
        }
    }

    public class MainCancelOrder : AbstractRequest {
        public override string GetRegName() {
            return "Cancel";
        }

        public override string GetRegType() {
            return "MAIN";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String customer = this.Parameters.Get(0);
                String orderDate = this.Parameters.Get(1);

                conn.Open();

                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "UPDATE customer_order set com_order_status = 'CANCEL' where com_order_date = ?orderDate and customer_id = ?customer";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?orderDate", orderDate);
                command.Parameters.AddWithValue("?customer", customer);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "Anda telah membatalkan menu tgl " + orderDate;
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class MainGetMenu : AbstractRequest {
        public override string GetRegName() {
            return "Get";
        }

        public override string GetRegType() {
            return "MAIN";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String orderDate = this.Parameters.Get(0);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT (select m_name from menu b where b.menu_id = m.ms_menu_a) as Menu_A , ";
                command.CommandText += "  (select m_name from menu b where b.menu_id = m.ms_menu_b) as Menu_B, ";
                command.CommandText += "  (select m_name from menu b where b.menu_id = m.ms_menu_c) as Menu_C ";
                command.CommandText += " FROM catering.menu_schedule m where ms_date=?schedule";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?schedule", orderDate);

                String teks = "";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    teks = "Menu tgl " + orderDate + " sbb, A:" + reader.GetString(0) + ",";
                    teks += " B:" + reader.GetString(1) + ",";
                    teks += " C:" + reader.GetString(2);
                }
                reader.Close();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = teks;
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class AdditionalMenuList : AbstractRequest {
        public override string GetRegName() {
            return "Get";
        }

        public override string GetRegType() {
            return "ADDT";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String kategori = this.Parameters.Get(0);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "select menu_id, m_name from menu where m_type='ADDT' and m_category=?category limit 5 offset 1";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?category", kategori);

                String teks = "Menu Tambahan (kode-nama):";
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    teks += reader.GetString(0) + "-" + reader.GetString(1) + ",";
                }
                reader.Close();
                teks = teks.Substring(0, teks.Length - 1);

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = teks;
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class AdditionalOrderMenu : AbstractRequest {
        public override string GetRegName() {
            return "Order";
        }

        public override string GetRegType() {
            return "ADDT";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String orderDate = this.Parameters.Get(0);
                String customer = this.Parameters.Get(1);
                String menuID = this.Parameters.Get(2);

                conn.Open();

                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "select m_price from menu where menu_id=?menu";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?menu", menuID);
                decimal price = (decimal)command.ExecuteScalar();

                command.CommandText = "select menu_schedule_customer_s from customer_order where com_order_date=?order and customer_id=?customer";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?order", orderDate);
                command.Parameters.AddWithValue("?customer", customer);
                decimal orderID = (decimal)command.ExecuteScalar();

                command.CommandText  = "insert into customer_order_detail(menu_id, price, menu_schedule_customer_s, id_input)";
                command.CommandText += " values(?menu, ?price, ?schedule, ?input)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?menu", menuID);
                command.Parameters.AddWithValue("?price", price);
                command.Parameters.AddWithValue("?schedule", orderID);
                command.Parameters.AddWithValue("?input", this.RequestSMS.ID);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "Pesanan anda telah berhasil diproses.";

            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class BillingInformation : AbstractRequest {
        public override string GetRegName() {
            return "Get";
        }

        public override string GetRegType() {
            return "BILL";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String customer = this.Parameters.Get(0);
                String typeBill = this.Parameters.Get(1);
                int month = 0;
                switch (typeBill) {
                    case "LAST":
                        month = DateTime.Now.Month - 1;
                        break;
                    case "CURRENT":
                        month = DateTime.Now.Month;
                        break;
                }

                conn.Open();

                // Customer Order
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "select sum(price) from (SELECT customer_id, (select m_price from menu b where b.menu_id = c.com_selected) price FROM customer_order c where c.customer_id=?customer and month(c.com_order_date) = ?bln ) d group by d.customer_id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?customer", customer);
                command.Parameters.AddWithValue("?bln", month);
                decimal master = (decimal)command.ExecuteScalar();

                 command = conn.CreateCommand();
                command.CommandText = "SELECT sum(price) FROM customer_order_detail c where c.menu_schedule_customer_s in (select d.menu_schedule_customer_s from customer_order d where d.customer_id = ?customer and month(d.com_order_date) = ?bln)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?customer", customer);
                command.Parameters.AddWithValue("?bln", month);
                decimal detail = (decimal)command.ExecuteScalar();

                decimal total = master + detail;

                String teks = "tagihan anda untuk periode " + month.ToString() + "-" + DateTime.Now.Year.ToString() + " adalah sebesar " + total.ToString();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = teks;
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class ProfilUpdate : AbstractRequest {
        public override string GetRegName() {
            return "Profile";
        }

        public override string GetRegType() {
            return "CUST";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String customer = this.Parameters.Get(0);
                String kolom = this.Parameters.Get(1);
                String nilai = this.Parameters.Get(2);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "update customer_profile set " + kolom + " = ?value where customer_id=?customer";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?value", nilai);
                command.Parameters.AddWithValue("?customer", customer);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "Profile anda telah berhasil diupdate";
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class AdmMenuAdd : AbstractRequest {
        public override string GetRegName() {
            return "Add";
        }

        public override string GetRegType() {
            return "MENU";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                string menuName = this.Parameters.Get(0);
                string menuType = this.Parameters.Get(1);
                string kategori = this.Parameters.Get(2);
                decimal price = Decimal.Parse(this.Parameters.Get(3));

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "insert into menu(m_name, m_type, m_category, m_price) values(?name, ?type, ?category, ?price)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?name", menuName);
                command.Parameters.AddWithValue("?type", menuType);
                command.Parameters.AddWithValue("?category", kategori);
                command.Parameters.AddWithValue("?price", price);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "menu berhasil ditambahkan";
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class AdmMenuUpdate : AbstractRequest {
        public override string GetRegName() {
            return "Chg";
        }

        public override string GetRegType() {
            return "MENU";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                string menuName = this.Parameters.Get(0);
                string menuType = this.Parameters.Get(1);
                string kategori = this.Parameters.Get(2);
                decimal price = Decimal.Parse(this.Parameters.Get(3));
                string menuID = this.Parameters.Get(4);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "update menu set m_name=?name, m_type=?type, m_category=?category, m_price=?price where menu_id=?id";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?name", menuName);
                command.Parameters.AddWithValue("?type", menuType);
                command.Parameters.AddWithValue("?category", kategori);
                command.Parameters.AddWithValue("?price", price);
                command.Parameters.AddWithValue("?id", menuID);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "menu berhasil diperbarui";
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class AdmMenuSet : AbstractRequest {
        public override string GetRegName() {
            return "Set";
        }

        public override string GetRegType() {
            return "MENU";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String scheduleDate = this.Parameters.Get(0);
                String menuA = this.Parameters.Get(1);
                String menuB = this.Parameters.Get(2);
                String menuC = this.Parameters.Get(3);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "insert into menu_schedule(ms_date, ms_menu_a, ms_menu_b, ms_menu_c) values (?schedule, ?menuA, ?menuB, ?menuC)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?schedule", scheduleDate);
                command.Parameters.AddWithValue("?menuA", menuA);
                command.Parameters.AddWithValue("?menuB", menuB);
                command.Parameters.AddWithValue("?menuC", menuC);
                command.ExecuteNonQuery();

                // harusnya ada trigger create schedule buat customer tp ini dipikir nantilah
                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "Jadwal menu berhasil disimpan.";
            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    public class AdmSendNews : AbstractRequest {
        public override string GetRegName() {
            return "News";
        }

        public override string GetRegType() {
            return "Add";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();
            try {
                String news = this.Parameters.Get(0);

                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "insert into news_broadcast(news_date, news, status) values (?date, ?news, 'nok')";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("?date", DateTime.Now.ToString("yyyyMMdd"));
                command.Parameters.AddWithValue("?news", news);
                command.ExecuteNonQuery();

                outgoing.RegisterType = this.GetRegType();
                outgoing.RegisterName = this.GetRegName();
                outgoing.Type = Com.Martin.SMS.Data.SMSType.RequestResponse;
                outgoing.SMSRequest = this.RequestSMS;
                outgoing.DateProcess = DateTime.Now;
                outgoing.MessageText = "Berita berhasil ditambah, akan dikirim pada jadwal berikutnya.";

            } finally {
                conn.Close();
            }
            return outgoing; ;
        }
    }

    #endregion

    #region Broadcast Command
    public class BroadcastNews : AbstractBroadcast {
        public override string GetRegName() {
            return "News";
        }

        public override string GetRegType() {
            return "BRC";
        }

        public override List<Com.Martin.SMS.Data.SMSOutgoing> Execute() {
            List<SMS.Data.SMSOutgoing> lst = new List<Com.Martin.SMS.Data.SMSOutgoing>();

            MySqlConnection conn = new MySqlConnection(CommandHelper.ConnectionString);
            try {
                conn.Open();
                MySqlCommand command = conn.CreateCommand();
                command.CommandText = "SELECT n.id_news, n.news, a.cp_mobile_number FROM catering.news_broadcast n, catering.customer_profile a where n.status='NOK'";

                Decimal id_news = -1;
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    SMS.Data.SMSOutgoing outgoing = new Com.Martin.SMS.Data.SMSOutgoing();

                    if (!reader.IsDBNull(0))
                        id_news = reader.GetDecimal(0);
                    if (!reader.IsDBNull(1))
                        outgoing.MessageText = reader.GetString(1);
                    if (!reader.IsDBNull(2))
                        outgoing.DestinationNo = reader.GetString(2);

                    outgoing.DateProcess = DateTime.Now;
                    outgoing.RegisterType = "BRC";
                    outgoing.RegisterName = "News";
                    outgoing.Type = Com.Martin.SMS.Data.SMSType.Broadcast;
                    lst.Add(outgoing);
                }
                reader.Close();

                if (id_news != -1) {
                    command.CommandText = "Update news_broadcast set status='OK' where id_news = ?id_news";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("id_news", id_news);
                    command.ExecuteNonQuery();
                }

            } finally {
                conn.Close();
            }

            return lst;
        }
    }
    #endregion

    #region Test Command
    public class TestRequest : AbstractRequest {
        public override string GetRegName() {
            return "Request";
        }

        public override string GetRegType() {
            return "Test";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute() {
            SMS.Data.SMSOutgoing outgoing = new SMS.Data.SMSOutgoing();
            outgoing.DestinationNo = "02191848465";
            outgoing.DateProcess = DateTime.Now;
            outgoing.RegisterName = "Request";
            outgoing.RegisterType = "Test";
            outgoing.MessageText = "Success Request" + DateTime.Now.ToString("dd-MM-yyyy");
            outgoing.SMSRequest = this.RequestSMS;

            return outgoing;
        }
    }

    public class TestBroadcast : AbstractBroadcast {
        public override string GetRegName() {
            return "Broadcast";
        }

        public override string GetRegType() {
            return "Test";
        }

        public override List<Com.Martin.SMS.Data.SMSOutgoing> Execute() {
            List<SMS.Data.SMSOutgoing> lstOutgoing = new List<Com.Martin.SMS.Data.SMSOutgoing>();

            SMS.Data.SMSOutgoing outgoing = new SMS.Data.SMSOutgoing();
            outgoing.DestinationNo = "02191848465";
            outgoing.DateProcess = DateTime.Now;
            outgoing.RegisterName = "Broadcast";
            outgoing.RegisterType = "Test";
            outgoing.MessageText = "Success Broadcast" + DateTime.Now.ToString("dd-MM-yyyy");
            lstOutgoing.Add(outgoing);

            SMS.Data.SMSOutgoing outgoing1 = new SMS.Data.SMSOutgoing();
            outgoing1.DestinationNo = "02191848465";
            outgoing1.DateProcess = DateTime.Now;
            outgoing1.RegisterName = "Broadcast";
            outgoing1.RegisterType = "Test";
            outgoing1.MessageText = "Success Broadcast1" + DateTime.Now.ToString("dd-MM-yyyy");
            lstOutgoing.Add(outgoing1);

            return lstOutgoing;
        }
    }
    #endregion
}

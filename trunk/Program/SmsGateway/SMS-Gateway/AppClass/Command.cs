using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Com.Martin.SMS.Command {

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

    public class GetMenuSchedule : AbstractRequest
    {
        public override string GetRegName()
        {
            return "Menu";
        }

        public override string GetRegType()
        {
            return "Get";
        }

        public override Com.Martin.SMS.Data.SMSOutgoing Execute()
        {
            SMS.Data.SMSOutgoing outgoing = new SMS.Data.SMSOutgoing();
            outgoing.DateProcess = DateTime.Now;
            outgoing.DateSent = DateTime.Now;
            outgoing.DestinationNo = "085668494684";
            outgoing.ID = "SMS1";
            outgoing.MessageText = "Sukses";
            outgoing.RegisterName = "Reg Name";
            outgoing.RegisterType = "Reg Type";

            return outgoing;
        }
    }

    public class BroadcastNews : AbstractBroadcast
    {
        public override string GetRegName()
        {
            return "News";
        }

        public override string GetRegType()
        {
            return "BRC";
        }

        public override List<Com.Martin.SMS.Data.SMSOutgoing> Execute()
        {
            List<SMS.Data.SMSOutgoing> lst = new List<Com.Martin.SMS.Data.SMSOutgoing>;
            // Query Number from customer_profile;
            // while read
            //   out = new smsoutgoing;
            //   isi outgoing.dest = number customer
            //   isi message.text = ngawur
            // lst.add(out)
        }
    }
}

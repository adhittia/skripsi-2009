using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Martin.SMS.Data {
    
    public struct SMSIncoming {
        public String ID;
        public String Sender;
        public String MessageText;
        public DateTime DateReceive;
    }

    public struct SMSOutgoing {
        public String ID;
        public String DestinationNo;
        public String MessageText;
        public DateTime DateSent;
        public DateTime DateProcess;
        public SMSType Type;
        public String RegisterName;
        public String RegisterType;
        public SMSIncoming SMSRequest;
    }

    public struct BroadcastScheduler {
        public int ID;
        public int MaximumLoop;
        public int CurrentLoop;
        public int IntervalDays;
        public String RegisterName;
        public String RegisterType;
        public DateTime NextExecuteTime;
        public DateTime LastExecuteTime;
    }

    public enum SMSType {
        RequestResponse,
        Broadcast
    }
}

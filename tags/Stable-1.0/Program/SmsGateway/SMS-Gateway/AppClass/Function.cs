using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Martin.Function
{
    public class InputLog
    {
        public String composeReportDetail(Com.Martin.SMS.Data.SMSIncoming incoming,Com.Martin.SMS.Data.SMSOutgoing outgoing)
        {   
            String s = String.Empty;

            if (outgoing.SMSRequest.ID == String.Empty)
            {
                s = "SMS TYPE : BROADCAST";
                s += "\r\n";
                for (int i = 0; i < 200; i++)
                    s += "-";

                s += "\r\n";
                s += "Res ID : " + outgoing.ID + " , Process At : " + outgoing.DateProcess.ToString("dd-MMM-yyyy") + " , Sent at : " + outgoing.DateSent.ToString("dd-MMM-yyyy");
                s += "\r\n";
                s += "Text : " + outgoing.MessageText;
                s += "\r\n";
                for (int i = 0; i < 100; i++)
                    s += "=";
            }
            else
            {
                s = "SMS TYPE : REQUEST-RESPONE";
                s += "\r\n";
                for (int i = 0; i < 200; i++)
                    s += "-";

                s += "\r\n";
                s += "Req ID : " + incoming.ID + " , Received : " + incoming.DateReceive.ToString("dd-MMM-yyyy") + " , Sender : " + incoming.Sender;
                s += "\r\n";
                s += "Command Type : " + outgoing.RegisterType + " , Command Name : " + outgoing.RegisterName;
                s += "\r\n";
                s += "Text : " + incoming.MessageText;

                s += "\r\n";
                s += "\r\n";
                s += "Res ID : " + outgoing.ID.ToString() + " , Process At : " + outgoing.DateProcess.ToString("dd-MMM-yyyy") + " , Sent at : " + outgoing.DateSent.ToString("dd-MMM-yyyy");
                s += "\r\n";
                s += "Text : " + outgoing.MessageText;
                s += "\r\n";
                for (int i = 0; i < 100; i++)
                    s += "=";
            }
            return s;
        }

        public String composeOutBoxDetail(Com.Martin.SMS.Data.SMSOutgoing outgoing)
        {
            String s = String.Empty;

            if (outgoing.SMSRequest.ID == String.Empty)
            {
                s = "SMS TYPE : BROADCAST";
                s += "\r\n";
                for (int i = 0; i < 200; i++)
                    s += "-";

                s += "\r\n";
                s += "Res ID : " + outgoing.ID + " , Process At : " + outgoing.DateProcess.ToString("dd-MMM-yyyy") + " , Sent at : " + outgoing.DateSent.ToString("dd-MMM-yyyy");
                s += "\r\n";
                s += "Text : " + outgoing.MessageText;
                s += "\r\n";
                for (int i = 0; i < 100; i++)
                    s += "=";
            }
            else
            {
                s = "SMS TYPE : REQUEST-RESPONE";
                s += "\r\n";
                for (int i = 0; i < 200; i++)
                    s += "-";

                s += "\r\n";
                s += "\r\n";
                s += "Res ID : " + outgoing.ID.ToString() + " , Process At : " + outgoing.DateProcess.ToString("dd-MMM-yyyy") + " , Sent at : " + outgoing.DateSent.ToString("dd-MMM-yyyy");
                s += "\r\n";
                s += "Text : " + outgoing.MessageText;
                s += "\r\n";
                for (int i = 0; i < 100; i++)
                    s += "=";
            }
            return s;
        }
    
    }
}

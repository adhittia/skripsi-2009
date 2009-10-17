using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Martin.SMS.Exception {
    public class SMSException : System.Exception {
        public SMSException(String message)
            : base("SMS Exception: " + message) {
        }
    }
}

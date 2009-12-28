using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMS_Gateway {
    using Castle.ActiveRecord;
    using Castle.ActiveRecord.Framework.Config;

    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            XmlConfigurationSource source = new XmlConfigurationSource("appconfig.xml");

            ActiveRecordStarter.Initialize(source, typeof(AppData.Menu), typeof(AppData.CustomerProfile),
                                           typeof(AppData.MenuSchedule));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
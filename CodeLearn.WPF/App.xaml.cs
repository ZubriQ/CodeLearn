using CodeLearn.Db;
using CodeLearn.Db.WPF;
using System;
using System.Windows;

namespace CodeLearn.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG         
            StartupUri = new Uri( "Windows/ControlWindow.xaml", UriKind.RelativeOrAbsolute);
#else
            StartupUri = new Uri("Windows/LoginWindow.xaml", UriKind.RelativeOrAbsolute);
#endif            
        }

        private static WPFDatabaseProvider _db;
        public static WPFDatabaseProvider DB
        {
            get
            {
                if( _db == null) {
                    _db = new WPFDatabaseProvider();
                }
                return _db;
            }
        }


        // A Signed-in user.
        public static Student? Student = null;
        public static Teacher? Teacher = null;
    }
}

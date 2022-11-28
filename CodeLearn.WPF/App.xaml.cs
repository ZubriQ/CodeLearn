using CodeLearn.Db;
using CodeLearn.Db.WPF;
using System.Windows;

namespace CodeLearn.WPF
{
    public partial class App : Application
    {
        public static WPFDatabaseProvider DB = new WPFDatabaseProvider();

        // Logged in test user.
        public static Student Student = new Student();
    }
}

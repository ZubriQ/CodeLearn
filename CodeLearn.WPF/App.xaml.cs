using CodeLearn.Db;
using CodeLearn.Db.WPF;
using System.Windows;

namespace CodeLearn.WPF
{
    public partial class App : Application
    {
        public static WPFDatabaseProvider DB = new();

        // A Signed-in user.
        public static Student? Student = null;
        public static Teacher? Teacher = null;
    }
}

using CodeLearn.Db.WPF;
using System.Windows;

namespace CodeLearn.WPF
{
    public partial class App : Application
    {
        public static WPFDatabaseProvider DB = new WPFDatabaseProvider();
    }
}

using CodeLearn.Db;
using CodeLearn.Db.WPF;
using CodeLearn.Lib;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows;

namespace CodeLearn.WPF
{
    public partial class App : Application
    {
        public static UserManager<ApplicationUser> UserManager { get; private set; }
        public static RoleManager<IdentityRole> RoleManager { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG         
            StartupUri = new Uri("Windows/ControlWindow.xaml", UriKind.RelativeOrAbsolute);
#else
            StartupUri = new Uri("Windows/LoginWindow.xaml", UriKind.RelativeOrAbsolute);
#endif

            base.OnStartup(e);

            // Read the connection string from App.config
            string connectionString = ConfigurationManager.ConnectionStrings["CodeLearnDatabase"].ConnectionString;

            // Configure the DbContext and Identity services
            var services = new ServiceCollection();
            services.AddDbContext<CodeLearnContext>(options =>
                options.UseSqlServer(connectionString));

            // Configure the UserManager and RoleManager services
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CodeLearnContext>();

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Set the DbContext, UserManager, and RoleManager properties
            UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _db = new WPFDatabaseProvider(UserManager);
        }

        private static WPFDatabaseProvider _db;
        public static WPFDatabaseProvider DB
        {
            get
            {
                return _db;
            }
        }

        // A Signed-in user.
        public static Student? Student = null;
        public static Teacher? Teacher = null;
    }
}

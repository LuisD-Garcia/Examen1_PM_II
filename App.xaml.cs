using Microsoft.Maui.Controls;
using System.IO;
using Microsoft.Maui.Storage;

namespace Examen1_PM_II
{
    public partial class App : Application
    {
        // ... otras propiedades y constructor

        static Database.EmpleadoDatabase database;

        public static Database.EmpleadoDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database.EmpleadoDatabase(Path.Combine(FileSystem.AppDataDirectory, "Empleados.db3"));
                }
                return database;
            }
        }

        // ... Constructor
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }

}
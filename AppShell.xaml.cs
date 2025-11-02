using Examen1_PM_II.Views; // Asegúrate de incluir el namespace

namespace Examen1_PM_II
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Registra la ruta para poder navegar desde cualquier parte.
            Routing.RegisterRoute(nameof(EmpleadoPage), typeof(EmpleadoPage));
        }
    }
}
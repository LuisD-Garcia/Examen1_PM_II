using Examen1_PM_II.Models;
using Microsoft.Maui.Controls;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Examen1_PM_II.Views
{
    public partial class EmpleadoPage : ContentPage
    {
        private string _fotoBase64;

        public EmpleadoPage()
        {
            InitializeComponent();
            DpFechaIngreso.Date = DateTime.Today; // Establece la fecha de hoy por defecto
        }

        private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
        {
            try
            {
                // Pide al usuario que seleccione un archivo de imagen
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Por favor selecciona una foto de empleado"
                });

                if (result != null)
                {
                    // Obtiene el stream del archivo
                    using (var stream = await result.OpenReadAsync())
                    {
                        // Convierte el stream a un array de bytes
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            byte[] imageBytes = memoryStream.ToArray();

                            // Convierte el array de bytes a Base64 string
                            _fotoBase64 = Convert.ToBase64String(imageBytes);

                            // Muestra la imagen en el control Image
                            ImgFoto.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No se pudo seleccionar la foto: {ex.Message}", "OK");
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text) || string.IsNullOrWhiteSpace(TxtPuesto.Text) || string.IsNullOrWhiteSpace(_fotoBase64))
            {
                await DisplayAlert("Advertencia", "Asegúrate de llenar el nombre, puesto y seleccionar una foto.", "OK");
                return;
            }

            var nuevoEmpleado = new Empleado
            {
                Nombre = TxtNombre.Text,
                Fecha_ingreso = DpFechaIngreso.Date,
                Puesto = TxtPuesto.Text,
                Correo = TxtCorreo.Text,
                Foto = _fotoBase64 // Guarda el Base64 en la BD
            };

            // Guarda en la base de datos
            int resultado = await App.Database.SaveEmpleado(nuevoEmpleado);

            if (resultado > 0)
            {
                await DisplayAlert("Éxito", "Empleado guardado correctamente.", "OK");
                // Regresa a la página anterior (donde se listan los empleados)
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo guardar el empleado.", "OK");
            }
        }
    }
}
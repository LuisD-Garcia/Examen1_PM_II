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

        // Método para seleccionar una foto de la galería
        private async void OnSeleccionarFotoClicked(object sender, EventArgs e)
        {
            await ProcessPhoto(MediaPicker.PickPhotoAsync);
        }

        // *************** NUEVO MÉTODO ***************
        // Método para tomar una foto con la cámara
        private async void OnTomarFotoClicked(object sender, EventArgs e)
        {
            // Se usa CapturePhotoAsync para abrir la cámara
            await ProcessPhoto(MediaPicker.CapturePhotoAsync);
        }

        // Método auxiliar genérico para procesar la foto (ya sea seleccionada o tomada)
        private async Task ProcessPhoto(Func<MediaPickerOptions?, Task<FileResult>> photoAction)
        {
            try
            {
                // La acción a ejecutar (PickPhotoAsync o CapturePhotoAsync)
                var result = await photoAction(new MediaPickerOptions
                {
                    Title = "Por favor selecciona o toma una foto de empleado"
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
                // Manejo de excepciones, por ejemplo, si el usuario deniega permisos de cámara
                await DisplayAlert("Error", $"No se pudo completar la acción de la foto: {ex.Message}", "OK");
            }
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtNombre.Text) || string.IsNullOrWhiteSpace(TxtPuesto.Text) || string.IsNullOrWhiteSpace(_fotoBase64))
            {
                await DisplayAlert("Advertencia", "Asegúrate de llenar el nombre, puesto y seleccionar/tomar una foto.", "OK");
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
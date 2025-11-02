using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Examen1_PM_II.Models;
using Microsoft.Maui.Controls;

namespace Examen1_PM_II.Views
{
    public partial class ListaEmpleadoPage : ContentPage
    {
        public ListaEmpleadoPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await CargarListaEmpleados();
        }

        private async Task CargarListaEmpleados()
        {
            // Usamos RefreshControl para indicar que estamos cargando
            RefreshControl.IsRefreshing = true;

            try
            {
                var lista = await App.Database.GetListaEmpleado();
                // Asignar la lista al ItemsSource del CollectionView
                ListaEmpleados.ItemsSource = new ObservableCollection<Empleado>(lista);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error de Carga", $"Ocurrió un error al cargar los empleados: {ex.Message}", "OK");
            }
            finally
            {
                RefreshControl.IsRefreshing = false;
            }
        }

        private void OnAgregarClicked(object sender, EventArgs e)
        {
            // Navega a la página de ingreso de empleados
            Shell.Current.GoToAsync(nameof(EmpleadoPage));
        }

        private async void OnRefreshViewRefreshing(object sender, EventArgs e)
        {
            // Llama a la función de carga al hacer pull-to-refresh
            await CargarListaEmpleados();
        }
    }
}
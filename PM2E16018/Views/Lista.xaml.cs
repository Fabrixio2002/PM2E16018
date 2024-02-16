using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;

namespace PM2E16018.Views;

public partial class Lista : ContentPage
{
	public Lista()
	{
		InitializeComponent();
	}
    string descripcion;
    string lo;
    string la;
    private void lisPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        {
            // Obtener el elemento seleccionado
            var selectedItem = (Models.Sitios)e.CurrentSelection.FirstOrDefault(); // Asegúrate de reemplazar "Models.Sitios" con el tipo correcto de tu modelo
            if (selectedItem != null)
            {
                // Acceder a los datos del item seleccionado
                 descripcion = selectedItem.Descripcion;
                 lo = selectedItem.Longitud;
                 la = selectedItem.Latitud;
                // Aquí puedes hacer lo que necesites con los datos del item seleccionado


            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listPerson.ItemsSource = await App.Database.GetListPersons();
    }

  

    private  async void Button_Clicked_1(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lo) && !string.IsNullOrEmpty(la))
        {
            double longitud = Convert.ToDouble(lo);
            double latitud = Convert.ToDouble(la);
            await Navigation.PushAsync(new Views.PageMaps(longitud, latitud,descripcion));
        }
        else
        {
            // Manejo de error si la longitud o latitud está vacía
        }
    }
}
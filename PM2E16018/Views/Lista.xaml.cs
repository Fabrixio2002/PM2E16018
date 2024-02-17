using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;
using System.Threading.Tasks;
using SQLite;
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
    string Foto;
    int idSeleccionado;
    private async void lisPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                Foto = selectedItem.foto;

                // Capturar la ID del elemento seleccionado
                idSeleccionado = selectedItem.Id;

                // Ahora puedes usar la variable "idSeleccionado" en cualquier otro lugar de tu código según sea necesario

            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listPerson.ItemsSource = await App.Database.GetListPersons();
    }



    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lo) && !string.IsNullOrEmpty(la))
        {
            double longitud = Convert.ToDouble(lo);
            double latitud = Convert.ToDouble(la);
            await Navigation.PushAsync(new Views.PageMaps(longitud, latitud, descripcion,Foto));

        }
        else
        {
            // Manejo de error si la longitud o latitud está vacía
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {



        // Convertir coordenadas a double

        // Eliminar el elemento seleccionado de la base de datos
        var selectedItem = new Models.Sitios()
        {
            Id = idSeleccionado,
            Descripcion = descripcion,
            Longitud = lo,
            Latitud = la,
            foto = Foto,

        };

        await App.Database.DeletePerson(selectedItem);

        DisplayAlert("ALERTA", "ELEMENTO BORRADO", " OK");

        // Actualizar la lista mostrada en la página
        listPerson.ItemsSource = await App.Database.GetListPersons();

        // Redirigir a la página de mapas con las coordenadas y descripción



    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new Views.Actualizar(lo, la, descripcion, Foto, idSeleccionado));


    }




    private async Task CompartirImagenDesdeBase64(string imagenBase64)
    {
        try
        {
            // Decodificar la cadena Base64 en un array de bytes
            byte[] bytesImagen = Convert.FromBase64String(imagenBase64);

            // Obtener la ruta del archivo temporal para guardar la imagen
            var rutaArchivoTemporal = Path.Combine(FileSystem.CacheDirectory, "imagen_compartida.png");

            // Guardar la imagen en el archivo temporal
            File.WriteAllBytes(rutaArchivoTemporal, bytesImagen);

            // Compartir la imagen utilizando Xamarin.Essentials
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Compartir imagen",
                File = new ShareFile(rutaArchivoTemporal)
            });
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción que pueda ocurrir durante el proceso
            Console.WriteLine($"Error al compartir la imagen: {ex.Message}");
            
        }
    }
}
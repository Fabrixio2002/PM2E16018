using System.Net.NetworkInformation;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Maps;




namespace PM2E16018.Views;

public partial class PageMaps : ContentPage
{
    private string direccion;

    private string img;


    public PageMaps(double longitud, double latitud,String des,String Foto)
    {
        InitializeComponent();
        direccion = des;
        img= Foto;
        Location location = new Location(latitud, longitud);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        Map map = new Map(mapSpan);

        Pin pin = new Pin
        {
            Label = "PIN: " + des,
            Type = PinType.Place,
            Location = location
        };
        map.Pins.Add(pin);

        Content = map;

        ToolbarItem compartirItem = new ToolbarItem
        {
            Text = "Compartir",
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };
        compartirItem.Clicked += ToolbarItem_Clicked;
        ToolbarItems.Add(compartirItem);

    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Text = "La dirección es: " + direccion,
                Title = "Compartir dirección"

        });

            // Verificar si la imagen en formato Base64 está disponible
            if (!string.IsNullOrEmpty(img))
            {
                // Compartir la imagen
                await CompartirImagenDesdeBase64(img);
            }
            else
            {
                // Manejar el caso en el que la imagen no esté disponible
                await DisplayAlert("Error", "La imagen no está disponible para compartir.", "OK");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al compartir la dirección: {ex.Message}");


        }

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
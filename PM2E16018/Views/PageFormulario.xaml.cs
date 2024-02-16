


namespace PM2E16018.Views;

public partial class PageFormulario : ContentPage
{
    Controllers.SitiosControllers sitioDb;
    FileResult photo;

    public PageFormulario()
    {
        InitializeComponent();
        sitioDb = new Controllers.SitiosControllers();
    }

    public string GetImagen()
    {
        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();

                String Base64 = Convert.ToBase64String(data);

                return Base64;
            }
        }
        return null;
    }


    public byte[] GetImagenArray()
    {
        if (photo != null)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Stream stream = photo.OpenReadAsync().Result;
                stream.CopyTo(ms);
                byte[] data = ms.ToArray();



                return data;
            }
        }
        return null;
    }




    private async void Agregar_Clicked(object sender, EventArgs e)
    {
        // Validar si hay una foto seleccionada
        if (foto.Source == null)
        {
            await DisplayAlert("Error", "Por favor, seleccione una foto.", "OK");
            return; // Salir del método si no hay foto
        }

        // Validar si se ha proporcionado una descripción
        if (string.IsNullOrWhiteSpace(txtarea.Text))
        {
            await DisplayAlert("Error", "Por favor, ingrese una descripción.", "OK");
            return; // Salir del método si no hay descripción
        }

        // Obtener la ubicación actual
        var request = new GeolocationRequest(GeolocationAccuracy.Default);
        var location = await Geolocation.GetLocationAsync(request);
        var latitude = location?.Latitude ?? 0;
        var longitude = location?.Longitude ?? 0;
        txtla.Text = latitude.ToString();
        txtlo.Text = longitude.ToString();

        // Crear el objeto Sitio con los datos proporcionados
        var sitio = new Models.Sitios
        {
            Longitud = txtlo.Text,
            Latitud = txtla.Text,
            Descripcion = txtarea.Text,
            foto = GetImagen()
        };

        // Utilizar la instancia existente de sitioDb para almacenar el sitio
        if (await sitioDb.StorePerson(sitio) > 0)
        {
            // Mostrar mensaje de éxito si se guarda correctamente
            await DisplayAlert("Éxito", $"Sitio \"{sitio.Descripcion}\" se ha guardado correctamente.", "OK");
        }
        else
        {
            // Mostrar mensaje de error si no se puede guardar
            await DisplayAlert("Error", "Se produjo un error al guardar el sitio.", "OK");
        }





    }

    private async void fotico_Clicked(object sender, EventArgs e)
    {
        photo = await MediaPicker.CapturePhotoAsync();
        if (photo != null)
        {
            String path = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using Stream sourcephoto = await photo.OpenReadAsync();
            using FileStream streamlocal = File.OpenWrite(path);

            foto.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);

            await sourcephoto.CopyToAsync(streamlocal);
        }
    }

    private void btnlist_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Views.Lista());//Para cambiar de Pantalla
    }
}
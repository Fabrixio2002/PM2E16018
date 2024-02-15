


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
        var request = new GeolocationRequest(GeolocationAccuracy.Default);
        var location = await Geolocation.GetLocationAsync(request);
        var latitude = location.Latitude;
        var longitude = location.Longitude;
        txtla.Text = latitude.ToString();
        txtlo.Text = longitude.ToString();


        var sitio = new Models.Sitios
        {
            Longitud = txtlo.Text,
            Latitud = txtla.Text,
            Descripcion= txtarea.Text,
            foto = GetImagen()

        };


        // Utilizar la instancia existente de PersonDB
        if (await sitioDb.StorePerson(sitio) > 0)
        {
            await DisplayAlert("Éxito", "Persona guardada exitosamente.", "OK");

        }
        else
        {
            await DisplayAlert("Error", $"Se produjo un error: ", "OK");

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
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;

namespace PM2E16018.Views;

public partial class Lista : ContentPage
{
	public Lista()
	{
		InitializeComponent();
	}

    private void lisPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        listPerson.ItemsSource = await App.Database.GetListPersons();
    }

}
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
    
    public PageMaps(double longitud, double latitud,String des)
    {
		InitializeComponent();

        Location location = new Location(latitud, longitud);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        Map map = new Map(mapSpan);

        Pin pin = new Pin
        {
            Label = "PIN:"+des,
            Type = PinType.Place,
            Location = location
        };
        map.Pins.Add(pin);

        Content = map;

    }


   
}
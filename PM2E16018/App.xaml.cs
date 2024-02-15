namespace PM2E16018
{
    public partial class App : Application
    {

        static Controllers.SitiosControllers database;

        public static Controllers.SitiosControllers Database
        {
            get
            {
                if (database == null)
                {
                    database = new Controllers.SitiosControllers();
                }
                return database;
            }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.PageFormulario());//PANTALLA QUE INICIA AL EJECUTAR

        }
    }
}

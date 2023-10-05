namespace PathfinderCampaignManager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.PlayerPage), typeof(Views.PlayerPage));
        }
    }
}
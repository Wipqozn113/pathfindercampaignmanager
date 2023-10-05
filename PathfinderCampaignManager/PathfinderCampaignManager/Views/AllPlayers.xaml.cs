namespace PathfinderCampaignManager.Views;

public partial class AllPlayersPage : ContentPage
{
    public AllPlayersPage()
    {
        InitializeComponent();

        BindingContext = new Models.AllPlayers();
    }

    protected override void OnAppearing()
    {
        ((Models.AllPlayers)BindingContext).LoadPlayers();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(PlayerPage));
    }

    private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var player = (Models.Player)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(PlayerPage)}?{nameof(PlayerPage.ItemId)}={player.Filename}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}
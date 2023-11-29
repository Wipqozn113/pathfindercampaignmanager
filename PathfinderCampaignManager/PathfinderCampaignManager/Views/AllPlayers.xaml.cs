namespace PathfinderCampaignManager.Views;

public partial class AllPlayersPage : ContentPage
{
    public AllPlayersPage()
    {
        InitializeComponent();

        BindingContext = new Models.Data.AllPlayers();
    }

    protected override void OnAppearing()
    {

/* Unmerged change from project 'PathfinderCampaignManager (net7.0-ios)'
Before:
        ((Models.AllPlayers)BindingContext).LoadPlayers();
After:
        ((AllPlayers)BindingContext).LoadPlayers();
*/
        ((Models.Data.AllPlayers)BindingContext).LoadPlayers();
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

/* Unmerged change from project 'PathfinderCampaignManager (net7.0-ios)'
Before:
            var player = (Models.Player)e.CurrentSelection[0];
After:
            var player = (Player)e.CurrentSelection[0];
*/
            var player = (Models.Data.Player)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Shell.Current.GoToAsync($"{nameof(PlayerPage)}?{nameof(PlayerPage.ItemId)}={player.Name}");

            // Unselect the UI
            notesCollection.SelectedItem = null;
        }
    }
}
using PathfinderCampaignManager.Helpers;
using PathfinderCampaignManager.Models;

namespace PathfinderCampaignManager.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class PlayerPage : ContentPage
{
	public PlayerPage()
	{
		InitializeComponent();
        LoadPlayer();
    }

    public string ItemId
    {
        set { LoadPlayer(value); }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Player player)
            player.Save();  
        //FileHelper.WriteToJsonFile(Path.Combine(FileSystem.AppDataDirectory, $"{player.Name}.players.txt"), player);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Player player)
        {
            // Delete the file.
            player.Delete();
        }

        await Shell.Current.GoToAsync("..");
    }

    private async void LoadPlayer(string playerName = "")
    {
        var PlayerModel = await Player.LoadPlayer(playerName);

        if (PlayerModel is null)
            PlayerModel = new Player();

        BindingContext = PlayerModel;
    }
}
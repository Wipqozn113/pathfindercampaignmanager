using PathfinderCampaignManager.Helpers;

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
            FileHelper.WriteToJsonFile(Path.Combine(FileSystem.AppDataDirectory, $"{player.Name}.players.txt"), player);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Player player)
        {
            // Delete the file.
            if (File.Exists(player.Filename))
                File.Delete(player.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadPlayer(string fileName = "")
    {
        Models.Player PlayerModel = new Models.Player();
        PlayerModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            PlayerModel.Date = File.GetCreationTime(fileName);
            var player = FileHelper.ReadFromJsonFile<Models.Player>(fileName);
            PlayerModel.Name = player.Name;
            PlayerModel.CharacterName = player.CharacterName;
            PlayerModel.PathbuilderLink = player.PathbuilderLink;
        }

        BindingContext = PlayerModel;
    }
}
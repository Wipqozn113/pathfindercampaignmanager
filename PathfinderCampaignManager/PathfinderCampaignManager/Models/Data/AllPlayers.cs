using System.Collections.ObjectModel;
using PathfinderCampaignManager.Data;

namespace PathfinderCampaignManager.Models.Data;

internal class AllPlayers
{
    public AllPlayers()
    {
        Database = new PlayerDBManager();
        LoadPlayers();
    }

    private PlayerDBManager Database { get; set; }

    public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

    public async void LoadPlayers()
    {
        Players.Clear();

        // Use Linq extensions to load the *.players.txt files.
        IEnumerable<Player> players = await Database.GetItemsAsync();

        // Add each player into the ObservableCollection
        foreach (Player player in players)
            Players.Add(player);
    }
}

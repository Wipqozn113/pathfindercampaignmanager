using System.Collections.ObjectModel;
using PathfinderCampaignManager.Helpers;

namespace PathfinderCampaignManager.Models;

internal class AllPlayers
{
    public ObservableCollection<Player> Players { get; set; } = new ObservableCollection<Player>();

    public AllPlayers() =>
        LoadPlayers();

    public void LoadPlayers()
    {
        Players.Clear();

        // Get the folder where the players are stored.
        string appDataPath = FileSystem.AppDataDirectory;

        // Use Linq extensions to load the *.players.txt files.
        IEnumerable<Player> players = Directory

                                    // Select the file names from the directory
                                    .EnumerateFiles(appDataPath, "*.players.txt")

                                    // Each file name is used to create a new Player
                                    .Select(filename => new Player()
                                    {
                                        Filename = filename,
                                        Name = FileHelper.ReadFromJsonFile<Player>(filename).Name,
                                        Date = File.GetCreationTime(filename)
                                    })

                                    // With the final collection of players, order them by date
                                    .OrderBy(player => player.Date);

        // Add each player into the ObservableCollection
        foreach (Player player in players)
            Players.Add(player);
    }
}

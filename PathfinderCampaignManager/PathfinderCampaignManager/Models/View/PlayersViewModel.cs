using CommunityToolkit.Mvvm.Input;
using PathfinderCampaignManager.Models.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System;
using PathfinderCampaignManager.Views;

namespace PathfinderCampaignManager.Models.View;

internal class PlayersViewModel : IQueryAttributable
{
    public ObservableCollection<PlayerViewModel> AllPlayers { get; }
    public ICommand NewCommand { get; }
    public ICommand SelectNoteCommand { get; }

    public PlayersViewModel()
    {
        AllPlayers = new ObservableCollection<PlayerViewModel>(Player.LoadAll().Result.Select(n => new PlayerViewModel(n)));
        NewCommand = new AsyncRelayCommand(NewPlayerAsync);
        SelectNoteCommand = new AsyncRelayCommand<PlayerViewModel>(SelectPlayerAsync);
    }

    private async Task NewPlayerAsync()
    {
        await Shell.Current.GoToAsync(nameof(Views.PlayerPage));
    }

    private async Task SelectPlayerAsync(PlayerViewModel player)
    {
        if (player is not null)
            await Shell.Current.GoToAsync($"{nameof(PlayerPage)}?load={player.Identifier}");
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("deleted"))
        {
            var playerId = int.Parse(query["deleted"].ToString());
            PlayerViewModel matchedPlayer = AllPlayers.Where((n) => n.Identifier == playerId).FirstOrDefault();

            // If note exists, delete it
            if (matchedPlayer != null)
                AllPlayers.Remove(matchedPlayer);
        }
        else if (query.ContainsKey("saved"))
        {
            var playerId = int.Parse(query["saved"].ToString());
            PlayerViewModel matchedPlayer = AllPlayers.Where((n) => n.Identifier == playerId).FirstOrDefault();

            // If note is found, update it
            if (matchedPlayer != null)
                matchedPlayer.Reload();

            // If note isn't found, it's new; add it.
            else
                AllPlayers.Add(new PlayerViewModel(Player.Load(playerId).Result));
        }
    }
}

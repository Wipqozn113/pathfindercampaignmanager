using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using PathfinderCampaignManager.Models;
using PathfinderCampaignManager.Models.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PathfinderCampaignManager.Models.View
{
    public class PlayerViewModel : ObservableObject, IQueryAttributable
    {
        private Player _player;

        public PlayerViewModel()
        {
            _player= new Player();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public PlayerViewModel(Player player)
        {
            _player = player;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        public string Name
        {
            get => _player.Name;
            set
            {
                if (_player.Name != value)
                {
                    _player.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CharacterName
        {
            get => _player.CharacterName;
            set
            {
                if (_player.CharacterName != value)
                {
                    _player.CharacterName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string PathbuilderLink
        {
            get => _player.PathbuilderLink;
            set
            {
                if (_player.PathbuilderLink != value)
                {
                    _player.PathbuilderLink = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Identifier => _player.ID;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        private async Task Save()
        {
            _player.Date = DateTime.Now;
            _player.Save();
            await Shell.Current.GoToAsync($"..?saved={_player.ID}");
        }

        private async Task Delete()
        {
            _player.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_player.ID}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _player = Player.Load(query["load"].ToString()).Result;
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _player = Player.Load(_player.Name).Result;
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
        }
    }
}

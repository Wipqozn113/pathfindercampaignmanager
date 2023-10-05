using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using PathfinderCampaignManager.Helpers;
using Microsoft.Maui.Storage;
using PathfinderCampaignManager.Data;
using PathfinderCampaignManager.Config;
using SQLite;

namespace PathfinderCampaignManager.Models
{
    public class Player
    {
        public Player()
        {
            Database = new PlayerDBManager();
        }

        private PlayerDBManager Database { get; set; }

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Filename { get; set; }

        public string Filepath
        {
            get
            {
                return System.IO.Path.Combine(FileSystem.AppDataDirectory, $"{Name}.players.txt");
            }
        }

        public string Name { get; set; }
        public string CharacterName { get; set; }
        public string PathbuilderLink { get; set; }
        public DateTime Date { get; set; }

        public async void Save() =>
            await Database.SaveItemAsync(this);

        public async void Delete() =>
            await Database.DeleteItemAsync(this);

        public async static Task<Player> LoadPlayer(string playerName)
        {
            var db = new PlayerDBManager();
            var player = await db.GetPlayerAsync(playerName);
            
            return player;
        }
    }
}

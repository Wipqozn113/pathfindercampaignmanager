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

/* Unmerged change from project 'PathfinderCampaignManager (net7.0-ios)'
Before:
using SQLite;
After:
using SQLite;
using PathfinderCampaignManager;
using PathfinderCampaignManager.Models;
using PathfinderCampaignManager.Models.Data;
*/
using SQLite;

namespace PathfinderCampaignManager.Models.Data
{
    public class Player : DatabaseModel
    {
        public Player()
        {
            Database = new PlayerDBManager();
        }

        private PlayerDBManager Database { get; set; }

        public string Filename { get; set; }

        public string Filepath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, $"{Name}.players.txt");
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

        public async static Task<Player> Load(int playerId)
        {
            var db = new PlayerDBManager();
            var player = await db.GetItemAsync(playerId);

            return player;
        }

        public async static Task<Player> Load(string playerName)
        {
            var db = new PlayerDBManager();
            var player = await db.GetPlayerAsync(playerName);

            return player;
        }

        public async static Task<List<Player>> LoadAll()
        {
            var db = new PlayerDBManager();
            var players = await db.GetItemsAsync();

            return players;
        }
    }
}

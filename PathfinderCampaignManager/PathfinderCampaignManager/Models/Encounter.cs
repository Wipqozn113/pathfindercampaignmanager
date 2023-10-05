using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderCampaignManager.Helpers;

namespace PathfinderCampaignManager.Models
{
    public class Encounter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public string Filename { get; set; }

        public string Filepath
        {
            get
            {
                return System.IO.Path.Combine(FileSystem.AppDataDirectory, $"{Name}.{Location}.encounters.txt");
            }
        }

        public string Creature1Name { get; set; }
        public string Creature1NethysLink { get; set; }
        public int Creature1Count { get; set; }

        public string Creature2Name { get; set; }
        public string Creature2NethysLink { get; set; }
        public int Creature2Count { get; set; }

        public string Creature3Name { get; set; }
        public string Creature3NethysLink { get; set; }
        public int Creature3Count { get; set; }

        public string Creature4Name { get; set; }
        public string Creature4NethysLink { get; set; }
        public int Creature4Count { get; set; }

        public void Save() =>
            FileHelper.WriteToJsonFile(Filepath, this);

        public void Delete() =>
            File.Delete(Filepath);

        public static Encounter LoadEncounter(string playerName)
        {
            var filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, $"{playerName}.players.txt");

            if (!File.Exists(filename))
                return null;

            var player = FileHelper.ReadFromJsonFile<Encounter>(filename);

            return player;
        }

        public static Encounter LoadEncounterFromFilename(string filename)
        {
            if (!File.Exists(filename))
                return null;

            var player = FileHelper.ReadFromJsonFile<Encounter>(filename);

            return player;
        }
    }
}

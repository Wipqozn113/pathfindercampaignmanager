using PathfinderCampaignManager.Data;
using PathfinderCampaignManager.Helpers;
using SQLite;
using System.Text.Json;

namespace PathfinderCampaignManager.Models.Data
{
    public class Encounter : DatabaseModel
    {
        private EncounterDBManager Database { get; set; }

        public Encounter()
        {
            Database = new EncounterDBManager();
        }


        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        [Ignore]
        public List<Creature> Creatures { get; set; } = new List<Creature>();

        public string CreaturesJson
        {
            get
            {
                if (_creaturesJson is null)
                    _creaturesJson = JsonSerializer.Serialize(Creatures);

                return _creaturesJson;
            }
            set
            {
                _creaturesJson = value;
                Creatures = JsonSerializer.Deserialize<List<Creature>>(value);
            }
        }

        private string _creaturesJson = null;


        public async void Save() =>
            await Database.SaveItemAsync(this);

        public async void Delete() =>
            await Database.DeleteItemAsync(this);

        public async static Task<Encounter> LoadEncounter(int id)
        {
            var db = new EncounterDBManager();
            var encounter = await db.GetItemAsync(id);

            return encounter;
        }

        public static Encounter LoadEncounterFromFilename(string filename)
        {
            if (!File.Exists(filename))
                return null;

            var player = FileHelper.ReadFromJsonFile<Encounter>(filename);

            return player;
        }
    }

    public class Creature
    {
        public string Name { get; set; }
        public string NethysLink { get; set; }
        public string Count { get; set; }
    }
}

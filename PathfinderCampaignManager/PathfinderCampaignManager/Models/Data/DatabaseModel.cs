using SQLite;

namespace PathfinderCampaignManager.Models.Data
{
    public class DatabaseModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}

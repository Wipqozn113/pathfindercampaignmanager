using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderCampaignManager.Models.Data;

namespace PathfinderCampaignManager.Data
{
    public class PlayerDBManager : DBManager<Player>
    {
        public async Task<Player> GetPlayerAsync(string name)
        {
            await Init();
            return await Database.Table<Player>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }
    }
}

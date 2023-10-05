using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderCampaignManager.Models;
using System.Threading.Tasks;

namespace PathfinderCampaignManager.Data
{
    public class PlayerDBManager : DBManager<Player>
    {
        public async Task<Player> GetPlayerAsync(string name)
        {
            await Init();
            return await Database.Table<Player>().Where(i => i.Name == name).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Player item)
        {
            await Init();
            if (item.ID != 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                return await Database.InsertAsync(item);
            }
        }
    }
}

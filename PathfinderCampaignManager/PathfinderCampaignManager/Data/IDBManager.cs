using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderCampaignManager.Models.Data;

namespace PathfinderCampaignManager.Data
{
    public interface IDBManager<T> where T : DatabaseModel, new()
    {
        public Task<int> SaveItemAsync(T item);
    }
}

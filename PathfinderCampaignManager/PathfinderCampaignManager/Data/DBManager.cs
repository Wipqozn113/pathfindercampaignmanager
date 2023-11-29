using SQLite;
using PathfinderCampaignManager.Config;
using PathfinderCampaignManager.Models.Data;

namespace PathfinderCampaignManager.Data;

public class DBManager<T>  where T : DatabaseModel, new()
{
    protected SQLiteAsyncConnection Database;
    public DBManager()
    {
    }
    protected async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(DatabaseConfig.DatabasePath, DatabaseConfig.Flags);
        var result = await Database.CreateTableAsync<T>();
    }

    public async Task<T> GetItemAsync(int id)
    {
        await Init();
        return await Database.Table<T>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<T>().ToListAsync();
    }  

    public async Task<int> DeleteItemAsync(T item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }

    public async Task<int> SaveItemAsync(T item)
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

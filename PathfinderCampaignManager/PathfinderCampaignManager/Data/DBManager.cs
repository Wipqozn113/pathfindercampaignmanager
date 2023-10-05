using SQLite;
using PathfinderCampaignManager.Models;
using PathfinderCampaignManager.Config;

namespace PathfinderCampaignManager.Data;

public class DBManager<T> where T : new()
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
}

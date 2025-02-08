using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AquaMate
{
    public class LocalDbService
    {
        //Datenbank

        private const string DbName = "AquaMate.db3";
        private static LocalDbService? _instance;
        private readonly SQLiteAsyncConnection _connection;

        //globaler Zugriff
        public static LocalDbService Instance => _instance ??= new LocalDbService();


        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DbName));
            _ = initializeDatabase();
        }

        private async Task initializeDatabase()
        {
           await _connection.CreateTableAsync<Streakdays>();
        }


        public async Task<List<Streakdays>> GetStreakdates()
        {
            return await _connection.Table<Streakdays>().ToListAsync();
        }

        public async Task<Streakdays> GetById(int id)
        {
            return await _connection.Table<Streakdays>().Where(x => x.Id == id).FirstOrDefaultAsync();  
        }

        public async Task<int> Create(Streakdays streakdays)
        {
            return await _connection.InsertAsync(streakdays);
        }

        public async Task<List<Streakdays>> GetDatesErreicht()
        {
            return await _connection.Table<Streakdays>()
                            .Where(x => x.Erreicht == true)
                            .ToListAsync();

        }


        public async Task Delete(Streakdays streakdays)
        {
            await _connection.DeleteAsync(streakdays);
        }   

        public async Task<List<Streakdays>> GetLast7Days()
        {
            return await _connection.Table<Streakdays>().OrderByDescending(x => x.Dates).Take(7).ToListAsync();
        }
    }
}

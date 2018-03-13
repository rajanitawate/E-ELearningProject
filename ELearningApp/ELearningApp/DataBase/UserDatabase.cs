using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELearningApp.DataBase
{
   public class UserDatabase
    {
        readonly SQLiteAsyncConnection database;

        public UserDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);          
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return database.Table<User>().ToListAsync();
        }

        public Task<int> SaveUserAsync(User user)
        {           
           return database.InsertAsync(user);            
        }        
    }

    public class User
    {
        [PrimaryKey, AutoIncrement]  
        public int StatusId { get; set; }
        public bool Status { get; set; }
    }
}

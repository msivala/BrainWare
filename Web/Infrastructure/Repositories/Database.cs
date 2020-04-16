using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Web.Infrastructure.Repositories
{
    public class Database
    {
        private readonly IDbConnection _connection;

        static Database()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public Database()
        {
            _connection = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=BrainWAre;Integrated Security=SSPI;AttachDBFilename=C:\Users\bigwheels\source\repos\BrainWare\Web\App_Data\BrainWare.mdf");

            _connection.Open();
        }

        public async Task ExecuteQueryAsync(Func<IDbConnection, Task> query)
        {
            await query(_connection);
        }
    }
}
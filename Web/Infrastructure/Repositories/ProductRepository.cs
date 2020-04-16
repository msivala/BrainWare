using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Infrastructure.Repositories
{
    public class ProductRepository
    {
        private const string ProductsQuery = @"
            SELECT * FROM Product
        ";

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var database = new Database();

            IEnumerable<Product> products = null;
            await database.ExecuteQueryAsync(async connection =>
            {
                products = await connection.QueryAsync<Product>(ProductsQuery);
            });

            return products;
        }
    }
}
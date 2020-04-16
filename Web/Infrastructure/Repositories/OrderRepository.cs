using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Web.Models;

namespace Web.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private const string OrdersQuery = @"
            SELECT c.name as CompanyName, o.*, op.*
            FROM
                Company c
                INNER JOIN
                [Order] o on c.company_id = o.company_id
                INNER JOIN
                OrderProduct op on op.order_id = o.order_id
            WHERE
                c.company_id = @id
        ";

        public async Task<IEnumerable<Order>> GetOrdersForCompany(int companyId)
        {
            var database = new Database();

            var orders = new Dictionary<int, Order>();
            await database.ExecuteQueryAsync(async connection =>
            {
                await connection.QueryAsync<Order, OrderProduct, Order>(
                    OrdersQuery, 
                    (order, orderProduct) => {
                        if (!orders.TryGetValue(order.OrderId, out Order existingOrder))
                        {
                            existingOrder = order;
                            orders[order.OrderId] = existingOrder;
                        }

                        existingOrder.OrderProducts.Add(orderProduct); 

                        return order; 
                    },
                    new { id = companyId },
                    splitOn: "order_id");
            });

            return orders.Values;
        }
    }
}
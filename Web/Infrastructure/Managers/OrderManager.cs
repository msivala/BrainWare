using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Infrastructure.Repositories;
using Web.Models;

namespace Web.Infrastructure.Managers
{
    public class OrderManager
    {
        public async Task<IEnumerable<Order>> GetOrdersForCompany(int companyId)
        {
            var productRepository = new ProductRepository();
            var orderRepository = new OrderRepository();

            var products = await productRepository.GetProducts();
            var orders = await orderRepository.GetOrdersForCompany(companyId);

            foreach(var order in orders)
            {
                foreach(var orderProduct in order.OrderProducts)
                {
                    orderProduct.Product = products.FirstOrDefault(product => product.ProductId == orderProduct.ProductId);
                }
            }

            return orders;
        }
    }
}
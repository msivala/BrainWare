using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using Web.Models;
using Web.Infrastructure.Managers;
using System.Web.Http.Description;
using System.Linq;

namespace Web.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Order>))]
        public async Task<IHttpActionResult> GetOrders(int id = 1)
        {
            var orderManager = new OrderManager();

            var data = await orderManager.GetOrdersForCompany(id);

            if (data.Count() > 0)
            {
                return Ok(data);
            }

            return NotFound();
        }
    }
}

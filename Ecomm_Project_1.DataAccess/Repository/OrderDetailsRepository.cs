using Ecomm_Project_1.DataAccess.Data;
using Ecomm_Project_1.DataAccess.Repository.IRepository;
using Ecomm_Project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Project_1.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailsRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public void Update(OrderDetails orderDetails)
        {
            _context.OrderDetails.Update(orderDetails);
        }
    }
}

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
    public class BookTypeRepository : Repository<BookType>, IBookTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public BookTypeRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public void Update(BookType bookType)
        {
            _context.Update(bookType);
        }
    }
}

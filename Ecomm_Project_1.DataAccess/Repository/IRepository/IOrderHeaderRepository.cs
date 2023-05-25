﻿using Ecomm_Project_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Project_1.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository: IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}

using Ecomm_Project_1.DataAccess.Repository.IRepository;
using Ecomm_Project_1.Models;
using Ecomm_Project_1.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_Project_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PendingOrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PendingOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll(StartEndDate d)
        {
            if (d! == null)
            {
                var orderListWithDate = _unitOfWork.OrderHeader.GetAll(oh => oh.OrderStatus == SD.OrderStatusPending)
                    .Where(oh => oh.OrderDate >= d.Start).Where(oh => oh.OrderDate <= d.End); ;
                return Json(new { data = orderListWithDate });
            }

            var orderList = _unitOfWork.OrderHeader.GetAll(oh => oh.OrderStatus == SD.OrderStatusPending);
            return Json(new { data = orderList });
        }
        #endregion
    }
}

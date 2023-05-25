using Ecomm_Project_1.DataAccess.Repository.IRepository;
using Ecomm_Project_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_Project_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BookTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region APIs
        public IActionResult GetAll()
        {
            var bookTypeList = _unitOfWork.BookType.GetAll();
            return Json(new { DataAccess = bookTypeList });
        }
        #endregion

        public IActionResult Upsert(int? id)
        {
            BookType bookType = new BookType();
            if (id == null) return View(bookType);

            bookType = _unitOfWork.BookType.Get(id.GetValueOrDefault());
            if (bookType == null) return NotFound();
            return View(bookType);
        }


    }
}

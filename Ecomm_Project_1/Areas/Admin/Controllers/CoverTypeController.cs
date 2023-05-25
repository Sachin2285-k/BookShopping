using Dapper;
using Ecomm_Project_1.DataAccess.Repository.IRepository;
using Ecomm_Project_1.Models;
using Ecomm_Project_1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_Project_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            // return Json(new { data = _unitOfWork.CoverType.GetAll() });

            return Json(new { data = _unitOfWork.SP_CALL.List<CoverType>(SD.Proc_GetCoverTypes) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // var coverTypeFromDb = _unitOfWork.CoverType.Get(id);
            var param = new DynamicParameters();
            param.Add("@id", id);
            var coverTypeFromDb = _unitOfWork.SP_CALL.OneRecord<CoverType>(SD.Proc_GetCoverType, param);
            if (coverTypeFromDb == null)
                return Json(new { success = false, message = "Something went wrong while delete data!!!" });

            _unitOfWork.SP_CALL.Execute(SD.Proc_DeleteCoverType, param);
            // _unitOfWork.CoverType.Remove(coverTypeFromDb);           
            // _unitOfWork.Save();
            return Json(new { success = true, message = "Data deleted successfully" });
        }
        #endregion

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null) return View(coverType); // Create

            //Edit

            var param = new DynamicParameters();
            param.Add("@id", id.GetValueOrDefault());
            coverType = _unitOfWork.SP_CALL.OneRecord<CoverType>(SD.Proc_GetCoverType, param);
           
            // coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

            if (coverType == null) return NotFound();
            return View(coverType);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return NotFound();
            if (!ModelState.IsValid) return View();
            var param = new DynamicParameters();
            param.Add("@name", coverType.Name);
            if (coverType.Id == 0)
                // _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.SP_CALL.Execute(SD.Proc_CreateCoverType, param);
            else
            {
                // _unitOfWork.CoverType.Update(coverType);
                param.Add("@id", coverType.Id);
                _unitOfWork.SP_CALL.Execute(SD.Proc_UpdateCoverType, param);
            }
            // _unitOfWork.Save();
            return RedirectToAction(nameof(Index));

        }
    }
}

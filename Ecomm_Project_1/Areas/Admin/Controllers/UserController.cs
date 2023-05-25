using Ecomm_Project_1.DataAccess.Data;
using Ecomm_Project_1.Models;
using Ecomm_Project_1.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_Project_1.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.Include(a => a.Company).ToList(); //AspNet Users
            var roles = _context.Roles.ToList(); //AspNet Roles
            var userRoles = _context.UserRoles.ToList(); //AspNet UserRoles

            foreach (var user in userList)
            {
                var roleId = userRoles.FirstOrDefault(r => r.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            userList.Remove(adminUser);

            return Json(new { data = userList });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            var isLocked = false;
            var userInDb = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (User.IsInRole(SD.Role_Admin))
            {
                if (userInDb == null)
                    return Json(new { success = false, message = "Something went wrong while lock or unlock users" });

                if (userInDb != null && userInDb.LockoutEnd > DateTime.Now)
                {
                    userInDb.LockoutEnd = DateTime.Now;
                    isLocked = false;
                }

                else
                {
                    userInDb.LockoutEnd = DateTime.Now.AddYears(100);
                    isLocked = true;
                }


                _context.SaveChanges();

                return Json(new { success = true, message = isLocked == true ? "User successfully locked" : "User successfully unlocked" });
            }

            return View();
        }

        #endregion
    }
}

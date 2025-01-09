using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Data;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class UserController : Controller
    {
        private readonly OnlineShoppingContext _context;

        public UserController(OnlineShoppingContext context)
        {
            _context = context;
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: User/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: User/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName,Password,ConfirmPassword")] UserViewModel userView)
        {
            if (ModelState.IsValid)
            {
                //linq
                var userExists=(from u in _context.User where u.UserName == userView.UserName select u).ToList();
                if (userExists.Count > 0)
                {
                    ViewData["ErrorMessage"] = "User already exist";

                }
                else
                {
                    User user = new User();
                    user.UserName = userView.UserName;
                    user.Password = userView.Password;
                    user.Role = "normal";
                    user.Status = false;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(Index));
                    ViewData["SuccessMessage"] = "User Successfully registered";

                }
            }
            return View(userView);
        }
        // GET: User/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                //linq
                var userLogin = (from u in _context.User where u.UserName == loginView.UserName && u.Password==loginView.Password select u).FirstOrDefault();
                if (userLogin!=null)
                {
                    //if (!userLogin.Status)
                    //{
                    //    //if the account is inactive then
                    //    ViewData["ErrorMessage"] = "Your account is inactive .Please contact the support";
                    //    return View(loginView);
                    //}
                    ViewData["SuccessMessage"] = "Account logged in successfully";
                    //For successful login setting session
                   // HttpContext.Session.SetString("UserName",userLogin.UserName);
                   // HttpContext.Session.SetString("User Role",userLogin.Role);
                    //Redirection to Home for successful Login
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    //if no matching user found
                    ViewData["ErrorMesage"] = "Invalid Username or Password";
              

                }
            }
            return View(loginView);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,Role,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,Role,Status")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
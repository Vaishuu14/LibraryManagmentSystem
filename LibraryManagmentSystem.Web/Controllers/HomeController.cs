using LibraryManagmentSystem.Domain.Entities;
using LibraryManagmentSystem.Domain.Interfaces;
using LibraryManagmentSystem.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagmentSystem.Web.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
      

        public HomeController(
            IUserService userService,
            IBookRepository bookRepository,
            IMemberRepository memberRepository
           ) // Inject IAuthService
        {
            _userService = userService;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
            // Initialize the auth service
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                var user = await _userService.ValidateUserAsync(model.UserName, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }

                // Optionally set a cookie for the logged-in user
                Response.Cookies.Append("UserId", user.Id.ToString(), new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(60)
                });

                // Redirect based on user role
                if (user.Role == "Admin")
                {
                    return RedirectToAction("AdminDashboard", "Home");
                }
                else if (user.Role == "Member")
                {
                    return RedirectToAction("MemberDashboard", "Home");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred.");
                return View(model);
            }
        }


       // [Authorize]
        public async Task<IActionResult> AdminDashboard()
        {
            var totalBooks = await _bookRepository.GetTotalBooksAsync();
            var totalMembers = await _memberRepository.GetTotalMembersAsync();

            var viewModel = new AdminDashboardView
            {
                TotalBooks = totalBooks,
                TotalMembers = totalMembers,
            };

            return View(viewModel);
        }


        //new
       // [Authorize(Roles = "Member")]
        [HttpGet]
        public async Task<IActionResult> MemberDashboard()
        {
            
            var books = await _bookRepository.GetAllAsync();
            
            return View(books);  
        }

        //new

        [HttpGet]
        public async Task<IActionResult> MemberBookDetails(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);  // Assuming the view is located at Views/Home/MemberBookDetails.cshtml
        }

        


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Registration model)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(model);
                // Optionally, sign in the user after registration
                return RedirectToAction("Login"); // Redirect to login page
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Remove the authentication token from cookies
            Response.Cookies.Delete("AuthToken");

            // Optionally, sign out from ASP.NET Identity (if used)
            // await _signInManager.SignOutAsync();

            TempData["LogoutMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login" , "Home");
        }
       
    }
}

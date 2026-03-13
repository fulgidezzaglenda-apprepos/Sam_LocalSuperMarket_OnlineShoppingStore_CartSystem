using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Sam_LocalSuperMarket_OnlineShoppingStore1.Data;
using System.Security.Claims;

namespace Sam_LocalSuperMarket_OnlineShoppingStore1.Controllers
{
    public class UserLoginController : Controller
    {
        private readonly SamOnlineShoppingStoreDBContext _dbContext;
        public UserLoginController(SamOnlineShoppingStoreDBContext dbContext)
        { 
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserLogin([FromQuery] string returnUrl)
        {
            //Pass the returnUrl into the new UserDTO.
            UserDTO user = new UserDTO()
            {
                //Check the login Url is not empty before passing it over.
                //If it is, change it to '/Home'.
                ReturnUrl = String.IsNullOrWhiteSpace(returnUrl) ? "/Home" : returnUrl
            };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserLogin(UserDTO user)
        {
            var account = _dbContext.Users.Where(a => a.UserName == user.UserName).FirstOrDefault();
            if (account == null)
            {
                ViewBag.LoginError = "Username or Password inccorrect";
                return View(user);
            }

            if (BCrypt.Net.BCrypt.EnhancedVerify(user.Password, account.PasswordHash) == false)
            {
                ViewBag.LoginError = "Username or Password incorrect";
                return View(user);
            }

            //Create a list of claims associated with the logged in user. These will normally be taken from their user profile.
            var claims = new List<Claim>
            { 
                //Add the claim details for the user.
                new Claim(ClaimTypes.Name, account.UserName),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim("Department", "Manager")
            };

            //Set any additional properties for this user's login.
            var userProperties = new AuthenticationProperties
            {
                //Sets whether the cliding expiry is allowed for this user. Default is true.
                AllowRefresh = true,
                //Whether the login is persistent across all requests.
                IsPersistent = true,
            };

            //Creates a user identity which will be used for the authentication system.
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Signs in the user using the previously setup details.
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                    new ClaimsPrincipal(claimIdentity),
                                    userProperties);
            HttpContext.Session.SetInt32("ID", account.Id);

            //Redirect the user back to where they were trying to go before being made to log in.
            return Redirect(user.ReturnUrl);
        }

        public IActionResult LogOff()
        {
            //Logs the current user out of the system.
            HttpContext.SignOutAsync();
            HttpContext.Session.SetInt32("ID", -1);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(CreateUserDTO user)
        {
            //Check that the username and confirmation are the same.
            if (user.Password.Equals(user.PasswordConfirmation) == false)
            {
                //Create message and return if not matching.
                ViewBag.CreateUserError = "Password and Password confirmation do not match";
                return View(user);
            }

            //Check if the usenamre is already taken.
            if (_dbContext.Users.Any(a => a.UserName == user.UserName))
            {
                //Create a message and return it is taken.
                ViewBag.CreateUserError = "Username already exists.";
                return View(user);
            }

            //Create a new admin user object and fill it using our bcrypt.
            User newUser = new User
            {
                UserName = user.UserName,
                PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.Password),
                Role = "Customer"
            };
            //Add the new user to the database and save it.
            _dbContext.Add(newUser);
            _dbContext.SaveChanges();

            return RedirectToAction("UserLogin", "UserLogin");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }

    public enum Roles
    {
        Admin,
        Customer,
        Guest
    }
}

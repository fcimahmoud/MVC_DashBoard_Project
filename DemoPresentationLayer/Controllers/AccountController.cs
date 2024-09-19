global using Microsoft.AspNetCore.Identity;

namespace DemoPresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;
            if (result.Succeeded)
                return RedirectToAction(nameof(Login));
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
            // 1. Server Side Validation
			if(!ModelState.IsValid) return View(model);

            // 2. Check If User Exist
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if(user is not null)
            {
                // 3. Check Password Correct
                if (_userManager.CheckPasswordAsync(user, model.Password).Result)
                {
                    // 4. Login If Password is Correct
                    var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", string.Empty));
                }
            }

			ModelState.AddModelError(string.Empty, "InCorrect Email Or Password");
			return View(model);
		}
	}
}

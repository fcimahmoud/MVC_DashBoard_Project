
using System.Data;

namespace DemoPresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(string? email)
		{
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Roles = _userManager.GetRolesAsync(u).GetAwaiter().GetResult()
                }).ToListAsync();

                return View(users);
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return View(Enumerable.Empty<UserViewModel>());

            // Manual Mapping
            var model = new List<UserViewModel>
            {
                new UserViewModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user)
                }
            };

            #region Auto Mapper

            //var model = new List<UserViewModel>
            //{
            //    _mapper.Map<ApplicationUser, UserViewModel>(user)
            //};

            #endregion

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
            => await UserControllerHandlerAsync(id, nameof(Details));

        public async Task<IActionResult> Edit(string id)
            => await UserControllerHandlerAsync(id, nameof(Edit));
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid) return View(model);

            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null) return NotFound();

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await _userManager.UpdateAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError(string.Empty, errorMessage: ex.Message);
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
            => await UserControllerHandlerAsync(id, nameof(Delete));
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id, UserViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null) return NotFound();

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, errorMessage: ex.Message);
            }
            return View();
        }
        private async Task<IActionResult> UserControllerHandlerAsync(string? id, string viewName)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var model = _mapper.Map<UserViewModel>(user);

            return View(viewName, model);
        }
    }
}

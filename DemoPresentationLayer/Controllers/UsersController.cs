
using System.Data;

namespace DemoPresentationLayer.Controllers
{
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

            //var model = _mapper.Map<ApplicationUser, UserViewModel>(user);

            #endregion

            return View((IEnumerable<UserViewModel>)model);
        }
	}
}

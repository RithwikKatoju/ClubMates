using System.Security.Claims;
using ClubMates.Web.Models;
using ClubMates.Web.Models.AccountViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClubMates.Web.Controllers
{
    public class AdminController(UserManager<ClubMatesUser> userManager) : Controller
    {

        private readonly UserManager<ClubMatesUser> _userManager = userManager;
        [Authorize("GuestOrSuperAdmin")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {

            return View(await GetUsersToManage());
        }

        private async Task<List<UserViewModel>> GetUsersToManage()
        {
            var users = await _userManager.Users
                            .Where(x => x.Role != ClubMatesRole.SuperAdmin)
                            .ToListAsync();
            var listOfUserAccounts = new List<UserViewModel>();
            foreach (var user in users)
            {
                listOfUserAccounts.Add(new UserViewModel
                {
                    Email = user.Email,
                    Name = user.UserName,
                    Role = user.Role
                });
            }
            return listOfUserAccounts;
        }


        //private async Task<string> GetNameForUsers(string Email)
        //{
        //    var account = await _userManager.FindByEmailAsync(Email);
        //    if (account != null)
        //    {
        //        var claims = await _userManager.GetClaimsAsync(account);
        //        if (claims != null)
        //        {
        //            return claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        //        }
        //    }

        //    return string.Empty;
        //}

        [HttpGet]
        public async Task<IActionResult> EditUser(string email)
        {
            var accountUser = await _userManager.FindByEmailAsync(email);
            if (accountUser != null)
            {
                UserViewModel userViewModel = new()
                {
                    Email = accountUser.Email,
                    Name = accountUser.UserName,
                    Role = accountUser.Role,
                    Roles = Enum.GetValues<ClubMatesRole>()
                    .Select(x => new SelectListItem
                    {
                        Text = Enum.GetName(x),
                        Value = x.ToString()
                    })

                };
                return View(userViewModel);
            }
            return NotFound();
        }


        public async Task<IActionResult> EditUser(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }
            else
            {
                try
                {
                    ClubMatesUser? clubMatesUser = await _userManager.FindByEmailAsync(userViewModel.Email);
                    if (clubMatesUser != null)
                    {
                        clubMatesUser.Role = userViewModel.Role;
                        clubMatesUser.UserName = userViewModel.Name;
                        var claims = await _userManager.GetClaimsAsync(clubMatesUser);
                        var removeResult = await _userManager.RemoveClaimsAsync(clubMatesUser, claims);
                        if (!removeResult.Succeeded)
                        {
                            //DisplayError
                            ModelState.AddModelError(string.Empty, "Unable to Update Claims - removing existing claim failed");
                            return View(userViewModel);
                        }
                        var claimsRequired = new List<Claim>
                    { new (ClaimTypes.Name, userViewModel.Name ?? ""),
                        new (ClaimTypes.Role, Enum.GetName(userViewModel.Role)?? ""),
                        new (ClaimTypes.NameIdentifier, clubMatesUser.Id),
                        new (ClaimTypes.Email, userViewModel.Email ?? "")
                    };
                        var addClaimResult = await _userManager.AddClaimsAsync(clubMatesUser, claimsRequired);
                        if (!addClaimResult.Succeeded)
                        {
                            //DisplayError
                            ModelState.AddModelError(string.Empty, "Unable to Update Claims - Adding new claim failed");
                            return View(userViewModel);
                        }
                        var userUpdateResult = await _userManager.UpdateAsync(clubMatesUser);
                        if (!userUpdateResult.Succeeded)
                        {
                            //DisplayError
                            ModelState.AddModelError(string.Empty, "Unable to Update Claims - Update new  claim failed");
                            return View(userViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return View(userViewModel);
                }
            }
            return View("ManageUsers", await GetUsersToManage());
        }


        public async Task<IActionResult> DeleteUser(string email)
        {
            var accountUser = await _userManager.FindByEmailAsync(email);
            if (accountUser != null)
            {

                await _userManager.DeleteAsync(accountUser);
                return View("ManageUsers", await GetUsersToManage());
            }
            return NotFound();
        }

        public IActionResult CreateUser()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]

        public async Task<IActionResult> CreateUser(CreateUserViewModel createUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserViewModel);
            }
            if (createUserViewModel != null
                && createUserViewModel.Email != null
                && !createUserViewModel.Email.Equals(createUserViewModel.ConfirmEmail))
            {
                ModelState.AddModelError(string.Empty, "Email and Confirm Email do not match!");
                return View(createUserViewModel);
            }
            if (createUserViewModel != null
                && createUserViewModel.Password != null
                && !createUserViewModel.Password.Equals(createUserViewModel.ConfirmPassword))
            {
                ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match!");
                return View(createUserViewModel);
            }
            if (createUserViewModel != null)
            {
                ClubMatesUser clubMatesUser = new()
                {
                    Email = createUserViewModel.Email,
                    UserName = createUserViewModel.Email,
                    Role = createUserViewModel.Role
                };
                var CreatedUser = await _userManager.CreateAsync(clubMatesUser, createUserViewModel?.Password ?? "Open@1234");
                if (!CreatedUser.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "User Not Created");
                    return View(createUserViewModel);
                }
                if (createUserViewModel != null)
                {
                    var claimsRequired = new List<Claim>
                    { new (ClaimTypes.Name, createUserViewModel.Name ?? ""),
                        new (ClaimTypes.Role, Enum.GetName(createUserViewModel.Role)?? ""),
                        new (ClaimTypes.NameIdentifier, clubMatesUser.Id),
                        new (ClaimTypes.Email, createUserViewModel.Email ?? "")
                    };
                    await _userManager.AddClaimsAsync(clubMatesUser, claimsRequired);
                    await _userManager.UpdateAsync(clubMatesUser);

                }
                return View("ManageUsers", await GetUsersToManage());
            }
            return View(new CreateUserViewModel());
        }

    }
}

 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MohammedBassem.G01.DAL.Models;
using MohammedBassem.G01.PL.ViewModels;

namespace MohammedBassem.G01.PL.Controllers
{
    //[Authorize (Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        //Get , GetAll , Create , Update  ,Delete


        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }



        public async Task<IActionResult> Index(string InputSearch)
        {
            var roles = Enumerable.Empty<RoleViewModel>();
            if (InputSearch.IsNullOrEmpty())
            {
                roles = await roleManager.Roles.Select(u => new RoleViewModel()
                {   //Mapping from user to uservm

                    Id = u.Id,
                    RoleName = u.Name

                }).ToListAsync();
            }
            else
            {
                roles = await roleManager.Roles.Where(u => u.Name.
                                                ToLower().
                                                Contains(InputSearch.ToLower()))
                                                .Select(u => new RoleViewModel()
                                                {   //Mapping from user to uservm

                                                    Id = u.Id,
                                                    RoleName = u.Name

                                                }).ToListAsync();
            }

            return View(roles);
        }



        //-----------------------------------------------------------Details-----------------------------------------


        [HttpGet]
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {

            if (id is null)
                return BadRequest();

            var roleFromDb = await roleManager.FindByIdAsync(id);

            if (roleFromDb is not null)
            {

                var role = new RoleViewModel()
                {
                    Id = roleFromDb.Id,
                    RoleName = roleFromDb.Name

                };

                return View(ViewName, role);
            }
            return NotFound();
        }



        //----------------------------------------------------------Create----------------------------------------------------


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]    //تمنع اي ابلكيشن خارجي يكلم الابلكيشن ده زي Postman
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)       //تعمل تاكيد علي الداتا الي رجعالي صح ولا لا
            {
                var role = new IdentityRole()
                {
                    Name = roleVM.RoleName,
                    NormalizedName = roleVM.RoleName.ToUpper()
                };
                var result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Invalid Role");
            }
            return View(roleVM);
        }



        //--------------------------------------------------------Edit------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]    //تمنع اي ابلكيشن خارجي يكلم الابلكيشن ده زي Postman
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleVM)
        {
            if (id != roleVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {

                var roleFromDb = await roleManager.FindByIdAsync(id);

                if (roleFromDb is not null)
                {
                    roleFromDb.Name = roleVM.RoleName;

                    await roleManager.UpdateAsync(roleFromDb);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(roleVM);
        }






        //---------------------------------------------------Delete------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }




        [HttpPost]
        [ValidateAntiForgeryToken]    //تمنع اي ابلكيشن خارجي يكلم الابلكيشن ده زي Postman
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel roleVM)
        {

            if (id != roleVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {

                var roleFromDb = await roleManager.FindByIdAsync(id);

                if (roleFromDb is not null)
                {
                    await roleManager.DeleteAsync(roleFromDb);
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(roleVM);

        }




        //----------------------------------------------------Add Or Remove User From Role----------------------------------------
        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUser(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            ViewData["RoleId"] = role.Id;
            var UsersInRole = new List<UsersInRoleViewModel>();
            var users = userManager.Users.ToList();
            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelect = true;
                }
                else
                {
                    userInRole.IsSelect = false;
                }
                UsersInRole.Add(userInRole);
            }
            return View(UsersInRole);



        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUser(string roleId, List<UsersInRoleViewModel> userInRoleVMs)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in userInRoleVMs)
                {
                    var appUser = await userManager.FindByIdAsync(user.UserId);
                    if (appUser != null)
                    {
                        if (user.IsSelect && !await userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            //Create
                            await userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelect && await userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            //Remove
                            await userManager.RemoveFromRoleAsync(appUser, role.Name);

                        }

                    }

                }
                return RedirectToAction(nameof(Edit), new { id = roleId });

            }
            return View(userInRoleVMs);


        }
    }
}

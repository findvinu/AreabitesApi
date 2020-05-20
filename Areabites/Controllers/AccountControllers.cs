using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Data.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Areabites
{
    public class AccountControllers : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountControllers(SignInManager<User> _signInManager, UserManager<User> _userManager)
        {
            this._signInManager = _signInManager;
            this._userManager = _userManager;
            //this.appSettings = appSettings.Value;
        }
        [HttpPost(), Route("api/register")]
        public async Task<ActionResult> Register([FromBody]RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var emailExist = _userManager.FindByEmailAsync(model.Email).Result;
                var usernameExist = _userManager.FindByEmailAsync(model.Email).Result;
                if (emailExist == null && usernameExist == null)
                {
                    var user = new User {FirstName = model.FirstName, LastName=model.LastName, Email = model.Email, PhoneNumber=model.MobileNumber, Address=model.Address, UserName=model.UserName };
                    var result = await _userManager.CreateAsync(user, model.PasswordHash);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.PasswordSignInAsync(user, model.PasswordHash, isPersistent: false, lockoutOnFailure:false);
                        return Ok();
                    }
                }
            } 
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("api/account/login")]
        public async Task<User> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return null;
                }
                return user;
            }
            return null;
        }

        [HttpPost(),Route("api/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost(), Route("api/deleteUser")]
        public async Task<IActionResult> DeleteUser(int ID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var usernameExist = _userManager.FindByIdAsync(ID.ToString()).Result;
            if (usernameExist == null) return BadRequest("User with ID not found");
            await _userManager.DeleteAsync(usernameExist);
            return Ok();
        }

    }
}

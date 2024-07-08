using System.Security.Claims;
using BlogApp.Data.Abstract;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BlogApp.Controllers{


public class UsersController:Controller
{
    private readonly string _folderPath = @"C:\Users\gorke\Desktop\asp.net\BlogApp\BlogApp\wwwroot\img\";

    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IActionResult Login()
    {
        if(User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("index", "Posts");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {

        if(User.Identity!.IsAuthenticated)
        {
            return RedirectToAction("index", "Posts");
        }

        return View();
    }

        [HttpPost]
        public IActionResult Register(IFormFile FormFile,RegisterViewModel registerViewModel)
        {

            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("index", "Posts");
            }

            if(ModelState.IsValid)
            {

            if (FormFile.Length == 0)
            {
                return Content("No file selected.");
            }
            try
            {

                if (!Directory.Exists(_folderPath))
                {
                    Directory.CreateDirectory(_folderPath);
                }
                var targetPath = Path.Combine(_folderPath, FormFile.FileName);

                using (var stream = new FileStream(targetPath, FileMode.Create))
                {
                    FormFile.CopyTo(stream);
                }

                _userRepository.CreateUser( new User(){
                    FirstName = registerViewModel.FirstName,
                    SurName = registerViewModel.SurName,
                    Email = registerViewModel.Email,
                    UserName = registerViewModel.UserName,
                    Password = registerViewModel.Password,
                    Image = FormFile.FileName,
                    Title = registerViewModel.Title
                    });

                return RedirectToAction("Login", "Users");
            }
            catch (Exception ex)
            {
                return Content($"Dosya yükleme hatası: {ex.Message}");
            }
            }
            else
            {
                return View(registerViewModel);
            }

        }




        [HttpPost]

    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if(ModelState.IsValid)
        {
            var isUser = _userRepository.Users.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password);  

            if(isUser != null)
            {
                var userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));  
                userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));  
                userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.FirstName ?? ""));  
                userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));  
                userClaims.Add(new Claim(ClaimTypes.PrimarySid, isUser.Title ?? ""));  

                if(isUser.Email == "gorkemkayas@hotmail.com")
                {
                    userClaims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties {
                    IsPersistent = true,
                };

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),authProperties);

                return RedirectToAction("Index", "Posts");
            }
            else
            {
                ModelState.AddModelError(""," Mail or password is wrong!");
                
            }
        
        }
        return View(loginViewModel);
    }


    public async  Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("index","Posts");
    }


    [Authorize]
    public IActionResult Profile(string? username)
    {

        if(string.IsNullOrEmpty(username))
        {
            return NotFound();
        }
        var user = _userRepository.
        Users.
        Include(x => x.Posts).
        Include(x => x.Comments).
        ThenInclude(x => x.Post).
        FirstOrDefault(u => u.UserName == username);
        return View(user);
    }
}
}
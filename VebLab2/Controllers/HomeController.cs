using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using System;
using VebLab2.Models;
using VebLab2.Services;
using VebLab2.Models;

namespace VebLab2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _appEnvironment; 

        public HomeController(IEmailSender emailSender, IWebHostEnvironment appEnvironment)
        {
            _emailSender = emailSender;
            _appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailSender.SendEmailAsync(model.Email, "Нове повідомлення з сайту", model.Message);
                ViewBag.Success = "Email успішно надіслано!";
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = Path.Combine(_appEnvironment.WebRootPath, "Files", uploadedFile.FileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FileList()
        {
            string path = Path.Combine(_appEnvironment.WebRootPath, "Files");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] filePaths = Directory.GetFiles(path);
            List<string> fileNames = new List<string>();
            foreach (string filePath in filePaths)
            {
                fileNames.Add(Path.GetFileName(filePath));
            }
            return View(fileNames);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //cookie
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        public IActionResult ProfileForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ProfileForm(UserProfile model)
        {
            // виконання вимоги "Провалідувати модель в action"
            var validator = new UserProfileValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model); 
            }

            return Content("Валідація пройдена успішно!");
        }

    }
}
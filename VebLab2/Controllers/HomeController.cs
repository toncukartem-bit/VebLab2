using Microsoft.AspNetCore.Mvc;
using VebLab2.Models;
using VebLab2.Services; 
using System.Threading.Tasks;

namespace VebLab2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmailSender _emailSender;

        
        public HomeController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
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
    }
}
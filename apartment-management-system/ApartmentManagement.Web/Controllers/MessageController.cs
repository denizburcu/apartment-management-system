using ApartmentManagement.Core.Contracts;
using ApartmentManagement.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApartmentManagement.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [ResponseCache(NoStore = true, Duration = 0)]
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var messages = await _messageService.GetAllMessages();
                await _messageService.UpdateNewMessageStatus();
                return View(messages);
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var messages = await _messageService.GetAllMessagesByUser(claim.Value);
                return View(messages);
            }

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var message = await _messageService.GetById(id);
            return View(message);
        }

        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                await _messageService.AddMessage(message);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}

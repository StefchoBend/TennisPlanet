using Microsoft.AspNetCore.Mvc;
using TennisPlanet.Core.Contracts;
using TennisPlanet.Models.Contact;

namespace TennisPlanet.Controllers
{
    public class ContactController : Controller
    {

        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        // GET: ContactController
        public ActionResult Index()
        {
            List<ContactIndexVM> messages = _contactService.GetMessage()
               .Select(item => new ContactIndexVM
               {
                   Id = item.Id,
                   Email = item.Email,
                   FirstName = item.FirstName,
                   Message = item.Message,
               }).ToList();
            return View(messages);
        }


        // GET: ContactController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContactCreateVM contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.Create(contact.Email, contact.FirstName, contact.Message);
                return RedirectToAction(nameof(Thanks));
            }
            return View(contact);
        }
        // GET: ContactController/Delete/5
        public ActionResult Delete(int id)
        {
            var contact = _contactService.GetMessage().FirstOrDefault(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            var contactDeleteVM = new ContactDeleteVM
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                Message = contact.Message
            };

            return View(contactDeleteVM);
        }

        // POST: ContactController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var success = _contactService.Delete(id);
            if (!success)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ContactController/Thanks
        public ActionResult Thanks()
        {
            return View();
        }


    }
}

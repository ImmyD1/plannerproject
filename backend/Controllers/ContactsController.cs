using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace ContactListApi.Controllers
{
    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        private readonly List<Contact> contacts = new List<Contact>
        {
        };
    
        [HttpGet]
        public IActionResult GetContact()
        {
            var db = new ContactDB();
            return Ok(db.GetAllContacts());
        }
    }

}

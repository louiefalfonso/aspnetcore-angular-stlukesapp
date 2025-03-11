using Microsoft.AspNetCore.Mvc;

namespace StLukesMedicalApp.API.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

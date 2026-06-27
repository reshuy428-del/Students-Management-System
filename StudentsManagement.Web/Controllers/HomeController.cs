using System.Web.Mvc;

namespace StudentsManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: /
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Home/Generate
        [HttpPost]
        public ActionResult Generate()
        {
            var svc = new Services.TimetableGeneratorService();
            var result = svc.GenerateSimpleTimetable(5, 8); // 5 days, 8 periods
            ViewBag.ResultMessage = $"Allocated {result.AssignedCount} slots. Unassigned events: {result.UnassignedCount}";
            return View("Index");
        }
    }
}

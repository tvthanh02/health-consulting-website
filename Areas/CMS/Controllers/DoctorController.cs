

using Microsoft.AspNetCore.Mvc;

public class DoctorController : Controller
{
    [Route("/he-thong/cms")]
    public ActionResult Index()
    {

        return View("~/Areas/CMS/Views/Doctor/Index.cshtml");
    }
}
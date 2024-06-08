using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{

    [Route("/he-thong/admin")]
    public ActionResult Index()
    {
        Console.WriteLine("hệ thống admin");
        return View("~/Areas/CMS/Views/Admin/Index.cshtml");
    }
}
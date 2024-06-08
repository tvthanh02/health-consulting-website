
using health_consulting_website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace health_consulting_website.Controllers;



public class DoctorController : Controller
{

    // [Authorize]
    [Route("bac-si")]
    [Breadcrumb("Bác sĩ")]
    public IActionResult Index(string? q)
    {
        List<BacSi> lsDoctor;
        using (ConsultContext dbcontext = new ConsultContext())
        {
            lsDoctor = !string.IsNullOrEmpty(q) ? dbcontext.BacSis
       .Include(b => b.SMaChuyenKhoaNavigation)
       .Where(b => b.STenBacSi.ToLower().Contains(q))
       .ToList() : dbcontext.BacSis
       .Include(b => b.SMaChuyenKhoaNavigation)
       .ToList();
            return View(lsDoctor);
        }
    }

    [Route("bac-si/{id}")]
    [Breadcrumb("Bác sĩ")]
    public IActionResult Detail(string id)
    {
        if (id != null)
        {

            BacSi bs;

            using (ConsultContext dbcontext = new ConsultContext())
            {
                bs = dbcontext.BacSis.Include(bs => bs.SMaDonViCongTacNavigation).Include(bs => bs.SMaChuyenKhoaNavigation).Where(b => b.SMaBacSi == id).FirstOrDefault();
                ViewBag.BreadcrumbParams = "Bs. " + bs.STenBacSi;
            }
            return View(bs);
        }
        else
        {
            return RedirectToAction("Index");
        }

    }

    [Route("/tim-kiem")]
    [HttpGet]
    public IActionResult Search(string q)
    {
        if (!string.IsNullOrEmpty(q))
        {
            return Json(new { error = "0", url = $"/bac-si?q={q}" });
        }
        else
        {
            return Json(new { error = "1", url = "bac-si" });
        }
    }

}

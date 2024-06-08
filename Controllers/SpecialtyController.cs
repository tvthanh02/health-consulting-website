using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using health_consulting_website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace health_consulting_website.Controllers;



public class SpecialtyController : Controller
{

    private readonly ConsultContext _consultContext;

    public SpecialtyController(ConsultContext consultContext)
    {
        _consultContext = consultContext;
    }

    [Route("chuyen-khoa")]
    [Breadcrumb("Chuyên Khoa")]
    [HttpGet]
    public IActionResult Index()
    {
        List<ChuyenKhoa> lsChuyenKhoa = _consultContext.ChuyenKhoas

       .Include(ck => ck.BacSis)
       .ToList();

        return View(lsChuyenKhoa);
    }


    [Route("chuyen-khoa/{id}")]
    [Breadcrumb("Chuyên khoa")]
    public IActionResult Detail(string id)
    {
        if (id != null)
        {

            // ChuyenKhoa ck

            ChuyenKhoa ck = _consultContext.ChuyenKhoas.Where(ck => ck.SMaChuyenKhoa == id).FirstOrDefault();
            ViewBag.BreadcrumbParams = ck.STenChuyenKhoa;
            return View(ck);
        }
        else
        {
            return RedirectToAction("Index");
        }

    }

    [Route("/them-chuyen-khoa")]
    [HttpGet]
    public IActionResult Add(ChuyenKhoa ck)
    {
        ModelState.Remove("SMaChuyenKhoa");

        if (ModelState.IsValid)
        {
            ck.SMaChuyenKhoa = MethodExtension.GenerateKeyField("ck");
            _consultContext.Add(ck);

            try
            {
                _consultContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return RedirectToAction("Index", "Specialty");

        }
        else
        {

            return View();
        }
    }
}

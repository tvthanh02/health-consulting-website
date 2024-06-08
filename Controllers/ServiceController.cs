using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using health_consulting_website.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography;

namespace health_consulting_website.Controllers;


public class ServiceController : Controller
{

    private readonly ConsultContext _consultContext;

    public ServiceController(ConsultContext consultContext)
    {
        _consultContext = consultContext;
    }

    [Route("dich-vu")]
    [Breadcrumb("Dịch vụ")]
    public IActionResult Index()
    {
        List<ChuyenKhoa> services = _consultContext.ChuyenKhoas.ToList();
        return View(services);
    }

    [Route("dich-vu/{id}")]
    [Breadcrumb("Dịch vụ")]
    public IActionResult Detail(string id, string? d, string? m, string? y)
    {
        if (id != null)
        {
            ChuyenKhoa ckdt = _consultContext.ChuyenKhoas
            .Include(ck => ck.BacSis)
            .ThenInclude(bs => bs.TuVans)
            .ThenInclude(tv => tv.TuVanBangThoiGians)
            .ThenInclude(tv => tv.SMaThoiGianTuVanNavigation) //
            .Where(ck => ck.SMaChuyenKhoa == id).FirstOrDefault();

            if (ckdt != null)
            {
                ServiceDetail serviceDetail = new ServiceDetail();
                serviceDetail.Specialty = ckdt;
                if (!string.IsNullOrEmpty(d) && !string.IsNullOrEmpty(m) && !string.IsNullOrEmpty(y))
                {
                    int day = Convert.ToInt32(d);
                    int month = Convert.ToInt32(m);
                    int year = Convert.ToInt32(y);

                    DateOnly userPickTime = new DateOnly(year, month, day);
                    serviceDetail.Time = userPickTime;
                }
                ViewBag.BreadcrumbParams = ckdt.STenChuyenKhoa.Replace("Chuyên khoa", "Tư vấn");
                return View(serviceDetail);
            }
        }
        return NotFound();
    }

    [HttpPost]
    [Route("/chon-thoi-gian")]
    public IActionResult PickDateTime(string specialtyid, DateTime birthdaytime)
    {
        if (birthdaytime == null) return Json(new { error = "1" });

        return Json(new
        {
            error = "0",
            url = $"/dich-vu/{specialtyid}?d={birthdaytime.Day}&m={birthdaytime.Month}&y={birthdaytime.Year}"
        });
    }

    [Route("thanh-toan-dich-vu/{id}")]
    [Breadcrumb("Dịch vụ/Thanh toán")]
    public IActionResult Payment(string id, string scheduleid)
    {

        TuVanBangThoiGian tvbtg = _consultContext.TuVanBangThoiGians
        .Include(b => b.SMaThoiGianTuVanNavigation)
        .Include(t => t.SMaLichNavigation)
        .ThenInclude(a => a.SMaBacSiNavigation)
        .FirstOrDefault(d => d.SMaLich == scheduleid && d.SMaThoiGianTuVan == id && d.SMaNguoiDung == null);

        if (tvbtg == null) return NotFound();

        return View(tvbtg);
    }

    [Route("/add-schedule")]
    [HttpPost]
    public IActionResult AddSchedule(string timeid, string scheduleid, string userid)
    {// Tìm kiếm TuVanBangThoiGian dựa trên các giá trị trong các tham số truyền vào
        TuVanBangThoiGian tuVanBangThoiGian = _consultContext.TuVanBangThoiGians
            .FirstOrDefault(t => t.SMaThoiGianTuVan == timeid && t.SMaLich == scheduleid);

        if (tuVanBangThoiGian != null)
        {
            tuVanBangThoiGian.SMaNguoiDung = userid;
            try
            {
                _consultContext.Update(tuVanBangThoiGian);
                _consultContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Json(new { error = 1 });
            }
        }
        else
        {
            return Json(new { error = 1 });
        }

        return Json(new { error = 0 });
    }

}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using health_consulting_website.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace health_consulting_website.Controllers;


public class HomeController : Controller
{

    private readonly ConsultContext _consultContext;

    public HomeController(ConsultContext consultContext)
    {
        _consultContext = consultContext;
    }

    [Route("/")]
    public IActionResult Index()
    {
        HttpContext.Request.Cookies.TryGetValue("SSID", out string sessionId);

        if (!string.IsNullOrEmpty(sessionId))
        {
            if (MethodExtension.CheckValidSession(sessionId))
            {
                Session session = MethodExtension.GetSession(sessionId);
                string jsonSessionData = session.SSessionData;
                var tk = MyJsonSerialize.Deserialize<TaiKhoan>(jsonSessionData);
                string userCurrentRole = tk.SMaQuyen;
                if (!string.IsNullOrEmpty(userCurrentRole))
                {
                    switch (userCurrentRole.ToUpper())
                    {
                        case "ADMIN":
                            return RedirectToRoute(new { controller = "Admin", action = "Index", area = "CMS" });


                        case "DOCTOR":
                            return RedirectToRoute(new { controller = "Doctor", action = "Index", area = "CMS" });


                        default:
                            // Xử lý trường hợp không xác định quyền
                            break;
                    }
                }
            }
        }

        // xử lý lấy dữ liệu về bác sĩ, chuyên khoa

        // bác sĩ nổi bật (take 10)

        // List<BacSi> bacsinoibat = _consultContext.BacSis
        // .Include(bs => bs.TuVans).Where(bs => bs.TuVans.Count(tv => tv.IDanhGia == 4 || tv.IDanhGia == 5) > 0)
        // .OrderByDescending(bs => bs.TuVans.Count(tv => tv.IDanhGia == 5)) // Sắp xếp theo số lượng đánh giá 5 giảm dần
        // .ThenByDescending(bs => bs.TuVans.Count(tv => tv.IDanhGia == 4)) // Tiếp theo, sắp xếp theo số lượng đánh giá 4 giảm dần
        // .Take(10) // Lấy tối đa 10 bác sĩ
        // .ToList();

        List<ChuyenKhoa> cks = _consultContext.ChuyenKhoas.Take(10).ToList();

        List<BacSi> bss = _consultContext.BacSis.Take(10).ToList();
        HomeModel homeModel = new HomeModel();
        homeModel.chuyenKhoas = cks;
        homeModel.bacSis = bss;

        return View(homeModel);
    }

    [Route("/privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [Route("/error")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

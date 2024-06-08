using System.Text.Json;
using System.Text.Json.Serialization;
using health_consulting_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


public class AuthController : Controller
{

    private readonly ConsultContext _consultContext;

    public AuthController(ConsultContext consultContext)
    {
        _consultContext = consultContext;
    }


    [Route("/dang-ky")]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [Route("/dang-ky")]
    [HttpPost]
    public IActionResult Register(Register register)
    {
        ModelState.Remove("taiKhoan.SMaTaiKhoan");
        ModelState.Remove("nguoiDung.SMaNguoiDung");

        if (ModelState.IsValid)
        {
            register.nguoiDung.SMaNguoiDung = MethodExtension.GenerateKeyField("bs");
            register.taiKhoan.SMaTaiKhoan = MethodExtension.GenerateKeyField("tk");
            register.taiKhoan.SMaQuyen = "USER";
            using (ConsultContext dbContext = new ConsultContext())
            {
                dbContext.NguoiDungs.Add(register.nguoiDung);
                dbContext.TaiKhoans.Add(register.taiKhoan);

                register.nguoiDung.SMaTaiKhoanNavigation = register.taiKhoan;

                try
                {
                    int rowsChange = dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }
        else
        {
            // foreach (var key in ModelState.Keys)
            // {
            //     var fieldState = ModelState[key];
            //     if (fieldState.ValidationState == ModelValidationState.Invalid)
            //     {
            //         // Trường này không hợp lệ, thực hiện xử lý tương ứng
            //         // Ví dụ: Thêm thông tin về trường không hợp lệ vào một danh sách lỗi
            //         var errors = fieldState.Errors.Select(e => e.ErrorMessage).ToList();
            //         // Tiếp tục xử lý...
            //         for (int i = 0; i < errors.Count; i++)
            //         {
            //             Console.WriteLine(errors[i]);
            //         }
            //     }
            // }
            return View();
        }
    }

    [Route("/dang-nhap")]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [Route("/dang-nhap")]
    [HttpPost]
    public IActionResult Login(TaiKhoan taiKhoan)
    {
        ModelState.Remove("SMaTaiKhoan");
        ModelState.Remove("SMaQuyen");

        if (ModelState.IsValid)
        {
            using (ConsultContext dbContext = new ConsultContext())
            {
                TaiKhoan tk = dbContext.TaiKhoans
                .Include(acc => acc.NguoiDungs)
                .Include(acc => acc.SMaQuyenNavigation)
                .Where(acc => acc.STenDangNhap == taiKhoan.STenDangNhap && acc.SMatKhau == taiKhoan.SMatKhau).FirstOrDefault();
                if (tk != null)
                {
                    string sessionId = MethodExtension.GenerateKeyField("ss");
                    string sessionData = MyJsonSerialize.Serialize(tk);
                    DateTime expireTime = DateTime.Now.AddMinutes(30);

                    Session session = new Session();
                    session.SSessionId = sessionId;
                    session.SSessionData = sessionData;
                    session.DExpireTime = expireTime;



                    dbContext.Sessions.Add(session);

                    try
                    {
                        dbContext.SaveChanges();
                        HttpContext.Response.Cookies.Append("SSID", sessionId);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }


                    return RedirectToAction("Index", "Home");
                }
            }
        }
        return View();
    }

    [Route("/thong-tin-nguoi-dung")]
    public IActionResult GetCurrentUser()
    {
        HttpContext.Request.Cookies.TryGetValue("SSID", out string sessionId);

        if (sessionId == null) return Json(new { error = 1, data = "" });
        if (!MethodExtension.CheckValidSession(sessionId)) return Json(new { error = 0, data = "" });
        // {
        //     Session session = MethodExtension.GetSession(sessionId);
        //     string jsonSessionData = session.sSessionData;
        //     var tk = JsonSerializer.Deserialize<TaiKhoan>(jsonSessionData, options);
        //     nd = tk.NguoiDungs.ToList<NguoiDung>()[0];

        // }
        Session session = MethodExtension.GetSession(sessionId);
        string jsonSessionData = session.SSessionData;
        var tk = MyJsonSerialize.Deserialize<TaiKhoan>(jsonSessionData);

        return Json(new { error = 0, uid = tk.NguoiDungs.ToList<NguoiDung>()[0].SMaNguoiDung, uname = tk.NguoiDungs.ToList<NguoiDung>()[0].SHoTen, uphone = tk.NguoiDungs.ToList<NguoiDung>()[0].SSdt });
    }

    [Route("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Request.Cookies.TryGetValue("SSID", out string sessionId);
        if (sessionId == null) return Json(new { error = 1, message = "Invalid Session" });
        try
        {
            MethodExtension.DeleteSession(sessionId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return Json(new { error = 0, message = "logout success" });
    }
}

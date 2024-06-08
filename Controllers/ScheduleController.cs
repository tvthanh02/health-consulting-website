using System.Diagnostics;
using health_consulting_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


//[Route("lich-hen")]
public class ScheduleController : Controller
{

    private readonly ConsultContext _consultContext;


    public ScheduleController(ConsultContext consultContext)
    {
        _consultContext = consultContext;
    }

    [Route("lich-hen")]
    public IActionResult Index(string loc)
    {
        HttpContext.Request.Cookies.TryGetValue("SSID", out string sessionId);

        Session s = MethodExtension.GetSession(sessionId);
        string jsonSessionData = s.SSessionData;
        var tk = MyJsonSerialize.Deserialize<TaiKhoan>(jsonSessionData);
        string uid = tk.NguoiDungs.ToList<NguoiDung>()[0].SMaNguoiDung;

        List<TuVanBangThoiGian> lsdata = _consultContext.TuVanBangThoiGians
        .Include(x => x.SMaTrangThaiNavigation)
        .Include(d => d.SMaThoiGianTuVanNavigation)
        .Include(p => p.SMaLichNavigation)
        .ThenInclude(t => t.SMaBacSiNavigation)
        .ThenInclude(b => b.SMaChuyenKhoaNavigation)
        .Where(tvtg => tvtg.SMaNguoiDung == uid).ToList();





        ScheduleFilterTag scheduleFilterTag = new ScheduleFilterTag();
        scheduleFilterTag.options = new List<SelectListItem>() {
        new SelectListItem("Tất cả", "all"),
        new SelectListItem("Hôm nay", "today"),
        };

        List<string> data;

        if (loc == null)
        {
            loc = "today";
        }

        if (loc == "today")
        {
            data = new List<string>(){
                "Data Today"
            };
            scheduleFilterTag.ValueSelected = loc;
        }
        else
        {
            data = new List<string>(){
                "Data All"
            };
            scheduleFilterTag.ValueSelected = "all";
        }
        scheduleFilterTag.Data = data;

        return View(lsdata);
    }

    [Route("chi-tiet-lich/")]
    public IActionResult Detail(string scheduleid, string timeid)
    {
        TuVanBangThoiGian schedule = _consultContext.TuVanBangThoiGians
        .Where(sc => sc.SMaLich == scheduleid && sc.SMaThoiGianTuVan == timeid).FirstOrDefault();
        return View(schedule);
    }


    [HttpPost]
    [Route("cap-nhat-lich")]
    public IActionResult UpdateSchedule(string scheduleid, string timeid, string userNote, int feedback)
    {
        TuVanBangThoiGian tvbtg = _consultContext.TuVanBangThoiGians.Where(sc => sc.SMaLich == scheduleid && sc.SMaThoiGianTuVan == timeid).FirstOrDefault();
        if (tvbtg == null) return Json(new { error = "1", MSG = "Đã có lỗi" });
        tvbtg.SGhiChuNguoiDuocTuVan = userNote;
        tvbtg.IDanhGia = feedback != 0 ? feedback : null;
        try
        {
            _consultContext.TuVanBangThoiGians.Update(tvbtg);
            _consultContext.SaveChanges();
        }
        catch (Exception e)
        {
            return Json(new { error = "1", MSG = e.Message });
        }
        return Json(new { error = "0", MSG = "Cập nhật thành công" });
    }

}
using System.Text.Json;
using System.Text.Json.Serialization;
using health_consulting_website.Models;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {

        PathString path = context.Request.Path;

        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve, // Duy trì các tham chiếu đối tượng
        };

        if (IsAuthenticationRequired(path))
        {
            // Kiểm tra cookie để xác định trạng thái đăng nhập
            if (!context.Request.Cookies.TryGetValue("SSID", out string session))
            {
                context.Response.Redirect("/dang-nhap");
                return;
            }

            // Kiểm tra xem sessionId có hợp lệ hay không trong cơ sở dữ liệu
            if (!MethodExtension.CheckValidSession(session))
            {
                context.Response.Redirect("/dang-nhap");
                return;
            }
        }

        if (context.Request.Cookies.TryGetValue("SSID", out string sessionId))
        {
            if (MethodExtension.CheckValidSession(sessionId))
            {
                Session session = MethodExtension.GetSession(sessionId);
                var taikhoan = JsonSerializer.Deserialize<TaiKhoan>(session.SSessionData, options);
                context.Items["u_role"] = taikhoan.SMaQuyen;
            }
            else
            {
                MethodExtension.DeleteSession(sessionId);
                context.Response.Cookies.Delete("SSID");
                context.Items["u_role"] = null;
            }
        }


        await _next(context);
    }

    private bool IsAuthenticationRequired(PathString path)
    {
        // Lấy đường dẫn tuyệt đối từ đối tượng PathString
        var absolutePath = path.Value.ToLower();

        // Các route mà bạn muốn yêu cầu xác thực
        var securedRoutes = new List<string>
        {
            "/lich-hen",
            "/dich-vu",
            "/thanh-toan-dich-vu",
             "/he-thong/cms",
            "/he-thong/admin" // Ví dụ: Mọi route bắt đầu bằng "/admin" đều yêu cầu xác thực
        };

        // Kiểm tra xem đường dẫn của route hiện tại có nằm trong danh sách các route cần xác thực hay không
        return securedRoutes.Any(route => absolutePath.StartsWith(route.ToLower()));
    }

}

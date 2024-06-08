

using health_consulting_website.Models;

public static class MethodExtension
{

    public static string GenerateKeyField(string startWith, Func<string>? callback = null)
    {
        string result = "";

        if (callback != null)
        {
            result = startWith + callback.Invoke();
        }

        result = startWith + "-" + Guid.NewGuid().ToString();

        return result;
    }

    public static bool CheckValidSession(string sessionId)
    {
        using (ConsultContext dbContext = new ConsultContext())
        {
            return dbContext.Sessions.Any(s => s.SSessionId == sessionId && s.DExpireTime > DateTime.Now);
        }
    }
    public static Session GetSession(string sessionId)
    {
        using (ConsultContext dbContext = new ConsultContext())
        {
            Session currentSession = dbContext.Sessions.FirstOrDefault(s => s.SSessionId == sessionId);
            return currentSession;
        }
    }

    public static void DeleteSession(string sessionId)
    {
        using (ConsultContext dbContext = new ConsultContext())
        {
            Session ss = MethodExtension.GetSession(sessionId);
            try
            {
                dbContext.Sessions.Remove(ss);
                dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }

    public static Role TryParseRole(string roleName)
    {
        Enum.TryParse<Role>(roleName.ToUpper(), out Role parsedRole);

        return parsedRole;

    }

    public static bool CompareRole(string role, Role roleCompare)
    {
        bool result = false;
        if (Enum.TryParse<Role>(role.ToUpper(), out Role parsedRole))
        {
            if (parsedRole == roleCompare) result = true;
        }

        return result;
    }
}
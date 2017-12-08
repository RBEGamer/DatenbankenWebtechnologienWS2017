using Microsoft.AspNetCore.Http;

public static class HttpContextNew
{
    private static Microsoft.AspNetCore.Http.IHttpContextAccessor m_httpContextAccessor;


    public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
    {
        m_httpContextAccessor = httpContextAccessor;
    }


    public static HttpContext con;
    public static void set_con(HttpContext _con)
    {
        con = _con;
    }

    public static HttpContext get_con()
    {
        return con;
    }

    public static Microsoft.AspNetCore.Http.HttpContext CurrentCon
    {
        get
        {
            return m_httpContextAccessor.HttpContext;
        }
    }


}



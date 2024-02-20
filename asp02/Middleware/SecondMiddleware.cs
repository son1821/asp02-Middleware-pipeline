namespace asp02.Middleware
{
    public class SecondMiddleware : IMiddleware
    {
        /*
         Url: "/xxx.html"
            - Khong goi Middleware phia sau 
            - Ban khong duoc truy cap
            - Header - SecondMiddleware: Ban khong duoc truy cap
         Url: != "/xxx.html"
            - Header - SecondMiddleware: Ban duoc truy cap
            - Chuyen HttpContext cho Middleware phia sau
         */
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           if(context.Request.Path == "/xxx.html") 
            {
                context.Response.Headers.Add("SecondMiddleware", "Ban khong duoc truy cap");
                var dataFromFirstMiddleware = context.Items["DataFirstMiddleware"];
                if(dataFromFirstMiddleware != null)
                
                   await context.Response.WriteAsync((string)dataFromFirstMiddleware);
                
                await context.Response.WriteAsync("Ban khong duoc truy cap");

                
            }
            else
            {
                context.Response.Headers.Add("SecondMiddleware", "Ban duoc truy cap");
                var dataFromFirstMiddleware = context.Items["DataFirstMiddleware"];
                if (dataFromFirstMiddleware != null)

                    await context.Response.WriteAsync((string)dataFromFirstMiddleware);
                await next(context);
            }

        }
    }
}

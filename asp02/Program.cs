using asp02.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
//Dang ky dich vu vao ung dung
services.AddSingleton<SecondMiddleware>();
var app = builder.Build();


app.UseStaticFiles();// StaticFileMiddleware
//Tao endpoint (Terminate Middleware)

//app.UseMiddleware<FirstMiddleware>();
app.UseFirstMiddleware(); //Dua vao pipeline FirstMiddleware
app.UseSecondMiddleware();//Dua vao pipeline SecondMiddleware

app.UseRouting(); //EndpointRoutingMiddleware

//Tao endpoint(terminate middleware)
app.UseEndpoints(async (endpoint) =>
{
    //E1
    endpoint.MapGet("/about.html", async (context) =>
    {
        await context.Response.WriteAsync("Day la trang gioi thieu");
    });
    //E2
    endpoint.MapGet("/sanpham.html", async (context) =>
    {
        await context.Response.WriteAsync("Day la trang san pham");
    });
});


//re nhanh pipeline
app.Map("/admin", (app1) =>
{
    app1.UseRouting();
    app1.UseEndpoints((endpoint) =>
    {
        //E1 - admin
        endpoint.MapGet("/about.html", async (context) =>
        {
            await context.Response.WriteAsync("Day la trang gioi thieu trong admin");
        });
        //E2 - admin
        endpoint.MapGet("/sanpham.html", async (context) =>
        {
            await context.Response.WriteAsync("Day la trang san pham trong admin");
        });
    });
    //Tao Terminate Middleware
    //M2
    app1.Run(async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Day la admin");
    });
});

//Terminate middleware M1
app.Run(async (HttpContext context) =>
{
   await context.Response.WriteAsync("Hello World");
});
app.Run();

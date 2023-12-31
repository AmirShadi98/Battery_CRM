using Battery_CRM.Core.Domain.Abstract;
using Battery_CRM.Infrastructure.Data.Sql.Cocrate;
using Battery_CRM.Infrastructure.Data.Sql.Common;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var ConnectionString = builder.Configuration.GetConnectionString("Battery_CRMCnn");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(ConnectionString));

//AddHttpContextAccessor

builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.TryAddScoped<IUserService, UserService>();
builder.Services.TryAddScoped<IRoleService, RoleService>();
builder.Services.TryAddScoped<IBranchService, BranchService>();
builder.Services.TryAddScoped<ICustomerService, CustomerService>();
builder.Services.TryAddScoped<IFactorService, FactorService>();
builder.Services.TryAddScoped<IMessageService, MessageService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Autorization
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.None;
    options.Secure = builder.Environment.IsDevelopment()
      ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Expiration = TimeSpan.FromDays(14);
});

builder.Services.AddAuthentication("Auth")
                .AddCookie("Auth", config =>
                {
                    config.Cookie.Name = "Auth";
                    config.LoginPath = "/Account/SignIn";
                    config.AccessDeniedPath = "/Account/AccessDenied";
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    config.SlidingExpiration = true;
                });

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotoRater.Models;
using PhotoRater.Services;
using PhotoRater.Services.Auth;
using Task11.Data;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

//builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
        };
    });
builder.Services.AddHttpLogging(o => { });
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<BaseApplicationContext, ApplicationContext>(ServiceLifetime.Transient);
//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<PhotoOnRateService>();
builder.Services.AddTransient<FeedbackService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
    {
        b.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
//app.MapIdentityApi<User>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UsePathBase(new PathString("/api"));
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.UseHttpLogging();

app.Run();

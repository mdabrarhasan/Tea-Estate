using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using TEALINK.GARDEN;
using TEALINK.GARDEN.Interface.Common;
using TEALINK.GARDEN.Interface.CompanyManagement;
using TEALINK.GARDEN.Interface.DailyNonPlucking;
using TEALINK.GARDEN.Interface.DailyPlucking;
using TEALINK.GARDEN.Interface.DashBoard;
using TEALINK.GARDEN.Interface.Device;
using TEALINK.GARDEN.Interface.Division;
using TEALINK.GARDEN.Interface.Excel;
using TEALINK.GARDEN.Interface.GardenManagement;
using TEALINK.GARDEN.Interface.Holyday;
using TEALINK.GARDEN.Interface.KamjariManagement;
using TEALINK.GARDEN.Interface.LeaveMnagement;
using TEALINK.GARDEN.Interface.Login;
using TEALINK.GARDEN.Interface.Menu;
using TEALINK.GARDEN.Interface.Mobile;
using TEALINK.GARDEN.Interface.Permission;
using TEALINK.GARDEN.Interface.PluckingAllocation;
using TEALINK.GARDEN.Interface.PluckingType;
using TEALINK.GARDEN.Interface.PruneType;
using TEALINK.GARDEN.Interface.Report;
using TEALINK.GARDEN.Interface.SalaryConfiguration;
using TEALINK.GARDEN.Interface.SalaryGenerate;
using TEALINK.GARDEN.Interface.SectionManagement;
using TEALINK.GARDEN.Interface.ShiftManagement;
using TEALINK.GARDEN.Interface.UserManagement;
using TEALINK.GARDEN.Interface.WageCalender;
using TEALINK.GARDEN.Interface.WorkerBatchManagement;
using TEALINK.GARDEN.Interface.WorkerBook;
using TEALINK.GARDEN.Interface.WorkerManagement;
using TEALINK.GARDEN.Interface.WorkerType;
using TEALINK.GARDEN.Interface.WorkLeave;
using TEALINK.GARDEN.Model.Kamjari;
using TEALINK.GARDEN.Repository.CommonRepository;
using TEALINK.GARDEN.Repository.CompanyManagementRepository;
using TEALINK.GARDEN.Repository.DailyNonPluckingRepository;
using TEALINK.GARDEN.Repository.DailyPluckingRepository;
using TEALINK.GARDEN.Repository.DashBoardRepository;
using TEALINK.GARDEN.Repository.DeviceRegistrationRepository;
using TEALINK.GARDEN.Repository.DivisionRepository;
using TEALINK.GARDEN.Repository.ExcelRepository;
using TEALINK.GARDEN.Repository.GardenManagementRepository;
using TEALINK.GARDEN.Repository.HolidayRepository;
using TEALINK.GARDEN.Repository.KamjariRepository;
using TEALINK.GARDEN.Repository.LeaveManagementRepository;
using TEALINK.GARDEN.Repository.LoginRepository;
using TEALINK.GARDEN.Repository.MenuRepository;
using TEALINK.GARDEN.Repository.PermissionRepository;
using TEALINK.GARDEN.Repository.PluckingAllocationRepository;
using TEALINK.GARDEN.Repository.PluckingTypeRepository;
using TEALINK.GARDEN.Repository.PruneingTypeRepository;
using TEALINK.GARDEN.Repository.ReportRepository;
using TEALINK.GARDEN.Repository.SalaryConfigurationRepository;
using TEALINK.GARDEN.Repository.SalaryGenerateRepository;
using TEALINK.GARDEN.Repository.SectionManagementRepository;
using TEALINK.GARDEN.Repository.ShiftManagementRepository;
using TEALINK.GARDEN.Repository.UserManagementRepository;
using TEALINK.GARDEN.Repository.WageCalenderRepository;
using TEALINK.GARDEN.Repository.WorkerBatchRepository;
using TEALINK.GARDEN.Repository.WorkerBook;
using TEALINK.GARDEN.Repository.WorkerManagementRepository;
using TEALINK.GARDEN.Repository.WorkerTypeRepository;
using TEALINK.GARDEN.Repository.WorkLeaveRepository;
using TEALINK.Repositories.Attendance;
using TEALINK.Repositories.FileUpload;
using TEALINK.Repositories.Generic;
using TEALINK.Repositories.Securities;
using TEALINK.Repositories.Utilities;
using TEALINK.Repositories.Worker;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), 
sqlOptions => sqlOptions.CommandTimeout(1000)));
builder.Services.AddAuthorization();
builder.Services.AddControllers();
//builder.Services.AddSession();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tea Leaf", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter Your JWT Access Token",
        Reference=new OpenApiReference
        {
            Id=JwtBearerDefaults.AuthenticationScheme,
            Type=ReferenceType.SecurityScheme
        }
    });


    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//Nuget file:Microsoft.AspNetCore.Mvc.NewtonsoftJson



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*", "*")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
        });
});

builder.Services.AddScoped<ICommon, RCommon>();
builder.Services.AddScoped<ILogin, RLogin>();
builder.Services.AddScoped<IMenu, RMenu>();
builder.Services.AddScoped<ICompanyManagement, RCompanyManagement>();
builder.Services.AddScoped<IDivision, RDivision>();
builder.Services.AddScoped<IGardenManagement, RGardenManagement>();
builder.Services.AddScoped<ISectionManagement, RSectionManagement>();
builder.Services.AddScoped<IPluckingType, RPluckingType>();
builder.Services.AddScoped<IKamjariManagement, RKamjari>();
builder.Services.AddScoped<IPruneingType, RPruneingType>();
builder.Services.AddScoped<IWorkerBook, RWorkerBook>();
builder.Services.AddScoped<IWorkerBatch, RWorkerBatch>();
builder.Services.AddScoped<IWorkerManagement, RWorkerManagement>();
builder.Services.AddScoped<IPluckingAllocation, RPluckingAllocation>();

builder.Services.AddScoped<IReportService, RReportLanding>();

builder.Services.AddScoped<IDailyPlucking, RDailyPlucking>();
builder.Services.AddScoped<IDailyNonPlucking, RDailyNonPlucking>();
builder.Services.AddScoped<IWorkerType, RWorkerType>();
builder.Services.AddScoped<IShiftManagement, RShiftManagement>();
builder.Services.AddScoped<IWageCalender, RWageCalender>();
builder.Services.AddScoped<ISalaryGenerate, RSalaryGenerate>();
builder.Services.AddScoped<IWorkLeave, RWorkLeave>();
builder.Services.AddScoped<ILeaveMangement, RLeaveManagement>();
builder.Services.AddScoped<IHoliday, RHoliday>();

builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDashBoard, RDashBoard>();
builder.Services.AddScoped<IExcel, RExcel>();
builder.Services.AddScoped<IDeviceRegistration, RDeviceRegistration>();
builder.Services.AddScoped<ISalaryConfiguration, RSalaryConfiguration>();
builder.Services.AddScoped<IOperatorPermission, ROperatorPermission>();
builder.Services.AddScoped<IPermissionCheck, RPermissionCheck>();
builder.Services.AddScoped<IUserManagement, RUserManagement>();
builder.Services.AddScoped<INotification, RNotification>();
// for mobile
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IWorkerRepo, WorkerRepo>();
builder.Services.AddScoped<ICommonRepo, CommonRepo>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddTransient<IAPIValidationRepository, APIValidationRepository>();
builder.Services.AddTransient<IAttendanceRepo, AttendanceRepo>();



//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromDays(1);
//    options.Cookie.Name = "Enroll";
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
    });


//Install-Package Swashbuckle.AspNetCore

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddHttpClient();
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "TEA LEAF";
});

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();

});


app.UseRouting();
app.UseAuthentication();
app.UseStaticFiles();
app.UseAuthorization();
//app.UseSession();

app.MapControllers();


app.Run();

using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using FirstApiCreated;
using FirstApiCreated.Services;
using FirstApiCreated.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// SeriLog Configuration 
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfoLog.txt",rollingInterval : RollingInterval.Day)
    .CreateLogger();    
// we finishedthe serilog minimum level configuration !! 
var builder = WebApplication.CreateBuilder(args);
// for the add console no problem with them , they won't be used even !! 
builder.Logging.AddConsole();
// we have to tell Asp.Net to use Serilog instead of the built in Logger !! 

builder.Host.UseSerilog();
// Add services to the container.
// we overloaded the AddTranscient , each time we inject the interface , the container has to create an instance of the localmailservice ! 
// CAN YOU BELIEVE WHAT WE HAVE ALREADY DONE  !!! 
#if DEBUG  
builder.Services.AddTransient<IMailService,localMailService>();
#else
builder.Services.AddTransient<IMailService,CloudMailService>();
#endif
// implementing the DI principle on the static instance current : 
builder.Services.AddSingleton <citiesDataStore > ();
// registring our database with a scope lifetime
//using the app setting configuration file 
builder.Services.AddDbContext<CityInfoContext>(
    DbContextOptions => DbContextOptions.UseSqlite(builder.Configuration["ConnectionStrings:CityInfoDBConnectionString"]));
//register the Repository Service with the best practices !! 
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();
//register the AutoMap Service 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Registering the Jwt bearer token specifications and JWTBearerMiddlewar ! 
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };

    });


builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); 
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
// Order Matters ! it is a Pipeline !!! 
app.UseHttpsRedirection();
app.UseRouting();   
app.UseAuthentication();    
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); } );

app.Run();

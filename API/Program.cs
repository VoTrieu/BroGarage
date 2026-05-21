using BroGarage.API.Middlewares;
using BroGarage.API.Models;
using BroGarage.API.Data;
using BroGarage.API.Shared.Models;
using BroGarage.API.Utilities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(option =>
    {
        option.InvalidModelStateResponseFactory = StateValidatorModel.ValidateModelState;
    })
    .AddJsonOptions(option =>
    {
        option.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        option.JsonSerializerOptions.WriteIndented = false;
        option.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? builder.Configuration.GetValue<string>("ConnectionString")
        ?? throw new InvalidOperationException("Missing database connection string.");
    int commandTimeoutInSecond = builder.Configuration.GetValue("CommandTimeoutInSecond", 30);

    options.UseSqlServer(connectionString, o => o
            .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            .CommandTimeout(commandTimeoutInSecond))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<JwtUtility>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bro Garage API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

app.UseMiddleware<AuthMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseExceptionHandler(action => action.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;
    string logFolderPath = Path.Combine(AppContext.BaseDirectory, "Logs");
    Directory.CreateDirectory(logFolderPath);
    string logFileName = DateTime.Now.ToString("yyyy_MM_dd_H_mm_ss_fff", CultureInfo.InvariantCulture) + ".txt";
    string logFilePath = Path.Combine(logFolderPath, logFileName);
    StringBuilder strLogBuilder = new();
    strLogBuilder.AppendLine(exception?.Message ?? "");
    strLogBuilder.AppendLine();
    strLogBuilder.AppendLine(exception?.StackTrace ?? "");
    await File.WriteAllTextAsync(logFilePath, strLogBuilder.ToString());

    var response = new ResponseModel
    {
        IsSuccess = false,
        Message = "He thong dang gap su co, vui long thu lai sau"
    };
    var result = JsonSerializer.Serialize(response);
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(result);
}));

app.UseCors(options =>
{
    options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

app.UseStaticFiles();

string appFolderPath = Directory.GetCurrentDirectory();
string resourceFolderPath = Path.Combine(appFolderPath, "Resources");
Directory.CreateDirectory(resourceFolderPath);
Directory.CreateDirectory(Path.Combine(appFolderPath, "Templates"));

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(resourceFolderPath),
    RequestPath = new PathString("/Resources")
});

app.UseAuthorization();

app.MapControllers();

app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Start2.Server.DBContext;
using Start2.Server.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["Appsettings:connectionString"];

builder.Services.AddDbContext<StartContext>(options =>
                options
                .UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());


var JWTPassword = builder.Configuration["Appsettings:JWTPassword"];
builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(JWTPassword))
        };
    });

builder.Services.AddAuthorization(options =>
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build()
);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .WithMethods("GET", "POST");
        });
});
builder.Services.AddScoped<ApiServices>();
builder.Services.AddScoped<AccountServices>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapPost("Account/Login", async (AccountServices services, object postParams) =>
    {
        return await services.RunService("Login", postParams);
    }).AllowAnonymous();


    endpoints.MapPost("Account/{service}", async (string service,
         AccountServices services, object postParams) =>
    {
        return await services.RunService(service, postParams);
    });

    //Endpoints.Map(endpoints);
});

app.Run();

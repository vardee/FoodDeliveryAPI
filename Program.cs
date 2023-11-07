using backendTask.AdditionalService;
using backendTask.DataBase;
using backendTask.InformationHelps;
using backendTask.Repository;
using backendTask.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using backendtask.Middleware;
using backendTask.DBContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<AddressDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AddressConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDishRepository, DishRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<TokenHelper>();
builder.Services.AddSingleton<EnumTranscription>(provider => new EnumTranscription("someStringValue"));
builder.Services.AddEndpointsApiExplorer();






builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 

        ValidIssuer = "JWTToken", 
        ValidAudience = "Human", 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BACKENDDEVELOPINGFORHITSBACKENDDEVELOPINGFORHITS"))
    };
});

builder.Services.AddAuthorization(services =>
{
    services.AddPolicy("TokenNotInBlackList", policy => policy.Requirements.Add(new TokenBlackListRequirment()));
});
builder.Services.AddSingleton<IAuthorizationHandler, TokenInBlackListHandler>();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

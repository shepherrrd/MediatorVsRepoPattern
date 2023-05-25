using Courseproject.API;
using Courseproject.API.CQRS.IdentityHandler;
using Courseproject.Business;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IGenericRepository<Job>, GenericRepository<Job>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddScoped<IGenericRepository<Team>, GenericRepository<Team>>();
builder.Services.AddSingleton<GenerateToken,GenerateToken>();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => 
{
    options.Password.RequiredLength =5;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    }).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters { 
        
        ValidateActor = true,
        ValidateIssuer= true,
        ValidateAudience=true,
        RequireExpirationTime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
        };
    }
    
    );
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
using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();

}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

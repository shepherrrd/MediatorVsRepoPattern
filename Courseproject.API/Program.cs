using Courseproject.Business;
using Courseproject.Common.Interfaces;
using Courseproject.Common.Model;
using Courseproject.Infrastructure;
using Microsoft.OpenApi.Writers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterServices(builder.Services);
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddScoped<IGenericRepository<Address>, GenericRepository<Address>>();
builder.Services.AddScoped<IGenericRepository<Job>, GenericRepository<Job>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();
builder.Services.AddControllers();
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

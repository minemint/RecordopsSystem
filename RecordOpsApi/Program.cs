using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecordOpsApi;
using RecordOpsApi.Repositories;
using RecordOpsApi.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");
}
builder.Services.AddDbContext<RecordOpsDbContext>(options =>
    options.UseMySQL(connectionString));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("MultipleOrigins",
    policy =>
    {
        policy.WithOrigins(
            //เปิดเฉพาะ domain ที่ต้องการให้เข้าถึง API
            "*"
        )
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyMethod()
        .AllowAnyHeader();

    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();

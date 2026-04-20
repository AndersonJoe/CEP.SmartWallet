using CEP.SmartWallet.Application.Transactions.Commands.CreateTransaction;
using CEP.SmartWallet.Infrastructure.Persistence;
using CEP.SmartWallet.Infrastructure.Services;
using CEP.SmartWallet.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTransactionCommand).Assembly));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IRiskService, RiskService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddCors( options =>
{
    options.AddPolicy("AllowAll", p => 
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");
app.MapControllers();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var dbRetries = 10;
    while (dbRetries > 0)
    {
        try
        {
            db.Database.Migrate();
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database migration failed: {ex.Message}");
            dbRetries--;
            if (dbRetries == 0)
            {
                Console.WriteLine("Exceeded maximum retry attempts for database migration !!!");
                //throw;
            }
            Console.WriteLine($"Retrying database migration... Attempts left: {dbRetries}");
            Thread.Sleep(3000); // Wait for 3 seconds before retrying
        }
    }
}

app.Run();


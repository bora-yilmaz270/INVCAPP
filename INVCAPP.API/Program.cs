using INVCAPP.Core.Repositories;
using INVCAPP.Core.Services;
using INVCAPP.Repository;
using INVCAPP.Repository.Repositories;
using INVCAPP.Service.Mapping;
using INVCAPP.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));

//builder.Services.AddDbContext<AppDbContext>(x => {
//    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
//    {
//        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
//    });
//});

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

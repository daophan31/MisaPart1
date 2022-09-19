

using MISA.WEB07.BL;
using MISA.WEB07.BL.BaseBL;
using MISA.WEB07.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IDeparmentBL, DepartmentBL>();
builder.Services.AddScoped<IDepartmentDL, DepartmentDL>();

//Lấy dữ liệu connection string từ file appsetting
DatabaseContext.ConnectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();

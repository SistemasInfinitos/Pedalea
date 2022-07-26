using CanonicalModel.Model.Configuration;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOptions();
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection($"JwtConfiguration"));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlConnectionStringBuilder connectionBuilder = new();

//connectionBuilder.DataSource = "SERVER";
//connectionBuilder.UserID = "Pepelera";
//connectionBuilder.Password = "Bell*900715*";
//connectionBuilder.InitialCatalog = "sa";
//string connectionString = connectionBuilder.ConnectionString;

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

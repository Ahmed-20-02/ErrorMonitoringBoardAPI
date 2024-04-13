using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DevelopmentProjectErrorBoardAPI.Business.AutofacDependencies;
using DevelopmentProjectErrorBoardAPI.Data;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var MyAllowAllOrigins = "_myAllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrigins,
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyMethod();;
        }
    );
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5015", "http://localhost:7213")
                .AllowAnyMethod();
        }
    );
});

////Reference
//https://stackoverflow.com/questions/69754985/adding-autofac-to-net-core-6-0-using-the-new-single-file-template/71448702#71448702
//02/01/2024
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContextFactory<DataContext>(
    options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

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

app.UseHttpsRedirection();

//Get around no CORS policy error
app.UseStaticFiles();
app.UseRouting();
app.UseCors(MyAllowAllOrigins /*MyAllowSpecificOrigins*/);

app.UseAuthorization();

app.MapControllers();

app.Run();

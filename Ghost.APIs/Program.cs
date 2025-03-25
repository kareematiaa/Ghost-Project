using Application.Extentions;
using Application.IService;
using Application.Services;
using Domain.IRepositories.IDataRepository;
using Domain.IRepositories.IExternalRepository;
using Domain.Users;
using Infrastructure.Context;
using Infrastructure.Context.Users;
using Infrastructure.Repositories.DataRepository;
using Infrastructure.Repositories.ExternalRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connection = builder.Configuration.GetConnectionString("Test");
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region Enable CORS
string allCorsName = "openAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allCorsName,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

#endregion


#region DbIdentity Context
builder.Services
    .AddDbContext<GhostContext>(options =>
    options.UseSqlServer(connection, /*ServerVersion.AutoDetect(connection),*/
    b =>
    {
        b.MigrationsAssembly("Infrastructure");

        //b.EnableRetryOnFailure(
        //    maxRetryCount: 5,
        //    maxRetryDelay: TimeSpan.FromSeconds(30),
        //    errorNumbersToAdd: null);
    }), ServiceLifetime.Scoped
    );

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<GhostContext>()
.AddDefaultTokenProviders();
#endregion

#region Services
builder.Services.AddScoped<IAdminDataRepository, AdminDataRepository>();
builder.Services.AddScoped<IAdminDataService, AdminDataService>();
builder.Services.AddScoped<IExternalRepository, ExternalRepository>();
builder.Services.AddScoped<IExternalService, ExternalService>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

#endregion

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

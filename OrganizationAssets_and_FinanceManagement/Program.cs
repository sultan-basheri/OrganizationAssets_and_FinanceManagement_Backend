using BusinessLayer.Interface;
using DatabaseLayer;
using DatabaseLayer.Repository;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// Services
// --------------------
builder.Services.AddControllers();

builder.Services.AddDBService();

builder.Services.AddScoped<IAdmin, ManageAdmin>();
builder.Services.AddScoped<IOrganization, ManageOrganization>();
builder.Services.AddScoped<IMember, ManageMember>();
builder.Services.AddScoped<IMosque, ManageMosque>();
builder.Services.AddScoped<IOfficeStaff, ManageOfficeStaff>();
builder.Services.AddScoped<IStaff, ManageStaff>();
builder.Services.AddScoped<IProperties, ManageProperties>();
builder.Services.AddScoped<IFinancialYear, ManageFinancialYear>();
builder.Services.AddScoped<IPropertyRentAgreement, ManagePropertyRentAgreement>();
builder.Services.AddScoped<IRentMaster, ManageRentMaster>();
// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------
// Middleware
// --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

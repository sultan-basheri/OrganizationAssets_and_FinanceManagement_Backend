using BusinessLayer.Interface;
using DatabaseLayer;
using DatabaseLayer.Repository;
using Microsoft.Extensions.FileProviders; // YEH ZARURI HAI STATIC FILES KE LIYE
using System.IO;

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
builder.Services.AddScoped<IDonation, ManageDonation>();
builder.Services.AddScoped<ISalaryMaster, ManageSalaryMaster>();
builder.Services.AddScoped<IWithdrawalSalary, ManageWithdrawalSalary>();
builder.Services.AddScoped<IExpenseCategory, ManageExpenseCategory>();
builder.Services.AddScoped<IExpenseMaster, ManageExpenseMaster>();
builder.Services.AddScoped<IPurchaseMaster, ManagePurchaseMaster>();
builder.Services.AddScoped<IPurchasePayment, ManagePurchasePayment>();
builder.Services.AddScoped<IPurchaseDetail, ManagePurchaseDetail>();
builder.Services.AddScoped<IVendor, ManageVendor>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Config
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

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

app.UseStaticFiles();

var documentsPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Documents");

if (!Directory.Exists(documentsPath))
{
    Directory.CreateDirectory(documentsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(documentsPath),
    RequestPath = "/Documents"
});

app.UseRouting();

app.UseCors("AllowReactApp");

// 5. AUTHORIZATION
app.UseAuthorization();

app.MapControllers();

app.Run();
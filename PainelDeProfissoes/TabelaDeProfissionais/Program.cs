using Microsoft.EntityFrameworkCore;
using TabelaDeProfissionais.Context;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura��o dos controladores e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configura��o do pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Mapeando a rota padr�o
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profissionais}/{action=Index}/{id?}");

app.Run();

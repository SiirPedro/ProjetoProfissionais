using Microsoft.EntityFrameworkCore;
using TabelaDeProfissionais.Context;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração dos controladores e views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisições
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

// Mapeando a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profissionais}/{action=Index}/{id?}");

app.Run();

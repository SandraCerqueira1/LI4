using DataLayer;
using demo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<ILeilaoRepository, LeilaoRepository>();
builder.Services.AddTransient<IEstatisticasRepository, EstatisticasRepository>();
builder.Services.AddTransient<IHistoricoCompraRepository, HistoricoCompraRepository>();
builder.Services.AddTransient<IHistoricoVendaRepository, HistoricoVendaRepository>();
builder.Services.AddTransient<ILegoRepository, LegoRepository>();
builder.Services.AddTransient<ILicitacaoRepository, LicitacaoRepository>();
builder.Services.AddTransient<IUtilizadorRepository, UtilizadorRepository>();
builder.Services.AddTransient<ICategoriasRepository, CategoriasRepository>();
builder.Services.AddScoped<LeilaoViewModel>();
builder.Services.AddSingleton<EstadoSite>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

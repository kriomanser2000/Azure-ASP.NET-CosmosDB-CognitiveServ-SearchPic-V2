using Microsoft.EntityFrameworkCore;
using SearchPic_V2.Models;
using SearchPic_V2.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CosmosClient>(cosmosClient =>
{
    string connectionString = builder.Configuration["CosmosDb:ConnectionString"];
    return new CosmosClient(connectionString);
});

builder.Services.AddScoped<ICosmosDbService, CosmosDbService>();

builder.Services.AddHttpClient<ICognitiveService, CognitiveService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["CognitiveServices:Endpoint"]);
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", builder.Configuration["CognitiveServices:ApiKey"]);
});

builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

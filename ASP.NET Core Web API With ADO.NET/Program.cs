using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//Bu kod, ASP.NET Core projesinde JSON serileþtirmesinin yapýlandýrýlmasý için kullanýlýr. 
//    AddControllersWithViews metodu, uygulamanýn kontrolörlerini ve görünümlerini eklerken,
//    AddNewtonsoftJson metodu ise JSON serileþtirmesi için Newtonsoft.Json kütüphanesini yapýlandýrýr.
//     satýrýyla, Newtonsoft.Json serileþtirme ayarlarýna eriþilir ve bu ayarlarla birlikte 
//     JSON nesnelerinin serileþtirilmesi için varsayýlan davranýþý deðiþtirebiliriz.
//     Burada DefaultContractResolver, serileþtirme iþlemi sýrasýnda JSON nesnelerinin tüm 
//     özelliklerini dikkate alarak doðrudan serileþtirme yapýlmasýný saðlar. Bu ayar, özellik 
//     isimlerinin PascalCase olmasýný ve baþka özel ayarlarýnýzý belirtmek için kullanýlabilir.
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

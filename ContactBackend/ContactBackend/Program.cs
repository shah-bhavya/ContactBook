
var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplicatonServices(builder.Configuration);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy",
    policy =>
    {
        policy
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("https://localhost:4200");
    });
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapping));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactBackend", Version = "v1" });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "SkiNet API v1"); });

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
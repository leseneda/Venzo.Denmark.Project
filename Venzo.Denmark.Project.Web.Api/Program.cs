using Venzo.Denmark.Project.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddDefaultPolicy(config =>
{
    config.AllowAnyHeader();
    config.AllowAnyMethod();
    config.AllowAnyOrigin();
}));

builder.Services.AddControllers();

builder.Services.AddSwaggerDocument(config =>
{
    config.Title = builder.Configuration.GetSection("Swagger:Document:Title").Get<string>();
});

// Services

builder.Services.AddProjectServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

// Swagger.
app.UseOpenApi();
app.UseSwaggerUi3();

await app.RunAsync();
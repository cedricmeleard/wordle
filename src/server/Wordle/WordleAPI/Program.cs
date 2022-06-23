var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCorsPolicy",
       policy =>
       {
           policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
       });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("./v1/swagger.json", "Wordle API V1"); //originally "./swagger/v1/swagger.json"
        });
}

app.UseHttpsRedirection();

app.UseCors("AllowCorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

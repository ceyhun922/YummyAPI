using YummyAPI.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApiContext>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("YummyAPI",policy =>
    {
         policy.WithOrigins("http://localhost:5016")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("YummyAPI");
app.UseHttpsRedirection();
app.MapControllers();

app.Run();


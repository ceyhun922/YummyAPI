<<<<<<< HEAD

using FluentValidation;
using FluentValidation.AspNetCore;
=======
>>>>>>> 6f2ea93 (Entities ve DTOs elave edildi)
using YummyAPI.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<ApiContext>();
<<<<<<< HEAD
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
=======
>>>>>>> 6f2ea93 (Entities ve DTOs elave edildi)
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


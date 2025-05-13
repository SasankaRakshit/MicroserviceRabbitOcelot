using MassTransit;
using Microservice;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
    };
});
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//add masstransit rabbitmq consumer
builder.Services.AddMassTransit(x =>
{
    // elided...
    x.AddConsumer<CommonConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var uri = new Uri(builder.Configuration["ServiceBus:Uri"]);
        cfg.Host(uri, h =>
        {
            h.Username(builder.Configuration["ServiceBus:Username"]);
            h.Password(builder.Configuration["ServiceBus:Password"]);
        });
        cfg.ReceiveEndpoint("Common", e =>
        {
            e.ConfigureConsumer<CommonConsumer>(context);
        });
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization(); 
app.Run();

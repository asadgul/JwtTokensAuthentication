using JwtAuthe.CustomJwt;
using JwtAuthe.IJwtServices;
using JwtAuthe.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Built in Jwt Authentication
//builder.Services.AddAuthentication(x =>
//{
//    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//}).AddJwtBearer(x =>
//{
//    x.RequireHttpsMetadata = false;
//    x.SaveToken = true;
//    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes("This is my test key")),
//        ValidateIssuer=false,
//        ValidateAudience=false    
//    };
//});
builder.Services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager("This is my test key"));
//Custome Jwt
builder.Services.AddAuthentication("Basic").AddScheme<BasicAuthenticationScheme, JwtCustomeHandler>("Basic", null);
builder.Services.AddSingleton<IJwtCustomManager,JwtCustomManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

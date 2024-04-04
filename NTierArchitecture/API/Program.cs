using API;
using Core.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.Context;
using Service.Mapping;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.ComponentModel;
using System.Text.Json;
using Core.CustomExceptions;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using API.ActionFilters;
using System.Net;
using Service.Validators.Estate;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//repository and service injections from "MyConfigures.cs" file
builder.Services.MyInjections();

//configure injection from "appsettings.json" file
builder.Services.Configure<StaticStringKeys>(builder.Configuration.GetSection("StaticStringKeys"));

//FluentValidation options
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AddEstateRequestDTOValidator>();
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

//Cors Policy Options
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true)));

//Caching Options
builder.Services.AddOutputCache(options => {
    //default cache is 15 minutes
    options.AddBasePolicy(builder=>builder.Expire(TimeSpan.FromSeconds(60*15)));
});

//swagger options
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//JWT Auth options
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("StaticStringKeys:JwtSecretKey").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,

        //jwttime expiration config
        ValidateLifetime = true,
        LifetimeValidator = (notBefore, expires, securityToken, ValidationParameters) => expires != null ? expires > DateTime.UtcNow : false
    };
});


builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);

//add action filter
builder.Services.AddControllers(opt => opt.Filters.Add<FluentValidationFilter>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database connection setup
builder.Services.AddDbContext<AppDbContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("Con_1")));

var app = builder.Build();

//use default cors policy
app.UseCors();

//use default caching policy
//app.UseOutputCache();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//JWT Auth options
app.UseAuthentication();

app.UseAuthorization();


app.Use(async (context, next) =>
{

    try
    {
        await next(context);
    }
    catch (Exception e)
    {
        if (e is EntityNotFoundException)
        {
            var customResponse = new CustomResponseDTO<NoResponseDTO>()
                  .SetStatusCode(HttpStatusCode.NotFound)
                  .SetIsSuccsess(false)
                  .AddError("the entity has sent id is not found")
                  .AddError("second dummy error");

            string responseJson = JsonSerializer.Serialize(customResponse);
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseJson);
        }

        else
        {
            var customResponse = new CustomResponseDTO<NoResponseDTO>()
                  .SetStatusCode(HttpStatusCode.InternalServerError)
                  .SetIsSuccsess(false)
                  .AddError("an internal error occured. please try agein or call your provider");

            string responseJson = JsonSerializer.Serialize(customResponse);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(responseJson);

        }
    }
});


app.MapControllers();

app.Run();

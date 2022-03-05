using HonsBackendAPI.Database;
using HonsBackendAPI.Services;
using HonsBackendAPI.JWT;
using Microsoft.AspNetCore.Mvc.Formatters;
using HonsBackendAPI.Services.Interfaces;
using HonsBackendAPI.Services.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Stripe;
using HonsBackendAPI.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 
 var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("OutdoorSuppliesDatabase"));
builder.Services.AddSingleton<CustomerRepository>();
builder.Services.AddSingleton<ProductRepository>();
builder.Services.AddSingleton<OrderLineRepository>();
builder.Services.AddSingleton<OrderRepository>();
builder.Services.AddSingleton<CategoryRepository>();
builder.Services.AddSingleton<ReviewRepository>();
builder.Services.AddSingleton<AdminRepository>();
builder.Services.AddSingleton<AddressRepository>();
builder.Services.AddSingleton<BasketRepository>();

builder.Services.AddJWTTokenServices(builder.Configuration);

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
StripeConfiguration.ApiKey = config["Stripe:SecretKey"];



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());





//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenKey")["TokenKey"])),
//            ValidateIssuer = false,

//            ValidateAudience = false,
//        };
//    });


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://uwssurvival.herokuapp.com");
                      });
});



builder.Services.AddControllers(setUpAction =>
{
    //if setUpAction set to false api will return response
    //in default supported format, IF unsuprted media type is requested
    setUpAction.ReturnHttpNotAcceptable = true;
   


    //support xml not as default but as possibility
}).AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();

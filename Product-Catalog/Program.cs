using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Product_Catalog.Data;
using Product_Catalog.Middleware;
using Product_Catalog.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
//Register SQL database configuration context as services.
builder.Services.AddDbContext<ProductContext>(options =>
        options.UseSqlServer(connection));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://product-catalog.com",
            ValidAudience = "http://product-catalog.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mY!8yvVonMgBmg>.p})BewBp_9ZQ+?nZTj,"))
        };
    });

builder.Services.AddAuthorization(options =>
{
//options.AddPolicy("Region", policy =>
//    policy.Requirements.Add());
options.AddPolicy("North America", policy =>
    policy.RequireClaim("Country", "Canada", "United States", "Mexico"));
});


// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigins",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:4200") // Specify the allowed origins
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

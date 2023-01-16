using Backend.Utils;
using DataRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using Swashbuckle.AspNetCore.Filters;

var myCorsPolicyName = "corspolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DataRepository and Services form Class Library

builder.Services.AddDataRepository();
builder.Services.AddServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<TokenUtil>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["jwtkey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new OpenApiInfo {Title = "Solbeg Task 5 api", Version = "v1"});
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Paste auth token here (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(p => p.AddPolicy(myCorsPolicyName, build =>
{
    foreach (var allowedHost in builder.Configuration.GetSection("AllowedCrossOrigin").Get<List<string>>())
    {
        Console.WriteLine(allowedHost);
        build.WithOrigins(allowedHost).AllowAnyMethod().AllowAnyHeader();
    }

}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "SolbegTask5Swagger"));
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(myCorsPolicyName);

app.Run();
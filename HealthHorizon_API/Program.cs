using HealthHorizon_API.Data;
using HealthHorizon_API.Interfaces;
using HealthHorizon_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddDbContext<HealthHorizonContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("HealthHorizonContext")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<HealthHorizonContext>()
	.AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
	}
);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllOrigins", policy =>
	{
		policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
	});
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

	await SeedRolesAsync(roleManager); // Ensure roles exist
	await SeedAdminUserAsync(userManager, roleManager); // Add default admin
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapScalarApiReference();
	app.MapOpenApi();
}

app.MapIdentityApi<IdentityUser>();

//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.UseCors("AllowReactApp");
app.UseCors("AllowAllOrigins");

app.MapControllers();

app.Run();

async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
{
	string[] roles = { "admin", "doctor", "staff", "patient"};
	foreach (string role in roles)
	{
		if (!await roleManager.RoleExistsAsync(role))
		{
			await roleManager.CreateAsync(new IdentityRole(role));
		}
	}
}

async Task SeedAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
	string adminUserName = "lord commander";
	string adminEmail = "healthhorizon790@gmail.com";
	string adminPassword = "healthhorizonproject";

	var adminUser = await userManager.FindByEmailAsync(adminEmail);
	if (adminUser == null)
	{
		adminUser = new IdentityUser
		{
			UserName = adminUserName,
			Email = adminEmail,
			EmailConfirmed = true
		};

		var result = await userManager.CreateAsync(adminUser, adminPassword);
		if (result.Succeeded)
		{
			await userManager.AddToRoleAsync(adminUser, "admin");
		}
	}
}

app.Run();

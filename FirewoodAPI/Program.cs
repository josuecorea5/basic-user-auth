using FirewoodAPI.Authentication;
using FirewoodAPI.Authentication.JwtOptionsSetup;
using FirewoodAPI.Automappers;
using FirewoodAPI.DTOs;
using FirewoodAPI.Models;
using FirewoodAPI.Repositories;
using FirewoodAPI.Services;
using FirewoodAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Cors
builder.Services.AddCors(options =>
{
	options.AddPolicy("corsPolicy", policy =>
	{
		policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
	});
});

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReadRepository<Role>, RoleRepository>();

//JWT Setup
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtProvider, JwtService>();

//Fluent validations
builder.Services.AddScoped<IValidator<UserInsertDto>, UserInsertValidator>();
builder.Services.AddScoped<IValidator<UserLoginDto>, UserLoginValidator>();
builder.Services.AddScoped<IValidator<UserUpdateDto>, UserUpdateValidator>();

//EF Injection
builder.Services.AddDbContext<FirewoodContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("FirewoodConnection"));
});

//Mappers
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Bcrypt
builder.Services.AddScoped<IBcryptService, BcryptService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo{ Title = "Basic User Role API", Version = "v1"});

	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Name = "Authorization",
		Description = "JWT must be provided",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
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

app.UseCors("corsPolicy");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

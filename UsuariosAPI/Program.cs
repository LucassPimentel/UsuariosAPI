using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using UsuariosAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configurando o banco de dados
builder.Services.AddDbContext<UserDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("UserConnection")));

//Adicionando o Identity
builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(
    // definindo que é necessário confirmar o email para fazer o login
    opt => opt.SignIn.RequireConfirmedEmail = true
    )

    //Informando ao Identity aonde deve ser armazenado os
    //dados que estao sendo usados para identificaçao
    .AddEntityFrameworkStores<UserDbContext>()

    // gera tokens para resetar senhas, emails, numero de telefone...
    .AddDefaultTokenProviders();


builder.Services.AddControllers();
builder.Services.AddScoped<RegisterService, RegisterService>();
builder.Services.AddScoped<LoginService, LoginService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<LogoutService, LogoutService>();
builder.Services.AddScoped<EmailService, EmailService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

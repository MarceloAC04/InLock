using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Adiciona servi�o de  Controller
builder.Services.AddControllers();

//Adiciona servi�o de Jwt Bearer (forma de autentica��o)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";
})

.AddJwtBearer("JwtBearer", options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         //Valida que esta solicitando
         ValidateIssuer = true,

         //Valida que esta recebendo
         ValidateAudience = true,

         //Define se o tempo de expira��o ser� validado
         ValidateLifetime = true,

         //Forma de criptografia que valida a chave de autentica��o
         IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Inlock_games-chave-autenticacao-webapi-dev")),

         //Valida o tempo  de expira��o do token
         ClockSkew = TimeSpan.FromMinutes(5),

         //nome do issuer (de onde esta vindo)
         ValidIssuer = "senai.inlock.webApi",

         //nome do audience (para onde esta indo)
         ValidAudience = "senai.inlock.webApi"

     };
 });

//Adiciona a servico do Swagger
builder.Services.AddSwaggerGen(options =>
{
    //Adiciona informa��es sobre a API no Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Inlock",
        Description = "API para gerenciamento de jogos - Introdu��o da sprint 2 - Backend API",
        Contact = new OpenApiContact
        {
            Name = "Marcelo",
            Url = new Uri("https://github.com/MarceloAC04")
        }
    });
    //Configura o Swagger para usar o arquivo XML gerado
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    //Usando a autentica�ao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

//Adiciona autentica��o
app.UseAuthentication();

//Adicona autoriza��o
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();

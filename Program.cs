using Operacao_curiosidade_API.Services.Interfaces; // Adicione este using
using Operacao_curiosidade_API.Services.Implementations; // Se necessário

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// **Adicionar registro de serviços aqui**
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICuriosityService, CuriosityService>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Isso adiciona a funcionalidade para mapear controladores

app.Run();
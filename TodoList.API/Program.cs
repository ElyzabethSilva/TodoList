using TodoList.API.Services;
using TodoList.Infrastructure.Repositories;
using TodoList.Core.Interfaces;
using TodoList.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Registrar repositório e use case
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<AddTodoItemUseCase>();

var app = builder.Build();

// Mapear o serviço gRPC
app.MapGrpcService<TodoService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
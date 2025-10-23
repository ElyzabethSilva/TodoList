using TodoList.API.Services;
using TodoList.Application.UseCases;
using TodoList.Core.Interfaces;
using TodoList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<AddTodoItemUseCase>();
builder.Services.AddScoped<GetTodoUseCase>();
builder.Services.AddScoped<GetAllTodosUseCase>();
builder.Services.AddScoped<UpdateTodoUseCase>();
builder.Services.AddScoped<DeleteTodoUseCase>();

var app = builder.Build();

app.MapGrpcService<TodoService>();
app.MapGet("/", () => "Use a gRPC client to communicate with endpoints.");

app.Run();

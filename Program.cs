using LoginREST;

public class Program
{
    public static void Main(string[] args)
    {
        UserDatabase.Initialize();

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSwaggerGen();

        WebApplication app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}
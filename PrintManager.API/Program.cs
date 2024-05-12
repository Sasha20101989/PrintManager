namespace PrintManager.API;

/// <summary>
/// Главный класс программы.
/// </summary>
public static class Program
{
    /// <summary>
    /// Главный метод, запускающий приложение.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    public static void Main(string[] args) =>
        CreateHostBuilder(args).Build().Run();

    /// <summary>
    /// Метод для создания построителя хоста.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    /// <returns>Построитель хоста.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
            });
}
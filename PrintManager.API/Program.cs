namespace PrintManager.API;

/// <summary>
/// ������� ����� ���������.
/// </summary>
public static class Program
{
    /// <summary>
    /// ������� �����, ����������� ����������.
    /// </summary>
    /// <param name="args">��������� ��������� ������.</param>
    public static void Main(string[] args) =>
        CreateHostBuilder(args).Build().Run();

    /// <summary>
    /// ����� ��� �������� ����������� �����.
    /// </summary>
    /// <param name="args">��������� ��������� ������.</param>
    /// <returns>����������� �����.</returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
            });
}
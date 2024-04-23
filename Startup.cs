public void ConfigureServices(IServiceCollection services)
{
    // Other configuration

    services.AddDbContext<ItemDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
}

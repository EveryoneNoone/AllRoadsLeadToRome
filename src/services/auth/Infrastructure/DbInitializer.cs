using Infrastructure.Context;

namespace Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            InitializeDatabase(context);
        }

        public static void InitializeDatabase(AppDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}

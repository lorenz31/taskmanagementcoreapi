using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoreApiProject.DAL.DataContext
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        private string _connectionString = @"Data Source=(localdb)\LocalDB;Initial Catalog=TasksDB;Integrated Security=true;Trusted_Connection=True;MultipleActiveResultSets=True";

        public DatabaseContext CreateDbContext(string[] args)
        {
            var dbBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            dbBuilder.UseSqlServer(_connectionString);

            return new DatabaseContext(dbBuilder.Options);
        }
    }
}

using EmployeeDirectory.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeDirectory.Data
{
    public class EmpDirectoryContext : DbContext
    {
        public EmpDirectoryContext(DbContextOptions<EmpDirectoryContext> options) : base(options) { }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Section> Section { get; set; }
    }
}
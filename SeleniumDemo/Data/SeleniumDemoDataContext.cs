using System.Data;
using System.Data.Entity;
using System.Data.Linq.Mapping;
using SeleniumDemo.Models;

namespace SeleniumDemo.Data
{
    public class SeleniumDemoDataContext : DbContext
    {
        public System.Data.Entity.DbSet<SeleniumDemo.Models.Country> Countries { get; set; }
    }
}
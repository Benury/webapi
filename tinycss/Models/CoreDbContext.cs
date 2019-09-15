using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyCSS_Webapi.Models
{
    public class CoreDbContext : DbContext
    {
        public virtual DbSet<tblelement> tblelement { get; set; } //创建实体类添加Context中

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL(Config.dbconn);
    }
}

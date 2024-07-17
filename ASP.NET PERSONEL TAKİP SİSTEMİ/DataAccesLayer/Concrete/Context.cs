using EntitiyLayer.Concrete.Entitiy;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Concrete
{
    public class Context : DbContext
    {
        public DbSet<kullanıcı> Kullanıcılar { get; set; }
        public DbSet<firma> firmalar { get; set; }
        public DbSet<personel> Personeller { get; set; }
        public DbSet<sube> Subeler { get; set; }


    }
}

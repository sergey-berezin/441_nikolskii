using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace PerceptionComponent
{
    public class LibraryContext: DbContext
    {
        public DbSet<ImageDB> Images { get; set; }
        public DbSet<ObjDescription> Rectangles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder o) =>
            o.UseLazyLoadingProxies().UseSqlite("DataSource=C:\\Users\\79663\\Desktop\\Вован\\Lab_2_3\\PerceptionComponent\\library.db");
    }
}

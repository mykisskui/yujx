﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mykisskui
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class yujingxueEntities : DbContext
    {
        public yujingxueEntities()
            : base("name=yujingxueEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<article> article { get; set; }
        public DbSet<Config> Config { get; set; }
        public DbSet<MyAdmin> MyAdmin { get; set; }
        public DbSet<Navi> Navi { get; set; }
        public DbSet<Bookmark> Bookmark { get; set; }
        public DbSet<Music> Music { get; set; }
    }
}

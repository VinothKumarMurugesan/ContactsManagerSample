using SMEasy.Domain.Entity;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
 

namespace SMEasy.Data
{
    public partial class SMEasyEntities : DbContext
    {
        public SMEasyEntities()
        {
            this.Configuration.ProxyCreationEnabled = false; 
        }

        public SMEasyEntities(string connectionString)
            : base(connectionString)
        {
        }

        public string GetTableName(Type entityType)
        {
            var sql = this.Set(entityType).ToString();
            var regex = new System.Text.RegularExpressions.Regex(@"FROM \[dbo\]\.\[(?<table>.*)\] AS");
            var match = regex.Match(sql);

            return match.Groups["table"].Value;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            

           //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            InitializeDecimal(modelBuilder);
            //modelBuilder.Entity<Product>().Property(x => x.BinLocationId).IsOptional();
            //modelBuilder.Entity<Product>().Property(x => x.LedgerAccountId).IsOptional();
            //modelBuilder.Entity<Product>().Property(x => x.BrandId).IsOptional();
            //modelBuilder.Entity<Product>().Property(x => x.ProductGroupId).IsOptional();
            //modelBuilder.Entity<Product>().Property(x => x.DepartmentId).IsOptional();
            //modelBuilder.Entity<Product>().Property(x => x.SectionId).IsOptional();
           
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contact> Contact { get; set; }

        public void InitializeDecimal(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 3));
        }
    }
}

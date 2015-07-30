using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Transactions;
 
using Xen.Entity;
using Xen.Helpers;
using System.Linq;

namespace SMEasy.Data
{
    internal sealed class SeedData : DbMigrationsConfiguration<SMEasyEntities>
    {
        public SeedData()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SMEasyEntities context)
        {
            using (System.Transactions.TransactionScope scope = new TransactionScope())
            {
 
            }            
        }

       
    }
}
 
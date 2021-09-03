using CSVEDITOR.Models.File;
using CSVEDITOR.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEDITOR.Models.Context
{
    public class MainContext : IdentityDbContext<UserModel>
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TransactionModel> Transactions { get; set; }
    }
}

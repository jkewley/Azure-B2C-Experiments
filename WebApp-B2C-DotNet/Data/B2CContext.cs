using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

using WebApp_OpenIDConnect_DotNet_B2C.Model;

namespace WebApp_OpenIDConnect_DotNet_B2C.Data
{
    public class B2CContext : DbContext
    {
        public B2CContext() : base("B2CContext") {}
        public DbSet<Applicant> Applicants { get; set; }
    }
}
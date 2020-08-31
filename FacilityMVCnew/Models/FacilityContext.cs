using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMVCnew.Models
{
    public class FacilityContext: DbContext
    {
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityStatus> FacilityStatuses { get; set; }

        public FacilityContext(DbContextOptions<FacilityContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

    }
}

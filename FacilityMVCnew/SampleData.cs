using FacilityMVCnew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacilityMVCnew
{
    public static class SampleData
    {
        public static void Initialize(FacilityContext context)
        {
            if (!context.FacilityStatuses.Any())
            {
                context.FacilityStatuses.AddRange(
                    new FacilityStatus
                    {
                        Name = "Active"                      
                    },
                     new FacilityStatus
                     {
                         Name = "InActive"
                     },
                     new FacilityStatus
                     {
                         Name = "OnHold"
                     }
                );
                context.SaveChanges();
            }
        }
    }
}


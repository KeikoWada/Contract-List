using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MvcContract.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcContractContext(
                serviceProvider.GetRequiredService<DbContextOptions<MvcContractContext>>()))
            {
                // Look for any contracts.
                if (context.Contract.Any())
                {
                    return;   // DB has been seeded
                }

                context.Contract.AddRange(
                     new Contract
                     {
                         FirstName = "John",
                         LastName = "M",
                         JobTitle = "Teacher",
                         Phone = 11111111,
                         Email = "j@gmail.com"
                     },

                    new Contract
                     {
                         FirstName = "Kat",
                         LastName = "M",
                         JobTitle = "Teacher",
                         Phone = 2222222,
                         Email = "k@gmail.com"
                     },

                    new Contract
                    {
                        FirstName = "Brian",
                        LastName = "M",
                        JobTitle = "Real Estate",
                        Phone = 3333333,
                        Email = "b@gmail.com"
                    },

                    new Contract
                   {
                       FirstName = "Chris",
                       LastName = "M",
                       JobTitle = "Teacher",
                       Phone = 4444444,
                       Email = "c@gmail.com"
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atm_Rod_Entities.Entity;

namespace Atm_Rod_DataAccess.FluentConfig
{
    public class CardConfig
    {
        public void Configure(EntityTypeBuilder<Card> modelBuilder)
        {
            //modelBuilder
            //    .HasMany<Model>(b => b.Models)
            //    .WithOne(b => b.Brand)
            //    .HasForeignKey(b => b.BrandId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

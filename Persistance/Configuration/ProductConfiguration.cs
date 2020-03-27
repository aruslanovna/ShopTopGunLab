using System;
using System.Collections.Generic;
using System.Text;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistance.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
 

            public void Configure(EntityTypeBuilder<Product> builder)
            {

                builder.Property(e => e.ProductId).HasColumnName("ProductID");

                builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(15);

                builder.Property(e => e.Quantity).HasColumnType("ntext");

               
            }
        }
    }

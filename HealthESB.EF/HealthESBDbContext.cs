using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

using HealthESB.Domain.Entities;
using HealthESB.Framework.Utility;
using System.Threading.Tasks;
using System.Threading;

namespace HealthESB.EF
{
    public class HealthESBDbContext : IdentityDbContext
    {
        public HealthESBDbContext()
            : base(SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), "DefaultConnection")
                .Options)
        {
        }

        public HealthESBDbContext(DbContextOptions<HealthESBDbContext> options) : base(options)
        {
        }

        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<PrescriptionBarcode> PrescriptionBarcode { get; set; }
        public DbSet<PrescriptionBarcodeDetailes> PrescriptionBarcodeDetailes { get; set; }
        public DbSet<PrescriptionBarcodeStatus> PrescriptionBarcodeStatus { get; set; }
        public DbSet<PrescriptionBarcodeDetailesType> PrescriptionBarcodeDetailesType { get; set; }
        public DbSet<Claims> Claims { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
            try
            {
                var modifiedEntries = this.ChangeTracker.Entries();

                foreach (var entry in modifiedEntries)
                {
                    IAuditableEntity entity = entry.Entity as IAuditableEntity;
                    if (entity != null)
                    {                    
                        DateTime now = DateTime.Now;
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedBy = Constants.LoginUserId;
                            entity.CreatedDate = now;
                        }
                        else
                        {
                            base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        }

                        entity.UpdatedBy = Constants.LoginUserId;
                        entity.UpdatedDate = now;
                        var validationContext = new ValidationContext(entity);
                        Validator.ValidateObject(entity, validationContext);
                    }
                }

                return await base.SaveChangesAsync();
            }

            catch (ValidationException e)
            {
                throw;
            }
        }
    }
}

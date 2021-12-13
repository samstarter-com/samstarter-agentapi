using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;
using System.Linq;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class ProcessorRepository : DbContextRepository<Processor>
    {
        public ProcessorRepository(DbContext context) : base(context)
        {
        }

        public override void Update(Processor entity, object id)
        {
            AddAssociations(entity);
            base.Update(entity, id);
        }

        private void AddAssociations(Processor entity)
        {
            string manufacturerName = entity.Manufacturer.Name;
            var manufacturerRepository = new DbContextRepository<Manufacturer>(Context);

            //Get existing Manufacturer for database for the Processor
            Manufacturer existingManufacturer =
                manufacturerRepository.GetAll().FirstOrDefault(m => m.Name == manufacturerName);

            if (existingManufacturer != null)
            {
                entity.Manufacturer = existingManufacturer;
            }
            else
            {
                manufacturerRepository.Add(entity.Manufacturer);
            }
        }

        public override void Add(Processor entity)
        {
            AddAssociations(entity);
            base.Add(entity);
        }
    }
}
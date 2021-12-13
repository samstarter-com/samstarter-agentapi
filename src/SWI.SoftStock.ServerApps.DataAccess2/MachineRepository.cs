using Microsoft.EntityFrameworkCore;
using SWI.SoftStock.ServerApps.DataAccess.Common2;
using SWI.SoftStock.ServerApps.DataModel2;
using System;
using System.Linq;

namespace SWI.SoftStock.ServerApps.DataAccess2
{
    public class MachineRepository : DbContextRepository<Machine>
    {
        public MachineRepository(DbContext context) : base(context)
        {
        }

        public override void Delete(Machine entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("Cannot delete a null entity.");
            }
            Context.Set<LicenseRequestDocument>().RemoveRange(entity.LicenseRequests.SelectMany(lr => lr.LicenseRequestDocuments));
            Context.Set<Process>().RemoveRange(entity.MachineObservedProcesses.SelectMany(mop => mop.Processes));

            Context.Set<MachineUser>().RemoveRange(entity.LinkedUsersHistory);
            Context.Set<MachineStructureUnit>().RemoveRange(entity.LinkedStructureUnitsHistory);
            if (entity.MachineOperationSystem != null)
            {
                Context.Set<MachineOperationSystem>().Remove(entity.MachineOperationSystem);
            }
            Context.Set<MachineObservedProcess>().RemoveRange(entity.MachineObservedProcesses);
            Context.Set<MachineDomainUser>().RemoveRange(entity.DomainUsers);
            Context.Set<LicenseRequestHistory>().RemoveRange(entity.LicenseRequests.SelectMany(lr=>lr.LicenseRequestHistories));
            Context.Set<LicenseRequest>().RemoveRange(entity.LicenseRequests);
            Context.Set<LicenseMachineSoftware>().RemoveRange(entity.MachineSoftwares.SelectMany(ms => ms.LicenseMachineSoftwares));
            Context.Set<MachineSoftwareHistory>().RemoveRange(entity.MachineSoftwareHistories);
            Context.Set<MachineSoftware>().RemoveRange(entity.MachineSoftwares);
          
            Context.Set<NetworkAdapter>().RemoveRange(entity.NetworkAdapters);

            base.Delete(entity);
        }

        public override void DeleteRange(Machine[] entities)
        {
            Context.Set<NetworkAdapter>().RemoveRange(entities.SelectMany(la => la.NetworkAdapters));
            base.DeleteRange(entities);

        }

        public override void Update(Machine entity, object id)
        {
            AddAssociations(entity);
            base.Update(entity, id);
        }

        public override void Add(Machine entity)
        {
            AddAssociations(entity);
            base.Add(entity);
        }

        private void AddAssociations(Machine entity)
        {
            var processorRepository = new ProcessorRepository(Context);
            string deviceId = entity.Processor.DeviceID;
            bool is64BitProcess = entity.Processor.Is64BitProcess;
            string socketDesignation = entity.Processor.SocketDesignation;
            string manufacturerName = entity.Processor.Manufacturer.Name;
            Processor existingProcessor =
                processorRepository.GetAll().FirstOrDefault(p => p.DeviceID == deviceId
                                                                 &&
                                                                 p.Is64BitProcess ==
                                                                 is64BitProcess
                                                                 &&
                                                                 p.SocketDesignation ==
                                                                 socketDesignation
                                                                 &&
                                                                 p.Manufacturer.Name ==
                                                                 manufacturerName);

            if (existingProcessor != null)
            {
                entity.Processor = existingProcessor;
                entity.ProcessorId = existingProcessor.Id;
            }
            else
            {
                processorRepository.Add(entity.Processor);
            }
            if (entity.Id != 0)
            {
                Machine existingMachine = DbSet.Find(entity.Id);
                if (existingProcessor == null)
                {
                    existingMachine.Processor = entity.Processor;
                    if (Context.Entry(existingMachine).State != EntityState.Modified)
                    {
                        Context.Entry(existingMachine).State = EntityState.Modified;
                    }
                }

                var networkAdapterRepository = new DbContextRepository<NetworkAdapter>(Context);
                IQueryable<NetworkAdapter> existingNetworkAdapters =
                    networkAdapterRepository.GetAll().AsNoTracking().Where(
                        na => na.Machine.Id == entity.Id);

                foreach (NetworkAdapter existingNetworkAdapter in existingNetworkAdapters)
                {
                    if (!entity.NetworkAdapters.Any(EqualNetworkAdapter(existingNetworkAdapter)))
                    {
                        existingNetworkAdapter.Machine = existingMachine;
                        var e = networkAdapterRepository.Context.Entry(existingNetworkAdapter);
                        if (e.State == EntityState.Detached)
                        {
                           networkAdapterRepository.DbSet.Attach(existingNetworkAdapter);
                        }
                        e.State = EntityState.Deleted;
                        if (Context.Entry(existingMachine).State != EntityState.Modified)
                        {
                            Context.Entry(existingMachine).State = EntityState.Modified;
                        }
                    }
                }
                foreach (NetworkAdapter clientnetworkAdapter in entity.NetworkAdapters)
                {
                    if (!existingNetworkAdapters.Any(EqualNetworkAdapter(clientnetworkAdapter)))
                    {
                        var e = networkAdapterRepository.Context.Entry(clientnetworkAdapter);
                        if (e.State == EntityState.Detached)
                        {
                            networkAdapterRepository.DbSet.Attach(clientnetworkAdapter);
                        }
                        e.State = EntityState.Added;
                        clientnetworkAdapter.Machine = existingMachine;
                        if (Context.Entry(existingMachine).State != EntityState.Modified)
                        {
                            Context.Entry(existingMachine).State = EntityState.Modified;
                        }
                    }
                }
            }
        }

        private static Func<NetworkAdapter, bool> EqualNetworkAdapter(NetworkAdapter other)
        {
            return na => na.Caption == other.Caption && na.MacAdress == other.MacAdress;
        }
    }
}
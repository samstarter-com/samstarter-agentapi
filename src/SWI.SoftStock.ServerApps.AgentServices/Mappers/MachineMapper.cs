using System.Linq;
using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.AgentServices.Mappers
{
    public static class MachineMapper
    {
        public static MachineResponse ToResponse(Machine machine)
        {
            var result = new MachineResponse();
            return result;
        }

        public static Machine ToModel(MachineRequest request)
        {
            var result = new Machine();
            result.CompanyUniqueId = request.Machine.CompanyUniqueId;
            result.UniqueId = request.Machine.UniqueId;
            result.Name = request.Machine.Name;
            result.MonitorCount = request.Machine.MonitorCount;
            result.MonitorsSameDisplayFormat = request.Machine.MonitorsSameDisplayFormat;
            result.MouseButtons = request.Machine.MouseButtons;
            result.ScreenOrientation = request.Machine.ScreenOrientation;
            result.ProcessorCount = request.Machine.ProcessorCount;
            result.MemoryTotalCapacity = request.Machine.MemoryTotalCapacity;
            result.Processor = ProcessorMapper.ToModel(request.Machine.Processor);
            result.NetworkAdapters = request.Machine.NetworkAdapters.Select(NetworkAdapterMapper.ToModel).ToArray();
            return result;
        }
    }

    public static class ProcessorMapper
    {
        public static Processor ToModel(ProcessorDto processor)
        {
            var result = new Processor();
            result.DeviceID = processor.DeviceID;
            result.Is64BitProcess = processor.Is64BitProcess;
            result.Manufacturer = ManufacturerMapper.ToModel(processor.Manufacturer);
            result.ProcessorId = processor.ProcessorId;
            result.SocketDesignation = processor.SocketDesignation;
            return result;
        }
    }

    public static class NetworkAdapterMapper
    {
        public static NetworkAdapter ToModel(NetworkAdapterDto networkAdapter)
        {
            var result = new NetworkAdapter();
            result.Caption = networkAdapter.Caption;
            result.MacAdress = networkAdapter.MacAdress;
            return result;
        }
    }

    public static class ManufacturerMapper
    {
        public static Manufacturer ToModel(ManufacturerDto manufacturer)
        {
            var result = new Manufacturer();
            result.Name = manufacturer.Name;
            return result;
        }
    }
}
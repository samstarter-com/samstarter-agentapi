namespace SWI.SoftStock.Common.Dto2
{
    public class DataResponse : Response
    {
        public ObservedProcessDto[] ObservedProcesses { get; set; }

        public AgentDataDto AgentData { get; set; }
    }
}
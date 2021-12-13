using SWI.SoftStock.Common.Dto2;
using SWI.SoftStock.ServerApps.DataModel2;

namespace SWI.SoftStock.ServerApps.AgentServices.Mappers
{
    public class CompanyMapper
    {
        public static CompanyResponse ToResponse(StructureUnit company)
        {
            var result = new CompanyResponse();
            result.Id = company.Id;
            result.UniqueId = company.UniqueId;
            return result;
        }

        //public static StructureUnit ToModel(CompanyRequest request)
        //{
        //    var result = new StructureUnit();
        //    result.Name = request.Name;
        //    result.ShortName = request.Name;
        //    result.UnitType=UnitType.Company;
        //    return result;
        //}
    }
}
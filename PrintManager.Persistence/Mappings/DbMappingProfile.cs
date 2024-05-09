using AutoMapper;
using PrintManager.Logic.Models;
using PrintManager.Persistence.Entities;

namespace PrintManager.Persistence.Mappings
{
    public class DbMappingProfile : Profile
    {
        public DbMappingProfile()
        {
            CreateMap<Installation, InstallationEntity>().ReverseMap();
            CreateMap<Status, StatusEntity>().ReverseMap();
            CreateMap<Printer, PrinterEntity>().ReverseMap();
            CreateMap<Job, JobEntity>().ReverseMap();
            CreateMap<Employee, EmployeeEntity>().ReverseMap();
            CreateMap<ConnectionType, ConnectionTypeEntity>().ReverseMap();
            CreateMap<Branch, BranchEntity>().ReverseMap();
        }
    }
}

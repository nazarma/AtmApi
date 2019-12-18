namespace Atm.Application.AutoMapper
{
    using Domain;
    using Domain.DataTransferObjects;
    using global::AutoMapper;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LegalTender, LegalTenderDto>();

//                .ForAllMembers(opt =>
//                {
//                    opt.UseDestinationValue();
//                    opt.Ignore();
//                });
        }
    }
}

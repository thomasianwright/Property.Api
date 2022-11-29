using AutoMapper;
using Property.Api.BusinessLogic.MappingProfiles.ValueConverters;
using Property.Api.Core.Models;
using Property.Api.Entities.Models;

namespace Property.Api.BusinessLogic.MappingProfiles;

public class AccountMapping : Profile
{
    public AccountMapping()
    {
        CreateMap<Account, AccountDto>()
            .ForMember(d => d.Id, opt => opt.ConvertUsing<IntToHash, int>())
            .ForMember(d => d.AccountUserOwnerId, opt => opt.ConvertUsing<IntToHash, int>());
    }
}
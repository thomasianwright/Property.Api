using AutoMapper;
using HashidsNet;

namespace Property.Api.BusinessLogic.MappingProfiles.ValueConverters;

public class IntToHash : IValueConverter<int, string>
{
    private readonly IHashids hashids;

    public IntToHash(IHashids hashids)
    {
        this.hashids = hashids;
    }

    public string Convert(int source, ResolutionContext context)
    {
        return hashids.Encode(source);
    }
}
namespace Property.Api.Contracts.Repositories;
using PropertyModel = Entities.Models.Property;

public interface IPropertyRepository
{
    Task<PropertyModel?> GetPropertyByIdAsync(int id);
    Task<IEnumerable<PropertyModel?>> GetPropertiesAsync();
    Task AddPropertyAsync(PropertyModel? property);
    Task UpdatePropertyAsync(PropertyModel? property);
    Task DeletePropertyAsync(PropertyModel? property);
}
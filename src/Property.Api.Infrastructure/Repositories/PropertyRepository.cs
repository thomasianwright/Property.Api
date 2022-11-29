using PropertyModel = Property.Api.Entities.Models.Property;

namespace Property.Api.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly ApiContext _apiContext;

    public PropertyRepository(ApiContext apiContext)
    {
        _apiContext = apiContext;
    }
    
    public async Task<PropertyModel?> GetPropertyByIdAsync(int id)
    {
        return await _apiContext.Properties.FirstOrDefaultAsync(x=> x!.Id == id);
    }
    
    public async Task<IEnumerable<PropertyModel?>> GetPropertiesAsync()
    {
        return await _apiContext.Properties.ToListAsync();
    }
    
    public async Task AddPropertyAsync(PropertyModel? property)
    {
        await _apiContext.Properties.AddAsync(property);
        await _apiContext.SaveChangesAsync();
    }
    
    public async Task UpdatePropertyAsync(PropertyModel? property)
    {
        _apiContext.Properties.Update(property);
        await _apiContext.SaveChangesAsync();
    }
    
    public async Task DeletePropertyAsync(PropertyModel? property)
    {
        _apiContext.Properties.Remove(property);
        await _apiContext.SaveChangesAsync();
    }
}
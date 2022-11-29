using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Property.Api.Contracts.Services;
using Property.Api.Core.Models;

namespace Property.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IHashids _hashids;
    private readonly ILogger<PropertyController> _logger;

    public PropertyController(IPropertyService propertyService, IHashids hashids, ILogger<PropertyController> logger)
    {
        _propertyService = propertyService;
        _hashids = hashids;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        try
        {
            var propertyId = _hashids.Decode(id).FirstOrDefault();

            var property = await _propertyService.GetPropertyByIdAsync(propertyId);

            return Ok(property);
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProperties([FromQuery] string id, [FromQuery] string type)
    {
        try
        {
            var companyId = _hashids.DecodeSingle(id);

            var properties = await _propertyService.GetProperties(companyId);

            return Ok(properties);
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePropertyDto request)
    {
        try
        {
            var property = await _propertyService.CreateProperty(request);

            return Ok(property);
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePropertyDto request, string id)
    {
        try
        {
            var propertyId = _hashids.DecodeSingle(id);
            var property = await _propertyService.UpdateProperty(request, propertyId);

            return Ok(property);
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var propertyId = _hashids.DecodeSingle(id);
            await _propertyService.DeleteProperty(propertyId);

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
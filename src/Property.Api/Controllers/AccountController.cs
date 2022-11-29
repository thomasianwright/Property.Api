using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Property.Api.Contracts.Services;
using Property.Api.Core.Models;

namespace Property.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly IAccountService _accountService;

    public AccountController(IHashids hashids, IAccountService accountService)
    {
        _hashids = hashids;
        _accountService = accountService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAccount(string id)
    {
        try
        {
            var accountId = _hashids.DecodeSingle(id);

            var account = await _accountService.GetAccount(accountId);

            return Ok(account);
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountDto request)
    {
        try
        {
            var account = await _accountService.CreateAccount(request);

            return Ok(account);
        }
        catch
        {
            return BadRequest();
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountDto request, string id)
    {
        try
        {
            var accountId = _hashids.DecodeSingle(id);
            
            var account = await _accountService.UpdateAccount(accountId, request);

            return Ok(account);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAccount(string id)
    {
        try
        {
            var accountId = _hashids.DecodeSingle(id);

            await _accountService.DeleteAccount(accountId);

            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }
}
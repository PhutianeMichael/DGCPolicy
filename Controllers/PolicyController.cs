using System;
using Microsoft.AspNetCore.Mvc;
using DgcPolicyApi.Models;
using DgcPolicyApi.Services;

namespace DgcPolicyApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PolicyController : ControllerBase
{
    private readonly IPolicyRepository _context;

    public PolicyController(IPolicyRepository context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<List<Policy>> Get() => await _context.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Policy>> Get(string id){

        var policy = await _context.GetAsync(id);

        if(policy is null) return NotFound();

        return policy;
    } 

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Policy policy){
        await _context.CreateAsync(policy);
        return CreatedAtAction(nameof(Get), new {id = policy.Id}, policy);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> AddToPolicy(string id, Policy policy){
        
        var policyFromDb = await _context.GetAsync(id);

        if(policyFromDb is null) return NotFound();

        policy.Id = policyFromDb.Id;

        await _context.UpdateAsync(id,policy);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id){

        var policy = await _context.GetAsync(id);

        if(policy is null) return NotFound();

        await _context.RemoveAsync(id);
        
        return NoContent();
    }
}

using Crypfolio.Application.DTOs;
using Crypfolio.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crypfolio.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IAssetService _service;

    public AssetsController(IAssetService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assets = await _service.GetAllAsync();
        return Ok(assets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var asset = await _service.GetByIdAsync(id);
        if (asset == null)
            return NotFound();

        return Ok(asset);
    }

    [HttpGet("{symbol}")]
    public async Task<IActionResult> GetBySymbolTask(string symbol)
    {
        var asset = await _service.GetBySymbolAsync(symbol);
        if (asset == null)
            return NotFound();

        return Ok(asset);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateAssetDto dto)
    {
        await _service.AddAsync(dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string symbol, AssetDto dto)
    {
        var asset = await _service.GetBySymbolAsync(symbol);
        if (asset == null)
            return NotFound();

        asset.Name = dto.Name;
        asset.Balance = dto.Balance;
        asset.AverageBuyPrice = dto.AverageBuyPrice;

        await _service.UpdateAsync(asset);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string symbol)
    {
        await _service.DeleteAsync(symbol);
        return NoContent();
    }
}
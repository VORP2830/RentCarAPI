using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs;
using RentCar.Application.Interfaces;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;

namespace RentCar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentController : ControllerBase
{
    private readonly IRentService _rentService;
    public RentController(IRentService rentService)
    {
        _rentService = rentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RentFilter rentFilter)
    {
        try
        {
            var result = await _rentService.GetAll(rentFilter);
            return Ok(result);
        }
        catch (RentCarException ex)
        {
            var errorResponse = new
            {
                Message = ex.Message
            };
            return BadRequest(errorResponse);
        }
        catch (Exception)
        {
            var errorResponse = new
            {
                Message = "Erro ao tentar obter aluguel"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _rentService.GetById(id);
            return Ok(result);
        }
        catch (RentCarException ex)
        {
            var errorResponse = new
            {
                Message = ex.Message
            };
            return BadRequest(errorResponse);
        }
        catch (Exception)
        {
            var errorResponse = new
            {
                Message = "Erro ao tentar obter aluguel"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(RentDTO model)
    {
        try
        {
            var result = await _rentService.Create(model);
            return Ok(result);
        }
        catch (RentCarException ex)
        {
            var errorResponse = new
            {
                Message = ex.Message
            };
            return BadRequest(errorResponse);
        }
        catch (Exception)
        {
            var errorResponse = new
            {
                Message = "Erro ao tentar adicionar aluguel"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(RentDTO model)
    {
        try
        {
            var result = await _rentService.Update(model);
            return Ok(result);
        }
        catch (RentCarException ex)
        {
            var errorResponse = new
            {
                Message = ex.Message
            };
            return BadRequest(errorResponse);
        }
        catch (Exception)
        {
            var errorResponse = new
            {
                Message = "Erro ao tentar alterar aluguel"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _rentService.Delete(id);
            return Ok(result);
        }
        catch (RentCarException ex)
        {
            var errorResponse = new
            {
                Message = ex.Message
            };
            return BadRequest(errorResponse);
        }
        catch (Exception)
        {
            var errorResponse = new
            {
                Message = "Erro ao tentar deletar aluguel"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }
}

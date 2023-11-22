using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs;
using RentCar.Application.Interfaces;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;

namespace RentCar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    public CarController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] CarFilter carFilter)
    {
        try
        {
            var result = await _carService.GetAll(carFilter);
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
                Message = "Erro ao tentar obter carros"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _carService.GetById(id);
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
                Message = "Erro ao tentar obter carro"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarDTO model)
    {
        try
        {
            var result = await _carService.Create(model);
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
                Message = "Erro ao tentar adicionar carro"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(CarDTO model)
    {
        try
        {
            var result = await _carService.Update(model);
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
                Message = "Erro ao tentar alterar carro"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _carService.Delete(id);
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
                Message = "Erro ao tentar deletar carro"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }
    
}

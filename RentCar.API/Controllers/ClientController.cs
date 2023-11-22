using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs;
using RentCar.Application.Interfaces;
using RentCar.Domain.Exceptions;
using RentCar.Domain.Filter;

namespace RentCar.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ClientFilter clientFilter)
    {
        try
        {
            var result = await _clientService.GetAll(clientFilter);
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
                Message = "Erro ao tentar obter cliente"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _clientService.GetById(id);
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
                Message = "Erro ao tentar obter cliente"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ClientDTO model)
    {
        try
        {
            var result = await _clientService.Create(model);
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
                Message = "Erro ao tentar adicionar cliente"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(ClientDTO model)
    {
        try
        {
            var result = await _clientService.Update(model);
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
                Message = "Erro ao tentar alterar cliente"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var result = await _clientService.Delete(id);
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
                Message = "Erro ao tentar deletar cliente"
            };
            return this.StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
        }
    }
}

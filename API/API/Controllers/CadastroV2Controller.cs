using Application.Services.Interface;
using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/cadastro")]
[ApiVersion("2.0")]
public class CadastroV2Controller : ControllerBase
{
    private readonly IPessoaService _service;
    private readonly IMapper _mapper;

    public CadastroV2Controller(IPessoaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("pessoas")]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var pessoas = await _service.GetAllAsync();

            if (pessoas == null)
            {
                return NotFound("Nenhuma pessoa encontrada.");
            }

            var pessoasRetorno = _mapper.Map<IEnumerable<PessoaView>>(pessoas);
            return Ok(pessoasRetorno);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter pessoas: {ex.Message}");
        }
        
    }

    [HttpGet("pessoas/{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var pessoa = await _service.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound($"Pessoa com ID {id} não encontrada.");
            }

            var pessoaDto = _mapper.Map<PessoaView>(pessoa);
            return Ok(pessoaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter pessoa: {ex.Message}");
        }
    }

    [HttpPost("pessoas")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] PessoaDtoV2 pessoaDto)
    {
        try
        {
            var pessoa = _mapper.Map<Pessoa>(pessoaDto);

            await _service.CreateAsync(pessoa);

            var pessoaView = _mapper.Map<PessoaView>(pessoa);
            return CreatedAtAction(nameof(GetById), new { id = pessoaView.Id }, pessoa);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar pessoa: {ex.Message}");
        }
    }

    [HttpPut("pessoas/{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] PessoaUpdate pessoaUpdate)
    {
        try
        {

            if (id == 0 || pessoaUpdate == null || pessoaUpdate.Id != id)
            {
                return BadRequest("Dados da pessoa são inválidos.");
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaUpdate);
            await _service.UpdateAsync(pessoa);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar pessoa: {ex.Message}");
        }
    }

    [HttpDelete("pessoas/{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var pessoaBanco = await _service.GetByIdAsync(id);
            if (pessoaBanco == null)
            {
                return NotFound($"Pessoa com ID {id} não encontrada.");
            }
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar pessoa: {ex.Message}");
        }
    }
}

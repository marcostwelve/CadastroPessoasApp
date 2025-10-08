using Application.Services.Interface;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interface;

namespace Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    public async Task<List<Pessoa>> GetAllAsync()
    {
        var pessoas = await _pessoaRepository.GetAllAsync();
        return pessoas;
    }

    public async Task<Pessoa> GetByIdAsync(int id)
    {
        
        var pessoa = await _pessoaRepository.GetByIdAsync(id);

        if (pessoa == null)
        {
            return null;
        }

        return pessoa;
    }
    public async Task CreateAsync(Pessoa pessoa)
    {
        
        var cpfCadastrado = await _pessoaRepository.CpfExistsAsync(pessoa.Cpf);
        if (cpfCadastrado)
        {
            throw new Exception("CPF já cadastrado");
        }

        await _pessoaRepository.CreateAsync(pessoa);
        
    }

    public async Task UpdateAsync(Pessoa pessoa)
    {
        
        var pessoaBanco = await _pessoaRepository.GetByIdAsync(pessoa.Id);

        if (pessoaBanco == null)
        {
            throw new Exception($"Pessoa com o ID {pessoaBanco.Id} não encontrada");
        }

        var cpfExisteParaOutraPessoa = await _pessoaRepository.CpfExistsAsync(pessoa.Cpf, pessoa.Id);

        if (cpfExisteParaOutraPessoa)
        {
            throw new Exception("CPF já cadastrado");
        }

        else
        {
            pessoaBanco.Nome = pessoa.Nome;
            pessoaBanco.Email = pessoa.Email;
            pessoaBanco.Sexo = pessoa.Sexo;
            pessoaBanco.Naturalidade = pessoa.Naturalidade;
            pessoaBanco.Nacionalidade = pessoa.Nacionalidade;
            pessoaBanco.DataAtualizacao = DateTime.Now;

            await _pessoaRepository.UpdateAsync(pessoaBanco);
        }
    }

    public async Task DeleteAsync(int id)
    {
        await _pessoaRepository.DeleteAsync(id);
    }

    public async Task<bool> VerificarCpfCadastrado(int id)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(id);

        bool cpfCadastrado = await _pessoaRepository.CpfExistsAsync(pessoa.Cpf, id);

        return cpfCadastrado;

    }
}

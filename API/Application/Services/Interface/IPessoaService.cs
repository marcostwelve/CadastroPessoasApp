using Domain.Entities;

namespace Application.Services.Interface;

public interface IPessoaService
{
    Task<Pessoa> GetByIdAsync(int id);
    Task<List<Pessoa>> GetAllAsync();
    Task CreateAsync(Pessoa pessoa);
    Task UpdateAsync(Pessoa pessoa);
    Task DeleteAsync(int id);
    Task<bool> VerificarCpfCadastrado(int id);
}

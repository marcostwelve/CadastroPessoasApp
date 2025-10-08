using Domain.Entities;

namespace Infrastructure.Repositories.Interface;

public interface IPessoaRepository
{
    Task<List<Pessoa>> GetAllAsync();
    Task<Pessoa?> GetByIdAsync(int id);
    Task CreateAsync(Pessoa pessoa);
    Task UpdateAsync(Pessoa pessoa);
    Task DeleteAsync(int id);
    Task SaveAsync();

    Task<bool> CpfExistsAsync(string cpf, int? excludeId = null);
}

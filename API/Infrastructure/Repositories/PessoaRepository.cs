using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly ApplicationDbContext _context;

    public PessoaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pessoa>> GetAllAsync()
    {
        var pessoas = await _context.Pessoas.ToListAsync();
        return pessoas;
    }

    public async Task<Pessoa?> GetByIdAsync(int id)
    {
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
        return pessoa;
    }
    public async Task CreateAsync(Pessoa pessoa)
    {
        await _context.Pessoas.AddAsync(pessoa);
        await SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id);
        if (pessoa != null)
        {
            _context.Pessoas.Remove(pessoa);
            await SaveAsync();
        }
    }

    public async Task UpdateAsync(Pessoa pessoa)
    {
        _context.Pessoas.Update(pessoa);

        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CpfExistsAsync(string cpf, int? pessoaId = null)
    {
        var exists = await _context.Pessoas.AnyAsync(p => p.Cpf == cpf && (pessoaId == null || p.Id != pessoaId));
        return exists;
    }
}

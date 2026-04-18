using CEP.SmartWallet.Domain.Entities;
using CEP.SmartWallet.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CEP.SmartWallet.Infrastructure.Persistence;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken)
    {
        await _context.Transactions.AddAsync(transaction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Transaction>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Transactions
            .AsNoTracking()
            .ToListAsync(ct);
    }

}


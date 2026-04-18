using CEP.SmartWallet.Domain.Entities;

namespace CEP.SmartWallet.Application.Abstractions;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction, CancellationToken cancellationToken);   
    Task<List<Transaction>> GetAllAsync( CancellationToken cancellationToken);  
}
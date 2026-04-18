using CEP.SmartWallet.Application.Abstractions;
using MediatR;

namespace CEP.SmartWallet.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsHandler : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
{
    private readonly ITransactionRepository _repo;

     public GetTransactionsHandler (ITransactionRepository repo)
    {
        _repo = repo;
    }
    public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken ct)
    {
       var transactions =  await _repo.GetAllAsync(ct);

       return transactions.Select (t => new TransactionDto
       {
           Id = t.Id,
           FromAccount = t.FromAccount,
           ToAccount = t.ToAccount,
           Amount = t.Amount,
           Currency = t.Currency,
           RiskScore = t.RiskScore,
           Status = t.Status.ToString()
        }).ToList();
    }
}
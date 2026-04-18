using MediatR;

namespace CEP.SmartWallet.Application.Transactions.Queries.GetTransactions;

public record GetTransactionsQuery () : IRequest<List<TransactionDto>>;

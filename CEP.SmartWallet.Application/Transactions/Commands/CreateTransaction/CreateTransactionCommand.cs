using MediatR;
namespace CEP.SmartWallet.Application.Transactions.Commands.CreateTransaction;

public record CreateTransactionCommand(string FromAccount, string ToAccount, decimal Amount, string Currency) : IRequest<Guid>;
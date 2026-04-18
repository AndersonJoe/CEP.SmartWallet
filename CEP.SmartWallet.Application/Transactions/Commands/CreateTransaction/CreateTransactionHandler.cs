using MediatR;
using CEP.SmartWallet.Application.Abstractions;
using CEP.SmartWallet.Domain.Entities;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;

namespace CEP.SmartWallet.Application.Transactions.Commands.CreateTransaction;

public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, Guid>
{
    private readonly ITransactionRepository _repository;
    private readonly IRiskService _riskService;

    public CreateTransactionHandler(
        ITransactionRepository repository,
        IRiskService riskService)
    {
        _repository = repository;
        _riskService = riskService;
    }

    public async Task<Guid> Handle (CreateTransactionCommand request, CancellationToken ct)
    {
        var transaction = new Transaction (
            request.FromAccount,
            request.ToAccount,
            request. Amount,
            request.Currency
        );

        var score = _riskService.Calculate(transaction);

        transaction.ApplyRisk(score);

        await _repository.AddAsync(transaction,ct);
        return transaction.Id;
    }
}

using CEP.SmartWallet.Domain.Entities;
using CEP.SmartWallet.Application.Abstractions;

namespace CEP.SmartWallet.Infrastructure.Services;

public class RiskService : IRiskService
{
    public int Calculate(Transaction transaction)
    {
        int score = 0;

        if (transaction.Amount > 1000) score += 50;
        if (transaction.FromAccount == transaction.ToAccount) score += 30;
        if (transaction.Currency != "EUR")  score += 10;

        return score; 
    }
}
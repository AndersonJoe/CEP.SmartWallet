using CEP.SmartWallet.Domain.Entities;

namespace CEP.SmartWallet.Application.Abstractions;

public interface IRiskService
{
    int Calculate(Transaction transaction);
}
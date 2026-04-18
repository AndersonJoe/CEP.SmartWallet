using CEP.SmartWallet.Domain.Enums;

namespace CEP.SmartWallet.Domain.Entities;

public class Transaction
{
    public Guid Id{get; private set; }
    public string FromAccount{get; private set; }
    public string ToAccount{get; private set; }
    public decimal Amount{get; private set; }
    public String Currency{get; private set; }
    public DateTime Timestamp{get; private set; }
    public int RiskScore{get; private set; }
    public TransactionStatus Status{get; private set; }

    private Transaction() { }
    public Transaction(string fromAccount, string toAccount, decimal amount, string currency)
    {
        if (string.IsNullOrWhiteSpace(fromAccount)) throw new ArgumentException("From account cannot be empty.");
        if (string.IsNullOrWhiteSpace(toAccount)) throw new ArgumentException("To account cannot be empty.");
        if (amount <= 0) throw new ArgumentException("Amount must be greater than zero.");

        Id = Guid.NewGuid();
        FromAccount = fromAccount;
        ToAccount = toAccount;
        Amount = amount;
        Currency = currency;
        Timestamp = DateTime.UtcNow;
        RiskScore = 0; // Default risk score
        Status = TransactionStatus.Pending; // Default status
    }

    public void ApplyRisk(int score)
    {
        RiskScore = score;
        if (RiskScore >= 80)
        {
            Status = TransactionStatus.Blocked;
        }
        else        
        {
            Status = TransactionStatus.Completed; 
        }
    }
}
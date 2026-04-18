namespace CEP.SmartWallet.Application.Transactions.Queries.GetTransactions;

public class TransactionDto
{
    public Guid Id {get; set;}
    public string FromAccount {get; set;} ="";
    public string ToAccount{get;set;} ="";
    public decimal Amount {get;set;}
    public string Currency { get;set;} ="";
    public int RiskScore {get;set;}
    public string Status {get;set;} ="";
}
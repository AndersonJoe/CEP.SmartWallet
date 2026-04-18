import { useState } from 'react';

function App() {
  const [result, setResult] = useState("");
  const [transactions, setTransactions] = useState([]);

  const loadTransactions = async () => {
    const res = await fetch ("http://localhost:5000/api/transactions");
    const data = await res.json();
    setTransactions (data);
  }

  const createTransaction = async () => {
    try {
      const response = await fetch ("http://localhost:5000/api/transactions", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          fromAccount: "ACC1",
          toAccount: "ACC2",
          amount: 15000,
          currency: "EUR"
        }),
      });

      const data = await response.text();
      setResult(data);
    } catch (err) {
      console.error(err);
      setResult("Error calling API");
    }
  }

  return (
    <div style={{ padding: 20 }}>
      <h1>CEP SmartWallet</h1>

      <button onClick={loadTransactions}>
        Load Transactions
      </button>

      <ul>
        {transactions.map(t=>(
          <li key={t.id}>
            {t.fromAccount} → {t.toAccount} | {t.amount} {t.currency} | {t.status}
          </li>
        ))}
      </ul>

      <button onClick={createTransaction}>
        Create Transaction
      </button>

      <p>Result: {result}</p>
    </div>
  );
}

export default App;
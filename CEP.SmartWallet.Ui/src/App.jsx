import { useState } from 'react';

function App() {
  const [form, setForm] = useState({
    fromAccount: "",
    toAccount: "",
    amount: "",
    currency: ""
  });

  const [transactions, setTransactions] = useState([]);
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState("");
  const [error, setError] = useState("");
  
  const handleChange = (e) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value
    });
  }

  const createTransaction = async () => {
    setError("");
    setResult("");

    if (!form.fromAccount || !form.toAccount || !form.amount || !form.currency) {
      setError("All fields are required");
      return;
    }
    
    try {
      const response = await fetch ("http://localhost:5000/api/transactions", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          ...form,
          amount: Number(form.amount),
        }),
      });

      loadTransactions();

      const data = await response.text();
      setResult(data);

    } catch (err) {
      console.error(err);
      setError("Error calling API");
    }
  };

  
  const loadTransactions = async () => {
    setLoading(true);

    const res = await fetch ("http://localhost:5000/api/transactions");
    const data = await res.json();
    setTransactions (data);
    setLoading(false);
  }

  return (
    <div style={{ padding: 20 }}>
      <h1>CEP SmartWallet</h1>

      <h2>Create Transaction</h2>
      <input
        name = "fromAccount"
        placeholder = "From"
        value = {form.fromAccount}
        onChange={handleChange}
        />
        
      <input
        name = "toAccount"
        placeholder = "To"
        value = {form.toAccount}
        onChange={handleChange}
        />

      <input
        name = "amount"
        placeholder = "Amount"
        value = {form.amount}
        onChange={handleChange}
        />
      
      <input
        name = "currency"
        placeholder = "Currency"
        value = {form.currency}
        onChange={handleChange}
        />

       <button onClick={createTransaction}>
        Create Transaction
      </button>

      {error && <p style={{ color: "red" }}>{error}</p>}
      {result && <p style={{ color: "green" }}>Created ID: {result}</p>}

      <h2>Transactions</h2>
      <button onClick={loadTransactions}>
        Load 
      </button>

      {loading && <p>Loading ... </p>}

      <ul>
        {transactions.map(t=>(
          <li key={t.id}>
            {t.fromAccount} → {t.toAccount} | {t.amount} {t.currency} | Risk: {t.risk} | {t.status}
          </li>
        ))}
      </ul>

     

      <p>Result: {result}</p>
    </div>
  );
}

export default App;
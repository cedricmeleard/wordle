import "./App.css";
import Line from "./components/Line";
import React, { useState, useEffect } from "react";
import axios from "axios";

function App() {
  const [lines, setLines] = useState([]);
  const [word, setWord] = useState("");

  const userid = "fakeid";

  useEffect(() => {
    axios.post(`https://localhost:7296/Wordle?userId=${userid}`).then(res => setLines(res.data.lines));
  }, []);

  function AddLine() {
    axios.put(`https://localhost:7296/Wordle?userId=${userid}&word=${word}`).then(res => {
      setLines(res.data.lines);
      setWord("");
    });
  }

  return (
    <div className="App">
      <div className="App-fields">
        <input className="input" type="text" placeholder="Mot..." value={word} onChange={e => setWord(e.target.value)} />
        <button className="primary-btn" onClick={() => AddLine()}>
          Valider
        </button>
      </div>
      {lines.map((line, indx) => <Line key={indx} line={line} />)}
    </div>
  );
}

export default App;

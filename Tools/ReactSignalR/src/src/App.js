import React from 'react';
import './App.css';
import Messages from './components/Messages'
import Operations from './components/Operations'

function App() {
  return (
    <div className="App">
      <Operations></Operations>
      <Messages></Messages>
    </div>
  );
}

export default App;

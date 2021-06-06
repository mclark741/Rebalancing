import './custom.scss';
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

export { default as Navigation } from "./components/Navigation";
export { default as Footer } from "./components/Footer"; 
export { default as Home } from "./components/Home";
export { default as About } from "./components/About";
export { default as Contact } from "./components/Contact";
export { default as TransactionsView } from "./components/TransactionsView";
export { default as Portfolio } from "./components/Portfolio";
export { default as ListView } from "./components/ListView";
export { default as RebalanceView } from "./components/RebalanceView";

import React from "react";

class Transaction extends React.Component {
  render() {
    const formatDate = (val) => new Date(val).toLocaleDateString();

    const formatCurrency = (val) =>
      Math.abs(this.props.totalAmount).toLocaleString(navigator.language, {
        style: "currency",
        currency: "USD",
      });

    return (
      <tr>
        <td>{this.props.transactionId}</td>
        <td className="text-right">{this.props.quantity}</td>
        <td className="text-right">{formatCurrency(this.props.totalAmount)}</td>
        <td>{this.props.description}</td>
        <td>{formatDate(this.props.transactionDate)}</td>
        <td>{formatDate(this.props.settlementDate)}</td>
        <td>{this.props.symbol}</td>
        <td>{this.props.action === 1 ? "buy" : "sell"}</td>
      </tr>
    );
  }
}

export default Transaction;

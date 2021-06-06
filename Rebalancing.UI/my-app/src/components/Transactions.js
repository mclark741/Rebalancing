import React from "react";
import Transaction from "./Transaction.js";
import Table from "react-bootstrap/Table";

const Transactions = (props) => {
  const { repos } = props;
  if (!repos || repos.length === 0) return <p>No transactions, sorry</p>;

  return (
    <Table striped bordered hover responsive>
      <thead>
        <tr>
          <th>ID</th>
          <th>Quantity</th>
          <th>Total Amount</th>
          <th>Description</th>
          <th>Transaction Date</th>
          <th>Settlement Date</th>
          <th>Symbol</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        {repos.map((t, idx) => (
          <Transaction key={idx} {...t} />
        ))}
      </tbody>
    </Table>
  );
};

export default Transactions;

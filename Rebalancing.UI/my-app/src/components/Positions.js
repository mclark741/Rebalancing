import React from "react";
import Position from "./Position";
import Table from "react-bootstrap/Table";

const Positions = (props) => {
  const { positions } = props;
  if (!positions || positions.length === 0) return <p>No positions, sorry</p>;

  return (
    <Table striped bordered hover responsive>
      <thead>
        <tr>
          <th>Symbol</th>
          <th>Current Value</th>
          <th>% of Account</th>
          <th>Quantity</th>
          <th>Security</th>
        </tr>
      </thead>
      <tbody>
        {positions.map((p, idx) => (
          <Position key={idx} {...p} />
        ))}
      </tbody>
    </Table>
  );
};
export default Positions;

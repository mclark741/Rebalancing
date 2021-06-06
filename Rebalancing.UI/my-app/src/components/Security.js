import React from "react";
import Card from "react-bootstrap/Card";

class Security extends React.Component {
  // "securityId": 0,
  // "symbol": "FLPSX",
  // "description": "Fidelity Low-Priced Stock Fund",
  // "price": 53.07,
  // "lastUpdateDate": "2021-03-06T16:06:29.585039-06:00"

  render() {
    const formatDate = (val) =>
      `${new Date(val).toLocaleDateString()} ${new Date(
        val
      ).toLocaleTimeString()}`;

    return (
      <Card>
        <Card.Body>
          <Card.Title>{this.props.symbol}</Card.Title>
          <Card.Subtitle className="mb-2 text-muted">
            {this.props.description}
          </Card.Subtitle>
          <Card.Text>${this.props.price}</Card.Text>
        </Card.Body>
        <Card.Footer className="text-muted">
          Last updated: {formatDate(this.props.lastUpdateDate)}
        </Card.Footer>
      </Card>
    );
  }
}

export default Security;

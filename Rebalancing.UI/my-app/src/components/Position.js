import React from "react";
import Security from "./Security";
import { formatValue } from "react-currency-input-field";
import InputGroup from "react-bootstrap/InputGroup";
import FormControl from "react-bootstrap/FormControl";

const Position = (props) => {
  // {
  //   "quantity": 43.59,
  //   "currentValue": 2313.32,
  //   "percentOfAccountRounded": 20.619,
  //   "symbol": "FLPSX",
  //   "security": {
  //     "securityId": 0,
  //     "symbol": "FLPSX",
  //     "description": "Fidelity Low-Priced Stock Fund",
  //     "price": 53.07,
  //     "lastUpdateDate": "2021-03-06T16:06:29.585039-06:00"
  //   },
  //   "positionId": 0,
  //   "percentOfAccount": 0.20619038196542755
  // },
  const formattedCurrentValue = formatValue({
    value: props.currentValue.toString(),
    prefix: "$",
  });

  return (
    <tr>
      <td>{props.symbol}</td>
      <td className="text-right">{formattedCurrentValue}</td>
      <td className="text-right">
        {props.isEditMode === true ? (
          <InputGroup className="mb-3">
            <FormControl
              type="number"
              name={`${props.symbol}-percent`}
              data-symbol={props.symbol}
              value={props.percentOfAccountRounded}
              onChange={props.onChange}
            />
            <InputGroup.Append>
              <InputGroup.Text id="basic-addon2">%</InputGroup.Text>
            </InputGroup.Append>
          </InputGroup>
        ) : (
          `${props.percentOfAccountRounded}%`
        )}
      </td>
      <td className="text-right">{props.quantity}</td>
      <td><Security {...props.security} /></td>
    </tr>
  );
};

export default Position;

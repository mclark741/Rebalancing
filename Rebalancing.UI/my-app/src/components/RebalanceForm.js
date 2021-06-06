import React from "react";
import CurrencyInput from "react-currency-input-field";

const AdditionalInvestment = (props) => {
  const handleChange = (val, name) => props.onChange(val);

  return (
    <label>
      Additional Investment:
      <CurrencyInput
        id="input-additional-investment"
        name="additionalInvestment"
        decimalsLimit={2}
        defaultValue={props.additionalInvestment}
        onValueChange={handleChange}
        allowNegativeValue="false"
        decimalScale="2"
        prefix="$"
      />
    </label>
  );
};

export default AdditionalInvestment;

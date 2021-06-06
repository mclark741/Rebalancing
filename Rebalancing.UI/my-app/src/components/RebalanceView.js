import React, { useEffect, useState } from "react";
import withComponentLoading from "./withComponentLoading";
import AdditionalInvestment from "./RebalanceForm.js";
import Transactions from "./Transactions.js";
import Portfolio from "./Portfolio";

const RebalanceView = (props) => {
  const TransactionsLoading = withComponentLoading(Transactions);
  const [transactions, setTransactions] = useState({
    loading: false,
    repos: null,
  });

  // const PositionsLoading = withComponentLoading(Positions);
  // const [portfolioState, setPortfolioState] = useState({
  //   loading: false,
  //   positions: null,
  // });

  // useEffect(() => {
  //   setPortfolioState({ loading: true });
  //   const apiUrl = `https://localhost:65402/api/Portfolio`;
  //   fetch(apiUrl)
  //     .then((res) => res.json())
  //     .then((positions) => {
  //       positions.forEach((el) => {
  //         el.isEditMode = true;
  //         el.onChange = handlePositionChange;
  //       });
  //       setPortfolioState({ loading: false, positions: positions });
  //     });
  // }, [setPortfolioState]);

  // const handleChange = (e) => {
  //   let items = [...portfolioState.positions];
  //   const indexFound = items.findIndex((x) => {
  //     const symbol = e.target.getAttribute("data-symbol");
  //     return x.symbol === symbol;
  //   });
  //   if (indexFound > -1 && indexFound < items.length) {
  //     let newItem = {
  //       ...portfolioState.positions[indexFound],
  //       percentOfAccountRounded: e.target.value,
  //     };
  //     items[indexFound] = newItem;
  //     setPortfolioState({ loading: false, positions: items });
  //   }
  // };

  const [additionalInvestment, setAdditionalInvestment] = useState(0);
  const [desiredPositions, setDesiredPositions] = useState([
    { symbol: "FLPSX", percentOfAccount: 0.2 },
    { symbol: "FPADX", percentOfAccount: 0.15 },
    { symbol: "FRESX", percentOfAccount: 0.1 },
    { symbol: "FSPSX", percentOfAccount: 0.15 },
    { symbol: "FSSNX", percentOfAccount: 0.2 },
    { symbol: "TBCIX", percentOfAccount: 0.2 },
  ]);

  const handleSubmit = (event) => {
    event.preventDefault();
    setTransactions({ loading: true });

    // [
    //   {
    //     "positionId": 0,
    //     "percentOfAccount": 0,
    //     "symbol": "string"
    //   }
    // ]

    const apiUrl = `https://localhost:65402/api/Portfolio/${additionalInvestment}`;
    fetch(apiUrl, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(desiredPositions),
    })
      .then((res) => res.json())
      .then((data) => {
        console.log(data);
        setTransactions({ loading: false, repos: data });
      });
  };

  return (
    <div className="rebalance">
      <div className="container">
        <div className="row align-items-center my-5">
          <div className="col-lg-12">
            <form onSubmit={handleSubmit}>
              <AdditionalInvestment
                additionalInvestment={additionalInvestment}
                onChange={setAdditionalInvestment}
              />
              <input type="submit" value="Calculate" />

              <Portfolio />
              <TransactionsLoading
                isLoading={transactions.loading}
                repos={transactions.repos}
              />
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RebalanceView;

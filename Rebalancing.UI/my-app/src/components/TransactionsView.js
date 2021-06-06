import React, { useEffect, useState } from "react";
import withComponentLoading from "./withComponentLoading";
import Transactions from "./Transactions.js";
import TransactionsUpload from "./TransactionsUpload.js";

const TransactionsView = () => {
  const TransactionsLoading = withComponentLoading(Transactions);
  const [appState, setAppState] = useState({
    loading: false,
    repos: null,
  });

  useEffect(() => {
    setAppState({ loading: true });
    const apiUrl = `https://localhost:65402/api/Transaction`;
    fetch(apiUrl)
      .then((res) => res.json())
      .then((repos) => {
        setAppState({ loading: false, repos: repos });
      });
  }, [setAppState]);

  return (
    <div className="transactions">
      <div className="container">
        <div className="row align-items-center my-5">
          <div className="col-lg-7">
            <img
              className="img-fluid rounded mb-4 mb-lg-0"
              src="https://loremflickr.com/900/400"
              alt=""
            />
          </div>
          <div className="col-lg-5">
            <h1 className="font-weight-light">Transactions</h1>
            <p>
              Lorem Ipsum is simply dummy text of the printing and typesetting
              industry. Lorem Ipsum has been the industry's standard dummy text
              ever since the 1500s, when an unknown printer took a galley of
              type and scrambled it to make a type specimen book.
            </p>
          </div>
        </div>
        <div className="row align-items-center my-5">
          <TransactionsUpload />
        </div>
        <div className="row align-items-center my-5">
          <div className="col-lg-12">
            <TransactionsLoading
              isLoading={appState.loading}
              repos={appState.repos}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default TransactionsView;

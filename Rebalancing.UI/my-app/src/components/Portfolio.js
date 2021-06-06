import React, { useEffect, useState } from "react";
import withComponentLoading from "./withComponentLoading";
import Positions from "./Positions";

const Portfolio = (props) => {
  const PositionsLoading = withComponentLoading(Positions);
  const [portfolioState, setPortfolioState] = useState({
    loading: false,
    positions: null,
  });

  useEffect(() => {
    setPortfolioState({ loading: true });
    const apiUrl = `https://localhost:65402/api/Portfolio`;
    fetch(apiUrl)
      .then((res) => res.json())
      .then((positions) => {
        positions.forEach(el => {
            el.isEditMode = props.isEditMode;
            el.onChange = props.onChange;
        });
        setPortfolioState({ loading: false, positions: positions });
      });
  }, [setPortfolioState]);

  return (
    <div className="portfolio">
      <div className="container">
        <div className="row align-items-center my-5">
          <div className="col-lg-12">
            <PositionsLoading
              isLoading={portfolioState.loading}
              positions={portfolioState.positions}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Portfolio;

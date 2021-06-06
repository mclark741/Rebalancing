import React from "react";
import { Link, withRouter } from "react-router-dom";

const Navigation = (props) => {
  const links = [
    {
      url: "/home",
      name: "Home",
    },
    {
      url: "/about",
      name: "About",
    },
    {
      url: "/contact",
      name: "Home",
    },
    {
      url: "/portfolio",
      name: "Portfolio",
    },
    {
      url: "/rebalance",
      name: "Rebalance",
    },
    {
      url: "/transactions",
      name: "Transactions",
    },
    {
      url: "/list",
      name: "List",
    },
  ];

  return (
    <div className="navigation">
      <nav className="navbar navbar-expand navbar-dark bg-dark">
        <div className="container">
          <Link className="navbar-brand" to="/">
            React Multi-Page Website
          </Link>

          <div>
            <ul className="navbar-nav ml-auto">
              {links.map((value, index) => {
                return (
                  <li
                    key={index}
                    className={`nav-item  ${
                      props.location.pathname === value.url ? "active" : ""
                    }`}
                  >
                    <Link key={index} className="nav-link" to={value.url}>
                      {value.name}
                      {props.location.pathname === value.url ? (
                        <span className="sr-only">(current)</span>
                      ) : (
                        ""
                      )}
                    </Link>
                  </li>
                );
              })}
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
};

export default withRouter(Navigation);

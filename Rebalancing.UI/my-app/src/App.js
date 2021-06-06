import "./custom.scss";
import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import {
  Navigation,
  Footer,
  Home,
  About,
  Contact,
  TransactionsView,
  ListView,
  Portfolio,
  RebalanceView
} from ".";

// This site has 3 pages, all of which are rendered
// dynamically in the browser (not server rendered).
//
// Although the page does not ever refresh, notice how
// React Router keeps the URL up to date as you navigate
// through the site. This preserves the browser history,
// making sure things like the back button and bookmarks
// work properly.

export default function App() {
  return (
    <div className="App">
      <Router>
        <Navigation />
        <Switch>
          <Route path="/" exact component={() => <Home />} />
          <Route path="/about" exact component={() => <About />} />
          <Route path="/contact" exact component={() => <Contact />} />
          <Route
            path="/transactions"
            exact
            component={() => <TransactionsView />}
          />
          <Route path="/portfolio" exact component={() => <Portfolio />} />
          <Route path="/list" exact component={() => <ListView />} />
          <Route path="/rebalance" exact component={() => <RebalanceView />} />
        </Switch>
        <Footer />
      </Router>
    </div>
  );
}

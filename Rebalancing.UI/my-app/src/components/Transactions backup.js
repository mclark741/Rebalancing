import React from "react";
import Transaction from "./Transaction.js";
import Table from "react-bootstrap/Table";

class Transactions extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      transactions: [
        {
          transactionId: 1,
          quantity: 0.117,
          totalAmount: -19.19,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2021-01-12T00:00:00",
          settlementDate: "2021-01-13T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 2,
          quantity: 1.418,
          totalAmount: -64.17,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 3,
          quantity: 6.378,
          totalAmount: -72.9,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 4,
          quantity: -0.281,
          totalAmount: 5.9,
          description:
            "YOU SOLD EXCHANGE FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "FSSNX",
          action: 2,
        },
        {
          transactionId: 5,
          quantity: 0.784,
          totalAmount: -31.39,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 6,
          quantity: 0.147,
          totalAmount: -5.9,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 7,
          quantity: 2.036,
          totalAmount: -84.48,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "FSPSX",
          action: 1,
        },
        {
          transactionId: 8,
          quantity: 0.597,
          totalAmount: -91.19,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-09-14T00:00:00",
          settlementDate: "2020-09-15T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 9,
          quantity: 3.997,
          totalAmount: -174.31,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-09-14T00:00:00",
          settlementDate: "2020-09-15T00:00:00",
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 10,
          quantity: 4.318,
          totalAmount: -47.76,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-09-14T00:00:00",
          settlementDate: "2020-09-15T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 11,
          quantity: 5.606,
          totalAmount: -109.55,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-09-14T00:00:00",
          settlementDate: "2020-09-15T00:00:00",
          symbol: "FSSNX",
          action: 1,
        },
        {
          transactionId: 12,
          quantity: 2.137,
          totalAmount: -83.54,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-09-14T00:00:00",
          settlementDate: "2020-09-15T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 13,
          quantity: 0.21,
          totalAmount: -34.55,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-10-12T00:00:00",
          settlementDate: "2020-10-13T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 14,
          quantity: 1.67,
          totalAmount: -68.63,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-09-14T00:00:00",
          settlementDate: "2020-09-15T00:00:00",
          symbol: "FSPSX",
          action: 1,
        },
        {
          transactionId: 15,
          quantity: 0.44,
          totalAmount: -17.14,
          description:
            "REINVESTMENT FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-09-04T00:00:00",
          settlementDate: null,
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 16,
          quantity: 9.144,
          totalAmount: -1376.62,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-08-04T00:00:00",
          settlementDate: "2020-08-05T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 17,
          quantity: 30.332,
          totalAmount: -1374.96,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-08-04T00:00:00",
          settlementDate: "2020-08-05T00:00:00",
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 18,
          quantity: 94.841,
          totalAmount: -1034.72,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-08-04T00:00:00",
          settlementDate: "2020-08-05T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 19,
          quantity: 71.463,
          totalAmount: -1375.67,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-08-04T00:00:00",
          settlementDate: "2020-08-05T00:00:00",
          symbol: "FSSNX",
          action: 1,
        },
        {
          transactionId: 20,
          quantity: 17.093,
          totalAmount: -684.9,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-08-04T00:00:00",
          settlementDate: "2020-08-05T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 21,
          quantity: 25.811,
          totalAmount: -1031.92,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-08-04T00:00:00",
          settlementDate: "2020-08-05T00:00:00",
          symbol: "FSPSX",
          action: 1,
        },
        {
          transactionId: 22,
          quantity: 1.517,
          totalAmount: -57.48,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-07-21T00:00:00",
          settlementDate: "2020-07-22T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 23,
          quantity: 0.772,
          totalAmount: -115,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-07-20T00:00:00",
          settlementDate: "2020-07-21T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 24,
          quantity: 2.606,
          totalAmount: -115,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-07-20T00:00:00",
          settlementDate: "2020-07-21T00:00:00",
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 25,
          quantity: 8.061,
          totalAmount: -86.25,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-07-20T00:00:00",
          settlementDate: "2020-07-21T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 26,
          quantity: 2.428,
          totalAmount: -104.15,
          description: "REINVESTMENT FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-09-11T00:00:00",
          settlementDate: null,
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 27,
          quantity: 0.682,
          totalAmount: -29,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-10T00:00:00",
          symbol: "FSPSX",
          action: 1,
        },
        {
          transactionId: 28,
          quantity: 1.835,
          totalAmount: -70.91,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-10T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 29,
          quantity: 0.792,
          totalAmount: -17.18,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-09T00:00:00",
          symbol: "FSSNX",
          action: 1,
        },
        {
          transactionId: 30,
          quantity: 0.094,
          totalAmount: -15.5,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2021-01-12T00:00:00",
          settlementDate: "2021-01-13T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 31,
          quantity: 0.428,
          totalAmount: -70.75,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2021-01-11T00:00:00",
          settlementDate: "2021-01-12T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 32,
          quantity: 1.401,
          totalAmount: -70.78,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2021-01-11T00:00:00",
          settlementDate: "2021-01-12T00:00:00",
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 33,
          quantity: 4.661,
          totalAmount: -62.08,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2021-01-11T00:00:00",
          settlementDate: "2021-01-12T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 34,
          quantity: -0.726,
          totalAmount: 19.19,
          description:
            "YOU SOLD TO BUY TBCIX FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2021-01-11T00:00:00",
          settlementDate: "2021-01-12T00:00:00",
          symbol: "FSSNX",
          action: 2,
        },
        {
          transactionId: 35,
          quantity: 2.333,
          totalAmount: -88.08,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2021-01-11T00:00:00",
          settlementDate: "2021-01-12T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 36,
          quantity: -0.333,
          totalAmount: 15.5,
          description:
            "YOU SOLD TO BUY TBCIX FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2021-01-11T00:00:00",
          settlementDate: "2021-01-12T00:00:00",
          symbol: "FSPSX",
          action: 2,
        },
        {
          transactionId: 37,
          quantity: 0.134,
          totalAmount: -21.7,
          description:
            "REINVESTMENT as of 12/14/2020 T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-12-15T00:00:00",
          settlementDate: null,
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 38,
          quantity: 0.575,
          totalAmount: -93.58,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-12-15T00:00:00",
          settlementDate: "2020-12-16T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 39,
          quantity: 0.044,
          totalAmount: -7.15,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-12-15T00:00:00",
          settlementDate: "2020-12-16T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 40,
          quantity: 0.704,
          totalAmount: -113.71,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-12-14T00:00:00",
          settlementDate: "2020-12-15T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 41,
          quantity: -0.151,
          totalAmount: 7.15,
          description:
            "YOU SOLD TO BUY TBCIX FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-12-14T00:00:00",
          settlementDate: "2020-12-15T00:00:00",
          symbol: "FLPSX",
          action: 2,
        },
        {
          transactionId: 42,
          quantity: 4.818,
          totalAmount: -59.36,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-12-14T00:00:00",
          settlementDate: "2020-12-15T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 43,
          quantity: -3.869,
          totalAmount: 93.58,
          description:
            "YOU SOLD TO BUY TBCIX FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-12-14T00:00:00",
          settlementDate: "2020-12-15T00:00:00",
          symbol: "FSSNX",
          action: 2,
        },
        {
          transactionId: 44,
          quantity: 2,
          totalAmount: -76.33,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-12-14T00:00:00",
          settlementDate: "2020-12-15T00:00:00",
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 45,
          quantity: 0.857,
          totalAmount: -38.21,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-12-14T00:00:00",
          settlementDate: "2020-12-15T00:00:00",
          symbol: "FSPSX",
          action: 1,
        },
        {
          transactionId: 46,
          quantity: 1.927,
          totalAmount: -91.22,
          description: "REINVESTMENT FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-12-11T00:00:00",
          settlementDate: null,
          symbol: "FLPSX",
          action: 1,
        },
        {
          transactionId: 47,
          quantity: 0.697,
          totalAmount: -16.84,
          description:
            "REINVESTMENT FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-12-11T00:00:00",
          settlementDate: null,
          symbol: "FSSNX",
          action: 1,
        },
        {
          transactionId: 48,
          quantity: 0.532,
          totalAmount: -20.43,
          description:
            "REINVESTMENT FIDELITY REAL ESTATE INVESTMENT (FRESX) (Cash)",
          transactionDate: "2020-12-11T00:00:00",
          settlementDate: null,
          symbol: "FRESX",
          action: 1,
        },
        {
          transactionId: 49,
          quantity: 0.61,
          totalAmount: -27.11,
          description:
            "REINVESTMENT FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-12-11T00:00:00",
          settlementDate: null,
          symbol: "FSPSX",
          action: 1,
        },
        {
          transactionId: 50,
          quantity: 1.828,
          totalAmount: -22.57,
          description:
            "REINVESTMENT FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-12-04T00:00:00",
          settlementDate: null,
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 51,
          quantity: 0.35,
          totalAmount: -57,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER T ROWE PRICE BLUE CHIP GRWTH CL I (TBCIX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-10T00:00:00",
          symbol: "TBCIX",
          action: 1,
        },
        {
          transactionId: 52,
          quantity: -0.368,
          totalAmount: 17.18,
          description:
            "YOU SOLD EXCHANGE FIDELITY LOW PRICED STOCK (FLPSX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-09T00:00:00",
          symbol: "FLPSX",
          action: 2,
        },
        {
          transactionId: 53,
          quantity: 4.567,
          totalAmount: -54.39,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY EMERGING MARKETS INDEX FUND (FPADX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-10T00:00:00",
          symbol: "FPADX",
          action: 1,
        },
        {
          transactionId: 54,
          quantity: 3.511,
          totalAmount: -76.19,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-11-09T00:00:00",
          settlementDate: "2020-11-10T00:00:00",
          symbol: "FSSNX",
          action: 1,
        },
        {
          transactionId: 55,
          quantity: 6.176,
          totalAmount: -115,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY SMALL CAP INDEX FUND (FSSNX) (Cash)",
          transactionDate: "2020-07-20T00:00:00",
          settlementDate: "2020-07-21T00:00:00",
          symbol: "FSSNX",
          action: 1,
        },
        {
          transactionId: 56,
          quantity: 2.15,
          totalAmount: -86.25,
          description:
            "YOU BOUGHT PROSPECTUS UNDER SEPARATE COVER FIDELITY INTERNATL INDEX FUND (FSPSX) (Cash)",
          transactionDate: "2020-07-20T00:00:00",
          settlementDate: "2020-07-21T00:00:00",
          symbol: "FSPSX",
          action: 1,
        },
      ],
    };
  }
  render() {
    return (
      <div className="contact">
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
                industry. Lorem Ipsum has been the industry's standard dummy
                text ever since the 1500s, when an unknown printer took a galley
                of type and scrambled it to make a type specimen book.
              </p>
            </div>
          </div>
          <div className="row align-items-center my-5">
            <div className="col-lg-12">
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
                  {this.state.transactions.map((transaction, idx) => (
                    <Transaction
                      key={transaction.transactionId}
                      {...transaction}
                    />
                  ))}
                </tbody>
              </Table>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Transactions;

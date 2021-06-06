using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rebalancing.Integrations
{
    public class Quarterly
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("actual")]
        public double Actual { get; set; }

        [JsonProperty("estimate")]
        public double Estimate { get; set; }

        [JsonProperty("revenue")]
        public object Revenue { get; set; }

        [JsonProperty("earnings")]
        public object Earnings { get; set; }
    }

    public class EarningsChart
    {
        [JsonProperty("quarterly")]
        public List<Quarterly> Quarterly { get; set; }

        [JsonProperty("currentQuarterEstimate")]
        public double CurrentQuarterEstimate { get; set; }

        [JsonProperty("currentQuarterEstimateDate")]
        public string CurrentQuarterEstimateDate { get; set; }

        [JsonProperty("currentQuarterEstimateYear")]
        public int CurrentQuarterEstimateYear { get; set; }

        [JsonProperty("earningsDate")]
        public List<int> EarningsDate { get; set; }
    }

    public class Yearly
    {
        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("revenue")]
        public object Revenue { get; set; }

        [JsonProperty("earnings")]
        public object Earnings { get; set; }
    }

    public class FinancialsChart
    {
        [JsonProperty("yearly")]
        public List<Yearly> Yearly { get; set; }

        [JsonProperty("quarterly")]
        public List<Quarterly> Quarterly { get; set; }
    }

    public class Earnings
    {
        [JsonProperty("maxAge")]
        public int MaxAge { get; set; }

        [JsonProperty("earningsChart")]
        public EarningsChart EarningsChart { get; set; }

        [JsonProperty("financialsChart")]
        public FinancialsChart FinancialsChart { get; set; }

        [JsonProperty("financialCurrency")]
        public string FinancialCurrency { get; set; }
    }

    public class QuoteSummary
    {
        [JsonProperty("earnings")]
        public Earnings Earnings { get; set; }
    }

    public class PageViews
    {
        [JsonProperty("midTermTrend")]
        public string MidTermTrend { get; set; }

        [JsonProperty("longTermTrend")]
        public string LongTermTrend { get; set; }

        [JsonProperty("shortTermTrend")]
        public string ShortTermTrend { get; set; }
    }

    public class Result
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("quoteSourceName")]
        public string QuoteSourceName { get; set; }

        [JsonProperty("triggerable")]
        public bool Triggerable { get; set; }

        [JsonProperty("quoteSummary")]
        public QuoteSummary QuoteSummary { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("components")]
        public List<string> Components { get; set; }

        [JsonProperty("firstTradeDateMilliseconds")]
        public object FirstTradeDateMilliseconds { get; set; }

        [JsonProperty("priceHint")]
        public int PriceHint { get; set; }

        [JsonProperty("totalCash")]
        public double TotalCash { get; set; }

        [JsonProperty("floatShares")]
        public long FloatShares { get; set; }

        [JsonProperty("ebitda")]
        public object Ebitda { get; set; }

        [JsonProperty("shortRatio")]
        public double ShortRatio { get; set; }

        [JsonProperty("preMarketChange")]
        public double PreMarketChange { get; set; }

        [JsonProperty("preMarketChangePercent")]
        public double PreMarketChangePercent { get; set; }

        [JsonProperty("preMarketTime")]
        public int PreMarketTime { get; set; }

        [JsonProperty("targetPriceHigh")]
        public double TargetPriceHigh { get; set; }

        [JsonProperty("targetPriceLow")]
        public double TargetPriceLow { get; set; }

        [JsonProperty("targetPriceMean")]
        public double TargetPriceMean { get; set; }

        [JsonProperty("targetPriceMedian")]
        public double TargetPriceMedian { get; set; }

        [JsonProperty("preMarketPrice")]
        public double PreMarketPrice { get; set; }

        [JsonProperty("heldPercentInsiders")]
        public double HeldPercentInsiders { get; set; }

        [JsonProperty("heldPercentInstitutions")]
        public double HeldPercentInstitutions { get; set; }

        [JsonProperty("postMarketChangePercent")]
        public double PostMarketChangePercent { get; set; }

        [JsonProperty("postMarketTime")]
        public int PostMarketTime { get; set; }

        [JsonProperty("postMarketPrice")]
        public double PostMarketPrice { get; set; }

        [JsonProperty("postMarketChange")]
        public double PostMarketChange { get; set; }

        [JsonProperty("regularMarketChange")]
        public double RegularMarketChange { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public double RegularMarketChangePercent { get; set; }

        [JsonProperty("regularMarketTime")]
        public int RegularMarketTime { get; set; }

        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public double RegularMarketDayHigh { get; set; }

        [JsonProperty("regularMarketDayRange")]
        public string RegularMarketDayRange { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public double RegularMarketDayLow { get; set; }

        [JsonProperty("regularMarketVolume")]
        public int RegularMarketVolume { get; set; }

        [JsonProperty("sharesShort")]
        public int SharesShort { get; set; }

        [JsonProperty("sharesShortPrevMonth")]
        public int SharesShortPrevMonth { get; set; }

        [JsonProperty("shortPercentFloat")]
        public double ShortPercentFloat { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public double RegularMarketPreviousClose { get; set; }

        [JsonProperty("bid")]
        public double Bid { get; set; }

        [JsonProperty("ask")]
        public double Ask { get; set; }

        [JsonProperty("bidSize")]
        public int BidSize { get; set; }

        [JsonProperty("askSize")]
        public int AskSize { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("messageBoardId")]
        public string MessageBoardId { get; set; }

        [JsonProperty("fullExchangeName")]
        public string FullExchangeName { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("regularMarketOpen")]
        public double RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public int AverageDailyVolume3Month { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public int AverageDailyVolume10Day { get; set; }

        [JsonProperty("beta")]
        public double Beta { get; set; }

        [JsonProperty("fiftyTwoWeekLowChange")]
        public double FiftyTwoWeekLowChange { get; set; }

        [JsonProperty("fiftyTwoWeekLowChangePercent")]
        public double FiftyTwoWeekLowChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekRange")]
        public string FiftyTwoWeekRange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChange")]
        public double FiftyTwoWeekHighChange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChangePercent")]
        public double FiftyTwoWeekHighChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }

        [JsonProperty("exDividendDate")]
        public int ExDividendDate { get; set; }

        [JsonProperty("earningsTimestamp")]
        public int EarningsTimestamp { get; set; }

        [JsonProperty("earningsTimestampStart")]
        public int EarningsTimestampStart { get; set; }

        [JsonProperty("earningsTimestampEnd")]
        public int EarningsTimestampEnd { get; set; }

        [JsonProperty("trailingPE")]
        public double TrailingPE { get; set; }

        [JsonProperty("pegRatio")]
        public double PegRatio { get; set; }

        [JsonProperty("dividendsPerShare")]
        public double DividendsPerShare { get; set; }

        [JsonProperty("revenue")]
        public double Revenue { get; set; }

        [JsonProperty("priceToSales")]
        public double PriceToSales { get; set; }

        [JsonProperty("marketState")]
        public string MarketState { get; set; }

        [JsonProperty("epsTrailingTwelveMonths")]
        public double EpsTrailingTwelveMonths { get; set; }

        [JsonProperty("epsForward")]
        public double EpsForward { get; set; }

        [JsonProperty("epsCurrentYear")]
        public double EpsCurrentYear { get; set; }

        [JsonProperty("epsNextQuarter")]
        public double EpsNextQuarter { get; set; }

        [JsonProperty("priceEpsCurrentYear")]
        public double PriceEpsCurrentYear { get; set; }

        [JsonProperty("priceEpsNextQuarter")]
        public double PriceEpsNextQuarter { get; set; }

        [JsonProperty("sharesOutstanding")]
        public long SharesOutstanding { get; set; }

        [JsonProperty("bookValue")]
        public double BookValue { get; set; }

        [JsonProperty("fiftyDayAverage")]
        public double FiftyDayAverage { get; set; }

        [JsonProperty("fiftyDayAverageChange")]
        public double FiftyDayAverageChange { get; set; }

        [JsonProperty("fiftyDayAverageChangePercent")]
        public double FiftyDayAverageChangePercent { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public double TwoHundredDayAverage { get; set; }

        [JsonProperty("twoHundredDayAverageChange")]
        public double TwoHundredDayAverageChange { get; set; }

        [JsonProperty("twoHundredDayAverageChangePercent")]
        public double TwoHundredDayAverageChangePercent { get; set; }

        [JsonProperty("marketCap")]
        public object MarketCap { get; set; }

        [JsonProperty("forwardPE")]
        public double ForwardPE { get; set; }

        [JsonProperty("priceToBook")]
        public double PriceToBook { get; set; }

        [JsonProperty("sourceInterval")]
        public int SourceInterval { get; set; }

        [JsonProperty("exchangeDataDelayedBy")]
        public int ExchangeDataDelayedBy { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName { get; set; }

        [JsonProperty("pageViews")]
        public PageViews PageViews { get; set; }

        [JsonProperty("gmtOffSetMilliseconds")]
        public int GmtOffSetMilliseconds { get; set; }

        [JsonProperty("esgPopulated")]
        public bool EsgPopulated { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("dividendDate")]
        public int? DividendDate { get; set; }

        [JsonProperty("trailingAnnualDividendRate")]
        public double? TrailingAnnualDividendRate { get; set; }

        [JsonProperty("dividendRate")]
        public double? DividendRate { get; set; }

        [JsonProperty("trailingAnnualDividendYield")]
        public double? TrailingAnnualDividendYield { get; set; }

        [JsonProperty("dividendYield")]
        public double? DividendYield { get; set; }
    }

    public class QuoteResponse
    {
        [JsonProperty("result")]
        public List<Result> Result { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
    }

    public class RapidApiYahooFinanceResponse
    {
        [JsonProperty("quoteResponse")]
        public QuoteResponse QuoteResponse { get; set; }
    }
}


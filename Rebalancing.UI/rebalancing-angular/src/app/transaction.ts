export interface Transaction {
  transactionId: number;
  quantity: number;
  totalAmount: number;
  description: string | null;
  transactionDate: Date;
  settlementDate: Date | null;
  symbol: string;
  action: number;
}

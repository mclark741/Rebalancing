import { RebalancePosition } from "./rebalance-position";

export interface RebalanceModel {
  desiredPositions: RebalancePosition[];
  additionalInvestment: number;
}

import { Security } from './security';

export interface Position {
  quantity: number;
  currentValue: number;
  percentOfAccountRounded: number;
  symbol: string;
  security: Security;
  positionId: number;
  percentOfAccount: number;
}

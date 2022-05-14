export interface IAppConfig {
  env: {
    name: string;
  };
  apiUrl: string;
  appInsights: {
    instrumentationKey: string;
  };
  modelPortfolioDesiredPositions: [];
  logging: {
    console: boolean;
    appInsights: boolean;
  };
  aad: {
    requireAuth: boolean;
    tenant: string;
    clientId: string;
  };
  apiServer: {
    metadata: string;
    rules: string;
  };
  helpDocumentation: string;
}

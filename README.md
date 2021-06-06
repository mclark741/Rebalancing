# Rebalancing

I needed a way to calculate how to rebalance my HSA investment portfolio back to my preferred allocations. This app has all the guts for calculations in the WebAPI. There's a create-react-app for the front end.

## React
I have no idea what I'm doing with react so that is very much a work in progress. I got to the point where I wanted to create the form for rebalances but then ran into an issue with a stale react state and got frustrated. It works well enough for now, so I'll keep using it as is and make improvements over time or rewrite the front end in some other technology to play around.

## Integrations
I wanted to integrate the backend with Plaid so I could connect directly with Fidelity but the Fidelity two-factor auth is not compatible with Plaid. Instead, I opted for a file import and wrote a parser to import the csv transaction files from Fidelity.

This also has an integration with Yahoo Finance from Rapid API to get stock quotes. 

## Database
This uses a MySQL community database in AWS free tier as the database provider for now. I wanted a way to host the database without having to install sql on my mac. EF Core doesn't seem to play nicely with MySQL so I had to use an older version of EF Core and mysql connectors that all finally worked together.

## Web API
The service is a regular .NET Core Web API with swagger documentation. I would still like to separate out more logic from the controllers and break them into services. This uses the secrets manager in .NET for the database connection string and the Yahoo Finance API key from Rapid API.

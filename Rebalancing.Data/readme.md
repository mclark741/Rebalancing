https://stackoverflow.com/questions/38705694/add-migration-with-different-assembly


```
dotnet ef --startup-project ../Rebalancing.Web/ migrations add InitialLocalhost
```

```
dotnet ef --startup-project ../Rebalancing.Web/ database update
```
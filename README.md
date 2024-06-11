## Repro Steps

1. `dotnet publish --os linux --arch x64 /t:PublishContainer`
2. `docker run -it -rm --port 8080:8080 --env APPLICATIONINSIGHTS_CONNECTION_STRING=$DEV_APP_INSIGHTS azuremonitorloggingscopes`
3. `curl http://localhost:8080/`

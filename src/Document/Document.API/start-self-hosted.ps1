dapr run `
    --app-id documents `
    --app-port 6008 `
    --dapr-http-port 8085 `
    --components-path ../../dapr/components `
    dotnet run
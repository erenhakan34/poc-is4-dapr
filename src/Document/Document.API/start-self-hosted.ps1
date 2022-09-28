dapr run `
    --app-id documents `
    --app-port 5008 `
    --dapr-http-port 7085 `
    --components-path ../../dapr/components `
    dotnet run
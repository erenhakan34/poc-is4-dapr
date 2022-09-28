dapr run `
    --app-id notification `
    --app-port 5010 `
    --dapr-http-port 7095 `
    --components-path ../../dapr/components `
    dotnet run
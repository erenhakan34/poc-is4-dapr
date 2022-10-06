dapr run `
    --app-id notification `
    --app-port 5010 `
    --dapr-http-port 6095 `
    --components-path ../../dapr/components `
    --config ../../dapr/configuration/daprpoc-config.yaml `
    dotnet run
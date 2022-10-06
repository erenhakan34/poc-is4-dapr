dapr run `
    --app-id documents `
    --app-protocol grpc `
    --app-port 6008 `
    --components-path ../../dapr/components `
    --config ../../dapr/configuration/daprpoc-config.yaml `
    dotnet run
dapr run `
    --app-id auth `
    --app-protocol grpc `
    --app-port 5008 `
    --components-path ../../dapr/components `
    --config ../../dapr/configuration/daprpoc-config.yaml `
    dotnet run
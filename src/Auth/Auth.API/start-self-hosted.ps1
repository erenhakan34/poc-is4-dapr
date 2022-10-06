dapr run `
    --app-id auth `
    --app-port 5008 `
    --dapr-http-port 7095 `
    --components-path ../../dapr/components `
    --config ../../dapr/configuration/daprpoc-config.yaml `
    dotnet run
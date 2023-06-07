# SpaceTradersClient
A client for the spacetraders.io v2 API

To begin working with this, you need to create a file in the root called `appconfig.local.json` with this content:

```json
{
  "settings": {
    "apiKey": "THIS IS THE JWT FROM SPACE TRADERS AVAILABLE AT https://docs.spacetraders.io/quickstart/new-game"
  }
}
```

## Generated API client

SpaceTraders.io documents its API via OpenAPI.  It is possible to generate stub API access code via OpenAPI's command-line tool.  This is conveniently packaged in a Docker image.  With Podman installed and active, run the following at a PowerShell command prompt from the SpaceTradersOpenApi directory to generate the API client code.  (Should work with Docker as well.)

https://openapi-generator.tech/docs/generators/csharp-netcore

```powershell
PS> podman run --rm -v "$pwd`:/local" openapitools/openapi-generator-cli generate -i https://raw.githubusercontent.com/SpaceTradersAPI/api-docs/main/reference/SpaceTraders.json -g csharp-netcore -o /local/gen --additional-properties=library=generichost,useCompareNetObjects=true,disallowAdditionalPropertiesIfNotPresent=false,targetFramework=net7.0,nullableReferenceTypes=true,packageName=SpaceTradersApiClient,validatable=false,packageGuid=1F31615E-14B1-4F92-B9B0-D94951F3E7D9,disallowAdditionalPropertiesIfNotPresent=false,netCoreProjectFile=true,useDateTimeOffset=true
```


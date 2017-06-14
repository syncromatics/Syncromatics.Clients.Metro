# Los Angeles Metropolitan Transportation Authority (Metro) API Client

This is a .NET library to interact with the [Metro API](http://developer.metro.net/).

## Building

This library is built using a Docker image. First build the image:

```
docker build -f Dockerfile.build -t syncromatics/dotnet .
```

Next, run `build.sh` in the Docker container:

```
docker run --rm -it -v "C:/source/metro-api-client:/app" -w /app syncromatics/dotnet-build ./build.sh
```

(Note, change `C:/source/metro-api-client` to whatever the full path to the root of the repo is on your computer.)

## Testing

To run the tests:

```
docker run --rm -it -v "C:/source/metro-api-client:/app" -w /app/tests/Syncromatics.Clients.Metro.Api.Tests syncromatics/dotnet-build dotnet xunit
```

## Contributing

Please see [CONTRIBUTING](CONTRIBUTING.md) for our guide to contributing and code of conduct.

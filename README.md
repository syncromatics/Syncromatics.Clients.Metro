# Los Angeles Metropolitan Transportation Authority (Metro) API Client

A .NET library to interact with the [Metro API](http://developer.metro.net/).

## Quickstart

Add the [Syncromatics.Clients.Metro.Api](https://www.nuget.org/packages/Syncromatics.Clients.Metro.Api/) NuGet package to your project in the manner applicable to you.

This package exposes .NET wrappers for select Metro APIs through the `IMetroApiClient` interface and its default implementation `MetroApiClient`.

### Initializing a client

By default, this client will use Metro's production API URL.

```csharp
using Syncromatics.Clients.Metro.Api;

IMetroApiClient client = new MetroApiClient();
```

If you need to point to a different URL, give the client a `ClientSettings` instance.  After that, the rest is easy:

```csharp
using Syncromatics.Clients.Metro.Api;

var clientSettings = new ClientSettings { ServerRootUrl = "http://example.com/" };
IMetroApiClient client = new MetroApiClient(clientSettings);
```

### Retrieving arrival times

With a known Node or Stop ID, you can retrieve upcoming arrivals at that particular Node or Stop:

```csharp
string nodeId = "11130-MB";
var nodeArrivals = client.GetNodeTimes(nodeId);

string stopId = "30002";
var stopArrivals = client.GetStopTimes(stopId);
```

### Stop and route discovery

The client provides a handful of methods for discovering stops and routes.

#### Retrieve Stops by node

`GetStopsByNodeId(string nodeId, string corner = null)` will return each stop and route associated with the Node ID.  You can specify `nodeId` as either a plane Node ID (e.g. `12345`) or a Node ID with Corner (e.g. `12345-NE`).  If no corner is specified in the `nodeId` parameter, you can specify one with `corner`.  The following three calls are equivalent:

```csharp
var results1 = client.GetStopsByNodeId("12345-NE");
var results2 = client.GetStopsByNodeId("12345", "NE");

// the corner parameter is ignored since corner is specified in nodeId.
var results3 = client.GetStopsByNodeId("12345-NE", "SW");
```
#### Retrieve Stops by Route ID

For a given Route ID, you can retrieve a list of all Stops along that route:

```csharp
var routeId = "12345";
var stops = client.GetStopsByRouteId(routeId);
```

#### Retrieve Stops by Stop ID

This endpoint may return multiple stops for a single Stop ID, since
it will return one result for each Route associated with the stop.

If desired, you can filter Stop results to return only routes that are serviced by a specified carrier code.

```csharp
var stopId = "12345";

// retrieve one record for each route that services this stop
var stops = client.GetStopsByStopId(stopId);

// retrieve only stops that are serviced by Metro
var metroStops = client.GetStopsByStopId(stopId, "MT");
```

## Building

[![Travis](https://img.shields.io/travis/syncromatics/Syncromatics.Clients.Metro.svg)](https://travis-ci.org/syncromatics/Syncromatics.Clients.Metro)
[![NuGet](https://img.shields.io/nuget/v/Syncromatics.Clients.Metro.Api.svg)](https://www.nuget.org/packages/Syncromatics.Clients.Metro.Api/)
[![NuGet Pre Release](https://img.shields.io/nuget/vpre/Syncromatics.Clients.Metro.Api.svg)](https://www.nuget.org/packages/Syncromatics.Clients.Metro.Api/)

This library is built using Cake. To build and test:

If running on windows

```
build.ps1 -Experimental
```

If Linux:
```
./build.sh
```


## Code of Conduct

We are committed to fostering an open and welcoming environment. Please read our [code of conduct](CODE_OF_CONDUCT.md) before participating in or contributing to this project.

## Contributing

We welcome contributions and collaboration on this project. Please read our [contributor's guide](CONTRIBUTING.md) to understand how best to work with us.

## License and Authors

[![GMV Syncromatics Engineering logo](https://secure.gravatar.com/avatar/645145afc5c0bc24ba24c3d86228ad39?size=16) GMV Syncromatics Engineering](https://github.com/syncromatics)

[![license](https://img.shields.io/github/license/syncromatics/Syncromatics.Clients.Metro.svg)](https://github.com/syncromatics/Syncromatics.Clients.Metro/blob/master/LICENSE)
[![GitHub contributors](https://img.shields.io/github/contributors/syncromatics/Syncromatics.Clients.Metro.svg)](https://github.com/syncromatics/Syncromatics.Clients.Metro/graphs/contributors)

This software is made available by GMV Syncromatics Engineering under the MIT license.
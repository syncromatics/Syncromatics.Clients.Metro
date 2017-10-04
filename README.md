# Los Angeles Metropolitan Transportation Authority (Metro) API Client
[![Build Status](https://travis-ci.org/syncromatics/Syncromatics.Clients.Metro.svg?branch=master)](https://travis-ci.org/syncromatics/Syncromatics.Clients.Metro)

A .NET library to interact with the [Metro API](http://developer.metro.net/).

## Usage

This package exposes .NET wrappers for select Metro APIs through the `IMetroApiClient` interface and
its default implementation `MetroApiClient`.

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
var metroStops = client.GetStopsById(stopId, "MT");
```

## Building

This library is built using Cake. To build and test:

If running on windows

```
build.ps1 -Experimental
```

If Linux:
```
./build.sh
```

## Contributing

Please see [CONTRIBUTING](CONTRIBUTING.md) for our guide to contributing and code of conduct.

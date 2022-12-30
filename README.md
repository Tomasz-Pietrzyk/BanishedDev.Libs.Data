# BanishedDev.Libs.Data

![License](https://img.shields.io/github/license/Tomasz-Pietrzyk/BanishedDev.Libs.Data?style=plastic) [![Build Status](https://banisheddevltd.visualstudio.com/BanishedDev.Libs.Data/_apis/build/status/Tomasz-Pietrzyk.BanishedDev.Libs.Data?branchName=master)](https://banisheddevltd.visualstudio.com/BanishedDev.Libs.Data/_build/latest?definitionId=7&branchName=master) ![Version](https://img.shields.io/nuget/vpre/BanishedDev.Libs.Data?style=plastic)

A library for operations on value type data. The main task is to limit the allocation of data on Heap.

![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/banisheddevltd/BanishedDev.Libs.Data/7?style=plastic)

## NuGet Package

```c
dotnet add package BanishedDev.Libs.Data --version 0.1.0-dev.22301201
```

## DataCollection

```c
DataCollection<T> where T: unmanaged
```

### Usage

```c
var intCollection = new DataCollection<int>(5);
var dateTimeCollection = new DataCollection<DateTime>(10);
```

```c#
struct dataStruct {
    public DateTime timestamp { get; set; }
    public decimal price { get; set; }
}

var dataStructCollection = new DataCollection<dataStruct>(100);
```

### IDisposable

Data Collection allocates 40 bytes. Data that is stored in native memory that dynamically allocates. Calling Dispose frees memory immediately.

```c#
intCollection.Dispose();
dateTimeCollection.Dispose();
dataStructCollection.Dispose();
```

#### Methods

```c#
void Add(T element);
bool Remove(T element);
int IndexOf(T element);
bool Contains(T element);
```

#### Properties

```c#
int Count { get; }
T this[int index] { get; }
```

### JsonSerialization

```c#
var string = JsonSerializer.Serialize(dataStructCollection);
```

```json
[
    {
        "timestamp":"",
        "price":1.04
    },
    {
        "timestamp":"",
        "price":1.04
    }
]
```

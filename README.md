# BanishedDev.Libs.Data

[License](LICENSE.md)

A library for operations on value type data. The main task is to limit the allocation of data on Heap.

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

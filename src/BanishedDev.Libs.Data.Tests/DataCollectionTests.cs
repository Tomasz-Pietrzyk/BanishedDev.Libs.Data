using System;
using System.Linq;
using System.Collections.Generic;
using BanishedDev.Libs.Data;
using Xunit;
using System.Text.Json;

namespace BanisehdDev.Libs.Data.Tests;

public class DataCollectionTests 
{
    public static Random rnd = new Random();

    public static IEnumerable<int> numbersGenerator()
    {
        for(int x = 0; x < 10000; x++)
        {
            yield return rnd.Next();
        }
    }

    public static int[] dataInput = numbersGenerator().Distinct().ToArray();

    [Theory]
    [InlineData(7495)]
    [InlineData(4632)]
    public void AddTest(int index)
    {
        using var collection = new DataCollection<int>(10000);

        for(int x = 0; x < dataInput.Length; x++)
        {
            collection.Add(dataInput[x]);
        }

        Assert.Equal(dataInput.Length, collection.Count);
        Assert.Equal(dataInput[index], collection[index]);
    }


    [Fact]
    public void RemoveTest()
    {
        using var collection = new DataCollection<int>(10000);
        var index = 3552;

        for(int x = 0; x < dataInput.Length; x++)
        {
            collection.Add(dataInput[x]);
        }

        Assert.Equal(dataInput.Length, collection.Count);
        Assert.Equal(dataInput[index], collection[index]);

        var nextIndexValue = collection[index+1];

        collection.Remove(collection[index]);

        Assert.NotEqual(dataInput.Length, collection.Count);
      

        Assert.NotEqual(dataInput[index], collection[index]);
        Assert.Equal(nextIndexValue, collection[index]);
    }

    [Theory]
    [InlineData(7495)]
    [InlineData(4632)]
    public void JsonSerializationTest(int index)
    {
        var collection = new DataCollection<int>(10000);

        for(int x = 0; x < dataInput.Length; x++)
        {
            collection.Add(dataInput[x]);
        }

        var jsonString = JsonSerializer.Serialize(collection);
        collection.Dispose();

        using var deserializedCollection = JsonSerializer.Deserialize<DataCollection<int>>(jsonString);
        if(deserializedCollection is not null) 
        {
            Assert.Equal(dataInput.Length, deserializedCollection.Count);
            Assert.Equal(dataInput[index], deserializedCollection[index]);
        } else {
            Assert.False(false);
        }
        
    }
}
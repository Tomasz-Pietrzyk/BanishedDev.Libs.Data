namespace BanishedDev.Libs.Data;

[System.Text.Json.Serialization.JsonConverter(typeof(Core.DataCollectionJsonFactory))]
public class DataCollection<T> : Core.Memory<T> where T : unmanaged
{
    public int Count { get; private set; }
    public T this[int index] => index < this.Count ? base.Read(index) : throw new DataExceptions("Index out of range");
    public DataCollection(int Length) : base((nuint) Length) { }

    public void Add(T element)
    {
        if(this.Count >= base.length) base.IncreaseMemory(10);
        base.Write(element, this.Count);
        this.Count++;
    }

    public bool Remove(T element)
    {
        var indexOf = this.IndexOf(element);
        if(indexOf == -1) return false;

        for(int x = indexOf+1, y = indexOf; x < this.Count; x++, y++)
        {
            base.Write(this[x], y);
        }
        this.Count--;
        return true;
    }

    public int IndexOf(T element)
    {
        for(int x = 0; x < this.Count; x++)
        {
            if(this[x].Equals(element)) return x;
        }

        return -1;
    }

    public bool Contains(T element) => this.IndexOf(element) != -1 ? true : false;

    public new void Dispose()
    {
        this.Count = 0;
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
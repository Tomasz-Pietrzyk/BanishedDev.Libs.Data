using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BanishedDev.Libs.Data.Core;

public abstract class Memory<T> : IDisposable where T: unmanaged
{
    private unsafe static nuint _elementSize = (nuint) sizeof(T);
    private bool disposedValue;

    private unsafe void* _memoryPointer;
    private nuint _length;
    private nuint _memorySize => this._length * Memory<T>._elementSize;

    protected int length => (int) this._length;

    protected unsafe T Read(int index) => *((T*)Unsafe.Add<T>(this._memoryPointer, index));
    protected unsafe void Write(T element, int index)
    {
        var indexPointer = (T*) Unsafe.Add<T>(this._memoryPointer, index);
        *indexPointer = element;
    }

    protected unsafe Memory(nuint initLength)
    {
        this._length = initLength;
        this._memoryPointer = NativeMemory.Alloc(this._memorySize);
    }

    protected void IncreaseMemory(int increaseCount)
    {
        this._length += (nuint) increaseCount;
        this.ReallocMemory();
    }

    private unsafe void ReallocMemory()
    {
        this._memoryPointer = NativeMemory.Realloc(this._memoryPointer, this._memorySize);
    }

    private unsafe void ReleaseMemory()
    {
        if(!this.disposedValue)
        {
            this._length = 0;
            NativeMemory.Free(this._memoryPointer);
            this.disposedValue = true;
        }
    }

    #region IDisposable

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~Memory()
    {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.ReleaseMemory();
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        this.ReleaseMemory();
        GC.SuppressFinalize(this);
    }

    #endregion
}
namespace Vortex.Net;

/// <summary>
/// <para>
/// Base type for all Vortex arrays.
/// </para>
///
/// <para>
/// All built-in Vortex array types can be safely cast to this type to pass into functions that
///  expect a generic array type. e.g.
/// </para>
/// 
/// <code>
/// auto primitive_array = vx_array_primitive_new(...);
/// vx_array_len((*vx_array) primitive_array));
/// </code>
/// </summary>
public readonly unsafe struct VxArray : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxArray Zero { get; } = IntPtr.Zero;

    private VxArray(IntPtr handle)
    {
        _handle = handle;
    }

    public VxArray(VxArray array)
    {
        _handle = array.Clone();
    }

    public VxArray(sbyte?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(byte?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(short?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(ushort?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(int?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(uint?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(long?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(ulong?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(float?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(double?[] data)
    {
        _handle = PrimitiveNullable(data, Vx.ArrayPrimitiveNew);
    }

    public VxArray(sbyte[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(byte[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(short[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(ushort[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(int[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(uint[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(long[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(ulong[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(float[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }
    public VxArray(double[] data)
    {
        _handle = Primitive(data, Vx.ArrayPrimitiveNew);
    }

    private delegate VxArray ArrayPrimitiveNewDelegate<T>(in T data, nuint length, IntPtr validity, ref VxError error) where T : unmanaged;
    private VxArray PrimitiveNullable<T>(T?[] data, ArrayPrimitiveNewDelegate<T> nativeFunction) where T : unmanaged
    {
        
        Span<T> dataSpan = data.Length < 255 ? stackalloc T[data.Length] : new T[data.Length];
        Span<byte> validitySpan = data.Length < 255 ? stackalloc byte[data.Length] : new byte[data.Length];
        for (int i = 0; i < dataSpan.Length; i++)
        {
            dataSpan[i] = data[i] ?? default;
            validitySpan[i] = (byte)(data[i].HasValue ? 1 : 0);
        }

        VxError error = new VxError();
        VxArray array;
        fixed (byte* validityPtr = &validitySpan.GetPinnableReference())
        {
            array = nativeFunction(dataSpan[0], (nuint)data.Length, (IntPtr)validityPtr, ref error);
        }
        error.Dispose();
        return array;
    } 
    private VxArray Primitive<T>(T[] data, ArrayPrimitiveNewDelegate<T> nativeFunction) where T : unmanaged
    {
        VxError error = new VxError();
        VxArray array = nativeFunction(data[0], (nuint)data.Length, IntPtr.Zero, ref error);
        error.Dispose();
        return array;
    }

    public static implicit operator IntPtr(VxArray value) => value._handle;
    public static implicit operator VxArray(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
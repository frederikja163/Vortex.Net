namespace Vortex.Net;

/// <summary>
/// <para>
/// The logical types of elements in Vortex arrays.
/// </para>
/// <para>
/// `DType` represents the different logical data types that can be represented in a Vortex array.
/// </para>
/// <para>
/// This is different from physical types, which represent the actual layout of data (compressed or
/// uncompressed). The set of physical types/formats (or data layout) is surjective into the set of
/// logical types (or in other words, all physical types map to a single logical type).
/// </para>
/// <para>
/// Note that a `DType` represents the logical type of the elements in the `Array`s, **not** the
/// logical type of the `Array` itself.
/// </para>
/// <para>
/// For example, an array with [`DType::Primitive`]([`I32`], [`NonNullable`]) could be physically
/// encoded as any of the following: <br/>
/// - A flat array of `i32` values. <br/>
/// - A run-length encoded sequence. <br/>
/// - Dictionary encoded values with bitpacked codes. <br/>
/// </para>
/// <para>
/// All of these physical encodings preserve the same logical [`I32`] type, even if the physical
/// data is different.
/// </para>
/// <para>
/// [`I32`]: PType::I32
/// [`NonNullable`]: Nullability::NonNullable
/// </para>
/// </summary>
public readonly struct VxDType : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxDType Zero { get; } = IntPtr.Zero;
    
    private VxDType(IntPtr handle)
    {
        _handle = handle;
    }

    public static implicit operator IntPtr(VxDType value) => value._handle;
    public static implicit operator VxDType(IntPtr value) => new VxDType(value);

    public void Dispose()
    {
        this.Free();
    }
}
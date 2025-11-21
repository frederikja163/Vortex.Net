namespace Vortex.Net;

/// <summary>
/// A handle to a Vortex file encapsulating the footer and logic for instantiating a reader.
/// </summary>
public readonly struct VxFile : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxFile Zero { get; } = IntPtr.Zero;

    private VxFile(IntPtr handle)
    {
        _handle = handle;
    }

    public VxFile(VxFile file)
    {
        _handle = file.Clone();
    }

    public static implicit operator IntPtr(VxFile value) => value._handle;
    public static implicit operator VxFile(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
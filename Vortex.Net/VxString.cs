namespace Vortex.Net;

/// <summary>
/// Strings for use within Vortex.
/// </summary>
public readonly struct VxString : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxString Zero { get; } = IntPtr.Zero;

    private VxString(IntPtr handle)
    {
        _handle = handle;
    }

    public VxString(string str)
    {
        _handle = Vx.NewFromCStr(str);
    }

    public static implicit operator IntPtr(VxString value) => value._handle;
    public static implicit operator VxString(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
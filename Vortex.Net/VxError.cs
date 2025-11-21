namespace Vortex.Net;

/// <summary>
/// The error structure populated by fallible Vortex C functions.
/// </summary>
public readonly struct VxError : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxError Zero { get; } = IntPtr.Zero;

    private VxError(IntPtr handle)
    {
        _handle = handle;
    }

    public VxError()
    {
        _handle = IntPtr.Zero;
    }

    public static implicit operator IntPtr(VxError value) => value._handle;
    public static implicit operator VxError(IntPtr value) => new(value);

    public void Dispose()
    {
        if (_handle == 0)
        {
            return;
        }

        string message = this.GetMessage().Ptr();
        this.Free();
        throw new Exception("Vortex exception: " + message);
    }
}
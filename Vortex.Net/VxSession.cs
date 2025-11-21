namespace Vortex.Net;

/// <summary>
/// A handle to a Vortex session.
/// </summary>
public readonly struct VxSession : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxSession Zero { get; } = IntPtr.Zero;

    private VxSession(IntPtr handle)
    {
        _handle = handle;
    }

    public VxSession()
    {
        _handle = Vx.NewSession();
    }

    public static implicit operator IntPtr(VxSession value) => value._handle;
    public static implicit operator VxSession(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
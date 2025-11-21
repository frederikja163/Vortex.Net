namespace Vortex.Net;

/// <summary>
/// Represents a Vortex struct data type, without top-level nullability.
/// </summary>
public readonly struct VxStructFields : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxStructFields Zero { get; } = IntPtr.Zero;

    private VxStructFields(IntPtr handle)
    {
        _handle = handle;
    }

    public VxStructFields(VxStructFields other)
    {
        _handle = other.Clone();
    }

    public static implicit operator IntPtr(VxStructFields value) => value._handle;
    public static implicit operator VxStructFields(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
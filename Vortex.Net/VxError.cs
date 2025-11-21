namespace Vortex;

/// <summary>
/// The error structure populated by fallible Vortex C functions.
/// </summary>
public readonly struct VxError : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxError Zero { get; } = default;

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

        string message = ToString();
        this.Free();
        throw new Exception("Vortex exception: " + message);
    }

    public override string ToString()
    {
        return this.GetMessage().ToString() ?? "";
    }
}
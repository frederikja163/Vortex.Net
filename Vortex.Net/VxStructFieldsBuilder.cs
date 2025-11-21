namespace Vortex.Net;

/// <summary>
/// Builder for creating a <see cref="VxStructFields"/>.
/// </summary>
public readonly struct VxStructFieldsBuilder : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxStructFieldsBuilder Zero { get; } = IntPtr.Zero;

    public VxStructFieldsBuilder()
    {
        _handle = Vx.NewFieldBuilder();
    }

    private VxStructFieldsBuilder(IntPtr handle)
    {
        _handle = handle;
    }

    public static implicit operator IntPtr(VxStructFieldsBuilder value) => value._handle;
    public static implicit operator VxStructFieldsBuilder(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
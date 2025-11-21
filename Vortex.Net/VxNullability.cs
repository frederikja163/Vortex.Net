namespace Vortex;

/// Whether an instance of a DType can be `null or not
public readonly struct VxNullability
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxNullability Zero { get; } = default;

    private VxNullability(IntPtr handle)
    {
        _handle = handle;
    }

    public static implicit operator IntPtr(VxNullability value) => value._handle;
    public static implicit operator VxNullability(IntPtr value) => new(value);
}
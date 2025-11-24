namespace Vortex.Net;

public struct VxVarBinViewBuilder
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxFile Zero { get; } = IntPtr.Zero;

    private VxVarBinViewBuilder(IntPtr handle)
    {
        _handle = handle;
    }

    public static implicit operator IntPtr(VxVarBinViewBuilder value) => value._handle;
    public static implicit operator VxVarBinViewBuilder(IntPtr value) => new(value);
}
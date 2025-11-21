namespace Vortex.Net;

/// <summary>
/// <para>
/// The `sink` interface is used to collect array chunks and place them into a resource
/// (e.g. an array stream or file (`vx_array_sink_open_file`)).
/// </para>
/// <para>
/// <b><i>Thread Safety</i></b><br/>
/// This struct is <b>not</b> thread-safe for concurrent operations. While the underlying
/// `Sender` is thread-safe, the FFI wrapper should only be accessed from a single thread
/// to avoid race conditions between `push` and `close` operations. The `close` operation
/// consumes the sink, making any subsequent operations undefined behavior.
/// </para>
/// <para>
/// Multiple threads may safely hold pointers to the same sink, but only one thread should
/// perform operations on it at a time, and coordination is required to ensure `close` is
/// called exactly once after all `push` operations are complete.
/// </para>
/// </summary>
public readonly struct VxArraySink : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxArraySink Zero { get; } = IntPtr.Zero;

    private VxArraySink(IntPtr handle)
    {
        _handle = handle;
    }

    public static implicit operator IntPtr(VxArraySink value) => value._handle;
    public static implicit operator VxArraySink(IntPtr value) => new(value);

    public void Dispose()
    {
        VxError error = new VxError();
        this.Close(ref error);
        error.Dispose();
    }
}
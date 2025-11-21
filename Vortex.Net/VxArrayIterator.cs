namespace Vortex;

/// <summary>
/// <para>
/// A Vortex array iterator.
/// </para>
/// <para>
/// Once the iterator is finished (returns `null` from [`vx_array_iterator_next`]), it may panic
///     on subsequent calls to [`vx_array_iterator_next`].
/// </para>
/// <para>
/// Even after the iterator is finished, an owned iterator must be released by calling
///     [`vx_array_iter_free`].
/// </para>
/// <para>
/// Iterators may be passed between threads, but calls to [`vx_array_iterator_next`] should be
/// serialized and not invoked concurrently.
/// </para>
/// </summary>
public readonly struct VxArrayIterator : IDisposable
{
    private readonly IntPtr _handle = IntPtr.Zero;

    public static VxArrayIterator Zero { get; } = default;

    private VxArrayIterator(IntPtr handle)
    {
        _handle = handle;
    }

    public static implicit operator IntPtr(VxArrayIterator value) => value._handle;
    public static implicit operator VxArrayIterator(IntPtr value) => new(value);

    public void Dispose()
    {
        this.Free();
    }
}
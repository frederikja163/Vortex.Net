using System.Runtime.InteropServices;

namespace Vortex;

/// <summary>
/// Options supplied for opening a file.
/// </summary>
public unsafe struct VxFileOpenOptions : IDisposable
{
    /// <summary>
    /// URI for opening the file.
    /// This must be a valid URI, even for files (file:///path/to/file)
    /// </summary>
    private readonly IntPtr _uri;
    /// <summary>
    /// Additional configuration for the file source (e.g. "s3.accessKey").
    /// This may be null, in which case it is treated as empty. 
    /// </summary>
    private readonly IntPtr _propertyKeys;
    /// <summary>
    /// Additional configuration values for the file source (e.g. S3 credentials).
    /// </summary>
    private readonly IntPtr _propertyVals;
    /// <summary>
    /// Number of properties in `property_keys` and `property_vals`.
    /// </summary>
    private readonly nint _propertyLen;
    
    public VxFileOpenOptions(string uri, string[] propertyKeys, string[] propertyVals)
    {
        if (propertyKeys.Length != propertyVals.Length)
        {
            throw new ArgumentException("PropertyKeys and PropertyVals must have the same length.");
        }

        _propertyLen = propertyKeys.Length;
        
        _uri = Marshal.StringToCoTaskMemAnsi(uri);
        _propertyKeys = Marshal.AllocCoTaskMem(sizeof(IntPtr) * (int)_propertyLen);
        _propertyVals = Marshal.AllocCoTaskMem(sizeof(IntPtr) * (int)_propertyLen);
        for (int i = 0; i < _propertyLen; i++)
        {
            IntPtr key = Marshal.StringToCoTaskMemAnsi(propertyKeys[i]);
            Marshal.WriteIntPtr(_propertyKeys, i, key);
            IntPtr value = Marshal.StringToCoTaskMemAnsi(propertyVals[i]);
            Marshal.WriteIntPtr(_propertyVals, i, value);
        }
    }

    public void Dispose()
    {
        Marshal.FreeCoTaskMem(_uri);

        for (int i = 0; i < _propertyKeys; i++)
        {
            IntPtr key = Marshal.ReadIntPtr(_propertyKeys, i);
            Marshal.FreeCoTaskMem(key);
            IntPtr value = Marshal.ReadIntPtr(_propertyVals, i);
            Marshal.FreeCoTaskMem(value);
        }
        
        Marshal.FreeCoTaskMem(_propertyKeys);
        Marshal.FreeCoTaskMem(_propertyVals);
    }
}
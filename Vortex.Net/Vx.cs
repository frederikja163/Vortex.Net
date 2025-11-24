using System.Reflection;
using System.Runtime.InteropServices;

namespace Vortex.Net;

public static unsafe partial class Vx
{
    static Vx()
    {
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), (name, assembly, path) =>
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return NativeLibrary.Load($"{name}.dll", assembly, path);
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return NativeLibrary.Load($"lib{name}.so", assembly, path);
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return NativeLibrary.Load($"lib{name}.dylib", assembly, path);
            }

            return NativeLibrary.Load(name,  assembly, path);
        });
    }
    
    /// <summary>
    /// Clone a borrowed <see cref="VxArray"/>, returning an owned <see cref="VxArray"/>.<br/>
    /// Must be released with <see cref="VxArray.Dispose"/>
    /// </summary>
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_clone")]
    public static partial VxArray Clone(this VxArray ptr);

    /// Free an owned <see cref="VxArray"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_free")]
    public static partial void Free(this VxArray ptr);

    /// <summary>
    /// Get the length of the array.
    /// </summary>
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_len")]
    public static partial nint Length(this VxArray array);

    /// <summary>
    /// Get the <see cref="VxDType"/> of the array.<br/>
    /// The returned pointer is valid as long as the array is valid. <br/>
    /// Do NOT free the returned dtype pointer - it shares the lifetime of the array.
    /// </summary>
    /// <returns></returns>
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_dtype")]
    public static partial VxDType DType(this VxArray array);

    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_field")]
    public static partial VxArray GetField(this VxArray array, uint index, ref VxError error);
    
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_is_null")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsNull(this VxArray array, uint index, ref VxError error);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_null_count")]
    public static partial nuint NullCount(this VxArray array, ref VxError error);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_u8")]
    public static partial nuint GetU8(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_u8")]
    public static partial nuint GetStorageU8(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_u16")]
    public static partial nuint GetU16(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_u16")]
    public static partial nuint GetStorageU16(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_u32")]
    public static partial nuint GetU32(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_u32")]
    public static partial nuint GetStorageU32(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_u64")]
    public static partial nuint GetU64(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_u64")]
    public static partial nuint GetStorageU64(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_i8")]
    public static partial nint GetI8(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_i8")]
    public static partial nint GetStorageI8(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_i16")]
    public static partial nint GetI16(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_i16")]
    public static partial nint GetStorageI16(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_i32")]
    public static partial nint GetI32(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_i32")]
    public static partial nint GetStorageI32(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_i64")]
    public static partial nint GetI64(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_i64")]
    public static partial nint GetStorageI64(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_f16")]
    public static partial nuint GetF16(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_f16")]
    public static partial nuint GetStorageF16(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_f32")]
    public static partial float GetF32(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_f32")]
    public static partial float GetStorageF32(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_f64")]
    public static partial double GetF64(this VxArray array, nuint index);
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_storage_f64")]
    public static partial double GetStorageF64(this VxArray array, nuint index);

    /// Create a new U8 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_u8")]
    public static partial VxArray ArrayPrimitiveNew(in byte data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new U16 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_u16")]
    public static partial VxArray ArrayPrimitiveNew(in ushort data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new U32 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_u32")]
    public static partial VxArray ArrayPrimitiveNew(in uint data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new U64 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_u64")]
    public static partial VxArray ArrayPrimitiveNew(in ulong data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new i8 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_i8")]
    public static partial VxArray ArrayPrimitiveNew(in sbyte data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new i16 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_i16")]
    public static partial VxArray ArrayPrimitiveNew(in short data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new i32 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_i32")]
    public static partial VxArray ArrayPrimitiveNew(in int data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new i64 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_i64")]
    public static partial VxArray ArrayPrimitiveNew(in long data, nuint length, IntPtr validity, ref VxError error);
    // /// Create a new f16 primitive array from raw data. <br/>
    // /// The `data` pointer must point to a valid array of `len` elements. <br/>
    // /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    // /// If `validity` is null, the array is assumed to have no null values. <br/>
    // /// Returns a new array, or null on error.
    // [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_f16")]
    // public static partial VxArray ArrayPrimitiveNewF16(in Half data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new f32 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_f32")]
    public static partial VxArray ArrayPrimitiveNew(in float data, nuint length, IntPtr validity, ref VxError error);
    /// Create a new f64 primitive array from raw data. <br/>
    /// The `data` pointer must point to a valid array of `len` elements. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// If `validity` is null, the array is assumed to have no null values. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_primitive_new_f64")]
    public static partial VxArray ArrayPrimitiveNew(in double data, nuint length, IntPtr validity, ref VxError error);

    /// Create a new boolean array from raw data.<br/>
    /// The `data` pointer must point to a valid array of `len` booleans.<br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans.<br/>
    /// If `validity` is null, the array is assumed to have no null values.<br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_bool_new")]
    public static partial VxArray ArrayBoolNew([MarshalAs(UnmanagedType.Bool)] in bool data, nuint length, IntPtr validity, ref VxError error);

    /// Create a new null array with the given length. <br/>
    /// All values in a null array are null by definition.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_null_new")]
    public static partial VxArray ArrayNullNew(nuint length);

    /// Create a new UTF8 array builder.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_utf8_builder_new")]
    public static partial VxVarBinViewBuilder ArrayUtf8BuilderNew([MarshalAs(UnmanagedType.Bool)] bool nullable);
    
    
    /// Create a new Binary array builder.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_binary_builder_new")]
    public static partial VxVarBinViewBuilder ArrayBinaryBuilderNew([MarshalAs(UnmanagedType.Bool)] bool nullable);
    
    /// Append a UTF8 string to the builder. <br/>
    /// The `value` pointer must point to a valid UTF-8 string of `len` bytes. <br/>
    /// This function takes ownership of neither the builder nor the string data.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_varbinview_builder_append_utf8")]
    public static partial void AppendUtf8(this VxVarBinViewBuilder binViewBuilder, [MarshalAs(UnmanagedType.LPUTF8Str)] string str, nuint length, ref VxError error);
    
    /// Append a binary value to the builder. <br/>
    /// The `value` pointer must point to a valid array of `len` bytes. <br/>
    /// This function takes ownership of neither the builder nor the binary data.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_varbinview_builder_append_binary")]
    public static partial void AppendBinary(this VxVarBinViewBuilder binViewBuilder, in byte str, nuint length, ref VxError error);
    
    /// Append a null value to the builder.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_varbinview_builder_append_null")]
    public static partial void AppendNull(this VxVarBinViewBuilder binViewBuilder);
    
    /// Append a null value to the builder.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_varbinview_builder_finish")]
    public static partial VxArray Finish(this VxVarBinViewBuilder binViewBuilder);

    /// Create a new struct array from field arrays. <br/>
    /// The `dtype` must be a struct dtype created with `vx_dtype_struct`. <br/>
    /// The `field_arrays` pointer must point to an array of `n_fields` array pointers. <br/>
    /// The `len` parameter specifies the length of each field array. <br/>
    /// The `validity` pointer, if not null, must point to a valid array of `len` booleans. <br/>
    /// This function does NOT take ownership of the field arrays. <br/>
    /// Returns a new array, or null on error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_struct_new")]
    public static partial VxArray ArrayStructNew(VxDType dtype, in VxArray vxArray, nuint len, IntPtr validity,
        ref VxError error);
    
    /// Write the UTF-8 string at `index` in the array into the provided destination buffer,
    /// recording the length in `len`.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_utf8")]
    public static partial void GetUtf8(this VxArray array, uint index, ref byte dst, ref int length);
    /// Write the UTF-8 string at `index` in the array into the provided destination buffer,
    /// recording the length in `len`.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_utf8")]
    public static partial void GetUtf8(this VxArray array, uint index, IntPtr dst, ref int length);
    /// Write the UTF-8 string at `index` in the array into the provided destination buffer, recording
    /// the length in `len`.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_binary")]
    public static partial void GetBinary(this VxArray array, uint index, ref byte dst, ref int length);
    /// Write the UTF-8 string at `index` in the array into the provided destination buffer, recording
    /// the length in `len`.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_get_binary")]
    public static partial void GetBinary(this VxArray array, uint index, IntPtr dst, ref int length);
    
    /// Free an owned <see cref="VxArrayIterator"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_iterator_free")]
    public static partial void Free(this VxArrayIterator ptr);

    /// Attempt to advance the `current` pointer of the iterator. <br/>
    ///  A return value of `true` indicates that another element was pulled from the iterator, and a return
    ///  of `false` indicates that the iterator is finished. <br/>
    ///  It is an error to call this function again after the iterator is finished.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_iterator_next")]
    public static partial VxArray Next(this VxArrayIterator iter, ref VxError error);
    
    /// Clone a borrowed <see cref="VxDType"/>, returning an owned <see cref="VxDType"/>.
    /// Must be released with <see cref="VxDType.Dispose"/>.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_clone")]
    public static partial VxDType Clone(this VxDType ptr);

    /// Free an owned <see cref="VxDType"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_free")]
    public static partial void Free(this VxDType ptr);

    /// Create a new null data type.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_null")]
    public static partial VxDType NewNull();
    
    /// Create a new boolean data type.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_bool")]
    public static partial VxDType NewBool([MarshalAs(UnmanagedType.Bool)] bool isNullable);

    /// Create a new primitive data type.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_primitive")]
    public static partial VxDType NewPrimitive(VxPType ptype, [MarshalAs(UnmanagedType.Bool)] bool isNullable);

    /// Create a new variable length UTF-8 data type.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_utf8")]
    public static partial VxDType NewUtf8([MarshalAs(UnmanagedType.Bool)] bool isNullable);

    /// Create a new variable length binary data type.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_binary")]
    public static partial VxDType NewBinary([MarshalAs(UnmanagedType.Bool)] bool isNullable);

    /// Create a new list data type.
    /// Takes ownership of the `element` pointer.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_list")]
    public static partial VxDType NewList(VxDType element, [MarshalAs(UnmanagedType.Bool)] bool isNullable);
    
    /// Create a new fixed-size list data type.
    /// Takes ownership of the `element` pointer.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_fixed_size_list")]
    public static partial VxDType NewFixedSizeList(this VxDType element, uint size, [MarshalAs(UnmanagedType.Bool)] bool isNullable);
    
    /// Create a new struct data type.
    /// Takes ownership of the <see cref="struct_dtype"/> pointer.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_struct")]
    public static partial VxDType NewStruct(VxStructFields structDtype, [MarshalAs(UnmanagedType.Bool)] bool isNullable);

    /// Create a new decimal data type.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_new_decimal")]
    public static partial VxDType NewDecimal(byte precision, sbyte scale, [MarshalAs(UnmanagedType.Bool)] bool isNullable);

    /// Get the variant of a <see cref="VxDType"/>.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_get_variant")]
    public static partial VxDTypeVariant GetVariant(this VxDType dtype);

    /// Return whether the given <see cref="VxDType"/> is nullable.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_is_null")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsNullable(this VxDType dtype);

    /// Returns the <see cref="VxPType"> of a primitive.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_primitive_ptype")]
    public static partial VxPType PrimitivePType(this VxDType dType);
    
    /// Returns the precision of a decimal.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_decimal_precision")]
    public static partial sbyte DecimalPrecision(this VxDType dType);
    
    /// Returns the scale of a decimal.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_decimal_scale")]
    public static partial sbyte DecimalScale(this VxDType dType);
        
    ///  Return a borrowed reference to the <see cref="VxStructFields"/> of a struct. <br/>
    ///  The returned pointer is valid as long as the struct dtype is valid.
    ///  Do NOT free the returned pointer - it shares the lifetime of the struct dtype.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_struct_dtype")]
    public static partial VxStructFields StructDType(this VxDType dtype);
    
    /// Returns the element type of a list. <br/>
    /// The returned pointer is valid as long as the list dtype is valid. <br/>
    /// Do NOT free the returned dtype pointer - it shares the lifetime of the list dtype.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_list_element")]
    public static partial VxDType ListElement(this VxDType dType);
    
    /// Returns the element type of a fixed-size list. <br/>
    /// The returned pointer is valid as long as the fixed-size list dtype is valid. <br/>
    /// Do NOT free the returned dtype pointer - it shares the lifetime of the fixed-size list dtype.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_fixed_size_list_element")]
    public static partial VxDType FixedSizeListElement(this VxDType dType);

    /// Returns the size of a fixed-size list.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_fixed_size_list_size")]
    public static partial uint FixedSizeListSize(this VxDType dType);
    
    
    /// Checks if the type is time.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_is_time")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsTime(this VxDType dType);

    /// Checks if the type is a date.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_is_date")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsDate(this VxDType dType);

    /// Checks if the type is a timestamp.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_is_timestamp")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool IsTimestamp(this VxDType dType);

    /// Returns the time unit, assuming the type is time.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_time_unit")]
    public static partial byte TimeUnit(this VxDType dType);

    /// Returns the time zone, assuming the type is time.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_time_zone")]
    public static partial void TimeZone(this VxDType dType, ref byte dst, ref int length);
    /// Returns the time zone, assuming the type is time.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_dtype_time_zone")]
    public static partial void TimeZone(this VxDType dType, IntPtr dst, ref int length);

    /// Free an owned <see cref="VxError"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_error_free")]
    public static partial void Free(this VxError error);
    
    /// Returns the error message from the given Vortex error. <br/>
    /// The returned pointer is valid as long as the error is valid. <br/>
    /// Do NOT free the returned string pointer - it shares the lifetime of the error.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_error_get_message")]
    public static partial VxString GetMessage(this VxError error);
    
    /// Clone a borrowed <see cref="VxFile"/>, returning an owned <see cref="VxFile"/>.
    /// Must be released with <see cref="VxFile.Dispose"/>
    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_clone")]
    public static partial VxFile Clone(this VxFile ptr);
    
    /// Free an owned <see cref="VxFile"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_free")]
    public static partial void Free(this VxFile ptr);

    /// Open a file at the given path on the file system.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_open_reader")]
    public static partial VxFile OpenReader(this VxSession session, ref VxFileOpenOptions options, ref VxError error);

    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_write_array")]
    public static partial void WriteArray(this VxSession session, [MarshalAs(UnmanagedType.LPStr)] string path, VxArray array, VxError errorOut);

    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_row_count")]
    public static partial ulong RowCount(this VxFile file);

    /// Return the DType of the file. <br/>
    /// The returned pointer is valid as long as the file is valid. <br/>
    /// Do NOT free the returned dtype pointer - it shares the lifetime of the file.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_dtype")]
    public static partial VxDType DType(this VxFile file);
    
    /// Can we prune the whole file using file stats and an expression
    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_dtype")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool CanPrune(this VxSession session, VxFile file, [MarshalAs(UnmanagedType.LPStr)] string filterExpression, nuint filterExpressionLength, ref VxError error);
    
    
    /// Build a new `vx_array_iterator` that returns a series of `vx_array`s from a scan over a `vx_layout_reader`.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_file_scan")]
    public static partial VxArrayIterator FileScan(this VxSession session, VxFile file, ref VxFileScanOptions opts, ref VxError errorOut);

    /// Set the stderr logger to output at the specified level.
    /// This function is optional, if it is not called then no logger will be installed
    [LibraryImport("vortex_ffi", EntryPoint = "vx_set_log_level")]
    public static partial void SetLogLevel(VxLogLevel level);
    
    /// Free an owned <see cref="session"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_session_free")]
    public static partial void Free(this VxSession session);
    
    /// Create a new Vortex session.
    /// The caller is responsible for freeing the session with <see cref="VxSession.Dispose"/>.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_session_new")]
    public static partial VxSession NewSession();

    /// Opens a writable array stream, where sink is used to push values into the stream. <br/>
    /// To close the stream close the sink with `vx_array_sink_close`.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_sink_open_file")]
    public static partial VxArraySink OpenFile(this VxSession session, [MarshalAs(UnmanagedType.LPStr)] string path, VxDType dType, ref VxError errorOut);
    
    /// Pushed a single array chunk into a file sink.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_session_new")]
    public static partial void Push(this VxArraySink sink, VxArray array, ref VxError error);
    
    /// Closes an array sink, must be called to ensure all the values pushed to the sink are written
    /// to the external resource.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_array_sink_close")]
    public static partial void Close(this VxArraySink sink, ref VxError error);
    
    /// Clone a borrowed <see cref="VxString"/>, returning an owned <see cref="VxArray"/>.
    /// Must be released with <see cref="VxString.Dispose"/>.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_string_clone")]
    public static partial VxString Clone(this VxString ptr);
    
    /// Free an owned <see cref="VxString"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_string_free")]
    public static partial void Free(this VxString ptr);
    
    /// Create a new Vortex UTF-8 string by copying from a pointer and length.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_string_new")]
    public static partial VxString NewString([MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 1)] string ptr, nint len);
    
    /// Create a new Vortex UTF-8 string by copying from a null-terminated C-style string.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_string_new_from_cstr")]
    public static partial VxString NewFromCStr([MarshalAs(UnmanagedType.LPStr)] string ptr);
    
    /// Return the length of the string in bytes.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_string_len")]
    public static partial void Length(VxString ptr);

    /// Return the pointer to the string data.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_string_ptr")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string Ptr(this VxString ptr);

    /// Clone a borrowed <see cref="VxStructFields"/>, returning an owned <see cref="VxStructFields"/>.
    /// Must be released with <see cref="VxStructFields.Dispose"/>.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_clone")]
    public static partial VxStructFields Clone(this VxStructFields ptr);

    /// Free an owned <see cref="VxStructFields"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_free")]
    public static partial void Free(this VxStructFields ptr);
    
    /// Return the number of fields in the struct dtype.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_nfields")]
    public static partial CULong NFields(this VxStructFields ptr);
    
    /// Return a borrowed reference to the name of the field at the given index.<br/>
    /// The returned pointer is valid as long as the struct fields is valid.<br/>
    /// Do NOT free the returned string pointer - it shares the lifetime of the struct fields.<br/>
    /// Returns null if the index is out of bounds.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_field_name")]
    public static partial VxString FieldName(this VxStructFields ptr, nint idx);
    
    /// Returns an *owned* reference to the dtype of the field at the given index. <br/>
    /// The return type is owned since struct dtypes can be lazily parsed from a binary format, in <br/>
    /// which case it's not possible to return a borrowed reference to the field dtype. <br/>
    /// Returns null if the index is out of bounds or if the field dtype cannot be parsed.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_field_dtype")]
    public static partial VxString FieldType(this VxStructFields ptr, CULong idx);
    
    /// Free an owned <see cref="VxStructFieldsBuilder"/> object.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_builder_free")]
    public static partial void Free(this VxStructFieldsBuilder ptr);
    
    /// Create a new struct dtype builder.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_builder_new")]
    public static partial VxStructFieldsBuilder NewFieldBuilder();
    
    /// Add a field to the struct dtype builder. <br/>
    /// Takes ownership of both the `name` and `dtype` pointers. <br/>
    /// Must either free or finalize the builder.
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_builder_add_field")]
    public static partial void AddField(this VxStructFieldsBuilder builder, VxString name, VxDType dType);
    
    
    /// Finalize the struct dtype builder, returning a new <see cref="VxStructFields"/>. <br/>
    /// Takes ownership of the <see cref="VxStructFieldsBuilder"/>. 
    [LibraryImport("vortex_ffi", EntryPoint = "vx_struct_fields_builder_finalize")]
    public static partial VxStructFields Finalize(this VxStructFieldsBuilder builder);
}

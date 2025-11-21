namespace Vortex.Net;

/// The variant tag for a Vortex data type.
public enum VxDTypeVariant
{
    /// Null type.
    Null = 0,
    /// Boolean type.
    Bool = 1,
    /// Primitive types (e.g., u8, i16, f32, etc.).
    Primitive = 2,
    /// Variable-length UTF-8 string type.
    Utf8 = 3,
    /// Variable-length binary data type.
    Binary = 4,
    /// Nested struct type.
    Struct = 5,
    /// Nested list type.
    List = 6,
    /// User-defined extension type.
    Extension = 7,
    /// Decimal type with fixed precision and scale.
    Decimal = 8,
    /// Nested fixed-size list type.
    FixedSizeList = 9,
}
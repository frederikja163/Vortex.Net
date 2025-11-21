using System.Runtime.InteropServices;

namespace Vortex;

public struct VxFileScanOptions : IDisposable
{
    private readonly IntPtr _projectionExpression;
    private readonly nuint _projectionExpressionLength;
    private readonly IntPtr _filterExpression;
    private readonly nuint _filterExpressionLen;
    private readonly nint _splitByRowCount;
    private readonly CULong _rowRangeStart;
    private readonly CULong _rowRangeEnd;
    private readonly CULong _rowOffset;

    public VxFileScanOptions(string projectionExpression, string filterExpression, nint splitByRowCount, CULong rowRangeStart, CULong rowRangeEnd, CULong rowOffset)
    {
        _projectionExpression = Marshal.StringToCoTaskMemAnsi(projectionExpression);
        _projectionExpressionLength = (nuint)projectionExpression.Length;
        _filterExpression = Marshal.StringToCoTaskMemAnsi(filterExpression);
        _filterExpressionLen = (nuint)filterExpression.Length;
        _splitByRowCount = splitByRowCount;
        _rowRangeStart = rowRangeStart;
        _rowRangeEnd = rowRangeEnd;
        _rowOffset = rowOffset;
    }

    public void Dispose()
    {
        Marshal.FreeCoTaskMem(_projectionExpression);
        Marshal.FreeCoTaskMem(_filterExpression);
    }
}
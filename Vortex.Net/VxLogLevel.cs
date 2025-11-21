namespace Vortex;

/// Log levels for the Vortex library.
public enum VxLogLevel
{
    /// No logging will be performed.
    Off = 0,
    /// Only error messages will be logged.
    Error = 1,
    /// Warnings and error messages will be logged.
    Warn = 2,
    /// Informational messages, warnings, and error messages will be logged.
    Info = 3,
    /// Debug messages, informational messages, warnings, and error messages will be logged.
    Debug = 4,
    /// All messages, including trace messages, will be logged.
    Trace = 5,
}
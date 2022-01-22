using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

// Example of lowered handler on Sharplab
// https://sharplab.io/#v2:EYLgZgpghgLgrgJwgZwLQDdkCYAMWuowAWEAthADQAmIA1AD4ACWAjALABQjOABIywDoAKkSRQqASwB2AcwDcnbn0EARCVBlSA9shgSAxsgVde/AQCU4UveQEBhLaQAOEgDYQEAZQ/oDKY4pYAMw8AN6cPJF8IfwAbHwALDwAMloyAJJSYFoAFMQSyClpMh48rsUeFDwA2pkwHk5arrAQVJ4wCNIyABJQUlTuCACCCDJw5NY5AETlMiUIUwCUALo8SGA87Qhw+vBIVKlzXZnZCKSwElpSdQ1NLW0dXb39gzxEfQMeixFR4RxRAJ4Eg2OXeLw8AnSyAAolIoMB3FRFmEfoDAbN5gJDqCPoNhFotl0cotFsY0ZEAL6onhUjjUgD09JSUAAXm4AJ48CDoKCuOD3IEbYgQZS8ApraCuVycjGtKpaYUIADuBRF2hgays1KcnR59WU8WkGt6CCoAHknB4LlccvxeLoEN9/r9qQDGAB2HgOrEQWTEMlRWnUxgxFjxRhJACyUGktpYOGqqygo2QyL+5J4+iuuiB1jKvpkxB4AF4eABWANorNSHN2r0dEs8KbAKDIAyiqaVwGHeZlCoIRtSCBKopzDzEgLOtGyhBYtInXIAEimIhF7j9RB4WiFJFFPAA5KEHRT90DCuqwuvC0QKVVgHANaETebLQhrVIcg7FhSBEsu7Sgw4WprFuZp6geTpZGeT4EGWRQQnWettl2TYOh2PZWh7Y4si0M53xuBBGjA1pCSg3EPE4dMohDCVxCuaUeGALQmh4KFYXhREux1CQ9RFMQqHozkVAgMAoDgVwYAIoj7lInpyIHAB9MEYMnN0Qi2dDEEw4psNOc49CuKS7nA2ToMGHIjTKCR6jfVxkgLYgqksvTYHAhwrBgKoe1KGd5QfRjmNcL0iC0cSqCGJxLX6J0ASotE2LhBFWkbGdIRhRLERyQ57PQCBXEhHC8IMqRSVdKJgR4HIAEIEo41oYvJOKMx4JT5MbKgRLEiSuwzZAQrCiKoqoRtRNcZAIB68kPUmylODKyJWvBAdSyHEdhNE8TJJAwjjJIx4yKWnJXGsq07IcogqhcmA3NC6xSqnQE+tC1xwsi31htLNCJupQC1MSHhBve5ITtsuN7WRYsAD4WuUvFAf6YGbN5T97uDGIknhqgADFcP08CAB4hEhnIhB4Hk+QgCHocWmCBExnGivAnJybgSnVOokItFyhBOg6vchAJfaZGJEtqdhiEBdkic5oemi60loWhn6Ox3GTEWoZh+T8VkpWqBV6AEGlukOEAmjfXGUccry6kmoBYT7xkCh5tYwr9MuKQnYe2kAXgvgsFHXsmpo7LuTylJQ9cLs5bDPcFKQZBNsbWSBGhZwYHZKOQiYljaqSqgsrSK2gtmIu02dj1w9yoLi1LEuI//NH/uxOtSGQGQy4et0WAAThyVv24bk2gA===

[InterpolatedStringHandler]
public ref struct StructuredLoggingTraceInterpolatedStringHandler
{
    public readonly bool IsEnabled;

    private DefaultInterpolatedStringHandler _handler;

    public StructuredLoggingTraceInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, out bool shouldAppend)
    {
        IsEnabled = logger.IsEnabled(LogLevel.Trace);
        if (!IsEnabled)
        {
            _handler = default;
            shouldAppend = false;
            return;
        }

        _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public override string ToString()
    {
        return _handler.ToString();
    }

    public string ToStringAndClear()
    {
        return _handler.ToStringAndClear();
    }

    public void AppendLiteral(string message)
    {
        _handler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _handler.AppendFormatted(message);
    }
}

[InterpolatedStringHandler]
public ref struct StructuredLoggingDebugInterpolatedStringHandler
{
    public readonly bool IsEnabled;

    private DefaultInterpolatedStringHandler _handler;

    public StructuredLoggingDebugInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, out bool shouldAppend)
    {
        IsEnabled = logger.IsEnabled(LogLevel.Debug);
        if (!IsEnabled)
        {
            _handler = default;
            shouldAppend = false;
            return;
        }

        _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public override string ToString()
    {
        return _handler.ToString();
    }

    public string ToStringAndClear()
    {
        return _handler.ToStringAndClear();
    }

    public void AppendLiteral(string message)
    {
        _handler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _handler.AppendFormatted(message);
    }
}

[InterpolatedStringHandler]
public ref struct StructuredLoggingInformationInterpolatedStringHandler
{
    public readonly bool IsEnabled;

    private DefaultInterpolatedStringHandler _handler;

    public StructuredLoggingInformationInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, out bool shouldAppend)
    {
        IsEnabled = logger.IsEnabled(LogLevel.Information);
        if (!IsEnabled)
        {
            _handler = default;
            shouldAppend = false;
            return;
        }

        _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public override string ToString()
    {
        return _handler.ToString();
    }

    public string ToStringAndClear()
    {
        return _handler.ToStringAndClear();
    }

    public void AppendLiteral(string message)
    {
        _handler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _handler.AppendFormatted(message);
    }

}

[InterpolatedStringHandler]
public ref struct StructuredLoggingWarningInterpolatedStringHandler
{
    public readonly bool IsEnabled;

    private DefaultInterpolatedStringHandler _handler;

    public StructuredLoggingWarningInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, out bool shouldAppend)
    {
        IsEnabled = logger.IsEnabled(LogLevel.Warning);
        if (!IsEnabled)
        {
            _handler = default;
            shouldAppend = false;
            return;
        }

        _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public override string ToString()
    {
        return _handler.ToString();
    }

    public string ToStringAndClear()
    {
        return _handler.ToStringAndClear();
    }

    public void AppendLiteral(string message)
    {
        _handler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _handler.AppendFormatted(message);
    }
}

[InterpolatedStringHandler]
public ref struct StructuredLoggingErrorInterpolatedStringHandler
{
    public readonly bool IsEnabled;

    private DefaultInterpolatedStringHandler _handler;

    public StructuredLoggingErrorInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, out bool shouldAppend)
    {
        IsEnabled = logger.IsEnabled(LogLevel.Error);
        if (!IsEnabled)
        {
            _handler = default;
            shouldAppend = false;
            return;
        }

        _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public override string ToString()
    {
        return _handler.ToString();
    }

    public string ToStringAndClear()
    {
        return _handler.ToStringAndClear();
    }

    public void AppendLiteral(string message)
    {
        _handler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _handler.AppendFormatted(message);
    }

}

[InterpolatedStringHandler]
public ref struct StructuredLoggingCriticalInterpolatedStringHandler
{
    public readonly bool IsEnabled;

    private DefaultInterpolatedStringHandler _handler;

    public StructuredLoggingCriticalInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, out bool shouldAppend)
    {
        IsEnabled = logger.IsEnabled(LogLevel.Critical);
        if (!IsEnabled)
        {
            _handler = default;
            shouldAppend = false;
            return;
        }

        _handler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        shouldAppend = true;
    }

    public override string ToString()
    {
        return _handler.ToString();
    }

    public string ToStringAndClear()
    {
        return _handler.ToStringAndClear();
    }

    public void AppendLiteral(string message)
    {
        _handler.AppendLiteral(message);
    }

    public void AppendFormatted<T>(T message)
    {
        _handler.AppendFormatted(message);
    }

}

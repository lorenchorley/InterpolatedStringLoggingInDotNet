using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

public static class InterpolatedLoggerExtensions
{

    public static void Trace(this ILogger logger, [InterpolatedStringHandlerArgument("logger")] ref StructuredLoggingTraceInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            logger.LogTrace(handler.ToString());
        }
    }

    public static void Debug(this ILogger logger, [InterpolatedStringHandlerArgument("logger")] ref StructuredLoggingDebugInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            logger.LogDebug(handler.ToString());
        }
    }

    public static void Information(this ILogger logger, [InterpolatedStringHandlerArgument("logger")] ref StructuredLoggingInformationInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            logger.LogInformation(handler.ToString());
        }
    }
    public static void Warning(this ILogger logger, [InterpolatedStringHandlerArgument("logger")] ref StructuredLoggingWarningInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            logger.LogWarning(handler.ToString());
        }
    }

    public static void Error(this ILogger logger, [InterpolatedStringHandlerArgument("logger")] ref StructuredLoggingErrorInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            logger.LogError(handler.ToString());
        }
    }

    public static void Critical(this ILogger logger, [InterpolatedStringHandlerArgument("logger")] ref StructuredLoggingCriticalInterpolatedStringHandler handler)
    {
        if (handler.IsEnabled)
        {
            logger.LogCritical(handler.ToString());
        }
    }
}

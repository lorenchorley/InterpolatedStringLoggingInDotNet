using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

[MemoryDiagnoser]
public class LoggerMessageComparaisonBenchmarks
{
    private static readonly Action<ILogger, Exception?> _log1 = LoggerMessage.Define(
        logLevel: LogLevel.Information,
        eventId: 0,
        formatString: " Just a string ");

    private static readonly Action<ILogger, string, Exception?> _log2 = LoggerMessage.Define<string>(
        logLevel: LogLevel.Information,
        eventId: 0,
        formatString: " {str1} ");
    
    private static readonly Action<ILogger, string, string, Exception?> _log3 = LoggerMessage.Define<string, string>(
        logLevel: LogLevel.Information,
        eventId: 0,
        formatString: " {str1} {str2} ");
    
    private static readonly Action<ILogger, string, string, string, string, string, string, Exception?> _log4 = LoggerMessage.Define<string, string, string, string, string, string>(
        logLevel: LogLevel.Information,
        eventId: 0,
        formatString: " {str1} {str2} {str3} {str4} {str5} {str6} ");

    private ILoggerFactory _loggerFactory = null!;
    private ILogger _logger = null!;

    [ParamsAllValues]
    public bool IsEnabled { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        Serilog.Log.Logger = new Serilog.LoggerConfiguration()
            .MinimumLevel.Is(IsEnabled ? Serilog.Events.LogEventLevel.Information : Serilog.Events.LogEventLevel.Error)
            .CreateLogger();

        _loggerFactory = LoggerFactory.Create(builder => Serilog.SerilogLoggingBuilderExtensions.AddSerilog(builder, dispose: true));
        _logger = _loggerFactory.CreateLogger<LoggerMessageComparaisonBenchmarks>();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _loggerFactory.Dispose();
    }

    [Benchmark]
    public void LoggerMessage_NoParameters()
    {
        _log1(_logger, null);
    }

    [Benchmark]
    public void Interpolated_NoParameters()
    {
        InterpolatedLoggerExtensions.Information(_logger, $" Just a string ");
    }
    
    [Benchmark]
    public void LoggerMessage_OneString()
    {
        const string str = "basic string";

        _log2(_logger, str, null);
    }
    
    [Benchmark]
    public void Interpolated_OneString()
    {
        const string str1 = "basic string";

        InterpolatedLoggerExtensions.Information(_logger, $" {str1} ");
    }
    
    [Benchmark]
    public void LoggerMessage_TwoStrings()
    {
        const string str1 = "basic string";
        const string str2 = "basic string2";

        _log3(_logger, str1, str2, null);
    }
    
    [Benchmark]
    public void Interpolated_TwoStrings()
    {
        const string str1 = "basic string";
        const string str2 = "basic string2";

        InterpolatedLoggerExtensions.Information(_logger, $" {str1} {str2} ");
    }
    
    [Benchmark]
    public void LoggerMessage_ManyStrings()
    {
        const string str1 = "basic string";
        const string str2 = "basic string2";
        const string str3 = "basic string3";
        const string str4 = "basic string4";
        const string str5 = "basic string5";
        const string str6 = "basic string6";

        _log4(_logger, str1, str2, str3, str4, str5, str6, null);
    }
    
    [Benchmark]
    public void Interpolated_ManyStrings()
    {
        const string str1 = "basic string";
        const string str2 = "basic string2";
        const string str3 = "basic string3";
        const string str4 = "basic string4";
        const string str5 = "basic string5";
        const string str6 = "basic string6";
        const string str7 = "basic string7";

        InterpolatedLoggerExtensions.Information(_logger, $" {str1} {str2} {str3} {str4} {str5} {str6} {str7} ");
    }

}

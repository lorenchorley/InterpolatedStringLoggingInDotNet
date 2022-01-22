using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

[MemoryDiagnoser]
public class AllPossibilitiesBenchmarks
{
    private static readonly Action<ILogger, string, int, int, Exception?> _logSegmentLength = LoggerMessage.Define<string, int, int>(
        logLevel: LogLevel.Information,
        eventId: 0,
        formatString: " '{@str}', {@length}, {@strLength}.");

    private ILoggerFactory _loggerFactory = null!;
    private ILogger _logger = null!;

    [ParamsAllValues]
    public bool IsEnabled { get; set; }
    
    [ParamsAllValues]
    public bool LongOp { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        Serilog.Log.Logger = new Serilog.LoggerConfiguration()
            .MinimumLevel.Is(IsEnabled ? Serilog.Events.LogEventLevel.Information : Serilog.Events.LogEventLevel.Error)
            .CreateLogger();

        _loggerFactory = LoggerFactory.Create(builder => Serilog.SerilogLoggingBuilderExtensions.AddSerilog(builder, dispose: true));
        _logger = _loggerFactory.CreateLogger<AllPossibilitiesBenchmarks>();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _loggerFactory.Dispose();
    }

    int LongOperation(string str, bool enable)
    {
        if (enable)
        {
            Thread.Sleep(1);
        }
        return str.Length;
    }

    [Benchmark(Baseline = true)]
    public void LoggerMessageDelegate()
    {
        const int length = 5;
        const string str = "basic string";

        _logSegmentLength(_logger, str, length, LongOperation(str, LongOp), null);
    }

    [Benchmark]
    public void InterpolatedStringWithHandler()
    {
        const int length = 5;
        const string str = "basic string";

        // Lazy evaluation of interpolated parameters
        InterpolatedLoggerExtensions.Information(_logger, $" '{str}', {length}, {LongOperation(str, LongOp)}.");
    }

    [Benchmark]
    public void InterpolatedStringWithHandlerExtensionMethod()
    {
        const int length = 5;
        const string str = "basic string";

        // Lazy evaluation of interpolated parameters
        _logger.Information($" '{str}', {length}, {LongOperation(str, LongOp)}.");
    }

    [Benchmark]
    public void StringConcatenation()
    {
        const int length = 5;
        const string str = "basic string";

        _logger.LogInformation(" '" + str + "', " + length + ", " + LongOperation(str, LongOp) + ".");
    }

    [Benchmark]
    public void StringConcat()
    {
        const int length = 5;
        const string str = "basic string";

        _logger.LogInformation(string.Concat(" '", str, "', ", length, ", ", LongOperation(str, LongOp), "."));
    }

    [Benchmark]
    public void StringFormat()
    {
        const int length = 5;
        const string str = "basic string";

        _logger.LogInformation(string.Format(" '{0}', {1}, {2}.", str, length, LongOperation(str, LongOp)));
    }

    [Benchmark]
    public void TemplateAndArgs()
    {
        const int length = 5;
        const string str = "basic string";

        _logger.LogInformation(" '{@str}', {@length}, {@getLength}.", str, length, LongOperation(str, LongOp));
    }

    [Benchmark]
    public void BasicInterpolatedString()
    {
        const int length = 5;
        const string str = "basic string";

        _logger.LogInformation($" '{str}', {length}, {LongOperation(str, LongOp)}.");
    }

}

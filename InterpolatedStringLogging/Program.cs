using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Compact;

// Inspired by https://habr.com/en/post/591171/

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console(new CompactJsonFormatter())
    .CreateLogger();   

using var loggerFactory = LoggerFactory.Create(builder => builder.AddSerilog(dispose: true));
var logger = loggerFactory.CreateLogger<Program>();

var str = "a string";
var length = str.Length;

logger.Information($"The length of segment {str} is {length}.");


//BenchmarkRunner.Run<AllPossibilitiesBenchmarks>();

/*

Avantages : 
    Lazy evaluation of parameters in string
    Almost as fast message delegate solution is disabled state
    Potentially much faster than delegate solution is enabled state


|                                       Method | IsEnabled | LongOp |             Mean |         Error |        StdDev |           Median | Ratio | RatioSD |  Gen 0 | Allocated |
|--------------------------------------------- |---------- |------- |-----------------:|--------------:|--------------:|-----------------:|------:|--------:|-------:|----------:|
|                        LoggerMessageDelegate |     False |  False |         14.69 ns |      0.313 ns |      0.429 ns |         14.50 ns |  1.00 |    0.00 |      - |         - |
|                InterpolatedStringWithHandler |     False |  False |         17.45 ns |      0.354 ns |      0.348 ns |         17.55 ns |  1.19 |    0.05 |      - |         - |
| InterpolatedStringWithHandlerExtensionMethod |     False |  False |         16.91 ns |      0.193 ns |      0.161 ns |         16.88 ns |  1.14 |    0.03 |      - |         - |
|                          StringConcatenation |     False |  False |         98.95 ns |      1.997 ns |      2.863 ns |         97.65 ns |  6.75 |    0.32 | 0.0401 |     168 B |
|                                 StringConcat |     False |  False |        147.62 ns |      2.866 ns |      3.726 ns |        146.63 ns | 10.04 |    0.30 | 0.0744 |     312 B |
|                                 StringFormat |     False |  False |        139.39 ns |      3.567 ns |     10.060 ns |        135.75 ns |  9.85 |    0.80 | 0.0286 |     120 B |
|                              TemplateAndArgs |     False |  False |        105.10 ns |      2.921 ns |      8.239 ns |        103.05 ns |  7.13 |    0.64 | 0.0229 |      96 B |
|                      BasicInterpolatedString |     False |  False |        110.66 ns |      2.221 ns |      4.875 ns |        109.63 ns |  7.60 |    0.38 | 0.0172 |      72 B |
|                                              |           |        |                  |               |               |                  |       |         |        |           |
|                        LoggerMessageDelegate |     False |   True | 15,443,169.53 ns | 59,746.304 ns | 55,886.730 ns | 15,454,297.66 ns | 1.000 |    0.00 |      - |       6 B |
|                InterpolatedStringWithHandler |     False |   True |         17.87 ns |      0.366 ns |      0.570 ns |         17.65 ns | 0.000 |    0.00 |      - |         - |
| InterpolatedStringWithHandlerExtensionMethod |     False |   True |         17.28 ns |      0.217 ns |      0.182 ns |         17.31 ns | 0.000 |    0.00 |      - |         - |
|                          StringConcatenation |     False |   True | 15,446,246.54 ns | 49,551.933 ns | 43,926.498 ns | 15,446,621.88 ns | 1.000 |    0.01 |      - |     175 B |
|                                 StringConcat |     False |   True | 15,472,981.93 ns | 36,632.754 ns | 34,266.301 ns | 15,466,882.03 ns | 1.002 |    0.00 |      - |     318 B |
|                                 StringFormat |     False |   True | 15,457,443.87 ns | 35,897.121 ns | 29,975.722 ns | 15,461,700.00 ns | 1.001 |    0.00 |      - |     127 B |
|                              TemplateAndArgs |     False |   True | 15,505,519.27 ns | 53,605.443 ns | 50,142.565 ns | 15,504,289.06 ns | 1.004 |    0.00 |      - |     103 B |
|                      BasicInterpolatedString |     False |   True | 15,486,127.81 ns | 41,998.557 ns | 39,285.476 ns | 15,489,378.12 ns | 1.003 |    0.00 |      - |      87 B |
|                                              |           |        |                  |               |               |                  |       |         |        |           |
|                        LoggerMessageDelegate |      True |  False |      2,861.67 ns |     53.728 ns |     47.629 ns |      2,850.05 ns |  1.00 |    0.00 | 0.2594 |   1,088 B |
|                InterpolatedStringWithHandler |      True |  False |        578.13 ns |      8.649 ns |     14.450 ns |        570.49 ns |  0.20 |    0.01 | 0.2537 |   1,064 B |
| InterpolatedStringWithHandlerExtensionMethod |      True |  False |        553.99 ns |     10.871 ns |     14.513 ns |        553.08 ns |  0.19 |    0.01 | 0.2537 |   1,064 B |
|                          StringConcatenation |      True |  False |        541.36 ns |     10.861 ns |     30.987 ns |        537.87 ns |  0.18 |    0.01 | 0.1488 |     624 B |
|                                 StringConcat |      True |  False |        563.61 ns |      9.062 ns |      8.033 ns |        560.63 ns |  0.20 |    0.00 | 0.1831 |     768 B |
|                                 StringFormat |      True |  False |        566.90 ns |     11.054 ns |     19.360 ns |        556.74 ns |  0.20 |    0.01 | 0.1373 |     576 B |
|                              TemplateAndArgs |      True |  False |      2,925.77 ns |     49.484 ns |     43.866 ns |      2,917.39 ns |  1.02 |    0.02 | 0.2708 |   1,136 B |
|                      BasicInterpolatedString |      True |  False |        535.12 ns |     10.733 ns |     12.777 ns |        533.71 ns |  0.19 |    0.00 | 0.1259 |     528 B |
|                                              |           |        |                  |               |               |                  |       |         |        |           |
|                        LoggerMessageDelegate |      True |   True | 15,490,268.23 ns | 75,501.473 ns | 70,624.124 ns | 15,471,582.81 ns |  1.00 |    0.00 |      - |   1,094 B |
|                InterpolatedStringWithHandler |      True |   True | 15,432,964.96 ns | 30,259.356 ns | 25,267.933 ns | 15,434,428.91 ns |  1.00 |    0.01 |      - |   1,071 B |
| InterpolatedStringWithHandlerExtensionMethod |      True |   True | 15,489,536.77 ns | 50,750.735 ns | 47,472.269 ns | 15,478,023.44 ns |  1.00 |    0.01 |      - |   1,071 B |
|                          StringConcatenation |      True |   True | 15,450,482.29 ns | 46,600.868 ns | 43,590.481 ns | 15,456,587.50 ns |  1.00 |    0.00 |      - |     630 B |
|                                 StringConcat |      True |   True | 15,481,664.79 ns | 39,008.892 ns | 36,488.942 ns | 15,468,818.75 ns |  1.00 |    0.00 |      - |     775 B |
|                                 StringFormat |      True |   True | 15,527,842.19 ns | 59,229.578 ns | 55,403.384 ns | 15,531,329.69 ns |  1.00 |    0.01 |      - |     583 B |
|                              TemplateAndArgs |      True |   True | 15,485,204.38 ns | 47,492.022 ns | 44,424.067 ns | 15,470,609.38 ns |  1.00 |    0.00 |      - |   1,142 B |
|                      BasicInterpolatedString |      True |   True | 15,485,666.15 ns | 61,431.371 ns | 57,462.942 ns | 15,480,843.75 ns |  1.00 |    0.01 |      - |     542 B |
 
*/


BenchmarkRunner.Run<LoggerMessageComparaisonBenchmarks>();

/*
 
                     Method | IsEnabled |        Mean |     Error |    StdDev |      Median |  Gen 0 | Allocated |
|--------------------------- |---------- |------------:|----------:|----------:|------------:|-------:|----------:|
| LoggerMessage_NoParameters |     False |    14.01 ns |  0.299 ns |  0.546 ns |    13.75 ns |      - |         - |
|  Interpolated_NoParameters |     False |    17.03 ns |  0.329 ns |  0.512 ns |    16.71 ns |      - |         - |
|    LoggerMessage_OneString |     False |    13.29 ns |  0.083 ns |  0.077 ns |    13.31 ns |      - |         - |
|     Interpolated_OneString |     False |    16.71 ns |  0.064 ns |  0.060 ns |    16.70 ns |      - |         - |
|   LoggerMessage_TwoStrings |     False |    15.00 ns |  0.325 ns |  0.487 ns |    14.73 ns |      - |         - |
|    Interpolated_TwoStrings |     False |    16.67 ns |  0.072 ns |  0.067 ns |    16.68 ns |      - |         - |
|  LoggerMessage_ManyStrings |     False |    16.90 ns |  0.367 ns |  0.716 ns |    16.69 ns |      - |         - |
|   Interpolated_ManyStrings |     False |    17.29 ns |  0.367 ns |  0.539 ns |    17.00 ns |      - |         - |
| LoggerMessage_NoParameters |      True |   419.35 ns |  8.270 ns | 11.040 ns |   413.00 ns | 0.1011 |     424 B |
|  Interpolated_NoParameters |      True |   503.22 ns |  8.817 ns | 16.774 ns |   495.24 ns | 0.2499 |   1,048 B |
|    LoggerMessage_OneString |      True | 1,901.25 ns | 36.690 ns | 47.707 ns | 1,887.71 ns | 0.1316 |     552 B |
|     Interpolated_OneString |      True |   497.26 ns |  5.066 ns |  4.491 ns |   496.02 ns | 0.2499 |   1,048 B |
|   LoggerMessage_TwoStrings |      True | 3,085.64 ns | 14.670 ns | 12.250 ns | 3,083.82 ns | 0.1488 |     624 B |
|    Interpolated_TwoStrings |      True |   525.69 ns |  7.054 ns |  6.253 ns |   523.70 ns | 0.2556 |   1,072 B |
|  LoggerMessage_ManyStrings |      True | 7,977.52 ns | 71.244 ns | 59.492 ns | 7,958.09 ns | 0.2899 |   1,248 B |
|   Interpolated_ManyStrings |      True |   655.76 ns | 12.924 ns | 12.694 ns |   653.05 ns | 0.2899 |   1,216 B |

 */
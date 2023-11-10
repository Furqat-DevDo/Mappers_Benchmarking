using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Mappers_Test.Tests;

IConfig config = DefaultConfig.Instance
    .With(Job.InProcess);

var summary = BenchmarkRunner.Run<MappersBenchmark>();


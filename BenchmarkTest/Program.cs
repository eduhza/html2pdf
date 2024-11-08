// See https://aka.ms/new-console-template for more information
using BenchmarkDotNet.Running;
using BenchmarkTest;

IronPdf.License.LicenseKey = "IRONSUITE.EDUARDOARRUDA.MIGRATE.INFO.28696-917FA71F08-P5KV2-I73BVCHBE2IL-WSX6MOKM4LAJ-4FEL5LCWP2BQ-UBQXQYPRSDYT-PMCX47D7ME23-IARLIMZ7HKBT-YXITTD-TQ3E524Q7FCOEA-DEPLOYMENT.TRIAL-MCGQHZ.TRIAL.EXPIRES.07.DEC.2024";

var summary = BenchmarkRunner.Run<PdfBenchmarks>();


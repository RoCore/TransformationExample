
using FluentAssertions;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Infrastructure.Files;
using Xunit.Abstractions;

namespace TestProject1;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper) => _testOutputHelper = testOutputHelper;

    [Fact]
    public void ToObject()
    {
        var watch = Stopwatch.StartNew();
        const string data = "05079160135100355                          98610000    007708531015694      MUSTER             JOHN                          JUNGMANNSTRASSE 6               DEU24762 RENDSBURG                            ";
        var rawData = System.Text.Encoding.UTF8.GetBytes(data);
        File.WriteAllBytes("test.txt", new byte[] { 50, 5, 15, (byte)'\n', 59, 5, 15 });
        var byteLength = Marshal.SizeOf(typeof(S50));
        var buffer = Marshal.AllocHGlobal(byteLength);
        Marshal.Copy(rawData, 0, buffer, byteLength);
        var retobj = (S50)Marshal.PtrToStructure(buffer, typeof(S50))!;
        Marshal.FreeHGlobal(buffer);
        watch.Stop();
        _testOutputHelper.WriteLine(watch.Elapsed.ToString());
        retobj.Id.Should().Be("05079160135100355                          ");
    }

    [Fact]
    public void DateTime()
    {
        var data = "20220609220600043000000000011";
        var x = DateTimeOffset.Now;
        var datetimeOffset = DateTimeOffset.ParseExact(data.Substring(0, 21), "yyyyMMddHHmmssFFFFFFF", CultureInfo.InvariantCulture);
        _testOutputHelper.WriteLine(datetimeOffset.ToString());
    }
}
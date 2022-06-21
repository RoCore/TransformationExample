using System.Runtime.InteropServices;
using static Infrastructure.S59LayoutDefinition;

namespace Infrastructure.Files;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Size = 53)]
public class S59 : IStructLayout
{
    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = FormatIdSize)]
    public string? FormatId { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = S50IdSize)]
    public string? S50Id { get; set; }

    [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = Number1Size)]
    public string? Number1 { get; set; }
}

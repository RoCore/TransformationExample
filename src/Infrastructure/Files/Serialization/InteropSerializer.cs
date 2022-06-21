using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Files.Serialization;

public class InteropSerializer : IInteropSerializer
{
    public T Deserialize<T>(byte[] data) where T : IStructLayout
    {
        var buffer = IntPtr.Zero;
        try
        {
            var byteLength = Marshal.SizeOf(typeof(T));
            buffer = Marshal.AllocHGlobal(byteLength);
            Marshal.Copy(data, 0, buffer, byteLength);
            var result = (T)Marshal.PtrToStructure(buffer, typeof(T))!;
            return result;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }
}
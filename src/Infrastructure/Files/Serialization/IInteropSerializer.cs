namespace Infrastructure.Files.Serialization
{
    public interface IInteropSerializer
    {
        /// <summary>
        /// Deserialize to an object using marshaling 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        T Deserialize<T>(byte[] data) where T : IStructLayout;
    }
}

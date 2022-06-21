using Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;

namespace Infrastructure.Files.Serialization
{
    /// <inheritdoc />
    public class Deserializer : IDeserializer
    {
        private enum FileFormat
        {
            NonFixedLengthFile,
            S50,
            S59
        }

        private readonly IInteropSerializer _interopSerializer;
        private readonly IOptions<DeserializationOptions> _options;
        private readonly ILogger<Deserializer> _logger;

        public Deserializer(IInteropSerializer interopSerializer, IOptions<DeserializationOptions> options, ILogger<Deserializer> logger)
        {
            _interopSerializer = interopSerializer;
            _options = options;
            _logger = logger;
        }

        /// <inheritdoc />
        public IEnumerable<IStructLayout> Read(Stream file, CancellationToken cancellationToken)
        {
            using var stream = new StreamReader(file);

            var lineNumber = 0;

            while (!stream.EndOfStream && !cancellationToken.IsCancellationRequested)
            {
                lineNumber++;
                var line = stream.ReadLine();

                if (line == null)
                {
                    break;
                }

                var rawLine = Encoding.UTF8.GetBytes(line);
                var format = GetFixedLengthFormat(line);

                // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
                switch (format)
                {
                    case FileFormat.NonFixedLengthFile:
                        _logger.LogWarning($"Cannot deserialize object on line {lineNumber}. Supports only fixed length files");
                        break;
                    case FileFormat.S50:
                        yield return _interopSerializer.Deserialize<S50>(rawLine);
                        break;
                    case FileFormat.S59:
                        yield return _interopSerializer.Deserialize<S59>(rawLine);
                        break;
                }
            }
        }

        private FileFormat GetFixedLengthFormat(string data)
        {
            if (data.Length > 3)
            {
                var header = data[..3];

                if (header.SequenceEqual(_options.Value.S50FileIdentifier!))
                {
                    return FileFormat.S50;
                }

                if (header.SequenceEqual(_options.Value.S59FileIdentifier!))
                {
                    return FileFormat.S59;
                }
            }

            return FileFormat.NonFixedLengthFile;
        }
    }
}

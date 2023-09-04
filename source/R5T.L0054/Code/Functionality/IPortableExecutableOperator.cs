using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Threading.Tasks;

using R5T.T0132;
 

namespace R5T.L0054
{
    [FunctionalityMarker]
    public partial interface IPortableExecutableOperator : IFunctionalityMarker
    {
        public async Task<PEReader> Get_Reader(string dllFilePath)
        {
            var bytes = await Instances.FileOperator.Read_Bytes(dllFilePath);

            var immutableBytes = ImmutableArray.Create(bytes);

            var reader = new PEReader(immutableBytes);
            return reader;
        }

        public PEReader Get_Reader_Synchronous(string dllFilePath)
        {
            // No using, the filesteram will be disposed of by the reader.
            var fileStream = Instances.FileStreamOperator.Open_Read(dllFilePath);

            var reader = new PEReader(fileStream);
            return reader;
        }

        public MetadataReaderWrapper Get_MetadataReaderWrapper(string dllFilePath)
        {
            var reader = this.Get_Reader_Synchronous(dllFilePath);

            var metadataReader = reader.GetMetadataReader();

            var output = new MetadataReaderWrapper(
                metadataReader,
                reader);

            return output;
        }

        /// <summary>
        /// Gets the full assembly name ("R5T.T0213, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null").
        /// Note, this is identical functionality to to <see cref="System.Reflection.AssemblyName.GetAssemblyName(string)"/>.
        /// </summary>
        public string Get_FullAssemblyName(PEReader reader)
        {
            var metadataReader = reader.GetMetadataReader();



            var assemblyDefinition = metadataReader.GetAssemblyDefinition();

            var assemblyName = metadataReader.GetString(assemblyDefinition.Name);
            var version = Instances.VersionOperator.ToString_Default(
                assemblyDefinition.Version);
            var culture = assemblyDefinition.Culture.IsNil
                ? "neutral"
                : metadataReader.GetString(assemblyDefinition.Culture)
                ;
            var publicKeyToken = assemblyDefinition.PublicKey.IsNil
                ? "null"
                : GetPublicKeyToken(
                    assemblyDefinition,
                    metadataReader)
                ;
            //var publicKeyToken = assemblyDefinition.Name.

            static string GetPublicKeyToken(
                AssemblyDefinition assemblyDefinition,
                MetadataReader metadataReader)
            {
                var bytes160 = metadataReader.GetBlobBytes(assemblyDefinition.PublicKey);

                var hash = SHA1.HashData(bytes160);

                byte[] token = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    token[i] = hash[^(i + 1)];
                }

                var output = BitConverter.ToString(token).Replace("-", "").ToLowerInvariant() ;
                return output;
            }

            var output = Instances.AssemblyNameOperator.Get_FullAssemblyName(
                assemblyName,
                version,
                culture,
                publicKeyToken);

            return output;
        }

        public async Task<string> Get_FullAssemblyName(string dllFilePath)
        {
            var output = await this.In_ReaderContext(
                dllFilePath,
                this.Get_FullAssemblyName);

            return output;
        }

        public void In_MetadataReaderContext(
            string dllFilePath,
            Action<MetadataReader> action)
        {
            using var wrapper = this.Get_MetadataReaderWrapper(dllFilePath);

            action(wrapper.MetadataReader);
        }

        public async Task In_MetadataReaderContext(
            string dllFilePath,
            Func<MetadataReader, Task> action)
        {
            using var wrapper = this.Get_MetadataReaderWrapper(dllFilePath);

            await action(wrapper.MetadataReader);
        }

        public void In_MetadataReaderWrapperContext(
            string dllFilePath,
            Action<MetadataReaderWrapper> action)
        {
            using var wrapper = this.Get_MetadataReaderWrapper(dllFilePath);

            action(wrapper);
        }

        public async Task In_MetadataReaderWrapperContext(
            string dllFilePath,
            Func<MetadataReaderWrapper, Task> action)
        {
            using var wrapper = this.Get_MetadataReaderWrapper(dllFilePath);

            await action(wrapper);
        }

        public void In_ReaderContext_Synchronous(
            string dllFilePath,
            Action<PEReader> action)
        {
            using var reader = this.Get_Reader_Synchronous(dllFilePath);

            action(reader);
        }

        public async Task In_ReaderContext_Synchronous(
            string dllFilePath,
            Func<PEReader, Task> action)
        {
            using var reader = this.Get_Reader_Synchronous(dllFilePath);

            await action(reader);
        }

        public TOut In_ReaderContext_Synchronous<TOut>(
            string dllFilePath,
            Func<PEReader, TOut> action)
        {
            using var reader = this.Get_Reader_Synchronous(dllFilePath);

            var output = action(reader);
            return output;
        }

        public async Task<TOut> In_ReaderContext<TOut>(
            string dllFilePath,
            Func<PEReader, TOut> action)
        {
            using var reader = await this.Get_Reader(dllFilePath);

            var output = action(reader);
            return output;
        }

        public async Task<TOut> In_ReaderContext_Synchronous<TOut>(
            string dllFilePath,
            Func<PEReader, Task<TOut>> action)
        {
            using var reader = this.Get_Reader_Synchronous(dllFilePath);

            var output = await action(reader);
            return output;
        }
    }
}

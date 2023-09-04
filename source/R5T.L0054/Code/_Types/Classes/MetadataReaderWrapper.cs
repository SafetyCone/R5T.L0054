using System;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;


namespace R5T.L0054
{
    public class MetadataReaderWrapper : IDisposable
    {
        #region IDisposable

        private bool zDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!zDisposed)
            {
                if (disposing)
                {
                    this.Reader.Dispose();
                }

                zDisposed = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }

        #endregion


        public MetadataReader MetadataReader { get; private set; }
        public PEReader Reader { get; private set; }


        public MetadataReaderWrapper(
            MetadataReader metadataReader,
            PEReader reader)
        {
            this.MetadataReader = metadataReader;
            this.Reader = reader;
        }
    }
}

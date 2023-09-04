using System;


namespace R5T.L0054
{
    public class FileOperator : IFileOperator
    {
        #region Infrastructure

        public static IFileOperator Instance { get; } = new FileOperator();


        private FileOperator()
        {
        }

        #endregion
    }
}

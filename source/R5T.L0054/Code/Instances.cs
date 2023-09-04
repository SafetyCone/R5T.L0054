using System;


namespace R5T.L0054
{
    public static class Instances
    {
        public static IAssemblyNameOperator AssemblyNameOperator => L0054.AssemblyNameOperator.Instance;
        public static IBuiltInTypes BuiltInTypes => L0054.BuiltInTypes.Instance;
        public static IFileOperator FileOperator => L0054.FileOperator.Instance;
        public static IFileStreamOperator FileStreamOperator => L0054.FileStreamOperator.Instance;
        public static IVersionOperator VersionOperator => L0054.VersionOperator.Instance;
    }
}
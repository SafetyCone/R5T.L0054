using System;


namespace R5T.L0054
{
    public class AssemblyNameOperator : IAssemblyNameOperator
    {
        #region Infrastructure

        public static IAssemblyNameOperator Instance { get; } = new AssemblyNameOperator();


        private AssemblyNameOperator()
        {
        }

        #endregion
    }
}

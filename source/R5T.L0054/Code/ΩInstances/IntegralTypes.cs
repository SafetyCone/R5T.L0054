using System;


namespace R5T.L0054
{
    public class IntegralTypes : IIntegralTypes
    {
        #region Infrastructure

        public static IIntegralTypes Instance { get; } = new IntegralTypes();


        private IntegralTypes()
        {
        }

        #endregion
    }
}

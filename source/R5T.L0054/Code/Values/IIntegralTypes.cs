using System;

using R5T.T0131;


namespace R5T.L0054
{
    /// <inheritdoc cref="L0053.IIntegralTypes"/>
    [ValuesMarker]
    public partial interface IIntegralTypes : IValuesMarker,
        L0053.IIntegralTypes
    {
        /// <inheritdoc cref="IBuiltInTypes.NInteger"/>
        public Type NInteger => Instances.BuiltInTypes.NInteger;

        /// <inheritdoc cref="IBuiltInTypes.NUInteger"/>
        public Type NUInteger => Instances.BuiltInTypes.NUInteger;
    }
}

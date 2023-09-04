using System;

using R5T.T0131;


namespace R5T.L0054
{
    /// <inheritdoc cref="L0053.IBuiltInTypes"/>
    [ValuesMarker]
    public partial interface IBuiltInTypes : IValuesMarker,
        L0053.IBuiltInTypes
    {
        /// <summary>
        /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.intptr?view=net-7.0"/>
        /// </summary>
        public Type NInteger => typeof(nint);

        /// <summary>
        /// <see href="https://learn.microsoft.com/en-us/dotnet/api/system.uintptr?view=net-7.0"/>
        /// </summary>
        public Type NUInteger => typeof(nuint);
    }
}

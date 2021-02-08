namespace Hazel.Core.Infrastructure.Mapper
{
    /// <summary>
    /// Mapper profile registrar interface.
    /// </summary>
    public interface IOrderedMapperProfile
    {
        /// <summary>
        /// Gets the Order
        /// Gets order of this configuration implementation.
        /// </summary>
        int Order { get; }
    }
}

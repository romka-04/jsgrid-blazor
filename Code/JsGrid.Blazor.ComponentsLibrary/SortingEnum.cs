using JsGrid.Blazor.ComponentsLibrary.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [JsonEnumConverter]
    public enum SortingEnum 
        : byte
    {
        /// <summary>
        /// string sorter.
        /// </summary>
        [JsonStringEnumMember("string")]
        String,
        /// <summary>
        /// Number sorter.
        /// </summary>
        [JsonStringEnumMember("number")]
        Number,
        /// <summary>
        /// Date sorter.
        /// </summary>
        [JsonStringEnumMember("date")]
        Date,
        /// <summary>
        /// Numbers are parsed before comparison.
        /// </summary>
        [JsonStringEnumMember("numberAsString")]
        NumberAsString,
        /// <summary>
        /// Custom sorting strategy.
        /// </summary>
        [JsonStringEnumMember("custom")]
        Custom
    }
}
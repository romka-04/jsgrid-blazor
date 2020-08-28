using JsGrid.Blazor.ComponentsLibrary.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [JsonEnumConverter]
    public enum AlignEnum 
        : byte
    {
        None,
        [JsonStringEnumMember("right")]
        Right,
        [JsonStringEnumMember("left")]
        Left,
        [JsonStringEnumMember("center")]
        Center
    }
}
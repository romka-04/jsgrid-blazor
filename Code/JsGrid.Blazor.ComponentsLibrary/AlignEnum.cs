using JsGrid.Blazor.ComponentsLibrary.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary
{
    [JsonEnumConverter]
    public enum AlignEnum 
        : byte
    {
        [JsonStringEnumMember("center")]
        Center,
        [JsonStringEnumMember("right")]
        Right,
        [JsonStringEnumMember("left")]
        Left,
    }
}
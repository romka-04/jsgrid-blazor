using System;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    [AttributeUsage(AttributeTargets.Field)]
    class JsonValueAttribute
        : Attribute
    {
        public string Value { get; }

        public JsonValueAttribute(string value)
        {
            Value = value;
        }
    }
}
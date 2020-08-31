using System;
using System.Text.Json.Serialization;

namespace JsGrid.Blazor.ComponentsLibrary.Serialization
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    class JsonStringEnumMemberAttribute
        : JsonAttribute
    {
        /// <summary>
        /// The name of the enum member.
        /// </summary>
        public string Name { get; }

        public JsonStringEnumMemberAttribute(string name)
        {
            Name = name;
        }
    }
}
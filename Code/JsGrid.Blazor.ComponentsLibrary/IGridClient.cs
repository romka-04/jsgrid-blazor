using System;
using System.Collections.Generic;

namespace JsGrid.Blazor.ComponentsLibrary
{
    public interface IGridClient<T>
    {
        public IList<T> Values { get; }
        public JsGridSettings Settings { get; }
    }

    class GridClient<T>
        : IGridClient<T>
    {
        private readonly GridClientBuilder<T> _builder;
        private JsGridSettings _settings;

        public IList<T> Values => _builder.Data;

        public JsGridSettings Settings
        {
            get { return _settings ??= _builder.BuildJsGridSettings(); }
        }

        public GridClient(GridClientBuilder<T> builder)
        {
            _builder = builder
                ?? throw new ArgumentNullException(nameof(builder));
        }
    }
}
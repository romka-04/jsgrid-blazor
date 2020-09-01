# jsGrid-blazor
Blazor Component wrapper for [jsGrid](http://js-grid.com/getting-started/) (Lightweight Grid jQuery Plugin)

It supports .NET Core 3.1 and Blazor WebAssembly 3.2.0

## Install

```<language>
nuget install JsGrid.Blazor
```

## Getting Started

1. Ensure that jQuery v1.8.3 or later is included.

```html
<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js" integrity="sha384-DfXdz2htPH0lsSSs5nCTpuj/zy4C+OGpamoFVy38MVBnE+IbbVYUew+OrCXaRkfj" crossorigin="anonymous"></script>
```

2. Include jsGrid script, "jsgrid-blazor.js" and css stylesheet files into your web page.

```html
<link href="_content/JsGrid.Blazor.ComponentsLibrary/jsgrid.min.css" rel="stylesheet" />
<link href="_content/JsGrid.Blazor.ComponentsLibrary/jsgrid-theme.min.css" rel="stylesheet" />
...
<script src="_content/JsGrid.Blazor.ComponentsLibrary/jsgrid.min.js"></script>
<script src="_content/JsGrid.Blazor.ComponentsLibrary/jsgrid-blazor.js"></script>
```

3. Include JsGrid.Blazor namespace into file _Imports.razor.

```razor
@using JsGrid.Blazor.ComponentsLibrary
```

4. Add a div for the grid to your web page markup and razor code required to render the grid.

```razor
@page "/"
@inject NavigationManager NavigationManager

@if (_task.IsCompleted)
{
    <JsGridBlazor T="Client" Value="_gridClient"></JsGridBlazor>
}
else
{
    <text>Loading...</text>
}

@code
{
    private readonly Client[] _clients = new[]
    {
        new Client("Otto Clay", 25, 1, "Ap #897-1459 Quam Avenue", false),
        new Client("Connor Johnston", 45, 2, "Ap #370-4647 Dis Av.", true),
        new Client("Lacey Hess", 29, 3, "Ap #365-8835 Integer St.", false),
        new Client("Timothy Henson", 56, 1, "911-5143 Luctus Ave", true),
        new Client("Ramona Benton", 32, 3, "Ap #614-689 Vehicula Street", false),
    };

    private readonly Country[] _countries = new[]
    {
        new Country(0, ""),
        new Country(1, "United States"),
        new Country(2, "Canada"),
        new Country(3, "United Kingdom"),
    };

    private Task _task;
    private IGridClient<Client> _gridClient;

    public static Action<IGridFieldCollection<Client>> Fields = c =>
    {
        c.Add(x => x.Name, width: 150);
        c.Add(x => x.Age, width: 50);
        c.Add(x => x.Address, width: 200);
        c.Add(x => x.Married, "Is Married" );
        c.AddControl();
    };

    protected override async Task OnParametersSetAsync()
    {
        _gridClient = new GridClientBuilder<Client>(Fields)
            .WithStaticData(_clients)
            .WithDimensions("100%", "500px")
            .Build();
        _task = Task.CompletedTask;
        await _task;
    }
}
```

## ToDo list

1. Implement other grid fields such as **select**, **textarea** and **control**.
2. Implement code that requests data from OData service with ajax. Add backend support of this functionality if needed.
3. Repeat all Demos at the jsGrid website. 

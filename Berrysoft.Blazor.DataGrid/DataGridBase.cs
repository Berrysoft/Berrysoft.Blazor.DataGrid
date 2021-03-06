﻿using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Berrysoft.Blazor.DataGrid
{
    public class DataGridBase : ComponentBase
    {
        [Parameter]
        public IEnumerable? Items { get; set; }

        protected IEnumerable? DisplayItems { get; set; }

        [Parameter]
        public RenderFragment? Headers { get; set; }

        public event EventHandler<string>? ColumnSorted;

        protected override void OnParametersSet()
        {
            DisplayItems = Items;
        }

        public void SetColumnSortDirection(string column, DataGridSortDirection direction, IComparer? comparer)
        {
            if (direction != DataGridSortDirection.None)
            {
                DisplayItems = Items?.OfType<object>()?.OrderBy(item => item.GetType().GetProperty(column)?.GetValue(item), direction, comparer);
            }
            else
            {
                DisplayItems = Items;
            }
            ColumnSorted?.Invoke(this, column);
            StateHasChanged();
        }
    }
}

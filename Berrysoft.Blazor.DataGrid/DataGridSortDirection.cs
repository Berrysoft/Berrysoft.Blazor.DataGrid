using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Berrysoft.Blazor.DataGrid
{
    public enum DataGridSortDirection
    {
        None,
        Ascending,
        Descending
    }

    static class DataGridSortHelper
    {
        public static IEnumerable<T>? OrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, DataGridSortDirection direction, IComparer? comparer)
        {
            return direction switch
            {
                DataGridSortDirection.Ascending => source.OrderBy(keySelector, new ComparerWrapper<TKey>(comparer)),
                DataGridSortDirection.Descending => source.OrderByDescending(keySelector, new ComparerWrapper<TKey>(comparer)),
                _ => source
            };
        }
    }

    class ComparerWrapper<T> : IComparer<T>
    {
        private readonly IComparer comparer;

        public ComparerWrapper(IComparer? comparer) => this.comparer = comparer ?? Comparer<T>.Default;

        public int Compare([AllowNull] T x, [AllowNull] T y) => comparer.Compare(x, y);
    }
}

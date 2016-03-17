using System.Collections.Generic;
using System.Linq;

namespace Sample.Service.Infrastructure.Extensions
{
    public static class CollectionExtensions
    {
        public static bool None<T>(this IEnumerable<T> items)
        {
            return !items.Any();
        }
    }
}
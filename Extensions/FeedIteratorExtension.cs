using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace HPW.Extensions
{
    public static class FeedIteratorExtensions
    {
        public static async Task<IEnumerable<T>> ReadAllAsync<T>(this FeedIterator<T> iterator)
        {
            var result = new List<T>();
            while (iterator.HasMoreResults)
            {
                var items = await iterator.ReadNextAsync();
                result.AddRange(items);
            }
            return result;
        }
    }
}
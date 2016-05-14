using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SimpleDataGenerator.Json.Extensions
{
    public static class JsonExtensions
    {
        public static string DumpAsJson<TEntity>(this TEntity entity)
        {
            return JsonConvert.SerializeObject(entity, Formatting.Indented);
        }

        public static string DumpAsJson<TEntity>(this IEnumerable<TEntity> entities)
        {
            return JsonConvert.SerializeObject(entities, Formatting.Indented);
        }
    }
}

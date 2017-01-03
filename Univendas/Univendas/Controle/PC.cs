using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Univendas.Controle
{
    public static class PC
    {
        static IQueryable<T> WhereLike<T>(this IQueryable<T> source,
        string propertyOrFieldName, string pattern, char escapeCharacter)
        {
            var param = Expression.Parameter(typeof(T), "row");
            var body = Expression.Call(
                null,
                typeof(SqlMethods).GetMethod("Like",
                    new[] { typeof(string), typeof(string), typeof(char) }),
                Expression.PropertyOrField(param, propertyOrFieldName),
                Expression.Constant(pattern, typeof(string)),
                Expression.Constant(escapeCharacter, typeof(char)));
            var lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return source.Where(lambda);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    internal static class MyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        internal static IEnumerable<TResult> LeftOuterLoopJoin<TA, TB, TResult>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TB, bool> match,
            Func<TA, TB, TResult> projection)
        {
            return a.Select(item => projection(item, b.FirstOrDefault(itemb => match(item, itemb))));
        }


        /// <summary>
        /// 
        /// </summary>
        internal static IEnumerable<TResult> BetterLeftOuterJoin<TA, TB, TKey, TResult>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TKey> keySelectorA,
            Func<TB, TKey> keySelectorB,
            Func<TA, TB, TKey, TResult> projection,            
            TB defaultB = default(TB))
        {
            var lookupB = b.ToLookup(keySelectorB);

            return a.Select(itemA => new { Key = keySelectorA(itemA), A = itemA })
                .SelectMany(left => lookupB[left.Key].DefaultIfEmpty(defaultB).Select(itemB => projection(left.A, itemB, left.Key)));
        }

        /// <summary>
        /// copied from http://stackoverflow.com/questions/5489987/linq-full-outer-join
        /// </summary>
        internal static IEnumerable<TResult> FullOuterJoin<TA, TB, TKey, TResult>(
            this IEnumerable<TA> a,
            IEnumerable<TB> b,
            Func<TA, TKey> selectKeyA,
            Func<TB, TKey> selectKeyB,
            Func<TA, TB, TKey, TResult> projection,
            TA defaultA = default(TA),
            TB defaultB = default(TB),
            IEqualityComparer<TKey> cmp = null)
        {
            cmp = cmp ?? EqualityComparer<TKey>.Default;
            var alookup = a.ToLookup(selectKeyA, cmp);
            var blookup = b.ToLookup(selectKeyB, cmp);

            var keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            var join = from key in keys
                       from xa in alookup[key].DefaultIfEmpty(defaultA)
                       from xb in blookup[key].DefaultIfEmpty(defaultB)
                       select projection(xa, xb, key);

            return join;
        }


    }
    
}

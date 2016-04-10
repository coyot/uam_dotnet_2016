using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Program
    {
        /// <summary>
        /// Zadania: 
        /// * Zapytanie, które policzy ile razy tradeów spłynęło z danego source systemu
        /// * Wypisze wszystkie nieobsługiwane waluty (których nie ma w tabeli FxSpot)
        /// * Wykorzystując FullOuterJoin dokonać pełnego złączenia danych Trades i SoruceSystems
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var trades = TestDataProvider.Trades;
            var fx = TestDataProvider.FxSpot;
            var ss = TestDataProvider.SourceSystems;

            Console.WriteLine("{0}", string.Join(Environment.NewLine, trades));
            Console.WriteLine();


            var ssJoin = trades.LeftOuterLoopJoin(ss,
                (t, s) => t.SourceSystemId == s.Id,
                (t, s) => new { Trade = t, SourceSystem = s })
                .Where(o => o.SourceSystem != null)
                .Select(o => string.Format("{0} comes from {1}", o.Trade, o.SourceSystem.Name));


            Console.WriteLine("{0}", string.Join(Environment.NewLine, ssJoin));
            Console.WriteLine();


            var converted = trades.BetterLeftOuterJoin(fx, t => t.MtmCcy, f => f.Key,
                (t, f, ccy) => new Trade(t.Id, t.SourceSystemId, t.Mtm / f.Value, "USD"),
                new KeyValuePair<string, decimal>("USD", 1));

            Console.WriteLine("{0}", string.Join(Environment.NewLine, converted));
            Console.WriteLine();

            Console.ReadKey();



        }
    }
}

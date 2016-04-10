using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public static class TestDataProvider
    {

        public static IDictionary<string, decimal> FxSpot
        {
            get
            {
                return new Dictionary<string, decimal> {
                    {"USD", 1.00m},
                    {"EUR", 0.87m},
                    {"JPY", 108m},
                    {"AUD", 0.755m},
                    {"CHF", 0.95m},
                    {"CAD", 1.29m}
                };
            }
        }

        public static ICollection<Trade> Trades
        {
            get
            {
                return new List<Trade> {
                    new Trade("T1", 1, 1e6m, "USD"),
                    new Trade("T2", 1, 1e6m, "ZAR"),
                    new Trade("T3", 1, 1e6m, "JPY"),
                    new Trade("T4", 2, 1e6m, "AUD"),
                    new Trade("T5", 3, 1e6m, "CHF"),
                    new Trade("T6", 3, 1e6m, "CAD"),
                    new Trade("T7", 4, 1e6m, "PLN"),
                    new Trade("T8", 4, 1e6m, "USD"),
                    new Trade("T9", 5, 1e6m, "EUR")
                };
            }
        }

        public static ICollection<SourceSystem> SourceSystems
        {
            get
            {
                return new List<SourceSystem> {
                    new SourceSystem { Id = 1, Name = "SS Alpha"},
                    new SourceSystem { Id = 2, Name = "SS Bravo"},
                    new SourceSystem { Id = 3, Name = "SS Charlie"},
                    new SourceSystem { Id = 4, Name = "SS Delta"},
                    new SourceSystem { Id = 6, Name = "SS Echo"},
                    new SourceSystem { Id = 7, Name = "SS Foxtrot"},
                };
            }
        }


    }
}

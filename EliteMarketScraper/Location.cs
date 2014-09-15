using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteMarketScraper
{
    public class Location
    {
        public string LocationName { get; set; }
        public string SystemName { get; set; }
        public string Jurisdiction { get; set; }
        public string Government { get; set; }
        public string Economy { get; set; }
        public string Distance { get; set; }

        public Location(string locationName, string systemName, string jurisdiction, string government, string economy, string distance)
        {
            this.LocationName = locationName;
            this.SystemName = systemName;
            this.Jurisdiction = jurisdiction;
            this.Government = government;
            this.Economy = economy;
            this.Distance = distance;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteMarketScraper
{
    public class Item
    {
        public int ItemDatabaseId { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public int Demand { get; set; }
        public int DemandLevel { get; set; }
        public int CargoStock { get; set; }
        public double CargoItemBoughtFor { get; set; }
        public int StationStock { get; set; }
        public int StationStockLevel { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string ItemInfo { get; set; }
        public double GalacticAverage { get; set; }
        public Location Location { get; set; }

        public Item(int itemDbId, double buyPrice, double sellPrice, int demand, int demandLeven, int cargoStock, double cargoItemBoughtFor, int stationStock, int stationstockLevel, string categoryName, string itemName, string itemInfo, double galacticAverage, Location location)
        {
            this.ItemDatabaseId = itemDbId;
            this.BuyPrice = buyPrice;
            this.SellPrice = sellPrice;
            this.Demand = demand;
            this.DemandLevel = DemandLevel;
            this.CargoStock = CargoStock;
            this.CargoItemBoughtFor = cargoItemBoughtFor;
            this.StationStock = stationStock;
            this.StationStockLevel = stationstockLevel;
            this.CategoryName = categoryName;
            this.ItemName = itemName;
            this.ItemInfo = itemInfo;
            this.GalacticAverage = galacticAverage;
            this.Location = location;
        }

        public void Dump()
        {
            try
            {
                System.IO.File.AppendAllText("C:\\dump\\dump.txt", this.ItemDatabaseId + ";" + this.BuyPrice + ";" + this.SellPrice + ";" + this.Demand + ";" + this.DemandLevel + ";" + this.CargoStock + ";" + this.CargoItemBoughtFor + ";" + this.StationStock + ";" + this.StationStockLevel + ";" + this.CategoryName + ";" + this.ItemName + ";" + this.ItemInfo + ";" + this.GalacticAverage + (this.Location != null ? ";" + this.Location.SystemName + ";" + this.Location.LocationName : "") + Environment.NewLine);
            }
            catch(Exception)
            {

            }
        }
    }
}

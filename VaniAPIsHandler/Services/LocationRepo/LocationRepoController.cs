using VaniAPIsHandler.CacheController;
using VaniAPIsHandler.MarketPlusWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VaniAPIsHandler.Models.Location;
using VaniAPIsHandler.MarketPlusWS;

namespace VaniAPIsHandler.Services.LocationRepo
{
    public class LocationRepoController
    {
        public MarketPlusSoapClient service = new MarketPlusSoapClient();

        public List<Location> getListLocation(int page, int size)
        {
            return DataAccessProvider.Instance().getListLocation(page, size);
        }
    }
}
using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IStyleDataAccess
    {
        Style CreateStyle(Style style);
        Style GetStyleById(int styleId);
        Style GetStyleByName(string styleName);
        List<Style> GetAllStyles();
        bool StyleWithThisNameExists(string styleName);
    }
}

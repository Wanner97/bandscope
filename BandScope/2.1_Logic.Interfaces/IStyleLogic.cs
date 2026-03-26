using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IStyleLogic
    {
        Style CreateStyle(Style style);
        List<Style> GetAllStyles();
    }
}

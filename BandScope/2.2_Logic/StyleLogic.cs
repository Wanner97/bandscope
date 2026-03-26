using BandScope.Common;
using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class StyleLogic : IStyleLogic
    {
        private readonly IStyleDataAccess _styleDataAccess;

        public StyleLogic(IStyleDataAccess styleDataAccess)
        {
            _styleDataAccess = styleDataAccess;
        }

        public Style CreateStyle(Style style)
        {
            if (string.IsNullOrWhiteSpace(style.Name))
            {
                return _styleDataAccess.GetStyleById(Const.DefaultUnknownId);
            }

            new StyleValidator(false).ValidateAndThrow(style);

            if (StyleWithThisNameExists(style.Name))
            {
                return _styleDataAccess.GetStyleByName(style.Name.Trim());
            }

            return _styleDataAccess.CreateStyle(style);
        }

        public List<Style> GetAllStyles()
        {
            return _styleDataAccess.GetAllStyles();
        }

        public bool StyleWithThisNameExists(string styleName)
        {
            return _styleDataAccess.StyleWithThisNameExists(styleName.Trim());
        }
    }
}

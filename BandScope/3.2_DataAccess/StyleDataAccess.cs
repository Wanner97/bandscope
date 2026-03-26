using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class StyleDataAccess : IStyleDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public StyleDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Style CreateStyle(Style style)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Styles.Add(style);
                context.SaveChanges();

                return style;
            }
        }

        public Style GetStyleById(int styleId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Styles.Find(styleId);
            }
        }

        public Style GetStyleByName(string styleName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Styles
                    .Where(s => s.Name.Equals(styleName))
                    .ToList()
                    .FirstOrDefault();
            }
        }

        public List<Style> GetAllStyles()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Styles.ToList();
            }
        }

        public bool StyleWithThisNameExists(string styleName)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var foundStyle = context.Styles
                    .Where(s => s.Name.Equals(styleName))
                    .ToList()
                    .FirstOrDefault();

                if (foundStyle == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}

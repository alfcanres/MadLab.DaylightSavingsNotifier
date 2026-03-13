using DSTN.Domain.Entities;
using DSTN.Domain.Interfaces;

namespace DSTN.Application.Services.EmailConfigurator.Filters
{
    internal class NameFilter : IQueryFilter<EmailConfiguration>
    {
        private readonly string _name;

        public NameFilter(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name), "Name cannot be null");
        }

        public IQueryable<EmailConfiguration> ApplyFilter(IQueryable<EmailConfiguration> queryable)
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                return queryable;
            }
            return queryable.Where(x => x.Name.ToLower().Contains(_name.ToLower()));
        }
    }
}

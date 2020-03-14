using OppJar.Core.ContextFactory;
using OppJar.Domain;

namespace OppJar.Core.CommandHandlers
{
    public abstract class CommandHandlerBase
    {
        protected readonly OppJarContext _context;
        protected readonly MongoFactory _mongoFactory;

        public CommandHandlerBase(OppJarContext context, MongoFactory mongoFactory)
        {
            _context = context;
            _mongoFactory = mongoFactory;
        }
    }
}

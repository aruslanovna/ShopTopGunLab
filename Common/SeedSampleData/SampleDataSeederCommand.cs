using Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.SeedSampleData
{
   
        public class SampleDataSeederCommand : IRequest
        {
        }

        public class SampleDataSeederCommandHandler : IRequestHandler<SampleDataSeederCommand>
        {
            private readonly IShopDbContext _context;
           

            public SampleDataSeederCommandHandler(IShopDbContext context)
            {
                _context = context;              
            }

            public async Task<Unit> Handle(SampleDataSeederCommand request, CancellationToken cancellationToken)
            {
                var seeder = new SampleDataSeeder(_context);

                await seeder.SeedAllAsync(cancellationToken);

                return Unit.Value;
            }
        }
    
}

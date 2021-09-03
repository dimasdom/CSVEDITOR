using CSVEDITOR.MediatR.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class AddCsvDataToDbHandler : IRequestHandler<AddCsvDataToDbCommand, bool>
    {
        public Task<bool> Handle(AddCsvDataToDbCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using CSVEDITOR.MediatR.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class DeleteTransactionHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        public Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

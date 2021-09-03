using CSVEDITOR.MediatR.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class EditTransactionHandler : IRequestHandler<EditTransactionCommand, bool>
    {
        public Task<bool> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

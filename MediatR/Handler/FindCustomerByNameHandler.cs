using CSVEDITOR.MediatR.Command;
using CSVEDITOR.Models.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class FindCustomerByNameHandler : IRequestHandler<FindCustomerByNameCommand, TransactionModel>
    {
        public Task<TransactionModel> Handle(FindCustomerByNameCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

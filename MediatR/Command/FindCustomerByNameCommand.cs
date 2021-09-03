using CSVEDITOR.Models.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Command
{
    public class FindCustomerByNameCommand : IRequest<TransactionModel>
    {
    }
}

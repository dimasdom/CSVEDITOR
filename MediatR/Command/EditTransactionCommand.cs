using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Command
{
    public class EditTransactionCommand :IRequest<bool> 
    {
    }
}

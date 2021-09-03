using CSVEDITOR.MediatR.Command;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEDITOR.MediatR.Handler
{
    public class LoginHandler: IRequestHandler<LoginCommand, bool>
    {
    }
}

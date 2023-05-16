using MediatR;
using Swapy.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllColorsQuery : IRequest<IEnumerable<Color>>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentManagement.Core.Helpers
{
    public class NotFoundException : Exception
    {

        public NotFoundException(string? message) : base(message)
        {
        }

    }
}

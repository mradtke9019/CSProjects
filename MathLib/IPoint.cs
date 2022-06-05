using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public interface IPoint
    {
        public IVector AsVector<T>();
    }
}

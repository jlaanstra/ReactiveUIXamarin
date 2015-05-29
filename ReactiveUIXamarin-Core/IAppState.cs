using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIXamarin.Core
{
    public interface IAppState
    {
        int Red { get; set; }

        int Green { get; set; }

        int Blue { get; set; }
    }
}

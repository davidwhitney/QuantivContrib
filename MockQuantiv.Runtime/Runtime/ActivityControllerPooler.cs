using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantiv.Runtime
{
    public class ActivityControllerPooler
    {
        public static ActivityController AllocateController(string controllerPool)
        {
            return new ActivityController();
        }
    }
}

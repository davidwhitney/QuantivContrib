using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quantiv.Runtime
{
    public class Activity
    {
        public EntityManager GetEntityManager(string name)
        {
            return new EntityManager();
        }

        public CustomDBProc CreateCustomDBProc(string procName)
        {
            return new CustomDBProc();
        }
    }
}

using GameEntityDll.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicDll.Enums;

namespace AsteroidDll.Input
{
    public class Command
    {
        private Action _func;

        public double PrivTimerValue = 1;
        public Command(Action func)
        {
            _func = func;

        }
        public void InvokeFunc()
        {
            _func.Invoke();
        }
    }
}

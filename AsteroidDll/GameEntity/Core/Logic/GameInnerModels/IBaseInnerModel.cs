using GameEntityDll;
using GameEntityDll.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.Logic.GameInnerModels
{
    public interface IBaseInnerModel
    {
 
        void CheckCondition(GameObject outerObject);

    }
}

using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.GameEntity.Core.Logic.GameInnerModels
{
    public class InertMoveModel : IBaseInnerModel
    {
        public void CheckCondition(GameObject outerObject)
        {
            outerObject.Controller.MoveByAngle(outerObject.Entity, Consts.DragSpeed);
        }
    }
}

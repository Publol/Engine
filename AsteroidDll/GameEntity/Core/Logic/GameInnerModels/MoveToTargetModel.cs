using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.GameEntity.Core.Logic.GameInnerModels
{

    public class MoveToTargetModel : IBaseInnerModel
    {
        private GameObject _targetObject;
        public MoveToTargetModel(GameObject target)
        {
            _targetObject = target;
        }
        public void CheckCondition(GameObject outerObject)
        {
            double xDistance = outerObject.Entity.XCoordinate - _targetObject.Entity.XCoordinate;
            double yDistance = outerObject.Entity.YCoordinate - _targetObject.Entity.YCoordinate;

            var angle = Math.Atan2(yDistance, xDistance) * 180 / Math.PI;
            if (_targetObject.Entity.YCoordinate > outerObject.Entity.YCoordinate)
            {
                angle = 360 - Math.Abs(angle);
                angle = (Math.PI * 2) / 360 * angle;
                angle = Math.PI * 2 - Math.Abs(Math.PI - angle);
            }
            else
            {
                angle = (Math.PI * 2) / 360 * angle;
                angle = Math.PI * 2 - (Math.PI + angle);
              
            }

            outerObject.Controller.MoveByAngle(outerObject.Entity, 0.0001, angle);
        }
    }
}

using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll;
using GameEntityDll.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AsteroidDll.GameEntity.Enums;

namespace AsteroidDll.GameEntity.Core.Logic.GameInnerModels
{
    /// <summary>
    ///  Moves Object from X and Y by angle.
    /// </summary>
    public class MoveByAngleModel : IBaseInnerModel
    {
        private double _angle = -1;

        private double xCoord;
        private double yCoord;

        private bool isStartCoordSetted = false;
       
        public MoveByAngleModel(double x, double y, double angle)
        {
            xCoord = x;
            yCoord = y;
            _angle = angle;
        }
        public void CheckCondition(GameObject outerObject)
        {
            if (!isStartCoordSetted)
            {
                outerObject.Controller.SetPosition(outerObject.Entity, xCoord, yCoord);
                outerObject.Entity.CurrentSpeed = outerObject.Entity.MaxSpeed;
                outerObject.Entity.RenderAngle = _angle;
                outerObject.Entity.Angle = _angle;
                isStartCoordSetted = !isStartCoordSetted;
            }
                
 
            outerObject.Controller.MoveByAngle(outerObject.Entity, 0, _angle); 
        }
     
      
    }
}

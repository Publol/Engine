using AsteroidDll;
using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll;
using GameEntityDll.Core.Game;
using GraphicDll;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AsteroidDll.Controller.Enums;
using static AsteroidDll.GameEntity.Enums;
using static GraphicDll.Enums;


namespace InputControllerDll
{
    public class Controller 
    {
        public void SetPosition(BaseEntity entity, double x, double y)
        {
            entity.XCoordinate = x;
            entity.YCoordinate = y;
        }

        public void Move(BaseEntity entity, DirectionEnum direction, double value)
        {
            entity.CurrentSpeed = 0;
            switch (direction)
            {
                case DirectionEnum.UP:
                {
                    entity.YCoordinate += value;
                    break;
                }
                case DirectionEnum.DOWN:
                {
                    entity.YCoordinate -= value;
                    break;
                }
                case DirectionEnum.LEFT:
                {
                    entity.XCoordinate -= value;
                    break;
                }
                case DirectionEnum.RIGHT:
                {
                    entity.XCoordinate += value;
                    break;
                }

            }
        }

        public void MoveByAngle(BaseEntity entity, double speed)
        {
            MoveByAngle(entity, speed, entity.RenderAngle);
          
        }

        public void MoveByAngle(BaseEntity entity, double speed, double angle)
        {
            entity.RenderAngle = angle;
            if (speed > 0)
                entity.Angle = entity.RenderAngle;
          
            entity.CurrentSpeed += speed;

            Matrix4 mat = Matrix4.CreateRotationZ((float)entity.Angle);

            var x = (float)((Vector4)mat.Matrix.GetValue(0)).Get(0);
            var y = (float)((Vector4)mat.Matrix.GetValue(1)).Get(0);

            entity.XCoordinate += x * entity.CurrentSpeed;
            entity.YCoordinate += y * entity.CurrentSpeed;

        }

        public void Rotate(BaseEntity entity, DirectionEnum direction, double radians)
        {

            switch (direction)
            {
                case DirectionEnum.LEFT:
                {
                    entity.RenderAngle -= radians;
                    break;
                }
                case DirectionEnum.RIGHT:
                {
                    entity.RenderAngle += radians;
                    break;
                }
            }

        }

    }
}

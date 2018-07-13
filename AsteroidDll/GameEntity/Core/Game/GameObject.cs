
using AsteroidDll.Graphic.Core.GraphicModules;
using AsteroidDll.Input;
using AsteroidDll.Logic.GameInnerModels;
using GraphicDll;
using GraphicDll.Core;
using InputControllerDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AsteroidDll.Controller.Enums;
using static GraphicDll.Enums;


namespace GameEntityDll.Core.Game
{
    public class GameObject : IDisposable
    {
        public GraphicModule Graphic { get; private set; }
        public BaseEntity Entity { get; private set; }
        public Controller Controller { get; private set; }
 
        public GameObject(GraphicModule module, BaseEntity entity, Controller controller)
        {
            Graphic = module;
            Entity = entity;
            Controller = controller;
            Entity.CreateRadiusAndAvarageValues(Graphic.Data);
        }
        #region IController interface
        public void SetPosition(double x, double y)
        {
            Controller.SetPosition(Entity, x, y);
        }
        public void Move(DirectionEnum direction, double value)
        {
            Controller.Move(Entity, direction, value);
        }

        public void MoveByAngle(double speed)
        {
            Controller.MoveByAngle(Entity, speed);
        }
        public void Rotate(DirectionEnum direction, double radians)
        {
            Controller.Rotate(Entity, direction, radians);
        }
        #endregion
        #region IBaseEntity interface
        public void AddInnerModel(IBaseInnerModel model)
        {
            Entity.AddInnerModel(model);
        }
        public bool IsDead()
        {
            return !Entity._isAlive;
        }
        public void GoToReverseCoordinates()
        {
            double deltaX = Entity.XCoordinate;
            double deltaY = Entity.YCoordinate;
            if (Entity.XCoordinate <= 0)
                deltaX = MainLoop.MonitorWidth - Entity.XCoordinate - Entity.Radius / 2;
            if (Entity.XCoordinate >= MainLoop.MonitorWidth)
                deltaX = MainLoop.MonitorWidth - Entity.XCoordinate + Entity.Radius / 2;

            if (Entity.YCoordinate <= 0)
                deltaY = MainLoop.MonitorHeight - Entity.YCoordinate - Entity.Radius / 2;
            if (Entity.YCoordinate >= MainLoop.MonitorHeight)
                deltaY = MainLoop.MonitorHeight - Entity.YCoordinate + Entity.Radius / 2;

            Controller.SetPosition(Entity, deltaX, deltaY);
        }
        #endregion
        #region IGraphicModule interface
        public void SetGraphicModule(GraphicModule module)
        {
            Graphic = module;
            Entity.CreateRadiusAndAvarageValues(Graphic.Data);
        }
        #endregion
     
        public void Dispose()
        {
            Graphic.Dispose();
            Entity.Dispose();
            
            if (MainLoop.GameObjectsArray.Contains(this))
                MainLoop.GameObjectsArray.Remove(this);
            if (MainLoop.BackendObjectArray.Contains(this))
                MainLoop.BackendObjectArray.Remove(this);
        }
    }
}

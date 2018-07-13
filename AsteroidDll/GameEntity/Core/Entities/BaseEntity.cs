
using AsteroidDll.Graphic.Core.Shaders;
using AsteroidDll.Logic.GameInnerModels;
using GraphicDll;
using GraphicDll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static AsteroidDll.GameEntity.Enums;
using static GraphicDll.Enums;

namespace GameEntityDll
{
    public class BaseEntity : ICloneable, IDisposable
    {
        public delegate void DeathHandler();
        public delegate void OutOfWindowSizeHandler();

        public event DeathHandler DeathEvent;
        public event OutOfWindowSizeHandler OutOfWindowEvent;

        #region Coordinates
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        #endregion
        #region Speed
        public double MaxSpeed { get; set; }    
        private double _currentSpeed;
        public double CurrentSpeed
        {
            get
            {
                return _currentSpeed;
            }
            set
            {

                if (value > MaxSpeed)
                    _currentSpeed = MaxSpeed;
                else
                {
                    if (_currentSpeed + value > 0)
                        _currentSpeed = value;
                }


            }
        }
        #endregion
        #region Angle
        private double _angle;
        public double Angle
        {
            get { return _angle; }
            set {
                if (_angle > Math.PI * 2 )
                    _angle = 0;
                else
                    if (_angle < 0)
                        _angle = Math.PI * 2 + value;
                    else
                        _angle = value;
            }
        }

        private double _renderAngle;
        public double RenderAngle
        {
            get { return _renderAngle; }
            set
            {
                if (_renderAngle > Math.PI * 2 )
                    _renderAngle = 0;
                else
                    if (_renderAngle < 0)
                        _renderAngle = Math.PI * 2 + value;
                    else
                         _renderAngle = value;
               
            }
        }
        #endregion
        public double Radius { get; private set; }
        public double AverageX, AverageY;

        public bool _isAlive { get; private set; }
        public List<IBaseInnerModel> InnerModels { get; private set; }


        public BaseEntity()
        {
            InnerModels = new List<IBaseInnerModel>();
            _isAlive = true;
        }

        public void AddInnerModel(IBaseInnerModel model)
        {
            InnerModels.Add(model);
        }
        public void CreateRadiusAndAvarageValues(ShaderData data)
        {
            Radius = ((Math.Abs(data.MinX) + Math.Abs(data.MaxX)) / 2 + (Math.Abs(data.MinY) + Math.Abs(data.MaxY)) / 2) / 1.5;
            AverageX = (data.MinX + data.MaxX) / 2;
            AverageY = (data.MinY + data.MaxY) / 2;
        }
        public void OnDeath()
        {
            //Console.WriteLine("im dead!");
            _isAlive = false;
        }
       
        #region Invoke Events region
        public void InvokeDeath()
        {
            if (DeathEvent != null)
                DeathEvent.Invoke();
        }
        public void InvokeOutOfWindowSize()
        {
            if (OutOfWindowEvent != null)
                OutOfWindowEvent.Invoke();
        }
        #endregion

        public void Dispose()
        {
            foreach (var p in InnerModels)
            {
                if (p is IDisposable)
                {
                    ((IDisposable)p).Dispose();
                    
                }

            }
            InnerModels.Clear();
        }

        public virtual object Clone()
        {
            Object clonedEntity = Activator.CreateInstance(this.GetType());
            ((BaseEntity)clonedEntity).Angle = Angle;
         
            return clonedEntity;
        }
     

    }
}

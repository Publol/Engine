using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll.Core.Game;
using GraphicDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.GameEntity.Core.Logic.GameInnerModels
{
    public class OutOfWindowSizeModel : IBaseInnerModel
    {
        private int _windowWidth;
        private int _windowHeight;

        public OutOfWindowSizeModel()
        {
            _windowWidth = MainLoop.MonitorWidth;
            _windowHeight = MainLoop.MonitorHeight;
        }

        public void CheckCondition(GameObject outerObject)
        {
            if (outerObject.Entity.XCoordinate < 0 || Math.Abs(outerObject.Entity.XCoordinate) > _windowWidth 
                || outerObject.Entity.YCoordinate < 0 || Math.Abs(outerObject.Entity.YCoordinate) > _windowHeight)
            {             
                outerObject.Entity.InvokeOutOfWindowSize();

            }
        }
    }
}

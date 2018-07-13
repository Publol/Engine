using AsteroidDll.Graphic.Core.GraphicModules;
using AsteroidDll.Input;
using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll.Core.Game;
using GraphicDll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.GameEntity.Core.Logic.GameInnerModels
{
    public class ExtraGraphicModul : IBaseInnerModel, IDisposable
    {
        private GraphicModule _extraModule;
        private GraphicModule _baseModule;
        private static bool _trigger;
        public ExtraGraphicModul(GraphicModule module)
        {
            _extraModule = module;
        }
        public void CheckCondition(GameObject outerObject)
        {
            if (_baseModule == null)
            {
                _baseModule = outerObject.Graphic;
            }
            if (_trigger)
            {
                outerObject.SetGraphicModule(_extraModule);
            }
            else
                outerObject.SetGraphicModule(_baseModule);

        }
        public static void SwapGraphicModule()
        {
            _trigger = !_trigger;
        }

        public void Dispose()
        {
            if (_extraModule != null)
               _extraModule.Dispose();
                
            if (_baseModule != null)
                _baseModule.Dispose();
        }
    }
}

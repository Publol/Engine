using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll.Core.Game;
using GraphicDll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.GameEntity.Core.Logic.GameInnerModels
{
    public class LifeTimeModel : IBaseInnerModel
    {
        private long _currentTime;
        private int _time;
        private Stopwatch _watch;
        public LifeTimeModel(int time)
        {
            _watch = new Stopwatch();
            _watch.Start();
            _time = time * 1000;
        }
        public void CheckCondition(GameObject outerObject)
        {
            _watch.Stop();

            _currentTime += (long)_watch.ElapsedMilliseconds;
            
            if (_currentTime > _time)
                outerObject.Entity.InvokeDeath();
            _watch.Restart();
        }
    }
}

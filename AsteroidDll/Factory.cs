using AsteroidDll.Input;
using GraphicDll;
using GraphicDll.Core;
using InputControllerDll;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntityDll.Core.Game
{
    public static class Factory
    {
        public static G Build<G>(GraphicModule graphicModule, BaseEntity entity, bool addToMainLoop = true)
            where G: GameObject
        {
            Type t = typeof(G);

            var obj = (G)Activator.CreateInstance(t, new Object[] { graphicModule, entity, new Controller() });
            if (addToMainLoop)
                MainLoop.AddGameObject(obj);
            else
            {
                obj.Entity.OnDeath();
                MainLoop.AddGameObjectToBackend(obj);
            }
                
            return obj;
        }
       
    }
}

using AsteroidDll.Graphic.Core;
using GameEntityDll.Core.Game;
using GraphicDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll
{
    public static class MainLogic
    {
        public static void CheckInnerModels(GameObject gameObject)
        {
            foreach (var model in gameObject.Entity.InnerModels)
            {
                model.CheckCondition(gameObject);
            }
        }
        public static void FullCleanup()
        {
            foreach (var obj in MainLoop.GameObjectsArray)
            {
                obj.Graphic.Dispose();
                obj.Graphic.Program.Dispose();
            }
            foreach (var obj in MainLoop.BackendObjectArray)
            {
                obj.Graphic.Dispose();
                obj.Graphic.Program.Dispose();
            }
            ClearGarbage(MainLoop.GameObjectsArray, true);
            ClearGarbage(MainLoop.BackendObjectArray, true);

            MainLoop.BackendObjectArray.Clear();
            MainLoop.GameObjectsArray.Clear();
            MainLoop.Score = 0;
            MainGraphic.Dispose();
            Prototype.CreanupPrototype();
            if (MainLoop.Close)
                MainLoop.ActiveLoop = false;
        }
        public static void ClearGarbage(List<GameObject> listOfObjectToClear, bool finalizeAll = false)
        {
            int i = 0;
            int arrCount = listOfObjectToClear.Count;
            GameObject tempObj;
            while (true && listOfObjectToClear.Count != 0)
            {
                tempObj = listOfObjectToClear[i];
                if (tempObj.IsDead() || finalizeAll)
                {
                    tempObj.Dispose();
                    arrCount--;
                    i = -1;
                }
                i++;
                if (arrCount == i)
                    break;

            }
        }
    }
}

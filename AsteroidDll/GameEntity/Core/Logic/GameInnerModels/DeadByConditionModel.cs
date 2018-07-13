using GameEntityDll;
using GameEntityDll.Core.Game;
using GraphicDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AsteroidDll.GameEntity.Enums;

namespace AsteroidDll.Logic.GameInnerModels
{
    public class DeadByConditionModel : IBaseInnerModel
    {
        public Type ModelObject { get; private set; }

        public DeadByConditionType Condition { get; private set; }
        public DeadByConditionModel(Type arg1)
        {
            ModelObject = arg1;
            Condition = DeadByConditionType.DEAD_BY_COLLISION;
        }
        public DeadByConditionModel(Type arg1, DeadByConditionType cond) : this(arg1)
        {
            Condition = cond;
        }


        public void CheckCondition(GameObject outerObject)
        {
            int arrCount = MainLoop.GameObjectsArray.Count;
            GameObject obj;
            int i = 0;
            while (true && MainLoop.GameObjectsArray.Count != 0)
            {
                if (arrCount == i)
                    break;
                obj = MainLoop.GameObjectsArray[i];

                BaseEntity targetEntity = obj.Entity;
                if (obj.Entity == outerObject.Entity)
                {
                    i++;
                    continue;
                }
                    
                // Console.WriteLine($"Starting to check... {FirstModelObject}, {targetEntity.GetType()}");
                if (!ModelObject.Equals(targetEntity.GetType()))
                {
                    i++;
                    continue;
                }
                // Console.WriteLine("Type Success");
                var distanse = Math.Sqrt(Math.Pow(Math.Abs(outerObject.Entity.XCoordinate - targetEntity.XCoordinate), 2) +
                               Math.Pow(Math.Abs(outerObject.Entity.YCoordinate - targetEntity.YCoordinate), 2));
                //Console.WriteLine($"Distance: {distanse}");
                if (distanse - (outerObject.Entity.Radius + targetEntity.Radius) / 2 > 0)
                {
                    i++;
                    continue;
                }
                switch (Condition)
                {
                    case DeadByConditionType.DEAD_BY_COLLISION:
                    {
                        outerObject.Entity.InvokeDeath();
                        targetEntity.InvokeDeath();
                        break;
                    }
                    case DeadByConditionType.IMMORTAL_DEAD_BY_COLLISION:
                    {
                        targetEntity.InvokeDeath();
                        break;
                    }
                }
                i++;
                
            }
           
           
            
        }

       
    }
}

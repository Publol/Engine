using GraphicDll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEntityDll.Core.Entities
{
    public class PlayerEntity : BaseEntity
    {
     
        public PlayerEntity(double maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        public override object Clone()
        {
            PlayerEntity clonedEntity = base.Clone() as PlayerEntity;
            return clonedEntity;
        }
       


    }
}

using GraphicDll.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEntityDll.Core.Entities
{
    public class EnemyEntity : BaseEntity
    {
        public EnemyEntity(){ }

        public EnemyEntity(double maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        public override object Clone()
        {
            EnemyEntity clonedEntity = (EnemyEntity)base.Clone();
            clonedEntity.MaxSpeed = MaxSpeed;

            return clonedEntity;
        }
    }
}

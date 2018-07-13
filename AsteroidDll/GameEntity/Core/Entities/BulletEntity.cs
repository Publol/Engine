using GameEntityDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll.GameEntity.Core.Entities
{
    public class BulletEntity : BaseEntity
    {
        public BulletEntity() { }
     
        public BulletEntity(double maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }
        public override object Clone()
        {
            BulletEntity clonedEntity = (BulletEntity)base.Clone();
            clonedEntity.MaxSpeed = MaxSpeed;

            return clonedEntity;
        }
      
    }
}

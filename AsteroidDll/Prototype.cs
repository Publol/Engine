using AsteroidDll.GameEntity.Core.Entities;
using AsteroidDll.Graphic.Core.GraphicModules;
using GameEntityDll;
using GameEntityDll.Core.Entities;
using GameEntityDll.Core.Game;
using GraphicDll;
using GraphicDll.Core;
using InputControllerDll;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidDll
{
    public static class Prototype
    {
        private static Dictionary<string, GameObject> _gameObjectPrototypes = new Dictionary<string, GameObject>();
        private static Dictionary<string, GraphicModule> _graphicModulePrototypes = new Dictionary<string, GraphicModule>();



        #region GameObjects
        public static void CreatePrototypeOfGameObject(string prototypeID, GraphicModule graphicModule, BaseEntity entity)
        {
            _gameObjectPrototypes.Add(prototypeID, Factory.Build<GameObject>(graphicModule, entity));
        }
        public static void CreatePrototypeOfGameObject(string prototypeID, GameObject obj)
        {
            _gameObjectPrototypes.Add(prototypeID, obj);
        }

        public static T CreateCopyOf<T>(string prototypeID)
            where T: class
        {


            if (typeof(T).Equals(typeof(GameObject)))
            {
                if (_gameObjectPrototypes.ContainsKey(prototypeID))
                {
                    BaseEntity entity = _gameObjectPrototypes[prototypeID].Entity;
                    GameObject obj = null;
                    if (entity.GetType().Equals(typeof(BulletEntity)))
                    {
                        BulletEntity bulletEntity = entity as BulletEntity;
                        obj = Factory.Build<GameObject>((GraphicModule)_gameObjectPrototypes[prototypeID].Graphic.Clone(),
                                                                     (BulletEntity)bulletEntity.Clone());
                    }
                    if (entity.GetType().Equals(typeof(EnemyEntity)))
                    {
                        EnemyEntity bulletEntity = entity as EnemyEntity;
                        obj = Factory.Build<GameObject>((GraphicModule)_gameObjectPrototypes[prototypeID].Graphic.Clone(),
                                                                    (EnemyEntity)bulletEntity.Clone());
                    }
                    if (entity.GetType().Equals(typeof(PlayerEntity)))
                    {
                        PlayerEntity bulletEntity = entity as PlayerEntity;
                        obj = Factory.Build<GameObject>((GraphicModule)_gameObjectPrototypes[prototypeID].Graphic.Clone(),
                                                                    (PlayerEntity)bulletEntity.Clone());
                    }
                    return obj as T;
                }
            }

            if (typeof(T).Equals(typeof(GraphicModule)))
            {
                if (_graphicModulePrototypes.ContainsKey(prototypeID))
                {
                    GraphicModule graphic = _graphicModulePrototypes[prototypeID];
                    GameObject obj = null;
                    if (graphic.GetType().Equals(typeof(TexturedGraphicModule)))
                        return (TexturedGraphicModule)_graphicModulePrototypes[prototypeID].Clone() as T;
                    if (graphic.GetType().Equals(typeof(GraphicModule)))
                        return (GraphicModule)_graphicModulePrototypes[prototypeID].Clone() as T;

                }
            }


            return null;
        }
      
        #endregion
        #region GraphicModules
        public static void CreatePrototypeOfGraphicModule(string prototypeID, GraphicModule graphicModule)
        {
            _graphicModulePrototypes.Add(prototypeID, graphicModule);
        }
    
        #endregion

        public static void CreanupPrototype()
        {
            _gameObjectPrototypes.Clear();
            _graphicModulePrototypes.Clear();
        }
    }
        
}

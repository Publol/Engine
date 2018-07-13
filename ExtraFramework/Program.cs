
using AsteroidDll;
using AsteroidDll.Controller;
using AsteroidDll.GameEntity.Core.Entities;
using AsteroidDll.GameEntity.Core.Logic.GameInnerModels;
using AsteroidDll.Graphic.Core;
using AsteroidDll.Graphic.Core.GraphicModules;
using AsteroidDll.Input;
using AsteroidDll.Logic.GameInnerModels;
using GameEntityDll;
using GameEntityDll.Core.Entities;
using GameEntityDll.Core.Game;
using GraphicDll;
using GraphicDll.Callbacks;
using GraphicDll.Core;
using GraphicDll.Core.Shaders;
using InputControllerDll;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Tao.FreeGlut;
using static AsteroidDll.Controller.Enums;
using static AsteroidDll.GameEntity.Enums;
using static GraphicDll.Enums;

namespace ExtraFramework
{
    public class Program
    {

        static void Main(string[] args)
        {
           
            while (MainLoop.ActiveLoop)
            {
                MainLoop.InitDefaultGraphicContext(1440, 900, "Asteroids");
                MainGraphic.CreateShaderProgram("SpriteProgram", ShaderSort.SPRITE);
                MainGraphic.CreateShaderProgram("PolyProgram", ShaderSort.POLY);

                #region Modules
                //Player
                GraphicModule playerModule = new GraphicModule("PolyProgram", 1, Color.Red);
                playerModule.CreateCustomShader(new Vector3[] { new Vector3(0, -25, 0), new Vector3(0, 25, 0), new Vector3(50, 0, 0) },
                                                new Vector3[] { new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0) });
                TexturedGraphicModule playerModule2 = new TexturedGraphicModule("SpriteProgram");
                playerModule2.CreateCustomShader(new Vector3[] { new Vector3(-50, -50, 0), new Vector3(-50, 50, 0), new Vector3(50, 50, 0), new Vector3(50, -50, 0) },
                                                                "Textures\\Player.png");

                //Bullet
                GraphicModule bulletModule = new GraphicModule("PolyProgram", 10, Color.White);
                bulletModule.CreateRandomShader(13);
                TexturedGraphicModule bulletModule2 = new TexturedGraphicModule("SpriteProgram");
                bulletModule2.CreateCustomShader(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 10, 0), new Vector3(20, 10, 0), new Vector3(20, 0, 0) },
                                                                "Textures\\bullet.png");
                Prototype.CreatePrototypeOfGraphicModule("bulletModule", bulletModule2);

                //UFO
                GraphicModule ufoModule = new GraphicModule("PolyProgram", 25, Color.Green);
                ufoModule.CreateRandomShader(5);
                TexturedGraphicModule ufoModule2 = new TexturedGraphicModule("SpriteProgram");
                ufoModule2.CreateCustomShader(new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 30, 0), new Vector3(30, 30, 0), new Vector3(30, -30, 0) },
                                                                "Textures\\Ufo.png");
                Prototype.CreatePrototypeOfGraphicModule("ufoModule", ufoModule2);

                //Asteroid_BIG
                GraphicModule asteroidModule = new GraphicModule("PolyProgram", 50, Color.Red);
                asteroidModule.CreateRandomShader(10);
                TexturedGraphicModule asteroidModule2 = new TexturedGraphicModule("SpriteProgram");
                asteroidModule2.CreateCustomShader(new Vector3[] { new Vector3(-50, -50, 0), new Vector3(-50, 50, 0), new Vector3(50, 50, 0), new Vector3(50, -50, 0) },
                                                                 "Textures\\Asteroid_big.png");
                Prototype.CreatePrototypeOfGraphicModule("asteroidBig", asteroidModule2);

                //Asteroid_small
                GraphicModule miniAsteroidModule = new GraphicModule("PolyProgram", 30, Color.Blue);
                miniAsteroidModule.CreateRandomShader(10);
                TexturedGraphicModule miniAsteroidModule2 = new TexturedGraphicModule("SpriteProgram");
                miniAsteroidModule2.CreateCustomShader(new Vector3[] { new Vector3(-15, -15, 0), new Vector3(-15, 15, 0), new Vector3(15, 15, 0), new Vector3(15, -15, 0) },
                                                                    "Textures\\Asteroid_small.png");
                Prototype.CreatePrototypeOfGraphicModule("asteroidSmall", miniAsteroidModule2);

                //LASER
                GraphicModule laserModule = new GraphicModule("PolyProgram", 30, Color.Red);
                laserModule.CreateCustomShader(new Vector3[] { new Vector3(-3, -3, 0), new Vector3(-3, 3, 0), new Vector3(27, 3, 0), new Vector3(27, -3, 0) },
                                                new Vector3[] { new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0) });
                TexturedGraphicModule laserModule2 = new TexturedGraphicModule("SpriteProgram");
                laserModule2.CreateCustomShader(new Vector3[] { new Vector3(-10, -10, 0), new Vector3(-10, 10, 0), new Vector3(30, 10, 0), new Vector3(30, -10, 0) },
                                                                    "Textures\\laser.png");
                Prototype.CreatePrototypeOfGraphicModule("laserModule", laserModule2);

                //Background
                TexturedGraphicModule backgroundModule = new TexturedGraphicModule("SpriteProgram");
                backgroundModule.CreateCustomShader(new Vector3[] { new Vector3(0, 0, 0), new Vector3(MainLoop.MonitorWidth, 0, 0),
                                                              new Vector3(MainLoop.MonitorWidth, MainLoop.MonitorHeight, 0), new Vector3(0, MainLoop.MonitorHeight, 0) },
                                                              "Textures\\back_simpled.png");
                TexturedGraphicModule backgroundModule2 = new TexturedGraphicModule("SpriteProgram");
                backgroundModule2.CreateCustomShader(new Vector3[] { new Vector3(0, 0, 0), new Vector3(MainLoop.MonitorWidth, 0, 0),
                                                              new Vector3(MainLoop.MonitorWidth, MainLoop.MonitorHeight, 0), new Vector3(0, MainLoop.MonitorHeight, 0) },
                                                              "Textures\\background.png");
                #endregion

                #region GameObject creation
                ////Background
                BaseEntity backEntity = new BaseEntity();
                backEntity.AddInnerModel(new ExtraGraphicModul(backgroundModule2));
                GameObject background = Factory.Build<GameObject>(backgroundModule, backEntity);
                background.SetPosition(MainLoop.MonitorWidth / 2, MainLoop.MonitorHeight / 2);
                //Player Object
                BaseEntity player = new PlayerEntity(2);
                player.AddInnerModel(new InertMoveModel());
                player.AddInnerModel(new OutOfWindowSizeModel());
                player.AddInnerModel(new ExtraGraphicModul(playerModule2));
                player.DeathEvent += player.OnDeath;
                player.DeathEvent += () =>
                {
                    MainLoop.StopMainLoop();
                    if (MessageBox.Show("You are dead. Are you want to play again?", $"You have {MainLoop.Score} scores!", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        MainLoop.ActiveLoop = false;
                    }
                    else
                    {
                        MainLoop.Close = false;
                    }
                  


                };
                GameObject playerObject = Factory.Build<GameObject>(playerModule, player);
                playerObject.Entity.OutOfWindowEvent += playerObject.GoToReverseCoordinates;
                playerObject.SetPosition(MainLoop.MonitorWidth / 2, MainLoop.MonitorHeight / 2);

                //Bullet Object
                BaseEntity bullet = new BulletEntity(5);
                GameObject bulletObject = Factory.Build<GameObject>(bulletModule, bullet, false);
                Prototype.CreatePrototypeOfGameObject("bullet", bulletObject);

                //Ufo Object
                BaseEntity ufo = new EnemyEntity(2);
                GameObject ufoObject = Factory.Build<GameObject>(ufoModule, ufo, false);
                Prototype.CreatePrototypeOfGameObject("ufo", ufoObject);

                //Asteroid_BIG Object
                BaseEntity asteroid = new EnemyEntity(2);
                GameObject asteroidObject = Factory.Build<GameObject>(asteroidModule, asteroid, false);
                Prototype.CreatePrototypeOfGameObject("asteroid", asteroidObject);

                //Asteroid_small Object
                BaseEntity miniAsteroid = new EnemyEntity(4);
                GameObject miniAsteroidObject = Factory.Build<GameObject>(miniAsteroidModule, miniAsteroid, false);
                Prototype.CreatePrototypeOfGameObject("miniAsteroid", miniAsteroidObject);

                //Laser Object
                BaseEntity laserEntity = new BulletEntity(4);
                GameObject laserObject = Factory.Build<GameObject>(laserModule, laserEntity, false);
                Prototype.CreatePrototypeOfGameObject("laser", laserObject);
                 #endregion

                #region Commands
                ////Player Move
                Command up = new Command(() => playerObject.MoveByAngle(0.1));
                Command left = new Command(() => playerObject.Rotate(DirectionEnum.LEFT, 0.1));
                Command right = new Command(() => playerObject.Rotate(DirectionEnum.RIGHT, 0.1));
                //Player Fire bullet
                Command fire = new Command(() =>
                {
                    var bul = Prototype.CreateCopyOf<GameObject>("bullet");
                    bul.Entity.DeathEvent += bul.Entity.OnDeath;
                    bul.AddInnerModel(new OutOfWindowSizeModel());
                    bul.AddInnerModel(new ExtraGraphicModul(Prototype.CreateCopyOf<GraphicModule>("bulletModule")));
                    bul.AddInnerModel(new MoveByAngleModel(playerObject.Entity.XCoordinate, playerObject.Entity.YCoordinate, playerObject.Entity.RenderAngle));
                    bul.AddInnerModel(new DeadByConditionModel(typeof(EnemyEntity), DeadByConditionType.DEAD_BY_COLLISION));
                    bul.AddInnerModel(new LifeTimeModel(5));
                    bul.Entity.OutOfWindowEvent += bul.GoToReverseCoordinates;
                });
                //Create Asteroid
                Command createAsteroid = new Command(() =>
                {
                    var asr = Prototype.CreateCopyOf<GameObject>("asteroid");
                    asr.Entity.DeathEvent += asr.Entity.OnDeath;
                    asr.Entity.DeathEvent += () => MainLoop.AddScore(100);
                    asr.Entity.DeathEvent += () =>
                    {
                        Random rand = new Random();
                        Random randMini = new Random();
                        var count = rand.Next(1, 4);
                        for (int i = 0; i < count; i++)
                        {
                            var mini = Prototype.CreateCopyOf<GameObject>("miniAsteroid");
                            var value = randMini.NextDouble() * Math.PI * 2;
                            mini.AddInnerModel(new OutOfWindowSizeModel());
                            mini.AddInnerModel(new ExtraGraphicModul(Prototype.CreateCopyOf<GraphicModule>("asteroidSmall")));
                            mini.AddInnerModel(new MoveByAngleModel(asr.Entity.XCoordinate + 1, asr.Entity.YCoordinate + 1, value));
                            mini.AddInnerModel(new DeadByConditionModel(typeof(PlayerEntity), DeadByConditionType.DEAD_BY_COLLISION));
                            mini.Entity.DeathEvent += mini.Entity.OnDeath;
                            mini.Entity.DeathEvent += () => MainLoop.AddScore(50);
                            mini.Entity.OutOfWindowEvent += mini.GoToReverseCoordinates;
                        }
                    };
                    asr.AddInnerModel(new OutOfWindowSizeModel());
                    asr.AddInnerModel(new ExtraGraphicModul(Prototype.CreateCopyOf<GraphicModule>("asteroidBig")));
                    asr.AddInnerModel(new MoveByAngleModel(GetRandomAccessiblePositionAndAngle(playerObject).Item1, GetRandomAccessiblePositionAndAngle(playerObject).Item2,
                                                                GetRandomAccessiblePositionAndAngle(playerObject).Item3));
                    asr.AddInnerModel(new DeadByConditionModel(typeof(PlayerEntity), DeadByConditionType.DEAD_BY_COLLISION));
                    asr.Entity.OutOfWindowEvent += asr.GoToReverseCoordinates;
                });
                //Create Ufo
                Command createUfo = new Command(() =>
                {
                    var ufoCopy = Prototype.CreateCopyOf<GameObject>("ufo");
                    ufoCopy.Entity.DeathEvent += ufoCopy.Entity.OnDeath;
                    ufoCopy.Entity.DeathEvent += () => MainLoop.AddScore(250);
                    ufoCopy.AddInnerModel(new ExtraGraphicModul(Prototype.CreateCopyOf<GraphicModule>("ufoModule")));
                    ufoCopy.AddInnerModel(new MoveToTargetModel(playerObject));
                    ufoCopy.AddInnerModel(new DeadByConditionModel(typeof(PlayerEntity), DeadByConditionType.DEAD_BY_COLLISION));
                    ufoCopy.AddInnerModel(new MoveByAngleModel(GetRandomAccessiblePositionAndAngle(playerObject).Item1, GetRandomAccessiblePositionAndAngle(playerObject).Item2,
                                                                GetRandomAccessiblePositionAndAngle(playerObject).Item3));

                });
                //Laser 
                int laserCount = 3;
                Command fireLaser = new Command(() =>
                {
                    if (laserCount > 0)
                    {
                        var laser = Prototype.CreateCopyOf<GameObject>("laser");
                        laser.Entity.DeathEvent += laser.Entity.OnDeath;
                        laser.AddInnerModel(new OutOfWindowSizeModel());
                        laser.AddInnerModel(new ExtraGraphicModul(Prototype.CreateCopyOf<GraphicModule>("laserModule")));
                        laser.AddInnerModel(new MoveByAngleModel(playerObject.Entity.XCoordinate, playerObject.Entity.YCoordinate, playerObject.Entity.RenderAngle));
                        laser.AddInnerModel(new DeadByConditionModel(typeof(EnemyEntity), DeadByConditionType.IMMORTAL_DEAD_BY_COLLISION));
                        laser.AddInnerModel(new LifeTimeModel(10));
                        laser.Entity.OutOfWindowEvent += laser.GoToReverseCoordinates;
                        laserCount--;
                    }


                });
                //Laser respawn
                Command respawnLaser = new Command(() =>
                {
                    if (laserCount < 3)
                        laserCount++;
                });
                //Swap Graphic
                Command activateExtraModule = new Command(() =>
                {
                    ExtraGraphicModul.SwapGraphicModule();
                });
                //Bindings
                CommandHandler.BindKeyToCommand("w", up, false);
                CommandHandler.BindKeyToCommand("a", left, false);
                CommandHandler.BindKeyToCommand("d", right, false);
                CommandHandler.BindKeyToCommand("x", fire);
                CommandHandler.BindKeyToCommand("z", activateExtraModule);
                CommandHandler.BindKeyToCommand("q", createAsteroid);
                CommandHandler.BindKeyToCommand("f", fireLaser);
                CommandHandler.BindTimerToCommand(0, createAsteroid, 5000);
                CommandHandler.BindTimerToCommand(1, createUfo, 10000);
                CommandHandler.BindTimerToCommand(2, respawnLaser, 10000);
                #endregion
                //Main loop starts
                MainLoop.StartMainLoop();

                //DIspose stuff
                playerModule2.Dispose();
                asteroidModule2.Dispose();
                miniAsteroidModule2.Dispose();
                bulletModule2.Dispose();
                laserModule2.Dispose();
                ufoModule2.Dispose();
                backgroundModule.Dispose();
                backgroundModule2.Dispose();
                CommandHandler.UnbindAll();
                MainLogic.FullCleanup();
            }
           

        }
      
        public static Tuple<int, int, double> GetRandomAccessiblePositionAndAngle(GameObject targObj)
        {
            Random position = new Random();
            var x = position.Next(MainLoop.MonitorWidth);
            var y = position.Next(MainLoop.MonitorHeight);
          
            for (int i = 0; i <100; i++)
            {
                x = position.Next(MainLoop.MonitorWidth);
                y = position.Next(MainLoop.MonitorHeight);
                
                    if (Math.Abs((targObj.Entity.XCoordinate - x)) > targObj.Entity.Radius * 2 && Math.Abs((targObj.Entity.YCoordinate - y)) > targObj.Entity.Radius * 2)
                    {
                        return new Tuple<int, int, double>(x, y, position.NextDouble() * Math.PI * 2);
                    }
            }
            return new Tuple<int, int, double>(x, y, position.NextDouble() * Math.PI * 2);

        }


    }
}

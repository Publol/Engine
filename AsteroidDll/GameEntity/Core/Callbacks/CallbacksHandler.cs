using AsteroidDll;
using AsteroidDll.Graphic.Core;
using AsteroidDll.Input;
using GameEntityDll;
using GameEntityDll.Core.Entities;
using GameEntityDll.Core.Game;
using GraphicDll.Core;
using GraphicDll.Core.Shaders;
using InputControllerDll;

using static OpenGL.Gl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using Tao.FreeGlut;
using Tao.OpenGl;
using static AsteroidDll.GameEntity.Enums;
using static GraphicDll.Enums;
using static Tao.FreeGlut.Glut;
using OpenGL;
using AsteroidDll.Graphic.Core.GraphicModules;

namespace GraphicDll.Callbacks
{
    public static class CallbacksHandler 
    {
        private static bool[] _keysArray = new bool[256];
        private static bool[] _keysArrayUp = new bool[256];
        

        public static void Build()
        {

            for (int i = 0; i < _keysArrayUp.Length; i++)
                _keysArrayUp[i] = true;

            Glut.glutDisplayFunc(OnDisplay);

            Glut.glutReshapeFunc(OnReshape);

            Glut.glutCloseFunc(OnClose);

            Glut.glutKeyboardFunc(OnKeyboard);

            Glut.glutKeyboardUpFunc(OnKeyboardUp);


            Glut.glutTimerFunc(100, RenderTimer, 0);

        }

        private static void RenderTimer(int index)
        {
            if (MainLoop.GameObjectsArray.Count != 0 || MainLoop.BackendObjectArray.Count != 0)
            {
                Glut.glutPostRedisplay();

                Glut.glutTimerFunc(1000 / 60, RenderTimer, 0);
            }               
        }

     
        private static void OnDisplay()
        {
            MainLoop.MainTimer += 1000 / 60;
            Viewport(0, 0, MainLoop.MonitorWidth, MainLoop.MonitorHeight);
            Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (MainLoop.GameObjectsArray.Count > 0)
            {
                CheckObjectTimers();
                CheckPlayerInput();
                int arrCount = MainLoop.GameObjectsArray.Count;
                GameObject obj;
                int j = 0;
             
                while (true && MainLoop.GameObjectsArray.Count != 0)
                {
                   
                    obj = MainLoop.GameObjectsArray[j];
                    MainLogic.CheckInnerModels(obj);
                    if (!obj.IsDead())
                    {
                        UseProgram(obj.Graphic.Program);
                        Matrix4 value = Matrix4.CreateTranslation(new Vector3(-(float)obj.Entity.AverageX, -(float)obj.Entity.AverageY, 0)) * //radius translation
                                    Matrix4.CreateRotationZ(-(float)obj.Entity.RenderAngle) *
                                    Matrix4.CreateTranslation(new Vector3((float)(obj.Entity.XCoordinate), (float)(obj.Entity.YCoordinate), 0));

                        obj.Graphic.Program["model_matrix"].SetValue(value);

                        BindBufferToShaderAttribute(obj.Graphic.Data._shaderVertices, obj.Graphic.Program, "vertexPosition");
                        if (obj.Graphic is TexturedGraphicModule)
                        {
                            BindTexture(TextureTarget.Texture2D, ((TexturedGraphicModule)obj.Graphic).Texture.TextureID);
                            BindBufferToShaderAttribute(obj.Graphic.Data._shaderTextureUV, obj.Graphic.Program, "vertexUV");
                        }
                        else
                        {
                            BindTexture(TextureTarget.Texture2D, 0);
                            BindBufferToShaderAttribute(obj.Graphic.Data._shaderColors, obj.Graphic.Program, "vertexColor");
                        }

                        BindBuffer(obj.Graphic.Data._shaderDrawQueue);
                        DrawElements(BeginMode.TriangleFan, obj.Graphic.Data._shaderDrawQueue.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
                    }
                    
                    j++;
                    if (arrCount == j)
                        break;
                    
                }
            }

            MainLogic.ClearGarbage(MainLoop.GameObjectsArray);
            glutSwapBuffers();
        }
        private static void CheckObjectTimers()
        {
            foreach (var timerFunc in CommandHandler.GetTimerCommands())
            {
                if (timerFunc.Value.Item1.PrivTimerValue == 0)
                {
                    timerFunc.Value.Item1.InvokeFunc();

                }
                timerFunc.Value.Item1.PrivTimerValue += 1000 / 60;
                if (timerFunc.Value.Item2 <= timerFunc.Value.Item1.PrivTimerValue)
                    timerFunc.Value.Item1.PrivTimerValue = 0;

            }
        }
        private static void CheckPlayerInput()
        {
            string key;
            var commands = CommandHandler.GetKeyCommands();
            for (int i = 0; i < 256; i++)
            {
                if (_keysArray[i])
                {
                    key = Convert.ToChar(i).ToString();
                    if (commands.ContainsKey(key))
                    {
                        if (commands[key].Item2)
                        {
                            if (_keysArrayUp[i])
                            {
                                commands[key].Item1.InvokeFunc();
                                _keysArrayUp[i] = false;
                            }
                        }
                        else
                            commands[key].Item1.InvokeFunc();

                    }

                }
            }
        }
        private static void OnReshape(int width, int hegith)
        {
            MainLoop.MonitorWidth = width;
            MainLoop.MonitorHeight = hegith;
            Viewport(0, 0, MainLoop.MonitorWidth, MainLoop.MonitorHeight);
            Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
        public static void OnClose()
        {
            //close stuff
            MainLogic.FullCleanup();
            MainLoop.StopMainLoop();
            if (MainLoop.Close)
                MainLoop.ActiveLoop = false;

        }
        private static void OnKeyboard(byte value, int x, int y)
        {
            _keysArray[value] = true;
          
        }
        private static void OnKeyboardUp(byte value, int x, int y)
        {
            _keysArray[value] = false;
            _keysArrayUp[value] = true;
        }

    }
}

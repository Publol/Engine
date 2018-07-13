using GameEntityDll.Core.Game;
using GraphicDll.Callbacks;

using OpenGL;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tao.FreeGlut;
namespace GraphicDll
{
    public static class MainLoop
    {
        private static string _monitorHeader;

        public static int MonitorWidth { get; set; }
        public static int MonitorHeight { get; set; }
        public static int MainTimer = 0;
        public static int Score = 0;
        public static bool ActiveLoop = true;
        public static bool Close = true;

        public static Matrix4 ProjectionMatrix { get; private set; }
        public static List<GameObject> GameObjectsArray { get; private set; }
        public static List<GameObject> BackendObjectArray { get; private set; }

        public static void InitDefaultGraphicContext(int width, int hegith, string header)
        {
            MonitorWidth = width;
            MonitorHeight = hegith;
            _monitorHeader = header;
            Close = true;
            GameObjectsArray = new List<GameObject>();
            BackendObjectArray = new List<GameObject>();

            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(MonitorWidth, MonitorHeight);
            Glut.glutSetOption(Glut.GLUT_ACTION_ON_WINDOW_CLOSE, Glut.GLUT_ACTION_CONTINUE_EXECUTION);
           
          
            Glut.glutCreateWindow(_monitorHeader);
          
          

            CreateProjectionMatrix();
            InitDefaultCallbacks();
        }

        public static int StartMainLoop()
        {
            Glut.glutMainLoop();
            return -1;
        }

        public static void StopMainLoop()
        {
            Glut.glutLeaveMainLoop();
        }

        public static void AddGameObject(params GameObject[] gObject)
        {
            foreach (var obj in gObject)
                GameObjectsArray.Add(obj);
        }
        public static void AddGameObjectToBackend(params GameObject[] gObject)
        {
            foreach (var obj in gObject)
                BackendObjectArray.Add(obj);
        }
       
        public static void AddScore(int score)
        {
            Score += score;
        }
        private static void CreateProjectionMatrix()
        {
         
            ProjectionMatrix = Matrix4.CreateOrthographicOffCenter(0, MonitorWidth, 0, MonitorHeight, 0.1f, 1000f);
        }
        
        private static void InitDefaultCallbacks()
        {
            CallbacksHandler.Build();
        }
      
    }
}

using GraphicDll;
using GraphicDll.Core.Shaders;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicDll.Enums;

namespace AsteroidDll.Graphic.Core
{
    public static class MainGraphic 
    {
        private static Dictionary<string, ShaderProgram> _programs = new Dictionary<string, ShaderProgram>();


        public static void CreateShaderProgram(string programID, ShaderSort _shaderType)
        {
            string vertexShader = "";
            string fragmentShader = "";

            switch (_shaderType)
            {
                case ShaderSort.POLY:
                {
                    vertexShader = ShadersSignature._vertexPolyShader;
                    fragmentShader = ShadersSignature._fragmentPolyShader;
                    break;
                }
                case ShaderSort.SPRITE:
                {
                    vertexShader = ShadersSignature._vertexTextureShader;
                    fragmentShader = ShadersSignature._fragmentTextureShader;
                    break;
                }
            }
            ShaderProgram Program = new ShaderProgram(vertexShader, fragmentShader);
            Program["projection_matrix"].SetValue(MainLoop.ProjectionMatrix);
            Program["model_matrix"].SetValue(Matrix4.Identity);
            Program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));


            _programs.Add(programID, Program);
        }
        public static ShaderProgram GetProgramById(string programID)
        {
            if (_programs.ContainsKey(programID))
            {
                return _programs[programID];
            }
            return null;
        }

        public static void Dispose()
        {
            foreach (var program in _programs)
            {
                program.Value.DisposeChildren = true;
                program.Value.Dispose();
            }
            _programs.Clear();
        }
    }
}

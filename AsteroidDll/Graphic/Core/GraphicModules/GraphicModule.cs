using AsteroidDll.Graphic.Core;
using AsteroidDll.Graphic.Core.GraphicModules;
using AsteroidDll.Graphic.Core.Shaders;
using GraphicDll.Core.Shaders;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using static GraphicDll.Enums;

namespace GraphicDll.Core
{
    public class GraphicModule : IGraphicModule, IDisposable, ICloneable
    {
        #region InnerFields
        public ShaderProgram Program { get; set; }
        
       
        public ShaderData Data { get; set; }

        protected string _programID;
        protected int _shaderSize;
        protected Color _shaderColor;
        #endregion

        #region CopyFields
        protected bool isRandom;
        protected Vector3[] _posData;
        protected Vector3[] _colorData;
    
        #endregion
        public GraphicModule() { }
        public GraphicModule(string programID, int shaderSize, Color shaderColor)
        {
            _programID = programID;
            _shaderSize = shaderSize;
            _shaderColor = shaderColor;
            Program = MainGraphic.GetProgramById(_programID);
            Program.DisposeChildren = true;
            Data = new ShaderData(shaderColor);
        }
     
        public void UseProgrm()
        {
            Program.Use();
        }

        public void CreateCustomShader(Vector3[] posData, Vector3[] colorData)
        {
           
            _posData = posData;
            _colorData = colorData;
            isRandom = false;
            Data.CreateCustomShader(posData, colorData);
        }
      
        public void CreateRandomShader(int verteciesCount = 0)
        {
            isRandom = true;
            if (verteciesCount == 0)
                Data.CreateRandomShader(new Random().Next(5,15), _shaderSize);
            else
                Data.CreateRandomShader(verteciesCount, _shaderSize);
        }
      
        public virtual void Dispose()
        {
            Data.Dispose();
        }

        public virtual object Clone()
        {
           
            GraphicModule clonedModule = new GraphicModule(_programID, _shaderSize, _shaderColor);
            clonedModule.Data = new ShaderData(_shaderColor);
            if (isRandom)
                clonedModule.CreateRandomShader();
            else
                clonedModule.CreateCustomShader(_posData, _colorData);
              
            return clonedModule;
        }
    }
}

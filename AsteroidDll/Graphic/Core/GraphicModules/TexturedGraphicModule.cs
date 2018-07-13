using AsteroidDll.Graphic.Core.Shaders;
using GraphicDll.Core;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidDll.Graphic.Core.GraphicModules
{

    public class TexturedGraphicModule : GraphicModule, IDisposable, ICloneable
    {
        public Texture Texture { get; private set; }
        private string _texturePath;
        public TexturedGraphicModule(string programID)
        {
            _programID = programID;
            Program = MainGraphic.GetProgramById(_programID);
            Program.DisposeChildren = true;
            Data = new ShaderData();
        }
        public void CreateCustomShader(Vector3[] posData, string texturePath)
        {

            _texturePath = texturePath;
            try
            {
                Texture = new Texture(texturePath);
            }catch (Exception ex)
            {
                MessageBox.Show($"Path {texturePath} could not be found.");
                Environment.Exit(-1);
            }
           
            _posData = posData;
            isRandom = false;
            Data.CreateCustomShader(posData);
        }
        public override void Dispose()
        {
            base.Dispose();
            if (Texture != null)
                Texture.Dispose();
        }

        public override object Clone()
        {

            GraphicModule clonedModule = new TexturedGraphicModule(_programID);
            clonedModule.Data = new ShaderData(_shaderColor);
            if (isRandom)
                clonedModule.CreateRandomShader();
            else
                ((TexturedGraphicModule)clonedModule).CreateCustomShader(_posData, _texturePath);
            return clonedModule;
        }
    }
}

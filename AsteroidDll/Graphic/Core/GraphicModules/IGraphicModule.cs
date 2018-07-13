using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GraphicDll.Enums;

namespace AsteroidDll.Graphic.Core.GraphicModules
{
    public interface IGraphicModule
    {
        void CreateCustomShader(Vector3[] posData, Vector3[] colorData);
        void CreateRandomShader(int verteciesCount = 0);
        void UseProgrm();
       
    }
}

using OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsteroidDll.Graphic.Core.Shaders
{
    public class ShaderData : IDisposable
    {
        public VBO<Vector3> _shaderVertices { get; private set; }
        public VBO<int> _shaderDrawQueue { get; private set; }
        public VBO<Vector2> _shaderTextureUV { get; private set; }
        public VBO<Vector3> _shaderColors { get; private set; }
        public double MinX, MaxX, MinY, MaxY;
   

        private Color _color = Color.White;
        public ShaderData()
        {
           
        } 
        public ShaderData(Color color) 
        {
            _color = color;
        }
       
        public void CreateCustomShader(Vector3[] posData, Vector3[] colorData)
        {
            int[] dataQueue = new int[posData.Length];
            for (int i = 0; i < posData.Length; i++)
            {
                dataQueue[i] = i;
            }
            MinAndMaxCoords(posData);
            _shaderVertices = new VBO<Vector3>(posData);
            _shaderDrawQueue = new VBO<int>(dataQueue, BufferTarget.ElementArrayBuffer);
            _shaderColors = new VBO<Vector3>(colorData);
        }
        public void CreateCustomShader(Vector3[] posData)
        {
            int[] dataQueue = new int[posData.Length];
            for (int i = 0; i < posData.Length; i++)
            {
                dataQueue[i] = i;
            }
         
            MinAndMaxCoords(posData);
            _shaderVertices = new VBO<Vector3>(posData);
            _shaderDrawQueue = new VBO<int>(dataQueue, BufferTarget.ElementArrayBuffer);
            _shaderTextureUV = new VBO<Vector2>(new Vector2[] {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1)
            });


        }

        public void CreateRandomShader(int dataSize, int shaderSize)
        {
            Vector3[] positionData = new Vector3[dataSize];
            int[] dataQueue = new int[dataSize];
            Vector3[] colorData = new Vector3[dataSize];
            Random random = new Random();
            float curX;
            float curY;
            for (int i = 0; i < dataSize; i++)
            {
                curX = (float)random.NextDouble() * shaderSize;
        
                curY = (float)random.NextDouble() * shaderSize;
               
             
                positionData[i] = new Vector3(curX, curY, 0);
                dataQueue[i] = i;
                colorData[i] = new Vector3(_color.R, _color.G, _color.B);
            }
            MinAndMaxCoords(positionData);
            _shaderVertices = new VBO<Vector3>(positionData);
            _shaderDrawQueue = new VBO<int>(dataQueue, BufferTarget.ElementArrayBuffer);
            _shaderColors = new VBO<Vector3>(colorData);

          
        }
        private void MinAndMaxCoords(Vector3[] values)
        {
            foreach (var value in values)
            {
                if (value[0] < MinX)
                    MinX = value[0];
                if (value[0] > MaxX)
                    MaxX = value[0];

                if (value[1] < MinY)
                    MinY = value[1];
                if (value[1] > MaxY)
                    MaxY = value[1];
            }
            //Console.WriteLine($"MinX: {MinX}, MaxX: {MaxX}, MinY: {MinY}, MaxY: {MaxY}");
    
        }
        public void Dispose()
        {
            _shaderVertices.Dispose();
            _shaderDrawQueue.Dispose();
            if (_shaderColors != null)
                _shaderColors.Dispose();
            if (_shaderTextureUV != null)
                _shaderTextureUV.Dispose();
        }
     
    }
}

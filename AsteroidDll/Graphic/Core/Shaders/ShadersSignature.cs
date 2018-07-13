using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicDll.Core.Shaders
{
    public static class ShadersSignature
    {

        #region Shaders for texture objects
        public static readonly string _vertexTextureShader = @"
                                                #version 130

                                                in vec3 vertexPosition;
                                                in vec2 vertexUV;

                                                varying out vec2 uv;

                                                uniform mat4 projection_matrix;
                                                uniform mat4 view_matrix;
                                                uniform mat4 model_matrix;

                                                void main(void)
                                                {
                                                    uv = vertexUV;
                                                    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
                                                }
                                                ";
        public static readonly string _fragmentTextureShader = @"
                                                #version 130

                                                uniform sampler2D texture;
                                                in vec2 uv;

                                                out vec4 fragment;
                                                void main(void)
                                                {
                                                    fragment = texture2D(texture, uv);
                                                    if (fragment.w == 0)
                                                        discard;
                                                }
                                                ";
        #endregion
        #region Shaders for poly Objects
        public static string _vertexPolyShader = @"
                                                #version 130

                                                in vec3 vertexPosition;
                                                in vec3 vertexColor;

                                                varying out vec3 color;

                                                uniform mat4 projection_matrix;
                                                uniform mat4 view_matrix;
                                                uniform mat4 model_matrix;

                                                void main(void)
                                                {
                                                    color = vertexColor;
                                                    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
                                                }
                                                ";

        public static string _fragmentPolyShader = @"
                                                #version 130
                                                in vec3 color;

                                                void main(void)
                                                {
                                                    gl_FragColor = vec4(color, 1);
                                                }
                                                ";
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkatePark.Primitives;
using SkatePark.Drawables;
using Tao.OpenGl;

namespace SkatePark
{
    public class ICubelet : IDrawable
    {
        public string MyType { get; set; }

        public ICubelet(string myType)
        {
            this.MyType = myType;
        }

        public ICubelet()
        {

        }

        public int PosX
        {
            get;
            set;
        }

        public int PosY
        {
            get;
            set;
        }
        public int Orientation { get; set; }

        public virtual void Draw()
        {
            CubletRenderingData data = CubletWarehouse.cubletDictionary[MyType];
            Vector3f normalVector1, normalVector2, normalVector3, vertex1, vertex2, vertex3;
            Vector2f texel1, texel2, texel3;
            Gl.glColor3f(1, 1, 1);
            float[] values = new float[4]; 
            foreach (Triangle triangle in data.triangleArray)
            {
                try
                {                    
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, triangle.material.GL_ID);
                    Gl.glBegin(Gl.GL_TRIANGLES);
                    Material material = triangle.material;
                    values[0] = material.ambient.X;
                    values[1] = material.ambient.Y;
                    values[2] = material.ambient.Z;
                    values[3] = 1;
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT, values);
                    values[0] = material.diffuse.X;
                    values[1] = material.diffuse.Y;
                    values[2] = material.diffuse.Z;
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_DIFFUSE, values);
                    values[0] = material.specular.X;
                    values[1] = material.specular.Y;
                    values[2] = material.specular.Z;
                    Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, values);
                    Gl.glMaterialf(Gl.GL_FRONT, Gl.GL_SHININESS, 0);
                    triangle.getNormals(data.normalArray, out normalVector1, out normalVector2, out normalVector3);
                    triangle.getTexels(data.texelArray, out texel1, out texel2, out texel3);
                    triangle.getVertices(data.vertexArray, out vertex1, out vertex2, out vertex3);
                    Gl.glNormal3f(normalVector1.X, normalVector1.Y, normalVector1.Z);
                    Gl.glTexCoord2f(texel1.X, texel1.Y);
                    Gl.glVertex3f(vertex1.X, vertex1.Y, vertex1.Z);
                    /*Console.WriteLine(vertex1.X + " " + vertex1.Y + " " + vertex1.Z);
                    Console.WriteLine(vertex2.X + " " + vertex2.Y + " " + vertex2.Z);
                    Console.WriteLine(vertex3.X + " " + vertex3.Y + " " + vertex3.Z);
                    Console.WriteLine("--");*/
                    Gl.glTexCoord2f(texel2.X, texel2.Y);
                    Gl.glVertex3f(vertex2.X, vertex2.Y, vertex2.Z);
                    Gl.glTexCoord2f(texel3.X, texel3.Y);
                    Gl.glVertex3f(vertex3.X, vertex3.Y, vertex3.Z);
                    Gl.glEnd();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }
    }
}

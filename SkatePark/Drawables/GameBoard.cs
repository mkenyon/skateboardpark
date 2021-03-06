﻿using System;
using Tao.OpenGl;
using SkatePark.Primitives;

namespace SkatePark.Drawables
{
    public class GameBoard : ICubelet
    {
        public int BlockPixelSize { get; private set; }
        public int NumBlocks { get; private set; }

        public GameBoard(int blockPixelSize, int numBlocks)
        { 
            this.BlockPixelSize = blockPixelSize;
            this.NumBlocks = numBlocks;
        }

        public bool ShowGrid { get; set; }

        public override void Draw()
        {
            float scaleFactor = 5;
            CubletRenderingData data = CubletWarehouse.cubletDictionary["floor"];
            Material floorMaterial = data.triangleArray[0].material;
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, floorMaterial.GL_ID);
            Gl.glBegin(Gl.GL_QUADS);
            {
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3f(0, 0, 0);
                Gl.glTexCoord2f(scaleFactor, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, 0);
                Gl.glTexCoord2f(scaleFactor, scaleFactor);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(0, scaleFactor);
                Gl.glVertex3f(0, 0, -BlockPixelSize * NumBlocks);
            }
            // Back face.
            {
                Gl.glTexCoord2f(0, scaleFactor);
                Gl.glVertex3f(0, -50, -BlockPixelSize * NumBlocks);

                Gl.glTexCoord2f(scaleFactor, scaleFactor);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, -50, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(scaleFactor, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, -50, 0);

                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3f(0, -50, 0);
            }
            scaleFactor = 2;
            // I don't know what face this is because I don't have enough caffeine.
            {
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3f(0, -50, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(scaleFactor, 0);
                Gl.glVertex3f(0, -50, 0);
                Gl.glTexCoord2f(scaleFactor, scaleFactor);
                Gl.glVertex3f(0, 0, 0);
                Gl.glTexCoord2f(0, scaleFactor);
                Gl.glVertex3f(0, 0, -BlockPixelSize * NumBlocks);
            }
            // This is the face on the front..side.. something
            {
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3f(0, -50, 0);
                Gl.glTexCoord2f(scaleFactor, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, -50, 0);
                Gl.glTexCoord2f(scaleFactor, scaleFactor);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, 0);
                Gl.glTexCoord2f(0, scaleFactor);
                Gl.glVertex3f(0, 0, 0);
            }
            // Other side
            {
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, -50, 0);
                Gl.glTexCoord2f(scaleFactor, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, -50, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(scaleFactor, scaleFactor);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(0, scaleFactor);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, 0);
            }
            {
                Gl.glTexCoord2f(0, 0);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, -50, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(scaleFactor, 0);
                Gl.glVertex3f(0, -50, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(scaleFactor, scaleFactor);
                Gl.glVertex3f(0, 0, -BlockPixelSize * NumBlocks);
                Gl.glTexCoord2f(0, scaleFactor);
                Gl.glVertex3f(BlockPixelSize * NumBlocks, 0, -BlockPixelSize * NumBlocks);
            }
            Gl.glEnd();
        }

        /// <summary>
        /// Draws the grids of the game board.
        /// For now, we colour each grid by its own random color.
        /// </summary>
        /// <param name="isSelectionMode">Specifies whether or not we're drawing in selection mode</param>
        public void DrawGrids(bool isSelectionMode)
        {
            int count = 0;
            if (!isSelectionMode)
            {
                Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_LINE);
                Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_LINE);
            }

            for (int i = 0; i < NumBlocks * BlockPixelSize; i += BlockPixelSize)
            {
                for (int j = 0; j < NumBlocks * BlockPixelSize; j += BlockPixelSize)
                {
                    if( isSelectionMode )
                        Gl.glPushName(count);

                    Gl.glBegin(Gl.GL_QUADS);

                    Gl.glVertex3f(j, 1.5f, -i);
                    Gl.glVertex3f(j + BlockPixelSize, 1.5f, -i);
                    Gl.glVertex3f(j + BlockPixelSize, 1.5f, -i - BlockPixelSize);
                    Gl.glVertex3f(j, 1.5f, -i - BlockPixelSize);

                    Gl.glEnd();

                    if( isSelectionMode )
                        Gl.glPopName();
                    count++;
                }
            }

            if (!isSelectionMode)
            {
                Gl.glPolygonMode(Gl.GL_FRONT, Gl.GL_FILL);
                Gl.glPolygonMode(Gl.GL_BACK, Gl.GL_FILL);
            }
        }
    }
}

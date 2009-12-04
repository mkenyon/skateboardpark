﻿using System;
namespace SkatePark
{
    public enum ToolPanelCommand
    {
        CameraDrag,
        BlockDrag, BlockAdd, BlockDelete
    }

    public enum DragMode
    {
        None, Move, Rotate, Zoom
    }

    public partial class Scene
    {
        private ToolPanelCommand SelectedCommand { get; set; }
        private DragMode SelectedDragMode { get; set; }
        private string SelectedBlockAdd { get; set; }

        private ICubelet[] gridArray;

        private void InitializeGridArray()
        {
            gridArray = new ICubelet[gameBoard.NumBlocks * gameBoard.NumBlocks];
        }

        private void OnBlockSelected(int blockNum)
        {
            
            switch (SelectedCommand)
            {
                case ToolPanelCommand.CameraDrag:
                    // Do nothing
                    break;
                case ToolPanelCommand.BlockDelete:
                    DeleteBlock(blockNum);
                    break;
                case ToolPanelCommand.BlockDrag:
                    if (IsBlockExists(blockNum) != null)
                    {
                        // First, tell everything that the user clicked here.
                        FirstDragCoordinate = blockNum;

                        if (SelectedDragMode == DragMode.Move)
                        {
                            CurrentDragMode = DragMode.Move;
                        }
                        else if (SelectedDragMode == DragMode.Rotate)
                        {

                        }
                    }
                    break;
                case ToolPanelCommand.BlockAdd:
                    BlockAdd(blockNum);
                    break;
                default:
                    break; // Do nothing
            }
        }

        private ICubelet IsBlockExists(int blockNum)
        {
            return gridArray[blockNum];
        }

        private void BlockAdd(int blockNum)
        {
            if (IsBlockExists(blockNum) != null)
            {
                // Do nothing
                return;
            }

            ICubelet newCube = new ICubelet(SelectedBlockAdd);
            newCube.PosX = blockNum % gameBoard.NumBlocks;
            newCube.PosY = blockNum / gameBoard.NumBlocks;

            drawables.Add(newCube);
            gridArray[blockNum] = newCube;
        }

        private void DeleteBlock(int blockNum)
        {
            ICubelet block = IsBlockExists(blockNum);

            if (block != null)
            {
                gridArray[blockNum] = null;
                drawables.Remove(block);
            }
        }

        private void MoveBlock(int firstCoordinate, int newCoordinate)
        {
            // Make sure the new block isn't full
            if (IsBlockExists(newCoordinate) != null)
            {
                return;
            }

            // Move it!
            ICubelet block = gridArray[firstCoordinate];
            block.PosX = newCoordinate % gameBoard.NumBlocks;
            block.PosY = newCoordinate / gameBoard.NumBlocks;

            // Delete old location
            gridArray[firstCoordinate] = null;
            // Move to new location
            gridArray[newCoordinate] = block;
        }
    }
}
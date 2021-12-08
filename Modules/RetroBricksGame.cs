using System;
using System.Drawing;
using System.Collections.Generic;
using RetroBricksGame.GameObjects;
using RetroBricksGame.GameObjects.Abstract;

namespace RetroBricksGame
{
    public class Arena
    {
        // Grid data
        public const int ARENA_WIDTH = 9, ARENA_HEIGHT = 25;
        public const int SPAWN_HEIGHT = 6;
        private Block[][] ArenaMatrix;

        // Shape data
        private Shape CurrentShape;
        private int CurrentShapeId;
        private bool GenerateNew;

        // Controls data
        private bool lockControls;

        // Placeholders
        public const char VACANT = '⬜',
                          OCCUPY = '⬛';

        // Locks
        private readonly object proceedLock = new object();

        public Arena()
        {
            ArenaMatrix = new Block[ARENA_HEIGHT][];

            // Initialize all values to blank
            for (int i = 0; i < ARENA_HEIGHT; i++)
            {
                ArenaMatrix[i] = new Block[ARENA_WIDTH];
                for (int j = 0; j < ARENA_WIDTH; j++)
                    ArenaMatrix[i][j] = new Block(VACANT, BlockType.Air);
            }

            CurrentShape = null;
            CurrentShapeId = -1;
            GenerateNew = true;

            lockControls = false;
        }

        public Block[][] GetArenaMatrix()
        {
            return ArenaMatrix;
        }

        // Re-aligns the center of the current block towards right by one unit
        public void MoveRight()
        {
            if (lockControls)
                return;

            lock (proceedLock)
            {
                Point center = CurrentShape.GetCenter();
                List<Point> hotspots = CurrentShape.GetPoints();

                // Validate movement before execution
                foreach (Point emulatedHP in CurrentShape.EmulateMoveRight())
                {
                    Point currentSpot = new Point(emulatedHP.X + center.X, emulatedHP.Y + center.Y);

                    if (currentSpot.X < 0 || currentSpot.X >= ARENA_HEIGHT)
                    {
                        // Validation failure
                        return;
                    }
                    else if (currentSpot.Y < 0 || currentSpot.Y >= ARENA_WIDTH)
                    {
                        // Validation failure
                        return;
                    }
                    else if (ArenaMatrix[currentSpot.X][currentSpot.Y].blockType == BlockType.Ground)
                    {
                        // Validation failure
                        return;
                    }
                }

                // Delete Occupied blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = VACANT;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Air;
                    }
                    catch (Exception) { }
                }

                CurrentShape.MoveRight();
                center = CurrentShape.GetCenter();
                hotspots = CurrentShape.GetPoints();

                // Reprint blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = OCCUPY;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Object;
                    }
                    catch (Exception) { }
                }
            }
        }
        // Re-aligns the center of the current block towards left by one unit
        public void MoveLeft()
        {
            if (lockControls)
                return;

            lock (proceedLock)
            {
                Point center = CurrentShape.GetCenter();
                List<Point> hotspots = CurrentShape.GetPoints();

                // Validate movement before execution
                foreach (Point emulatedHP in CurrentShape.EmulateMoveLeft())
                {
                    Point currentSpot = new Point(emulatedHP.X + center.X, emulatedHP.Y + center.Y);

                    if (currentSpot.X < 0 || currentSpot.X >= ARENA_HEIGHT)
                    {
                        // Validation failure
                        return;
                    }
                    else if (currentSpot.Y < 0 || currentSpot.Y >= ARENA_WIDTH)
                    {
                        // Validation failure
                        return;
                    }
                    else if (ArenaMatrix[currentSpot.X][currentSpot.Y].blockType == BlockType.Ground)
                    {
                        // Validation failure
                        return;
                    }
                }

                // Delete Occupied blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = VACANT;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Air;
                    }
                    catch (Exception) { }
                }

                CurrentShape.MoveLeft();
                center = CurrentShape.GetCenter();
                hotspots = CurrentShape.GetPoints();

                // Reprint blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = OCCUPY;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Object;
                    }
                    catch (Exception) { }
                }
            }
        }

        // Computes and performs clockwise rotation to the current block by -90 degrees
        public void RotateClockwise()
        {
            if (lockControls)
                return;

            lock (proceedLock)
            {
                Point center = CurrentShape.GetCenter();

                // Validate rotation before execution
                foreach (Point emulatedHP in CurrentShape.EmulateRotateClockwise())
                {
                    Point currentSpot = new Point(emulatedHP.X + center.X, emulatedHP.Y + center.Y);

                    if (currentSpot.X < 0 || currentSpot.X >= ARENA_HEIGHT)
                    {
                        // Validation failure
                        return;
                    }
                    else if (currentSpot.Y < 0 || currentSpot.Y >= ARENA_WIDTH)
                    {
                        // Validation failure
                        return;
                    }
                    else if (ArenaMatrix[currentSpot.X][currentSpot.Y].blockType == BlockType.Ground)
                    {
                        // Validation failure
                        return;
                    }
                }

                List<Point> hotspots = CurrentShape.GetPoints();

                // Delete Occupied blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = VACANT;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Air;
                    }
                    catch (Exception) { }
                }

                CurrentShape.RotateClockwise();
                hotspots = CurrentShape.GetPoints();

                // Reprint blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = OCCUPY;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Object;
                    }
                    catch (Exception) { }
                }
            }
        }
        // Computes and performs counter-clockwise rotation to the current block by +90 degrees
        public void RotateCClockwise()
        {
            if (lockControls)
                return;

            // Validate rotation before execution
            foreach (Point emulatedHP in CurrentShape.EmulateRotateCClockwise())
            {
                Point currentSpot = new Point(emulatedHP.X + CurrentShape.GetCenter().X, emulatedHP.Y + CurrentShape.GetCenter().Y);

                if (currentSpot.X < 0 || currentSpot.X >= ARENA_HEIGHT)
                {
                    // Validation failure
                    return;
                }
                else if (currentSpot.Y < 0 || currentSpot.Y >= ARENA_WIDTH)
                {
                    // Validation failure
                    return;
                }
                else if (ArenaMatrix[currentSpot.X][currentSpot.Y].blockType == BlockType.Ground)
                {
                    // Validation failure
                    return;
                }
            }

            lock (proceedLock)
            {
                List<Point> hotspots = CurrentShape.GetPoints();
                Point center = CurrentShape.GetCenter();

                // Delete Occupied blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = VACANT;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Air;
                    }
                    catch (Exception) { }
                }

                CurrentShape.RotateCClockwise();
                hotspots = CurrentShape.GetPoints();

                // Reprint blocks
                foreach (Point hotspot in hotspots)
                {
                    Point currentSpot = new Point(center.X + hotspot.X, center.Y + hotspot.Y);
                    try
                    {
                        ArenaMatrix[currentSpot.X][currentSpot.Y].character = OCCUPY;
                        ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Object;
                    }
                    catch (Exception) { }
                }
            }
        }

        // Generates new shape data and places in the Arena matrix
        private void GenerateNewShape()
        {
            CurrentShapeId = new Random().Next(6) + 1;

            switch (CurrentShapeId)
            {
                case 1:
                    {
                        CurrentShape = new L();
                        break;
                    }
                case 2:
                    {
                        CurrentShape = new Line();
                        break;
                    }
                case 3:
                    {
                        CurrentShape = new L_Tall();
                        break;
                    }
                case 4:
                    {
                        CurrentShape = new L_Tall_Inverted();
                        break;
                    }
                case 5:
                    {
                        CurrentShape = new Plus();
                        break;
                    }
                default:
                    {
                        CurrentShape = new Triangle();
                        break;
                    }
            }

            Point startingPoint = new Point(CurrentShape.ComputeWidth() / 2, ARENA_WIDTH / 2);
            CurrentShape.SetCenter(startingPoint);

            // Insert into the matrix
            List<Point> hotspots = CurrentShape.GetPoints();

            foreach (Point hotspot in hotspots)
            {
                Point currentSpot = new Point(startingPoint.X + hotspot.X, startingPoint.Y + hotspot.Y);
                ArenaMatrix[currentSpot.X][currentSpot.Y].character = OCCUPY;
                ArenaMatrix[currentSpot.X][currentSpot.Y].blockType = BlockType.Object;
            }
        }

        // Update the state of the game
        public void Proceed()
        {
            lock (proceedLock)
            {
                if (GenerateNew)
                {
                    GenerateNewShape();

                    GenerateNew = false;
                    lockControls = false;
                }

                bool needsGenerateNew = true;
                for (int i = ARENA_HEIGHT - 1; i > 0; i--)
                    for (int j = ARENA_WIDTH - 1; j >= 0; j--)
                    {
                        if (ArenaMatrix[i - 1][j].blockType == BlockType.Object)
                            if (ArenaMatrix[i][j].blockType == BlockType.Air)
                            {
                                // Swap the blocks
                                Block temp = ArenaMatrix[i - 1][j];
                                ArenaMatrix[i - 1][j] = ArenaMatrix[i][j];
                                ArenaMatrix[i][j] = temp;

                                Point center = CurrentShape.GetCenter();
                                if (center.X == i - 1 && center.Y == j)
                                {
                                    // Assign new center
                                    center.X = i;
                                    center.Y = j;

                                    CurrentShape.SetCenter(center);
                                }

                                // Detect collision
                                if (i == ARENA_HEIGHT - 1)
                                {
                                    // Collision detected
                                    lockControls = true;
                                }
                                else if (ArenaMatrix[i + 1][j].blockType == BlockType.Ground)
                                {
                                    // Collision detected
                                    lockControls = true;
                                }

                                needsGenerateNew = false;
                            }
                            else
                            {
                                // Collision detected
                                if (ArenaMatrix[i][j].blockType == BlockType.Ground)
                                    lockControls = true;
                            }
                    }

                if (needsGenerateNew)
                {
                    // Change type of objects to ground
                    for (int i = 0; i < ARENA_HEIGHT; i++)
                        for (int j = 0; j < ARENA_WIDTH; j++)
                            if (ArenaMatrix[i][j].blockType == BlockType.Object)
                                ArenaMatrix[i][j].blockType = BlockType.Ground;

                    GenerateNew = true;
                }
            }
        }
    }
}
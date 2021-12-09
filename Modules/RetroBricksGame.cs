using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using RetroBricksGame.UI;
using RetroBricksGame.UI.Abstract;
using RetroBricksGame.GameObjects;
using RetroBricksGame.GameObjects.Abstract;

namespace RetroBricksGame
{
    public class Arena
    {
        // Grid data
        public static int ARENA_WIDTH = 9, ARENA_HEIGHT = 25;
        public static int SPAWN_HEIGHT = 6;
        private Block[][] ArenaMatrix;
        private int GroundHeight = 0;

        // Game data
        private bool gameRunning;
        private bool gamePaused;

        // Score data
        private int score;
        private int groundStreak;

        // Shape data
        private Shape CurrentShape;
        private int CurrentShapeId;
        private bool GenerateNew;

        // Controls data
        private bool lockControls;

        // Placeholders
        public const char VACANT = '⬜', OCCUPY = '⬛';

        // Locks
        private readonly object proceedLock = new object();
        private readonly object consoleLock = new object();

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

            gameRunning = true;
            gamePaused = false;

            score = groundStreak = 0;

            lockControls = false;
        }

        public Block[][] GetArenaMatrix()
        {
            return ArenaMatrix;
        }

        public int getScore()
        {
            return score;
        }
        public bool isGameRunning()
        {
            return gameRunning;
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

        // Computes the new value of GroundHeight and also rewards scores based on the new height
        public void ComputeHeight()
        {
            int NewHeight = 0;

            for (int i = 0; i < ARENA_HEIGHT; i++)
            {
                int j;
                for (j = 0; j < ARENA_WIDTH; j++)
                {
                    if (ArenaMatrix[i][j].blockType == BlockType.Ground)
                    {
                        NewHeight = ARENA_HEIGHT - (i + 1) + 1;
                        break;
                    }
                }

                if (j != ARENA_WIDTH)
                    break;
            }

            if (NewHeight != GroundHeight)
            {
                GroundHeight = NewHeight;
                groundStreak = 0;
            }
            else
                score += 50 * ++groundStreak;

            score += 100;

            if (GroundHeight >= (ARENA_HEIGHT - SPAWN_HEIGHT))
                gameRunning = false;
        }

        // Update the state of the game
        public void Proceed()
        {
            if (!gameRunning)
                return;

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
                        {
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
                                {
                                    lockControls = true;
                                }
                            }
                        }
                    }

                if (needsGenerateNew)
                {
                    // Change type of objects to ground
                    for (int i = 0; i < ARENA_HEIGHT; i++)
                        for (int j = 0; j < ARENA_WIDTH; j++)
                            if (ArenaMatrix[i][j].blockType == BlockType.Object)
                                ArenaMatrix[i][j].blockType = BlockType.Ground;

                    ComputeHeight();

                    GenerateNew = true;
                }
            }
        }

        // Prints the arena onto the console
        private void PrintArena()
        {
            Console.Clear();

            Block[][] matrix = GetArenaMatrix();

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                    Console.Write(matrix[i][j].character);
                if (i == SPAWN_HEIGHT)
                {
                    // Set a marker
                    Console.Write("<-- The sky's the limit!");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Score: " + score);
            Console.WriteLine("\n(Press ESC to exit game)");
        }
        // Emulates the game play until the game is over or until user chooses to exit
        public void Play()
        {
            Thread controller = new Thread(delegate ()
            {
                ConsoleKeyInfo cki;

                while (true)
                {
                    if (!gameRunning)
                        if (!gamePaused)
                            break;
                        else
                        {
                            Thread.Sleep(750);
                            continue;
                        }

                    cki = Console.ReadKey();
                    ConsoleKey ck = cki.Key;

                    if (ck == ConsoleKey.Escape)
                    {
                        gameRunning = false;
                        gamePaused = true;

                        Menu warningMenu = new Menu();

                        MenuInfo warningInfo = new MenuInfo("Warning");
                        warningInfo.setDescription("Do you want to exit the game?");

                        MenuButton yesButton = new MenuButton("Yes");
                        yesButton.setAction(delegate ()
                        {
                            warningMenu.stopPrint();
                            gamePaused = false;
                        });

                        MenuButton noButton = new MenuButton("Return back to game");
                        noButton.setAction(delegate ()
                        {
                            warningMenu.stopPrint();
                            gameRunning = true;
                            gamePaused = false;
                        });

                        warningMenu.addElement(warningInfo);
                        warningMenu.addElement(yesButton);
                        warningMenu.addElement(noButton);

                        warningMenu.print();
                    } else if (ck == ConsoleKey.RightArrow)
                    {
                        RotateClockwise();

                        lock (consoleLock)
                        {
                            PrintArena();
                        }
                    }
                    else if (ck == ConsoleKey.LeftArrow)
                    {
                        RotateCClockwise();

                        lock (consoleLock)
                        {
                            PrintArena();
                        }
                    }
                    else if (ck == ConsoleKey.S)
                    {
                        Proceed();

                        lock (consoleLock)
                        {
                            PrintArena();
                        }
                    }
                    else if (ck == ConsoleKey.A)
                    {
                        MoveLeft();

                        lock (consoleLock)
                        {
                            PrintArena();
                        }
                    }
                    else if (ck == ConsoleKey.D)
                    {
                        MoveRight();

                        lock (consoleLock)
                        {
                            PrintArena();
                        }
                    }
                }
            });
            controller.Start();

            while (true)
            {
                if (!gameRunning)
                    if (!gamePaused)
                        break;
                    else {
                        Thread.Sleep(750);
                        continue;
                    }

                lock (consoleLock)
                {
                    PrintArena();
                }

                Thread.Sleep(500);
                Proceed();
            }

            PrintArena();
            Console.WriteLine("\nGame Over!");

            Thread.Sleep(5000);
        }
    }

    public class Menu
    {
        // Menu Controls Data
        private List<MenuInteract> elements;

        // Menu Choice Data
        private int selected;

        // Menu Display Data
        private int whitespace_length;
        private bool printMenu;

        public Menu()
        {
            elements = new List<MenuInteract>();

            selected = 0;
            whitespace_length = 0;
            printMenu = false;
        }
        public Menu(params MenuInteract[] elements)
        {
            this.elements = new List<MenuInteract>();

            updateWhitespaceLength();
            setSelected(0);
            printMenu = false;
        }

        // Computes the maximum whitespace length for each control and assigns it to whitespace_length
        private void updateWhitespaceLength()
        {
            int largest = elements[0].getTitle().Length;
            for (int i = 0; i < elements.Count; i++)
            {
                MenuInteract element = elements[i];

                if (element.getId() == 3)
                    continue;

                if (element.getTitle().Length > largest)
                    largest = element.getTitle().Length;
            }

            whitespace_length = largest + 5;
        }

        // Records change of choice
        public void setSelected(int selected)
        {
            if (selected < 0 || selected >= elements.Count)
                return;

            this.selected = selected;
        }
        public int getSelected()
        {
            return selected;
        }

        public void addElement(MenuInteract element)
        {
            elements.Add(element);
            updateWhitespaceLength();
        }
        public void addElement(Func<int, MenuInteract> callback)
        {
            elements.Add(callback.Invoke(selected));
            updateWhitespaceLength();
        }

        public int getElementsCount()
        {
            return elements.Count;
        }
        public List<MenuInteract> getElements()
        {
            return elements;
        }
        // Formats a desired control to it's string equivalent
        public string getFormatElement(int index)
        {
            string result = " ";
            MenuInteract element = elements[index];

            if (index == selected)
                result += "> ";
            else
                result += "  ";

            int id = element.getId();

            switch (id)
            {
                case 0:
                    {
                        // MenuButton

                        MenuButton refactored_element = (MenuButton)element;
                        result += refactored_element.getTitle();

                        int leading_ws_count = whitespace_length - result.Length;
                        while (leading_ws_count-- > 0)
                            result += ' ';

                        break;
                    }
                case 1:
                    {
                        // MenuSelectRange
                        MenuSelectRange refactored_element = (MenuSelectRange)element;
                        result += refactored_element.getTitle();

                        int leading_ws_count = whitespace_length - result.Length;
                        while (leading_ws_count-- > 0)
                            result += ' ';

                        result += "◀ " + refactored_element.getSelected() + " ▶";

                        break;
                    }
                case 2:
                    {
                        // MenuSelect
                        MenuSelect refactored_element = (MenuSelect)element;
                        result += refactored_element.getTitle();

                        int leading_ws_count = whitespace_length - result.Length;
                        while (leading_ws_count-- > 0)
                            result += ' ';

                        result += "◀ " + refactored_element.getOptions()[refactored_element.getSelected()] + " ▶";

                        break;
                    }
                case 3:
                    {
                        // MenuInfo
                        MenuInfo refactored_element = (MenuInfo)element;
                        result += refactored_element.getTitle() + '\n';
                        result += refactored_element.getDescription() + '\n';

                        break;
                    }
            }

            return result;
        }

        // Stops the printing process
        public void stopPrint()
        {
            printMenu = false;
        }
        // Prints the whole menu once
        private void _printMenu()
        {
            int length = getElementsCount();

            Console.Clear();
            for (int i = 0; i < length; i++)
                Console.WriteLine(getFormatElement(i));
        }
        // Emulates the menu
        public void print()
        {
            ConsoleKeyInfo cki;

            printMenu = true;
            _printMenu();

            while (true)
            {
                if (!printMenu)
                    break;

                cki = Console.ReadKey();
                ConsoleKey ck = cki.Key;

                if (ck == ConsoleKey.Enter)
                {
                    performEnter();
                    _printMenu();
                }
                else if (ck == ConsoleKey.RightArrow)
                {
                    performRight();
                    _printMenu();
                }
                else if (ck == ConsoleKey.LeftArrow)
                {
                    performLeft();
                    _printMenu();
                }
                else if (ck == ConsoleKey.DownArrow)
                {
                    setSelected(getSelected() + 1);
                    _printMenu();
                }
                else if (ck == ConsoleKey.UpArrow)
                {
                    setSelected(getSelected() - 1);
                    _printMenu();
                }

                Thread.Sleep(50);
            }
        }

        // Perform enter operation on the selected control
        public void performEnter()
        {
            if (elements[selected].getId() == 0)
                ((MenuButton)elements[selected]).GetAction()();
        }
        // Perform enter operation on the right control
        public void performRight()
        {
            int id = elements[selected].getId();

            switch (id)
            {
                case 1:
                    {
                        // MenuSelectRange
                        MenuSelectRange refactored_element = (MenuSelectRange)elements[selected];
                        refactored_element.setSelected(refactored_element.getSelected() + 1);

                        break;
                    }
                case 2:
                    {
                        // MenuSelect
                        MenuSelect refactored_element = (MenuSelect)elements[selected];
                        refactored_element.setSelected(refactored_element.getSelected() + 1);

                        break;
                    }
            }
        }
        // Perform enter operation on the left control
        public void performLeft()
        {
            int id = elements[selected].getId();

            switch (id)
            {
                case 1:
                    {
                        // MenuSelectRange
                        MenuSelectRange refactored_element = (MenuSelectRange)elements[selected];
                        refactored_element.setSelected(refactored_element.getSelected() - 1);

                        break;
                    }
                case 2:
                    {
                        // MenuSelect
                        MenuSelect refactored_element = (MenuSelect)elements[selected];
                        refactored_element.setSelected(refactored_element.getSelected() - 1);

                        break;
                    }
            }
        }
    }
}
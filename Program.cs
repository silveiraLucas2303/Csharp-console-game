/*
- The code declares the following variables:
    1- Variables to determine the size of the Terminal window.
    2- Variables to track the locations of the player and food.
    3- Arrays `states` and `foods` to provide available player and food appearances
    4- Variables to track the current player and food appearance

- The code provides the following methods:
    - A method to determine if the Terminal window was resized.
    - A method to display a random food appearance at a random location.
    - A method that changes the player appearance to match the food consumed.
    - A method that temporarily freezes the player movement.
    - A method that moves the player according to directional input.
    - A method that sets up the initial game state.

- The code doesn't call the methods correctly to make the game playable. The following features are missing:
    - Code to determine if the player has consumed the food displayed.
    - Code to determine if the food consumed should freeze player movement.
    - Code to determine if the food consumed should increase player movement.
    - Code to increase movement speed.
    - Code to redisplay the food after it's consumed by the player.
    - Code to terminate execution if an unsupported key is entered.
    - Code to terminate execution if the terminal was resized.
*/

using System.Runtime.InteropServices;
internal class Program
{
    // Structure used by GetWindowRect
    struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    private static void Main(string[] args)
    {
        // Import the necessary functions from user32.dll
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
        [DllImport("user32.dll")]
        static extern bool MoveWindow(IntPtr hWnd, int x, int y, int nWidth, int nHeight, bool bRepaint);
        // Constants for the ShowWindow function
        const int SW_MAXIMIZE = 3;
        // Get the handle of the console window
        IntPtr consoleWindowHandle = GetForegroundWindow();
        // Maximize the console window
        ShowWindow(consoleWindowHandle, SW_MAXIMIZE);
        // Get the screen size
        Rect screenRect;
        GetWindowRect(consoleWindowHandle, out screenRect);
        // Resize and reposition the console window to fill the screen
        int width = screenRect.Right - screenRect.Left;
        int height = screenRect.Bottom - screenRect.Top;
        MoveWindow(consoleWindowHandle, screenRect.Left, screenRect.Top, width, height, true);
        Console.ReadKey();



        Random random = new Random();
        Console.CursorVisible = false; //desativa o mouse
        int height1 = 29;//altura limite do alcance do personagem no console
        int width1 = 115;
        bool shouldExit = false;
        bool onlyDirectionals = true;
        bool increaseSpeed = true;


        //bool testData = false;

        bool showPositionData = true;
        // Console position of the player
        int playerX = 0;
        int playerY = 0;

        // Console position of the food
        int foodX = 0;
        int foodY = 0;

        // Available player and food strings
        string[] states = { "('-')", "(^-^)", "(X_X)" };
        string[] foods = { "@@@@@", "$$$$$", "#####" };

        // Current player string displayed in the Console
        string player = states[0];

        // Index of the current food
        int food = 0;

        InitializeGame();
        while (!shouldExit)
        {
            if (TerminalResized())
                break;
            if (AteFood())
            {
                ShowFood();
                ChangePlayer();
                if (checkBadAperance())
                {
                    FreezePlayer();
                }
            }
            if (checkGoodAperance() && increaseSpeed)
            {
                Move(3);
            }
            else Move();
        }

        Console.Clear();
        Console.Write("Console was resized. Program exiting in ");
        for (int i = 5; i > 0; i--)
        {
            Console.SetCursorPosition(40, 0);
            Console.Write(i);
            Thread.Sleep(400);
        }


        // Returns true if the Terminal was resized 
        bool TerminalResized()
        {
            return 1 == 2;//height != Console.WindowHeight - 1 || width != Console.WindowWidth - 5;
        }

        // Displays random food at a random location
        void ShowFood()
        {
            // Update food to a random index
            food = random.Next(0, foods.Length);

            // Update food position to a random location
            foodX = random.Next(0, width1 - player.Length);
            foodY = random.Next(0, height1 - 1);

            // Display the food at the location
            Console.SetCursorPosition(foodX, foodY); //metodo da classe Conosole que configura o local do cursor 
            Console.Write(foods[food]);
        }

        // Changes the player to match the food consumed
        void ChangePlayer()
        {
            player = states[food];
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(player);
        }
;

        // Temporarily stops the player from moving
        void FreezePlayer()
        {
            System.Threading.Thread.Sleep(1000);
            player = states[0];
        }

        // Reads directional input from the Console and moves the player
        void Move(int speed = 1)
        {
            //int currWidth = Console.WindowWidth;
            //int currHeigth = Console.WindowHeight;

            int lastX = playerX;
            int lastY = playerY;

            switch (Console.ReadKey(true).Key) //captura a tecla pressionada. possui a sobrecarga bool, que indica a condição de devolver a tecla pressionada
            {
                case ConsoleKey.UpArrow:
                    playerY--;
                    break;
                case ConsoleKey.DownArrow:
                    playerY++;
                    break;
                case ConsoleKey.LeftArrow:
                    playerX -= speed;

                    break;
                case ConsoleKey.RightArrow:

                    playerX += speed;

                    break;
                case ConsoleKey.Escape:     //tecla esc
                    shouldExit = true;
                    break;
                default:
                    if (onlyDirectionals)
                    {
                        shouldExit = true;
                    }
                    break;
            }

            // Clear the characters at the previous position
            Console.SetCursorPosition(lastX, lastY);
            for (int i = 0; i < player.Length; i++)
            {
                Console.Write(" ");
            }

            // Keep player position within the bounds of the Terminal window
            playerX = (playerX < 0) ? 0 : (playerX >= width1 ? width1 : playerX); // garante que o jogador não sairá do terminal. Se a posição for maior ou igual que o width do terminal ele não sai do lugar
            playerY = (playerY < 0) ? 0 : (playerY >= height1 ? height1 : playerY);

            // Draw the player at the new location  
            Console.SetCursorPosition(playerX, playerY);
            Console.Write(player);

            if (showPositionData)
            {
                Console.SetCursorPosition(1, 32);
                Console.Write($"Heigth: {playerY} - Width: {playerX}\n");
                Console.Write($"food Heigth: {foodY} - Food Width: {foodX}");
            }
        }

        // Clears the console, displays the food and player
        void InitializeGame()
        {
            Console.Clear();
            ShowFood();
            Console.SetCursorPosition(0, 0);
            Console.Write(player);
        }

        bool AteFood()
        {
            /*
            if(checkGoodAperance())
            {
             return playerX == foodX + 2 && playerY == foodY || playerX == foodX + 1 && playerY == foodY;     
            }
            */
            //else

            return playerX == foodX && playerY == foodY;
        }

        bool checkBadAperance()
        {
            return player == "(X_X)";
        }

        bool checkGoodAperance()
        {
            return player == "(^-^)";
        }
    }
}

Console.Title="Console";
        string[,] playArea={
                    {"██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██"},
                    {"██",":)","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","  ","██"},
                    {"██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██","██"},
};

        renderPlayarea(playArea);

        int x = 1; // Initial player X position
        int y = 1; // Initial player Y position
        int lastX=0;
        int lastY=0;
        bool exCaught=false;

        while (true)
        {
            string[] XYreturned = renderPlayer(playArea, x, y).Split(' ');
           try
           {
             x = Convert.ToInt32(XYreturned[0]);
             y = Convert.ToInt32(XYreturned[1]);
             lastX=Convert.ToInt32(XYreturned[2]);
             lastY=Convert.ToInt32(XYreturned[3]);
           }
           catch (IndexOutOfRangeException)
           {
            exCaught=true;
            playArea[x, y] = ":("; 
            playArea[0,0] = "██";
           }
           if (exCaught==false)
           {
            playArea[x, y] = ":)";
            playArea[lastX,lastY] = "  ";
           }
           else
           {
            exCaught=false;
           }
            
            Console.Clear();
            renderPlayarea(playArea);
        }
    

    static void renderPlayarea(string[,] playArea)
    {
        int rows = playArea.GetLength(0);
        int columns = playArea.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write($"{playArea[i, j],-1}");
            }
            Console.WriteLine();
        }
    }

    static string renderPlayer(string[,] playArea, int playerX, int playerY)
    {
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    return tryMove(playerX, playerY, playerX - 1, playerY, playArea);
                case ConsoleKey.DownArrow:
                    return tryMove(playerX, playerY, playerX + 1, playerY, playArea);
                case ConsoleKey.LeftArrow:
                    return tryMove(playerX, playerY, playerX, playerY - 1, playArea);
                case ConsoleKey.RightArrow:
                    return tryMove(playerX, playerY, playerX, playerY + 1, playArea);
                case ConsoleKey.W:
                    return tryMove(playerX, playerY, playerX - 1, playerY, playArea);
                case ConsoleKey.S:
                    return tryMove(playerX, playerY, playerX + 1, playerY, playArea);
                case ConsoleKey.A:
                    return tryMove(playerX, playerY, playerX, playerY - 1, playArea);
                case ConsoleKey.D:
                    return tryMove(playerX, playerY, playerX, playerY + 1, playArea);
                case ConsoleKey.Q:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    static string tryMove(int currentX, int currentY, int nextX, int nextY, string[,] playArea)
    {
        if (playArea[nextX, nextY] == "██")
        {
            return $"{currentX} {currentY}";
        }
        else
        {
            return $"{nextX} {nextY} {currentX} {currentY}";
        }
    }

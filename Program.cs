using System.ComponentModel.Design;
using Custom.ColoredTextHandling;
using Custom.UserInputHandling;
using System.Threading;
using System.Reflection;

class Variables
{
    // playarea definition:
    static public string[,] playArea ={ 
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


    
    static public int x = 1; // player x position(render ":)" at x)
    static public int y = 1; // player y position(render ":)" at y)
    static public int lastX = 0; // last player x (render "  " at x)
    static public int lastY = 0; // last player x (render "  " at y)
    static public int score = 0; // player score (defalut)
    static public int boostPickup = 0; // boost item count (defalut)
    static public int maxScore=50; // win condition score (defalut)
}
internal class Program
{
    
    private static void Main(string[] args)
    {
        Console.Title = "Console";
        int selectedLine = 1;
        menu();

        void menu()
        {
            Console.Clear();

            //pre launch tasks:
            Console.CursorVisible=true;
            renderMenu(selectedLine);

            // menu loop
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow: // move up
                        if (selectedLine != 1)
                        {
                            selectedLine--;
                            renderMenu(selectedLine);
                        }
                        break;
                    case ConsoleKey.DownArrow: // move down
                        if (selectedLine != 3)
                        {
                            selectedLine++;
                            renderMenu(selectedLine);
                        }
                        break;
                    case ConsoleKey.Enter: // preform the function tied to a menu button on "enter"
                        switch (selectedLine)
                        {
                            case 1:
                                game();
                                break;
                            case 2:
                                settings();
                                break;
                            case 3:
                                Environment.Exit(0);
                                break;
                        }
                        break;
                }
            }
        }
        //↓game code↓
        void game()
        {
            // pre-launch tasks
            Console.Clear();
            renderPlayarea();
            // writes the stats
            Console.Write((Variables.score >= Variables.maxScore) ? "You have won" : $"You have {Variables.score} / {Variables.maxScore}$");
            Console.Write((Variables.boostPickup > 0) ? $"\nThe next {Variables.boostPickup} money will be doubled" : "\nYou have no boost items");
            // var + console setup
            Variables.score=0;
            Console.CursorVisible=false;
            Console.ForegroundColor = ConsoleColor.White;

            // game loop
            while (true)
            {
                string[] XYreturned = getNextPlayerPos(Variables.x, Variables.y).Split(' ');
                // player render handling
                if (XYreturned[0] != "x")
                {
                    // converting the string values to int
                    Variables.x = Convert.ToInt32(XYreturned[0]);
                    Variables.y = Convert.ToInt32(XYreturned[1]);
                    Variables.lastX = Convert.ToInt32(XYreturned[2]);
                    Variables.lastY = Convert.ToInt32(XYreturned[3]);

                    // rendering player
                    Variables.playArea[Variables.x,Variables.y] = ":)";
                    Variables.playArea[Variables.lastX,Variables.lastY] = "  ";

                }
                // item spawn handling
                string[] iXYreturned = placeItems().Split(' ');

                if (iXYreturned[0] != "x")
                {
                    switch (Convert.ToInt32(iXYreturned[2]))
                    {
                        case 1:
                            Variables.playArea[Convert.ToInt32(iXYreturned[0]), Convert.ToInt32(iXYreturned[1])] = "1$";
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Green;
                            Variables.playArea[Convert.ToInt32(iXYreturned[0]), Convert.ToInt32(iXYreturned[1])] = "$+";
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case 3:
                            Variables.playArea[Convert.ToInt32(iXYreturned[0]), Convert.ToInt32(iXYreturned[1])] = "2$";
                            break;
                        case 4:
                            Variables.playArea[Convert.ToInt32(iXYreturned[0]), Convert.ToInt32(iXYreturned[1])] = "5$";
                            break;
                    }

                }
                // item effect handling
                if (XYreturned.Length == 5)
                {
                    switch (Convert.ToInt32(XYreturned[4]))
                    {
                        case 1:
                            {
                               if (Variables.boostPickup > 0)
                                {
                                    Variables.score = Variables.score + 2;
                                    Variables.boostPickup--;
                                }
                                else
                                {
                                    Variables.score++;
                                }
                            }
                            break;
                        case 2:
                            menu();
                            break;
                        case 3:
                            Variables.boostPickup = Variables.boostPickup + 3;
                            break;
                        case 4:
                            if (Variables.boostPickup > 0)
                            {
                                Variables.score = Variables.score + 4;
                                Variables.boostPickup--;
                            }
                            else
                            {
                                Variables.score = Variables.score + 2;
                            }
                            break;
                        case 5:
                            if (Variables.boostPickup > 0)
                            {
                                Variables.score = Variables.score + 10;
                                Variables.boostPickup--;
                            }
                            else
                            {
                                Variables.score = Variables.score + 5;
                            }
                            break;
                    }
                    if (Variables.score >= Variables.maxScore)
                    {
                    Variables.playArea[1, 1] = " #"; // if max score reached place exit item
                    }
                }

                // paly area + stats updating
                Console.Clear();
                renderPlayarea();
                Console.Write((Variables.score >= Variables.maxScore) ? "You have won" : $"You have {Variables.score} / {Variables.maxScore}$");
                Console.Write((Variables.boostPickup > 0) ? $"\nThe next {Variables.boostPickup} money will be doubled" : "\nYou have no boost items");
                // delay between frames (ms)
                Thread.Sleep(25);
            }
        }


        void renderPlayarea()
        {
            //renders play area
            int rows = Variables.playArea.GetLength(0);
            int columns = Variables.playArea.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{Variables.playArea[i, j],-1}");
                }
                Console.WriteLine();
            }
        }

        string getNextPlayerPos(int playerX, int playerY)
        {
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    //update player position
                    case ConsoleKey.UpArrow:
                        return tryMove(playerX, playerY, playerX - 1, playerY);
                    case ConsoleKey.DownArrow:
                        return tryMove(playerX, playerY, playerX + 1, playerY);
                    case ConsoleKey.LeftArrow:
                        return tryMove(playerX, playerY, playerX, playerY - 1);
                    case ConsoleKey.RightArrow:
                        return tryMove(playerX, playerY, playerX, playerY + 1);
                    case ConsoleKey.W:
                        return tryMove(playerX, playerY, playerX - 1, playerY);
                    case ConsoleKey.S:
                        return tryMove(playerX, playerY, playerX + 1, playerY);
                    case ConsoleKey.A:
                        return tryMove(playerX, playerY, playerX, playerY - 1);
                    case ConsoleKey.D:
                        return tryMove(playerX, playerY, playerX, playerY + 1);
                    case ConsoleKey.Q:
                        menu();
                        break;
                }
            }
        }
        string tryMove(int currentX, int currentY, int nextX, int nextY)
        {
            switch (Variables.playArea[nextX, nextY])
            {
                //check next tile
                case "██":
                    return "x x";
                case "  ":
                    return $"{nextX} {nextY} {currentX} {currentY}";
                case "1$":
                    return $"{nextX} {nextY} {currentX} {currentY} {1}";
                case " #":
                    return $"{nextX} {nextY} {currentX} {currentY} {2}";
                case "$+":
                    return $"{nextX} {nextY} {currentX} {currentY} {3}";
                case "2$":
                    return $"{nextX} {nextY} {currentX} {currentY} {4}";
                case "5$":
                    return $"{nextX} {nextY} {currentX} {currentY} {5}";
                default:
                    return "";//INTENTIONALY BLANK IF RUN A FAIL OCCURS
            }
        }
        string placeItems()
        {
            Random randomPos = new Random();
            Random tryItem = new Random();


            int itemX = randomPos.Next(1, Variables.playArea.GetLength(0));
            int itemY = randomPos.Next(1, Variables.playArea.GetLength(0));

            if (Variables.playArea[itemX, itemY] == "  " && itemX != 1 && itemY != 1 && tryItem.Next(1, 11) == 1) //place 1$
            {
                return $"{itemX} {itemY} {1}";
            }
            else if (Variables.playArea[itemX, itemY] == "  " && itemX != 1 && itemY != 1 && tryItem.Next(1, 26) == 1) //place boosts
            {
                return $"{itemX} {itemY} {2}";
            }
            else if (Variables.playArea[itemX, itemY] == "  " && itemX != 1 && itemY != 1 && tryItem.Next(1, 21) == 1) //place 2$
            {
                return $"{itemX} {itemY} {3}";
            }
            else if (Variables.playArea[itemX, itemY] == "  " && itemX != 1 && itemY != 1 && tryItem.Next(1, 51) == 1) //place 5$
            {
                return $"{itemX} {itemY} {4}";
            }
            else
            {
                return "x x";
            }
        }
        void renderMenu(int selectedLine)
        {
            Thread.Sleep(50);
            Console.Clear();
            Console.Write("=-=-=-++-=-=-=\nTAKE THE MONEY\n=-=-=-++-=-=-=\n\n", Console.ForegroundColor = ConsoleColor.White);
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 1:
                        if (selectedLine == 1)
                        {
                            Console.WriteLine($"PLAY", Console.ForegroundColor = ConsoleColor.Green);
                        }
                        else
                        {
                            Console.WriteLine("PLAY", Console.ForegroundColor = ConsoleColor.White);
                        }
                        break;
                    case 2:
                        if (selectedLine == 2)
                        {
                            Console.WriteLine($"Settings", Console.ForegroundColor = ConsoleColor.Green);
                        }
                        else
                        {
                            Console.WriteLine("Settings", Console.ForegroundColor = ConsoleColor.White);
                        }
                        break;
                    case 3:
                        if (selectedLine == 3)
                        {
                            Console.WriteLine($"Quit", Console.ForegroundColor = ConsoleColor.DarkGreen);
                        }
                        else
                        {
                            Console.WriteLine("Quit", Console.ForegroundColor = ConsoleColor.Red);
                        }
                        break;
                }
            }
        }
        void settings()
        {
            //settigs screen
            Console.CursorVisible=true;
            Console.Clear();
            Console.ForegroundColor=ConsoleColor.White;
            Variables.maxScore=UserInput.GetInt($"Set goal score (recomanded: 50, now {Variables.maxScore}): ",0,10000);
            //return to menu
            menu();
        }
    }
}

// how to:
// -----------
// Add a new item:
//
// 1. go into the placeItems methode
// 2. go above the else return "x x" statement, and copy one of the existing if else statements
// 3. change the second number in tryItem.Next to set the rarity (higher number = more rare)
// 4. change the number in the return statement to the one above + 1
// 5. go into the tryMove methode
// 6. copy one of the existing cases above the defalut
// 7. in the quotes enter a 2 character item skin
// 8. change the number in the return statement to the one above + 1
// 9. go into the part of the game loop labeled "iten spawn handling"
// 10. copy one of the existing cases and change its number to the one above + 1
// 11. in the quotes enter your item skin
// 12. go into the part labeled "item effect handling"
// 13. create a new case under the last one and change its number to the one above + 1
// 14. put the desired code into that case if you don't know how watch https://www.youtube.com/watch?v=u_Qv5IrMUqg&list=PLPV2KyIb3jR4CtEelGPsmPzlvP7ISPYzR&index=4 at 12 minutes in
// -----------
// modify the play area:
//
// 1. go in the Variables class (Ln.7)
// 2. edit the playarea array
// 3. change the units to "██" for a wall, or "  " for air
// 4. leave the perimeter walls intact
// ----------- 
// change the player starting position:
//
// 1. go in the Variables class (Ln.7)
// 2. change the x and y variables
// 3. change the position of the player sprite to the x y coordinates in the playArea
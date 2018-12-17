using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMemory
{
    class program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("New Game!");
            Console.WriteLine("Choose board size");
            int size = int.Parse(Console.ReadLine());           
            int[,] board = GameBoard(size);
                       
            
        }
        private static int[,] GameBoard(int size)
        {

            Random rnd = new Random();
            size = size % 2 == 0 ? size : size + 1;
            int[,] gBoard = new int[size, size];
            int[] arrTemplate = new int[(size * size)];
            for (int i = 0; i < arrTemplate.Length; i++)
            {
                arrTemplate[i] = arrTemplate[i + 1] = rnd.Next(1, 99);
                i++;
            }
            for (int i = 0; i < arrTemplate.Length-1; i++)
            {
                int counter = 0;
                for (int j = 0; j < arrTemplate.Length-1; j++)
                {
                    if (arrTemplate[i] == arrTemplate[j])
                    {
                        counter++;
                        if (counter > 2)
                        {
                            arrTemplate[i] = arrTemplate[i + 1] = rnd.Next(1, 99);
                            counter--;
                            i++;
                        }
                    }
                }

                

            }

            int pos = 0;
            for (int i = 0; i < gBoard.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < gBoard.GetLength(1); j++)
                {
                    gBoard[i, j] = arrTemplate[pos];
                    pos++;
                }

            }
            
            Shuffle(gBoard);
            Guess(gBoard, 1);
            return gBoard;
        }
        private static void Shuffle(int[,] gBoard)
        {
            Random rnd = new Random();
            for (int i = 0; i < gBoard.GetLength(0); i++)
            {

                for (int j = 0; j < gBoard.GetLength(1); j++)
                {
                    Swap(gBoard, i, j, rnd.Next(gBoard.GetLength(0) - i), rnd.Next(gBoard.GetLength(1) - j));
                    Swap(gBoard, i, j, rnd.Next(gBoard.GetLength(0) - i), rnd.Next(gBoard.GetLength(1) - j));
                }

            }

        }
        private static void Swap(int[,] gBoard, int index1, int index2, int rnd1, int rnd2)
        {
            int temp = 0;

            temp = gBoard[index1, index2];
            gBoard[index1, index2] = gBoard[rnd1, rnd2];
            gBoard[rnd1, rnd2] = temp;

        }
        private static void Print(int[,] gBoard,int current1,int current2)
        {
            bool found1 = false, found2 = false;
            for (int i = 0; i < gBoard.GetLength(0); i++)
            {                
                Console.WriteLine();
                for (int j = 0; j < gBoard.GetLength(0); j++)
                {
                    if(gBoard[i, j] == 0)
                    {
                        Console.Write(gBoard[i, j] + " ");
                    }
                    else if(gBoard[i, j] == current1&&found1==false)
                    {
                        found1 = true;
                        Console.Write(current1+" ");                        
                    }
                    else if(gBoard[i, j]==current2&&found2==false)
                    {
                        found2=true;
                        Console.Write(current2+" ");                        
                    }
                    else
                        Console.Write("X ");
                }

            }
        }        
        private static void Guess(int[,] gBoard, int turn)
        {
            
            int player1Score= 0,player2Score= 0,row1,col1,row2,col2;
            bool guess;
            do
            { 
            do
            {

                do
                {
                    Print(gBoard,-1,-1);
                    Console.WriteLine($"Player{turn} turn");
                    do
                    {
                        do
                        {
                            Console.WriteLine("Now choose first card row!");
                            row1 = int.Parse(Console.ReadLine());
                            if (row1 < 0 || row1 >= gBoard.GetLength(0))
                                Console.WriteLine($"Invalid input try again! (Values must be between 0-{gBoard.GetLength(0)-1})");
                        } while (row1 < 0 || row1 >= gBoard.GetLength(0));
                        do
                        { 
                        Console.WriteLine("Now choose first card collum!");
                        col1 = int.Parse(Console.ReadLine());
                            if (col1 < 0 || col1 >= gBoard.GetLength(1))
                                Console.WriteLine($"Invalid input try again! (Values must be between 0-{gBoard.GetLength(1)-1})");
                        } while (col1 < 0 || col1 >= gBoard.GetLength(0));
                        if (gBoard[row1, col1] != 0)
                            break;
                        Console.WriteLine("This card already been revealed pick another one!");
                    } while (gBoard[row1, col1] == 0);
                    Console.WriteLine($"The value is {gBoard[row1, col1]}!");
                        Print(gBoard, gBoard[row1, col1],-1);
                    do
                    {
                        Console.WriteLine("Now choose Second card row!");
                        row2 = int.Parse(Console.ReadLine());
                        if (row2 < 0 || row2 >= gBoard.GetLength(0))
                            Console.WriteLine($"Invalid input try again! (Values must be between 0-{gBoard.GetLength(0)-1})");
                    } while (row2 < 0 || row2 >= gBoard.GetLength(0));
                    do
                    {     
                    Console.WriteLine("Now choose Second card collum!");
                    col2 = int.Parse(Console.ReadLine()); 
                       if (col2 < 0 || col2 >= gBoard.GetLength(1))
                                Console.WriteLine($"Invalid input try again! (Values must be between 0-{gBoard.GetLength(1)-1})");
                        } while (col2 < 0 || col2 >= gBoard.GetLength(1));
                        if (gBoard[row1, col1] != 0)
                            break;
                        Console.WriteLine("This card already been revealed pick another one!");
                    } while (gBoard[row2, col2] == 0);
                    if (row1 == row2 && col1 == col2)
                    {
                        do
                        {
                            Console.WriteLine("You have chosen the same card, Please try again");
                            Console.WriteLine("Now guess Second number row!");
                            row2 = int.Parse(Console.ReadLine());
                            Console.WriteLine("Now guess Second number collum!");
                            col2 = int.Parse(Console.ReadLine());
                        } while (row1 == row2 && col1 == col2);
                    }
                    Console.WriteLine($"The value is {gBoard[row2, col2]}!");
                    Print(gBoard, gBoard[row1, col1], gBoard[row2, col2]);
                    guess = gBoard[row1, col1] == gBoard[row2, col2];
                    if (guess)
                    {
                        gBoard[row1, col1] = gBoard[row2, col2] = 0;
                        Console.WriteLine("Correct!");
                        if (turn == 1)
                        {
                            player1Score++;
                          
                        }
                        else
                        {
                            player2Score++;
                        }
                        if (Checkwin(gBoard, player1Score, player2Score))
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong!");

                    }
                } while(guess);
                turn = turn == 1 ? turn=2: turn = 1;
                Console.WriteLine();
                ScoreCount(player1Score, player2Score);
            } while(!Checkwin(gBoard, player1Score, player2Score));

            NewGame();
        }
        private static void ScoreCount(int player1Score, int player2Score)
        {

            Console.WriteLine($"Player 1 score {player1Score}!. Player 2 score {player2Score}!");

        }
       private static bool Checkwin(int[,]gBoard,int player1Score,int player2Score)
        {
            for (int i = 0; i < gBoard.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < gBoard.GetLength(0); j++)
                {
                    if (gBoard[i, j] != 0)
                        return false;  
                }

            }
            ScoreCount(player1Score, player2Score);
            if (player1Score>player2Score)
            {
                Console.WriteLine("Congrats Player 1 wins!");
            }else if (player2Score >player1Score)
            {
                Console.WriteLine("Congrats Player 2 wins!");
            }
            else
                Console.WriteLine("A tie!");
            return true;
        }
       private static void NewGame()
        {
            Console.WriteLine("Start new game? (Y/N)");
            string start = Console.ReadLine();
            switch (start.ToUpper())
            {
                case "Y":
                    program.Main(null);
                    break;
                case "N":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    NewGame();
                    break;
            }
        }
    }
}



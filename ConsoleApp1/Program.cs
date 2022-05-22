using System;
using System.Text.RegularExpressions;

namespace myApp;

class Program
{
    private static char[,] _board = new char[3, 3]
        {
            {'1', '2', '3' },
            {'4', '5', '6' },
            {'7', '8', '9' },
        };
    private static char playerOneMark = 'O';
    private static char playerTwoMark = 'X';

    public static void Main(string[] args)
    {
        int hasWinner = -1;
        int playerOneChosenField;
        int playerTwoChosenField;

        DrawBoard();

        while (true)
        {
            playerOneChosenField = GetUserInput("Player one");
            SelectField(playerOneMark, playerOneChosenField);
            DrawBoard();
            hasWinner = CheckWinner();

            if (hasWinner  != -1) { break; }
            if (IsDraw())
            {
                Console.WriteLine("It is a draw!");
                break;
            }

            playerTwoChosenField = GetUserInput("Player two");
            SelectField(playerTwoMark, playerTwoChosenField);
            DrawBoard();
            hasWinner = CheckWinner();

            if(hasWinner != -1) { break; }
            if (IsDraw())
            {
                Console.WriteLine("It is a draw!");
                break;
            }
        }

        Console.WriteLine($"The winner is {(hasWinner == 1 ? "player one" : "player two")}!");
    }

    public static int GetUserInput(string playerName)
    {
        int userInput;
        bool isCorrect = true;
        
        do
        {
            Console.WriteLine($"{playerName} choose field");
            int.TryParse(Console.ReadLine(), out userInput);

            if(userInput > 9 || userInput < 1 || IsFieldTaken(userInput))
            {
                Console.WriteLine($"Incorrect value. It is still {playerName} turn!");
                isCorrect = false;
                continue;
            }

            isCorrect = true;
        } while(!isCorrect);

        return userInput;
    }

    public static int CheckWinner()
    {
        //Vertical and horizontal check
        for (int i = 0; i < _board.GetLength(0); i++)
        {
            if(_board[i, 0] == _board[i, 1] && _board[i, 0] == _board[i, 2])
            {
                return GetPlayerIndexByMark(_board[i, 0]); 
            }

            if(_board[0, i] == _board[1, i] && _board[0, i] == _board[2, i])
            {
                return GetPlayerIndexByMark(_board[i, 0]);
            }

        }

        // Diagonal check
        if (_board[0, 2] == _board[1,1] && _board[0, 2] == _board[2,0])
        {
            return GetPlayerIndexByMark(_board[0, 2]);
        }
        
        if(_board[0, 0] == _board[1, 1] && _board[0, 0] == _board[2,2])
        {
            return GetPlayerIndexByMark(_board[0, 0]);
        }

        return -1;
    }

    public static void DrawBoard()
    {
        Console.Clear();

        for (int i = 0; i < _board.GetLength(0); i++)
        {
            for(int j = 0; j < _board.GetLength(1); j++) { 
                if(j == _board.GetLength(1) - 1 && i != _board.GetLength(0) - 1)
                {
                    Console.WriteLine("_____|_____|_____");
                } else if(j == 1)
                {
                    Console.WriteLine($"  {_board[i, 0]}  |  {_board[i, 1]}  |  {_board[i, 2]}  ");
                } 
                else
                {
                    Console.WriteLine("     |     |     ");
                }
            }
        }
    }

    public static void SelectField(char mark, int fieldValue)
    {
        var fieldCoords = GetFieldCoords(fieldValue);
        
        _board[fieldCoords[0], fieldCoords[1]] = mark;
    }
    
    public static int[] GetFieldCoords(int fieldValue)
    {
        int firstDimension;
        int secondDimension;
        int normalizedIndex = fieldValue - 1;

        if (normalizedIndex < 3)
        {
            firstDimension = 0;
            secondDimension = normalizedIndex;
        }
        else if (normalizedIndex < 6)
        {
            firstDimension = 1;
            secondDimension = normalizedIndex - 3;
        }
        else
        {
            firstDimension = 2;
            secondDimension = normalizedIndex - 6;
        }

        return new int[] { firstDimension, secondDimension };
    }

    public static bool IsDraw()
    {
        foreach (char tile in _board)
        {
            if(tile != playerOneMark && tile != playerTwoMark)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsFieldTaken(int fieldValue)
    {
        var fieldCoords = GetFieldCoords(fieldValue);

        return _board[fieldCoords[0], fieldCoords[1]] == playerOneMark || _board[fieldCoords[0], fieldCoords[1]] == playerTwoMark;
    }

    public static int GetPlayerIndexByMark(char playerMark)
    {
        if(playerMark == playerOneMark)
        {
            return 1;
        } else if (playerMark == playerTwoMark)
        {
            return 2;
        } else
        {
            return -1;
        }
    }
}

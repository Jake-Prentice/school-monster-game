
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace monster

{
    class Program
    {
        public static GameCanvas Cavern;
        public static Actor Player;
        public static Actor Monster;
        public static Actor Flask;
        public static List<Actor> Traps = new List<Actor>();

        public static int NumOfTraps = 2;

        static void Main(string[] args)
        {

            Cavern = new GameCanvas(width: 7, height: 5);

            Player = Cavern.CreateActor(graphic: '*');
            Monster = Cavern.CreateActor(graphic: 'M', isVisible: false);
            Flask = Cavern.CreateActor(graphic: 'F');

            CreateTraps(n: 2);
         
            int Choice = 0;
            while (Choice != 4)
            {
                DisplayMenu();
                Choice = GetMainMenuChoice();

                switch(Choice) {
                    //start game
                    case 1:
                        
                        Player.MoveTo(Cavern.GetRandomPosition());
                        Monster.MoveTo(Cavern.GetRandomPosition());
                        Flask.MoveTo(Cavern.GetRandomPosition());

                        PlayGame();
                        break;

                    //load game
                    case 2:
                        break;
                    
                    //save game
                    case 3:
                        break;

                    //quit game;
                    case 4:
                        break;

                }
            }
        }

        public static void CreateTraps(int n)
        {

            for (int i=0; i<n; i++)
            {
                Actor newTrap = Cavern.CreateActor(graphic: 'T', isVisible: true);
                newTrap.MoveTo(Cavern.GetRandomPosition());
                Traps.Add(newTrap);
            }
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("\nMAIN MENU\n");
            Console.WriteLine("1. Start new game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("3. Save current game");
            Console.WriteLine("4. Quit");
            Console.WriteLine();
            Console.WriteLine("Please enter your choice: ");
        }
        public static int GetMainMenuChoice()
        {
            int Choice; Choice = int.Parse(Console.ReadLine());
            Console.WriteLine();
            return Choice;
        }

        public static void DisplayMoveOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Enter N to move NORTH");
            Console.WriteLine("Enter E to move EAST");
            Console.WriteLine("Enter S to move SOUTH");
            Console.WriteLine("Enter W to move WEST");
            Console.WriteLine("Enter M to return to the Main Menu");
            Console.WriteLine();
        }
        public static char GetMove()
        {
            char Move;
            Move = char.Parse(Console.ReadLine());
            Console.WriteLine();
            return Move;
        }
        public static void MakeMove(char Direction)
        {
            char[] validMoves = { 'n', 's', 'w', 'e', 'm'};
            if (!validMoves.Contains(Direction)) throw new Exception();

            switch (Direction)
            {
                case 'n':
                    Player.SetY(Player.Y - 1);
                    return;
                case 's':
                    Player.SetY(Player.Y + 1);
                    return;
                case 'w':
                    Player.SetX(Player.X - 1);
                    return;
                case 'e':
                    Player.SetX(Player.X + 1);
                    return;
            }
        }

        public static void MakeMonsterMove()
        {

            if (Monster.Y < Player.Y)
            {
                Monster.SetY(Monster.Y + 1);
            }
            else if (Monster.Y > Player.Y)
            {
                Monster.SetY(Monster.Y - 1);
            }

            else if (Monster.X < Player.X)
            {
                Monster.SetX(Monster.X + 1);
            }

            else if (Monster.X > Player.X)
            {
                Monster.SetX(Monster.X - 1);
            }
        }

        public static void DisplayLostGameMessage()
        {
            Console.WriteLine("ARGHHHHHH! The monster has eaten you. GAME OVER.");
            Console.WriteLine("Maybe you will have better luck the next time you play MONSTER!");
            Console.WriteLine();
        }

        public static void DisplayWonGameMessage()
        {
            Console.WriteLine("you have won the game!");
        }


        public static void PlayGame()
        {
            bool Eaten = false;
            bool TrapIsTriggered = false;
            bool HasWon = false;

            int Count = 0;
            char MoveDirection = ' ';

            //I just wanted to try out events :)
            Player.OnMove += (Actor self) => {
                if (self.DoesCollideMultiple(Traps)) TrapIsTriggered = true;
            };

            Cavern.Draw();

            while (!(Eaten || MoveDirection == 'm' || HasWon))
            {
                while (true)
                {
                    try
                    {
                        DisplayMoveOptions();
                        
                        MoveDirection = GetMove();
                        if (MoveDirection == 'm') return;

                        MakeMove(MoveDirection);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nInvalid Input!");
                        Cavern.Draw();
                    }

                }
                
                Cavern.Draw();

                Eaten = Monster.DoesCollideWith(Player);
                HasWon = Player.DoesCollideWith(Flask);
                
                if (!Eaten && !HasWon) {
                    Cavern.Draw();

                    Count = 0;

                    while (Count < 2 && !Eaten)
                    {
                        if (TrapIsTriggered)
                        {
                            Monster.IsVisible = true;
                            MakeMonsterMove();
                        }

                        Eaten = Monster.DoesCollideWith(Player);

                        Console.WriteLine();
                        Console.WriteLine("Press Enter key to continue");
                        Console.ReadLine();

                        Cavern.Draw();
                        Count = Count + 1;
                    }
                }
                
                if (Eaten) DisplayLostGameMessage();
                else if (HasWon) DisplayWonGameMessage();

            }

        }
    }

}

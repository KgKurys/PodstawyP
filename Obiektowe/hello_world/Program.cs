using System;
using System.IO.Pipelines;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Xml.Serialization;

namespace MyFirstProgram
{
    class Program
    {
        static void Main(string[] args)
        {
          /*  Console.WriteLine("Podaj długość dłuższej przy prostokątnej: ");
            double a = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Podaj długość krótszej przy prostokątnej: ");
            double b = Convert.ToDouble(Console.ReadLine());

            double c = Math.Sqrt((a*a)+(b*b));
            Console.WriteLine("Dlgosc przeciwprostokątnej to: " + c);
        */

        // Losowe liczby zgadywanka
        
            Random random = new Random();        
            bool playAgain = true;
            /*
            int max = 100;
            int min = 1;
            int number;
            int guess;
            int guesses;
            String response;
            
            
            while(playAgain)
            {
                guess = 0;
                guesses = 0;
                number = random.Next(min, max) + 1;
                int hint1 = number - 10;
                int hint2 = number + 10;
                while(guess !=number)
                {
                    Console.WriteLine("Guess the number between 1 and 100: ");
                    Console.WriteLine($"The number is between: {hint1} and {hint2}");
                    guess = Convert.ToInt32(Console.ReadLine());
                    if(guess == number)
                    {
                        Console.WriteLine("Congratulations you guessed Right!");                        
                    }
                    else
                    {
                        Console.WriteLine("You Guessed worng, the number was " + number);
                        break;
                    }
                    guesses++;
                    
                    
                    
                }
                Console.WriteLine("Do uou wanna play again? yes/no: ");
                response = Console.ReadLine().ToLower();
                playAgain = response == "yes" || response =="y";
                

            }
            */




            //Papier kamien
            /*
            String player;
            String computer;
            String answer;

            while(playAgain==true)
            {
                player="";
                computer="";
                answer="";

                while(player != "ROCK" && player != "PAPER" && player != "SCISSORS")
                {
                  Console.WriteLine("enter ROCK, PAPER or SCISSORS: ");
                  player = Console.ReadLine();
                  player = player.ToUpper(); 
                }

                switch(random.Next(1,4))
                {
                    case 1:
                    computer = "ROCK";
                    break;

                    case 2:
                    computer = "PAPER";
                    break;

                    case 3:
                    computer = "SCISSORS";
                    break;
                }
                Console.WriteLine("Player: " + player);
                Console.WriteLine("Computer: " + computer);
                
                switch(player)
                {
                    case "ROCK":
                    if (computer == "ROCK")
                    {
                        Console.WriteLine("Its a draw");
                    }
                    else if(computer == "PAPER")
                    {
                       Console.WriteLine("You Lose"); 
                    }
                    else
                    {
                        Console.WriteLine("You Win");
                    }
                    break;

                    case "PAPER":
                    if (computer == "ROCK")
                    {
                        Console.WriteLine("You Win");
                    }
                    else if(computer == "PAPER")
                    {
                       Console.WriteLine("Its a draw"); 
                    }
                    else
                    {
                        Console.WriteLine("You Lose");
                    }
                    break;

                    case "SCISSORS":
                    if (computer == "ROCK")
                    {
                        Console.WriteLine("You Lose");
                    }
                    else if(computer == "PAPER")
                    {
                       Console.WriteLine("You Win"); 
                    }
                    else
                    {
                        Console.WriteLine("You Lose");
                    }
                    break;
                }
                Console.Write("Would you like to play again (Y/N): ");
                answer = Console.ReadLine();
                answer = answer.ToUpper();

                if (answer == "Y")
                {
                    playAgain = true;
                }
                else
                {
                    playAgain = false;
                }
            }
            Console.WriteLine("Thanks for playing!");
            */

            //Kalkulator

            bool IsRunning = true;
            double Number = 0;
            double Number1 = 0;
            double Number2 = 0;
            double result = 0;
            String choice;

            
            

            while(IsRunning)
            {
                String answer = "";
                Console.WriteLine("Welcome in my calculator, pick your poison");
                Console.WriteLine("+, -, * , / or sqr");
                choice = Console.ReadLine();

                if(choice == "sqr")
                {
                    Console.WriteLine("Enter number: ");
                    Number = Convert.ToDouble(Console.ReadLine());
                }
                else
                {

                    Console.WriteLine("Enter first number: ");
                    Number1 = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Enter second number: ");
                    Number2 = Convert.ToDouble(Console.ReadLine());
                }

                
                switch(choice)
                {
                    case "+":
                    result = Number1 + Number2;
                    Console.WriteLine("Thats equal to: " + result);
                    break;

                    case "-":
                    result = Number1 - Number2;
                    Console.WriteLine("Thats equal to: " + result);
                    break;

                    case "*":
                    result = Number1 * Number2;
                    Console.WriteLine("Thats equal to: " + result);
                    break;

                    case "/":
                    result = Number1 / Number2;
                    Console.WriteLine("Thats equal to: " + result);
                    break;

                    case "sqr":
                    result = Math.Sqrt(Number);
                    Console.WriteLine("Thats equal to: " + result);
                    break;
                    
                    default:
                    Console.WriteLine("Thats not a valid option");
                    break;

                    

                }
                Console.WriteLine("Do you wanna continue ? y/n" );
                answer = Console.ReadLine().ToLower();
                if(answer == "y")
                {
                    playAgain = true;
                }
                else
                {
                    playAgain = false;
                }
                

            }

        

        }
    }
}
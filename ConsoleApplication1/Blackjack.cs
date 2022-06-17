using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ch10CardLib;

namespace Ch10CardClient
{
    class Blackjack
    {
        public const int INTIAL_CASH = 500;
        static void Main(string[] args)
        {
            Dealer d = new Dealer();
            Player p = new Player(INTIAL_CASH, d);
            char c;
            int b;
            Console.WriteLine("Your balance is: {0}", p.GetCash());
            Console.WriteLine();
            b = GetBet();
            while (b != 0)
            {
                Console.Write("Your cards: ");
                p.Hit();
                p.Hit();
                Console.WriteLine(p.HandToString());
                d.Hit();
                Console.Write("Dealer show card: ");
                Console.WriteLine(d.HandToString());
                Console.WriteLine("Your score: {0}", p.GetScore());
                Console.Write("Would you like to (H)it or to (S)tay? ");
                c = Convert.ToChar(Console.ReadLine());
                Console.WriteLine();
                bool bust = false;
                while (c == 'h' && !bust)
                {
                    p.Hit();
                    Console.Write("Your cards: ");
                    Console.WriteLine(p.HandToString());
                    Console.WriteLine("Your score: {0}", p.GetScore());
                    if (p.GetScore() > 21)
                    {
                        Console.WriteLine("You are BUST!!! Dealer WINS!");
                        bust = true;
                        break;
                    }
                    Console.Write("Would you like to (H)it or to (S)tay? ");
                    c = Convert.ToChar(Console.ReadLine());
                    Console.WriteLine();
                }
                if (bust)
                    p.AddCash(-b);
                else
                {
                    while (d.GetScore() < 17)
                        d.Hit();
                    Console.WriteLine("Dealer's cards: " + d.HandToString());
                    Console.WriteLine("Dealer's score: {0}", d.GetScore());
                    if (d.GetScore() > 21)
                    {
                        Console.WriteLine("The dealer is BUST!!!! You WIN!!! Congratulations!!!");    //YAY!
                        p.AddCash(b);
                    }
                    else if (d.GetScore() < p.GetScore())
                    {
                        Console.WriteLine("You WIN!!! Congratulations!!!");  //YAY!
                        p.AddCash(b);
                    }
                    else
                    {
                        Console.WriteLine("Dealer WINS!");
                        p.AddCash(-b);
                    }
                }
                p.HandEmpty();
                d.HandEmpty();
                Console.WriteLine("Your balance is: {0}", p.GetCash());
                Console.WriteLine();
                b = GetBet();
            }
            if (p.GetCash() > 500)
                Console.WriteLine("Congratulations and come back again soon.");
            else if (p.GetCash() < 0)
                Console.WriteLine("Billy, the Bone Breaker, is responsible for collections and will be in touch very soon.");
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static int GetBet()
        {
            int b;
            Console.Write("Bet amount in dollars: ");
            b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            while (b > 250)
            {
                Console.WriteLine("You cannot bet more than $250! Bet again.");
                Console.Write("Bet amount in dollars: ");
                b = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
            }
            return b;
        }
    }
}
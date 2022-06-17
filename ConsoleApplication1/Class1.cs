using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ch10CardLib
{
    public enum Rank
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }

    public enum Suit
    {
        Club,
        Diamond,
        Heart,
        Spade,
    }

    public class Card
    {
        public readonly Rank rank;
        public readonly Suit suit;
        private Card()
        {
            throw new System.NotImplementedException();
        }
        public Card(Suit newSuit, Rank newRank)
        {
            suit = newSuit;
            rank = newRank;
        }
        public int GetRank()
        {
            return (int)rank;
        }
        public override string ToString()
        {
            string[] srank = { "X", "A", "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K" };
            string[] ssuit = { "\x05", "\x04", "\x03", "\x06" };
            return srank[(int)rank] + ssuit[(int)suit];
        }
    }

    public class Deck
    {
        private Card[] cards;
        public Deck()
        {
            cards = new Card[52];
            for (int suitVal = 0; suitVal < 4; suitVal++)
                for (int rankVal = 1; rankVal < 14; rankVal++)
                    cards[suitVal * 13 + rankVal - 1] = new Card((Suit)suitVal, (Rank)rankVal);
        }
        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= 51)
                return (cards[cardNum]);
            else
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum, "Value must be between 0 and 51"));
        }
        public void Shuffle()
        {
            Card[] newDeck = new Card[52];
            bool[] assigned = new bool[52];
            Random sourceGen = new Random();
            for (int i = 0; i < 52; i++)
            {
                int destCard = 0;
                bool foundCard = false;
                while (foundCard == false)
                {
                    destCard = sourceGen.Next(52);
                    if (assigned[destCard] == false)
                        foundCard = true;
                }
                assigned[destCard] = true;
                newDeck[destCard] = cards[i];
            }
            newDeck.CopyTo(cards, 0);
        }
    }

    public class Player
    {
        private int cash;
        private Hand hand;
        protected Dealer dealer;
        public Player()
        {
            cash = 0;
            hand = new Hand();
            dealer = null;
        }
        public Player(int c, Dealer d)
        {
            cash = c;
            hand = new Hand();
            dealer = d;
        }
        public void Hit()
        {
            hand.AddCard(dealer.DealCard());
        }
        public int GetScore()
        {
            return hand.GetScore();
        }
        public void AddCash(int v)
        {
            cash += v;
        }
        public int GetCash()
        {
            return cash;
        }
        public string HandToString()
        {
            return hand.ToString();
        }
        public void HandEmpty()
        {
            hand.Empty();
        }
    }

    public class Hand
    {
        private Card[] cards;
        private int size;
        public Hand()
        {
            cards = new Card[12];
            size = 0;
        }
        public int GetScore()
        {
            int value = 0;
            int countAce = 0;
            for (int i = 0; i < size; i++)
            {
                if (cards[i].GetRank() <= 10 && cards[i].GetRank() >= 2)
                    value += cards[i].GetRank();
                else if (cards[i].GetRank() >= 11 && cards[i].GetRank() <= 13)
                    value += 10;
                else
                {
                    value += cards[i].GetRank();
                    countAce++;
                }
            }
            if (countAce >= 1 && value +10 <= 21)
                value += 10;
            return value;
        }
        public void AddCard(Card c)
        {
            cards[size++] = c;
        }
        public void Empty()
        {
            cards = new Card[12];
            size = 0;
        }
        public override string ToString()
        {
            string hand = "";
            for (int i = 0; i < size; i++)
                hand += cards[i].ToString() + " ";
            return hand;
        }
    }

    public class Dealer : Player
    {
        Deck deck;
        int count;
        private void Init()
        {
            deck = new Deck();
            deck.Shuffle();
            count = 0;
        }
        public Dealer() : base()
        {
            Init();
            dealer = this;
        }
        public Card DealCard()
        {
            if (count == 52)
                Init();
            Card c;
            c = deck.GetCard(count++);
            return c;
        }
    }
}

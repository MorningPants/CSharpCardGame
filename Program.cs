// See https://aka.ms/new-console-template for more information
Console.WriteLine("Rules of the Game:");
Console.WriteLine("A deck of cards is separated by suits into four decks, and the spades are discarded.");
Console.WriteLine("Both players are dealt one card from each suit face up, and hold a second card of each suit in their hand.");
Console.WriteLine("Each round, both players secretly choose a card from their hand to play.");
Console.WriteLine("Its value is added to your face up card of matching suit.");
Console.WriteLine("Face cards are all worth 10 points.");
Console.WriteLine("An extra five points are granted if your suit has an advantage over your opponent's suit.");
Console.WriteLine("Hearts have an advantage over diamonds, diamonds over clubs, and clubs over hearts.");
Console.WriteLine("The player with the higher score wins the round.");
Console.WriteLine("The used cards are then replaced with new ones of the same suit from the deck.");
Console.WriteLine("Unused cards remain and can be chosen in future rounds.");
Console.WriteLine("At the end of the game, the player who has won the most rounds wins the game.");
Console.WriteLine("The game ends when a card cannot be replaced due to an empty deck");


List<Deck> decks = new List<Deck>();
List<string> suits = new List<string>() { "Hearts", "Diamonds", "Clubs" };

void SetUpGame()
{
    foreach (string suit in suits)
    {
        Deck deck = new Deck(suit);
        for (int i = 2; i <= 10; i++)
        {
            deck.Add(new Card(suit, i, i.ToString()));
        }
        deck.Add(new Card(suit, 10, "Jack"));
        deck.Add(new Card(suit, 10, "Queen"));
        deck.Add(new Card(suit, 10, "King"));
        deck.Add(new Card(suit, 10, "Ace"));
        //shuffle the deck
        deck.Cards = deck.Cards.OrderBy(x => Guid.NewGuid()).ToList();
        decks.Add(deck);
    }
}

SetUpGame();

class Card
{
    public string Suit { get; set; }
    public int Points { get; set; }
    public string Name { get; set; }
    public Card(string suit, int points, string name)
    {
        Suit = suit;
        Points = points;
        Name = name;
    }
}

class Deck
{
    public List<Card> Cards { get; set; }
    public string Name { get; set;}
    public Deck(string name)
    {
        Name = name;
        Cards = new List<Card>();
    }
    
    public void Add(Card card)
    {
        Cards.Add(card);
    }
}

class Player
{
    public int Id { get; set; }
    public int GameScore { get; set; }
    public int RoundScore { get; set; }
    public List<Card> Hand { get; set; }
    public List<Card> Play { get; set; }
    public Player(int id)
    {
        Id = id;
        GameScore = 0;
        RoundScore = 0;
        Hand = new List<Card>();
        Play = new List<Card>();
    }
}





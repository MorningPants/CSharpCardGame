// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Rules of the Game:");
// Console.WriteLine("A deck of cards is separated by suits into four decks, and the spades are discarded.");
// Console.WriteLine("Both players are dealt one card from each suit face up, and hold a second card of each suit in their hand.");
// Console.WriteLine("Each round, both players secretly choose a card from their hand to play.");
// Console.WriteLine("Its value is added to your face up card of matching suit.");
// Console.WriteLine("Face cards are all worth 10 points.");
// Console.WriteLine("An extra five points are granted if your suit has an advantage over your opponent's suit.");
// Console.WriteLine("Hearts have an advantage over diamonds, diamonds over clubs, and clubs over hearts.");
// Console.WriteLine("The player with the higher score wins the round.");
// Console.WriteLine("The used cards are then replaced with new ones of the same suit from the deck.");
// Console.WriteLine("Unused cards remain and can be chosen in future rounds.");
// Console.WriteLine("At the end of the game, the player who has won the most rounds wins the game.");
// Console.WriteLine("The game ends when a card cannot be replaced due to an empty deck");


List<Deck> decks = new List<Deck>();
List<string> suits = new List<string>() { "Hearts", "Diamonds", "Clubs" };
Player user = new Player(1);
Player computer = new Player(2);

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
        deck.Add(new Card(suit, 1, "Ace"));
        //shuffle the deck
        deck.Cards = deck.Cards.OrderBy(x => Guid.NewGuid()).ToList();
        decks.Add(deck);
    }

    //deal the cards
    foreach (Deck deck in decks)
    {
        user.Hand.Add(deck.Cards[0]);
        user.Play.Add(deck.Cards[1]);
        computer.Hand.Add(deck.Cards[2]);
        computer.Play.Add(deck.Cards[3]);
        deck.Cards.RemoveRange(0, 4);
    }
}

void displayCards(Player player)
{
    foreach (Card card in player.Play)
    {
        Console.WriteLine(card.Name + " of " + card.Suit);
    }
}

void displayHand(Player player)
{
    foreach (Card card in player.Hand)
    {
        Console.WriteLine(card.Name + " of " + card.Suit);
    }
}

void displayBoard()
{
    Console.WriteLine("Your cards:");
    displayCards(user);
    Console.WriteLine("Your hand:");
    displayHand(user);
    Console.WriteLine("Computer's cards:");
    displayCards(computer);
    Console.WriteLine("Deck size:");
    foreach (Deck deck in decks)
    {
        Console.WriteLine(deck.Name + ": " + deck.Cards.Count);
    }
    Console.WriteLine("Score:" + user.RoundScore + " - " + computer.RoundScore);
}


void playRound(){

    displayBoard();

        
    Console.WriteLine("Choose a card to play:");
    Console.WriteLine("1: " + user.Hand[0].Name + " of " + user.Hand[0].Suit);
    Console.WriteLine("2: " + user.Hand[1].Name + " of " + user.Hand[1].Suit);
    Console.WriteLine("3: " + user.Hand[2].Name + " of " + user.Hand[2].Suit);

    int userChoice = Convert.ToInt32(Console.ReadLine()) - 1;

    Console.WriteLine("You chose " + user.Hand[userChoice].Name + " of " + user.Hand[userChoice].Suit);

    int computerChoice = new Random().Next(0, computer.Hand.Count);


    int userPoints = 0;
    int computerPoints = 0;
    Card userPlay;
    Card computerPlay;

    foreach (Card card in user.Play)
    {
        if (card.Suit == user.Hand[userChoice].Suit)
        {
            userPlay = card;
            userPoints += card.Points + user.Hand[userChoice].Points;
            Console.WriteLine("This adds to your " + card.Name + " of " + card.Suit + " for a total of " + userPoints + " points.");
        }
    }

    Console.WriteLine("Computer chose " + computer.Hand[computerChoice].Name + " of " + computer.Hand[computerChoice].Suit);

    foreach (Card card in computer.Play)
    {
        if (card.Suit == computer.Hand[computerChoice].Suit)
        {
            computerPlay = card;
            computerPoints += card.Points + computer.Hand[computerChoice].Points;
            Console.WriteLine("This adds to the computer's " + card.Name + " of " + card.Suit + " for a total of " + computerPoints + " points.");
        }
    }
    //check for advantage
    if (user.Hand[userChoice].Suit == "Hearts" && computer.Hand[computerChoice].Suit == "Diamonds" ||
    user.Hand[userChoice].Suit == "Diamonds" && computer.Hand[computerChoice].Suit == "Clubs" ||
    user.Hand[userChoice].Suit == "Clubs" && computer.Hand[computerChoice].Suit == "Hearts")
    {
        userPoints += 5;
        Console.WriteLine("Your suit advantage gives 5 points.");
    }
    else if (computer.Hand[computerChoice].Suit == "Hearts" && user.Hand[userChoice].Suit == "Diamonds" ||
    computer.Hand[computerChoice].Suit == "Diamonds" && user.Hand[userChoice].Suit == "Clubs" ||
    computer.Hand[computerChoice].Suit == "Clubs" && user.Hand[userChoice].Suit == "Hearts")
    {
        computerPoints += 5;
        Console.WriteLine("The opponent's suit advantage gives them 5 points.");
    }

    if (userPoints > computerPoints)
    {
        Console.WriteLine("You win this round " + userPoints + " to " + computerPoints + "!");
        user.RoundScore++;
    }
    else if (computerPoints > userPoints)
    {
        Console.WriteLine("You lose this round " + userPoints + " to " + computerPoints + "!");
        computer.RoundScore++;
    }
    else
    {
        Console.WriteLine("It's a tie- " + userPoints + " to " + computerPoints + "!");
    }


    //select the deck of the same suit used by the player
    Deck userDeck = decks.Find(x => x.Name == user.Hand[userChoice].Suit);
    Deck computerDeck = decks.Find(x => x.Name == computer.Hand[computerChoice].Suit);
    
    //remove cards from hand and play
    user.Hand.RemoveAt(userChoice);
    user.Play.RemoveAt(userChoice);
    computer.Hand.RemoveAt(computerChoice);
    computer.Play.RemoveAt(computerChoice);

    //if there are no more cards in the deck, end the game
    if (userDeck.Cards.Count <= 2 || computerDeck.Cards.Count <= 2)
    {
        Console.WriteLine("Game over!");
        if(userDeck.Cards.Count <= 1)
        {
            Console.WriteLine("The " + userDeck.Name + " deck is empty.");
        }
        if(computerDeck.Cards.Count <= 1)
        {
            Console.WriteLine("The " + computerDeck.Name + " deck is empty.");
        }
        if (user.RoundScore > computer.RoundScore)
        {
            Console.WriteLine("You win!");
        }
        else if (computer.RoundScore > user.RoundScore)
        {
            Console.WriteLine("You lose.");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
        Console.WriteLine("Your score: " + user.RoundScore);
        Console.WriteLine("Computer's score: " + computer.RoundScore);
        Console.ReadLine();
        Environment.Exit(0);
    }

    //add cards to play
    user.Play.Add(userDeck.Cards[0]);
    user.Hand.Add(userDeck.Cards[1]);
    userDeck.Cards.RemoveRange(0, 2);
    computer.Play.Add(computerDeck.Cards[0]);
    computer.Hand.Add(computerDeck.Cards[1]);
    computerDeck.Cards.RemoveRange(0, 2);
    
    playRound();

}


SetUpGame();


playRound();











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





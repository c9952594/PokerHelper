module PokerTypes

type Suit = 
    Heart = 1 
    | Club = 2 
    | Spade = 3 
    | Diamond = 4

type Rank = 
    Ace = 1 
    | Two = 2 
    | Three = 3 
    | Four = 4 
    | Five = 5 
    | Six = 6 
    | Seven = 7 
    | Eight = 8 
    | Nine = 9 
    | Ten = 10 
    | Jack = 11 
    | Queen = 12 
    | King = 13 
    | Ace' = 14

type Card = { 
    suit: Suit; 
    rank: Rank }

type Hand = list<Card>

type HandType = 
    StraightFlush = 9
    | FourOfAKind = 8 
    | FullHouse = 7
    | Flush = 6
    | Straight = 5
    | ThreeOfAKind = 4
    | TwoPair = 3
    | Pair = 2
    | HighCard = 1

type RankedHand = { 
    rank:HandType; 
    hand:Hand; 
    ranking:list<int> }
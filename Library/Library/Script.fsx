type Suit = 
    | Heart = 1
    | Diamond = 2
    | Spade = 3
    | Club = 4

type Rank =
    | Ace = 1
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

type Card = { rank: Rank; suit: Suit }

let deck = [
    for s in 1..4 do
        for r in 1..13 do
            yield { rank = enum<Rank>r; suit = enum<Suit>s }
]

let possibleHands = [
    for firstCard in 0..47 do
        for secondCard in (firstCard + 1)..48 do
            for thirdCard in (secondCard + 1)..49 do
                for fourthCard in (thirdCard + 1)..50 do
                    for fifthCard in (fourthCard + 1)..51 do
                        yield [deck.[firstCard]; deck.[secondCard]; deck.[thirdCard]; deck.[fourthCard]; deck.[fifthCard]]]

let isFlush (hand:list<Card>) = 
    let suit = hand.[0].suit
    hand |> List.forall (fun card -> card.suit = suit)

let rec isStraight (hand:list<Card>) =
    let sortedHand = hand |> List.sortBy (fun card -> card.rank)
    let first = List.head sortedHand
    let last = List.last sortedHand
    if (first.rank = Rank.Ace && last.rank = Rank.King)
    then
        isStraight ((List.tail sortedHand)@[{ rank = Rank.Ace'; suit = first.suit }])
    else
        ((int last.rank) - (int first.rank)) = 4

let isStraightFlush (hand:list<Card>) = 
    (isFlush hand) && (isStraight hand)

let isFourOfAKind (hand:list<Card>) = 
    hand 
    |> List.map(fun card -> card.rank) 
    |> List.countBy id 
    |> List.exists (fun (rank,count) -> count = 4)

let isFullHouse (hand:list<Card>) = 
    let groups = 
        hand 
        |> List.map(fun card -> card.rank) 
        |> List.countBy id 

    let threeOfAKind = 
        groups
        |> List.exists (fun (rank,count) -> count = 3)

    let twoOfAKind = 
        groups
        |> List.exists (fun (rank,count) -> count = 2)

    threeOfAKind && twoOfAKind

let isThreeOfAKind (hand:list<Card>) = 
    let groups = 
        hand 
        |> List.map(fun card -> card.rank) 
        |> List.countBy id 

    let threeOfAKind = 
        groups
        |> List.exists (fun (rank,count) -> count = 3)

    let twodifferent = (groups.Length = 3)
        
    threeOfAKind && twodifferent

let isTwoPair (hand:list<Card>) = 
    let groups = 
        hand 
        |> List.map(fun card -> card.rank) 
        |> List.countBy id 
        |> List.countBy (fun (rank, count) -> count = 2)
    groups.Length = 2

let isPair (hand:list<Card>) = 
    let groups = 
        hand 
        |> List.map(fun card -> card.rank) 
        |> List.countBy id 
        |> List.countBy (fun (rank, count) -> count = 2)
    groups.Length = 4

let isHighcard (hand:list<Card>) = 
    let groups = 
        hand 
        |> List.map(fun card -> card.rank) 
        |> List.countBy id 
    groups.Length = 5



isFullHouse [ 
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Spade };
    { rank = Rank.Five; suit = Suit.Heart };
    { rank = Rank.Five; suit = Suit.Club };
    { rank = Rank.Five; suit = Suit.Diamond }]


isStraightFlush [ 
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Two; suit = Suit.Diamond };
    { rank = Rank.Three; suit = Suit.Diamond };
    { rank = Rank.Four; suit = Suit.Diamond };
    { rank = Rank.Five; suit = Suit.Diamond }]

isStraightFlush [ 
    { rank = Rank.Ten; suit = Suit.Diamond };
    { rank = Rank.Jack; suit = Suit.Diamond };
    { rank = Rank.Queen; suit = Suit.Diamond };
    { rank = Rank.King; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Diamond }]

isFlush [ 
    { rank = Rank.Ten; suit = Suit.Diamond };
    { rank = Rank.Jack; suit = Suit.Diamond };
    { rank = Rank.Queen; suit = Suit.Diamond };
    { rank = Rank.King; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Diamond }]


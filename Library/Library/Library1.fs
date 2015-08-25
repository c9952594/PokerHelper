module PokerLibrary

#time
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

type HandType =
    | StraightFlush
    | FourOfAKind
    | FullHouse
    | Flush
    | Straight
    | ThreeOfAKind
    | TwoPair
    | Pair
    | HighCard

let hasStraight (hand:list<Card>) =
    match hand.Length with
    | 0 | 1 | 2 | 3 | 4 -> false
    | _ ->
        let sortedHand = hand |> List.sortBy (fun card -> card.rank)
        let firstCard = List.head sortedHand

        match firstCard.rank with
        | Rank.Ace ->
            let highAce = { rank = Rank.Ace'; suit = firstCard.suit }
            sortedHand@[highAce]
        | _ ->
            sortedHand
        |> List.map (fun card -> (int card.rank))
        |> List.pairwise
        |> List.map (fun (a,b) -> b - a)
        |> List.windowed 4
        |> List.contains [1;1;1;1]

let hasFlush (hand:list<Card>) = 
    hand
    |> List.distinctBy (fun card -> card.suit)
    |> List.length = 1

let hasStraightFlush (hand:list<Card>) = 
    (hasFlush hand) && (hasStraight hand)

let countByRank hand =
    hand 
    |> List.map(fun card -> (int card.rank)) 
    |> List.countBy id
    |> List.map (fun (_, count) -> count)

let hasFourOfAKind (hand:list<Card>) = 
    hand
    |> countByRank 
    |> List.contains 4

let hasFullHouse (hand:list<Card>) = 
    let ranked = hand |> countByRank 
    ranked
    |> List.filter (fun item -> item = 3)
    |> List.length
    |> function 
       | 0 -> false
       | 1 -> ranked |> List.contains 2
       | _ -> true

let hasThreeOfAKind (hand:list<Card>) =
    hand
    |> countByRank 
    |> List.contains 3

let hasTwoPair (hand:list<Card>) = 
    hand
    |> countByRank
    |> List.filter (fun item -> item = 2)
    |> List.length
    |> function
       | 0 | 1 -> false
       | _ -> true

let hasPair (hand:list<Card>) = 
    hand
    |> countByRank 
    |> List.contains 2

let matchHand hand = 
    match hand with
    | _ when hand |> hasStraightFlush -> HandType.StraightFlush
    | _ when hand |> hasFourOfAKind -> HandType.FourOfAKind
    | _ when hand |> hasFullHouse -> HandType.FullHouse
    | _ when hand |> hasFlush -> HandType.Flush
    | _ when hand |> hasStraight -> HandType.Straight
    | _ when hand |> hasThreeOfAKind -> HandType.ThreeOfAKind
    | _ when hand |> hasTwoPair -> HandType.TwoPair
    | _ when hand |> hasPair -> HandType.Pair
    | _ -> HandType.HighCard

//let deck = [
//    for s in 1..4 do
//        for r in 1..13 do
//            yield { rank = enum<Rank>r; suit = enum<Suit>s }
//]
//
//let possibleHands = [
//    for firstCard in 0..47 do
//        for secondCard in (firstCard + 1)..48 do
//            for thirdCard in (secondCard + 1)..49 do
//                for fourthCard in (thirdCard + 1)..50 do
//                    for fifthCard in (fourthCard + 1)..51 do
//                        let hand = [deck.[firstCard]; deck.[secondCard]; deck.[thirdCard]; deck.[fourthCard]; deck.[fifthCard]]
//                        let handType = matchHand hand
//                        yield (handType, hand)]

open NUnit.Framework
open FsUnit

let highCardExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ten; suit = Suit.Diamond };
    { rank = Rank.Eight; suit = Suit.Diamond };
    { rank = Rank.Six; suit = Suit.Diamond };
    { rank = Rank.Four; suit = Suit.Club };
]

let pairExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Heart };
    { rank = Rank.Eight; suit = Suit.Diamond };
    { rank = Rank.Six; suit = Suit.Diamond };
    { rank = Rank.Four; suit = Suit.Club };
]

let twoPairExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Spade };
    { rank = Rank.Eight; suit = Suit.Diamond };
    { rank = Rank.Eight; suit = Suit.Club };
    { rank = Rank.Four; suit = Suit.Club };
]

let threeOfAKindExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Club };
    { rank = Rank.Ace; suit = Suit.Spade };
    { rank = Rank.Eight; suit = Suit.Diamond };
    { rank = Rank.Four; suit = Suit.Club };
]

let straightLowExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Two; suit = Suit.Club };
    { rank = Rank.Three; suit = Suit.Spade };
    { rank = Rank.Four; suit = Suit.Diamond };
    { rank = Rank.Five; suit = Suit.Club };
]

let straightHighExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ten; suit = Suit.Club };
    { rank = Rank.Jack; suit = Suit.Spade };
    { rank = Rank.Queen; suit = Suit.Diamond };
    { rank = Rank.King; suit = Suit.Club };
]

let straightHighExample2 = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ten; suit = Suit.Club };
    { rank = Rank.Jack; suit = Suit.Spade };
    { rank = Rank.Jack; suit = Suit.Diamond };
    { rank = Rank.King; suit = Suit.Club };
]

let flushExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ten; suit = Suit.Diamond };
    { rank = Rank.Eight; suit = Suit.Diamond };
    { rank = Rank.Six; suit = Suit.Diamond };
    { rank = Rank.Four; suit = Suit.Diamond };
]

    
let fullHouseExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Club };
    { rank = Rank.Ace; suit = Suit.Spade };
    { rank = Rank.Eight; suit = Suit.Diamond };
    { rank = Rank.Eight; suit = Suit.Club };
]


let fourOfAKindExample = [
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Ace; suit = Suit.Club };
    { rank = Rank.Ace; suit = Suit.Spade };
    { rank = Rank.Ace; suit = Suit.Diamond };
    { rank = Rank.Eight; suit = Suit.Club };
]

let straightFlushExample = [
    { rank = Rank.Ace; suit = Suit.Club  };
    { rank = Rank.King; suit = Suit.Club };
    { rank = Rank.Queen; suit = Suit.Club  };
    { rank = Rank.Jack; suit = Suit.Club  };
    { rank = Rank.Ten; suit = Suit.Club };
]

[<TestFixture>] 
type ``matchHand test`` ()=
   [<Test>] member test.
    ``with straightFlushExample`` ()=
        matchHand straightFlushExample |> should equal HandType.StraightFlush
   [<Test>] member test.
    ``with fourOfAKindExample`` ()=
        matchHand fourOfAKindExample |> should equal HandType.FourOfAKind
   [<Test>] member test.
    ``with fullHouseExample`` ()=
        matchHand fullHouseExample |> should equal HandType.FullHouse
   [<Test>] member test.
    ``with flushExample`` ()=
        matchHand flushExample |> should equal HandType.Flush
   [<Test>] member test.
    ``with straightHighExample2`` ()=
        matchHand straightHighExample2 |> should equal HandType.Pair
   [<Test>] member test.
    ``with straightHighExample`` ()=
        matchHand straightHighExample |> should equal HandType.Straight
   [<Test>] member test.
    ``with straightLowExample`` ()=
        matchHand straightLowExample |> should equal HandType.Straight
   [<Test>] member test.
    ``with threeOfAKindExample`` ()=
        matchHand threeOfAKindExample |> should equal HandType.ThreeOfAKind
   [<Test>] member test.
    ``with twoPairExample`` ()=
        matchHand twoPairExample |> should equal HandType.TwoPair
   [<Test>] member test.
    ``with pairExample`` ()=
        matchHand pairExample |> should equal HandType.Pair
   [<Test>] member test.
    ``with highCardExample`` ()=
        matchHand highCardExample |> should equal HandType.HighCard
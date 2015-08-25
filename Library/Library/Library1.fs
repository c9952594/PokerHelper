module PokerLibrary

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
    | HighCard
    | Pair
    | TwoPair
    | ThreeOfAKind
    | Straight
    | Flush
    | FullHouse
    | FourOfAKind
    | StrightFlush

let deck = [
    for s in 1..4 do
        for r in 1..13 do
            yield { rank = enum<Rank>r; suit = enum<Suit>s }
]

//let possibleHands = [
//    for firstCard in 0..47 do
//        for secondCard in (firstCard + 1)..48 do
//            for thirdCard in (secondCard + 1)..49 do
//                for fourthCard in (thirdCard + 1)..50 do
//                    for fifthCard in (fourthCard + 1)..51 do
//                        yield [deck.[firstCard]; deck.[secondCard]; deck.[thirdCard]; deck.[fourthCard]; deck.[fifthCard]]]

    
let rec isStraight (hand:list<Card>) =
    let rec inner previousCard hand longestRun =
        match hand with
        | head::tail -> 
            if (((int previousCard.rank) + 1) = (int head.rank)) 
            then inner head tail (longestRun + 1)
            else inner head tail longestRun
        | [] -> longestRun = 5
    
    let lowSortedHand = List.sortBy (fun card -> card.rank) hand
    
    match lowSortedHand with
    | (head:Card)::tail -> 
        match head.rank with
        | Rank.Ace -> 
            let low = inner head tail 1

            let highAce = { rank = Rank.Ace'; suit = head.suit }
            let highSortedHand = tail@[highAce]
            let previous::hand = highSortedHand
            let high = inner previous hand 1
            low || high
        | _ -> 
            inner head tail 1
    | [] -> false


//
//let rec isStraight (hand:list<Card>) =
//    let sortedHand = hand |> List.sortBy (fun card -> card.rank)
//    let first = List.head sortedHand
//    let last = List.last sortedHand
//    match (first.rank, last.rank) with
//    | (Rank.Ace, Rank.King) -> 
//        let highAce = { rank = Rank.Ace'; suit = first.suit }
//        let sortedHandWithoutLowAce = List.tail sortedHand
//        let handWithHighAce = highAce::sortedHandWithoutLowAce
//        isStraight handWithHighAce
//    | _ -> 
//        (int (last.rank - first.rank)) = 4
//       


let isFlush (hand:list<Card>) = 
    hand
    |> List.distinctBy (fun card -> card.suit)
    |> List.length = 1

let isStraightFlush (hand:list<Card>) = 
    (isFlush hand) && (isStraight hand)

let countByRank hand =
    hand 
    |> List.map(fun card -> card.rank) 
    |> List.countBy id
    |> List.map (fun (_, count) -> count)
    |> List.sort 

let isFourOfAKind (hand:list<Card>) = 
    countByRank hand = [1;4]

let isFullHouse (hand:list<Card>) = 
    countByRank hand = [2;3]

let isThreeOfAKind (hand:list<Card>) =
    countByRank hand = [1;1;3]

let isTwoPair (hand:list<Card>) = 
    countByRank hand = [1;2;2]

let isPair (hand:list<Card>) = 
    countByRank hand = [1;1;1;2]

let matchHand hand = 
    match hand with
    | _ when isStraightFlush hand -> HandType.StrightFlush
    | _ when isFourOfAKind hand -> HandType.FourOfAKind
    | _ when isFullHouse hand -> HandType.FullHouse
    | _ when isFlush hand -> HandType.Flush
    | _ when isStraight hand -> HandType.Straight
    | _ when isThreeOfAKind hand -> HandType.ThreeOfAKind
    | _ when isTwoPair hand -> HandType.TwoPair
    | _ when isPair hand -> HandType.Pair
    | _ -> HandType.HighCard

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
        matchHand straightFlushExample |> should equal HandType.StrightFlush
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
module Poker

open PokerTypes


let addLowAces (cards:Cards) = 
    cards
    |> List.choose (fun card -> 
        match card.rank with
        | Rank.Ace -> Some { rank = Rank.Ace'; suit = card.suit }
        | _ -> None)
    |> List.append cards



let sortByRank (cards:Cards) = 
    cards
    |> List.sortByDescending (fun card -> card.rank)

let sortBySuitThenRank (cards:Cards) = 
    cards
    |> List.sortByDescending (fun card -> card.suit, card.rank)



let suitsAreTheSame ((first:Card), (second:Card)) =
    first.suit = second.suit

let cardsAreDescending ((first:Card), (second:Card)) = 
    ((int first.rank) - 1 = (int second.rank))

let cardsAreTheSameRank ((first:Card), (second:Card)) =
    first.rank = second.rank



let sameSuitAndDescendingRank (hand:Cards) =
    hand
    |> List.pairwise
    |> List.forall (fun cardPair -> 
        suitsAreTheSame cardPair && cardsAreDescending cardPair)

let descendingRank (hand:Cards) =
    hand
    |> List.pairwise
    |> List.forall (fun cardPair -> 
        cardsAreDescending cardPair)

let sameSuit (hand:Cards) =
    hand
    |> List.pairwise
    |> List.forall (fun cardPair -> 
        suitsAreTheSame cardPair)

let sameRank (hand:Cards) =
    hand
    |> List.pairwise
    |> List.forall (fun cardPair -> 
        cardsAreTheSameRank cardPair)



let analyseCardsByHandSize (number:int) cards = 
    cards
    |> List.windowed number

let removeCards (found:Cards) (cards:Cards) =
    found
    |> List.head
    |> (fun rankcard -> 
        cards
        |> List.filter (fun card -> card.rank <> rankcard.rank))
    
let ofAKind (cards:Cards) (number:int) = 
    cards
    |> sortByRank
    |> analyseCardsByHandSize number
    |> List.tryFind sameRank
    |> function
    | None -> None
    | Some found ->
        cards
        |> removeCards found
        |> sortByRank
        |> List.take (5 - number)
        |> (fun kicker ->
            Some (found@kicker))



let (|StraightFlush|_|) (cards:Cards) =
    cards
    |> addLowAces
    |> sortBySuitThenRank
    |> analyseCardsByHandSize 5
    |> List.tryFind sameSuitAndDescendingRank

let (|FourOfaKind|_|) (cards:Cards) = 
    ofAKind cards 4
    
let (|FullHouse|_|) (cards:Cards) = 
    cards
    |> sortByRank
    |> analyseCardsByHandSize 3
    |> List.tryFind sameRank
    |> function
    | None -> None
    | Some threeOfAKind ->
        cards
        |> removeCards threeOfAKind
        |> sortByRank
        |> analyseCardsByHandSize 2
        |> List.tryFind sameRank
        |> function
        | None -> None
        | Some pair ->
            Some (threeOfAKind@pair)

let (|Flush|_|) (hand:Cards) =
    hand
    |> sortBySuitThenRank
    |> analyseCardsByHandSize 5
    |> List.tryFind sameSuit

let (|Straight|_|) (cards:Cards) =
    cards 
    |> addLowAces
    |> sortByRank   
    |> List.groupBy (fun card -> card.rank)
    |> function
    | notEnoughCards when notEnoughCards.Length < 5 -> None
    | grouped ->
        grouped
        |> List.windowed 5
        |> List.tryFind(fun window ->
            window
            |> List.pairwise
            |> List.forall (fun ((firstRank, _), (secondRank, _)) -> 
                (int firstRank) - 1 = (int secondRank)))
        |> function
        | None -> None
        | Some found ->
            found
            |> List.map (fun (_, groupedCards) -> List.head groupedCards)
            |> Some
        
let (|ThreeOfAKind|_|) (cards:Cards) =
    ofAKind cards 3

let (|TwoPair|_|) (cards:Cards) =
    cards
    |> sortByRank
    |> analyseCardsByHandSize 2
    |> List.tryFind sameRank
    |> function
    | None -> None
    | Some highPair ->
        cards
        |> removeCards highPair
        |> sortByRank
        |> analyseCardsByHandSize 2
        |> List.tryFind sameRank
        |> function
        | None -> None
        | Some lowPair ->
            cards
            |> removeCards highPair
            |> removeCards lowPair
            |> sortByRank
            |> List.take 1
            |> function
            | kickerCard ->
                kickerCard
                |> List.append lowPair
                |> List.append highPair
                |> Some
    
let (|Pair|_|) (cards:Cards) =
    ofAKind cards 2

let (|HighCard|_|) (hand:Cards) = 
    match hand.Length with
    | 0 | 1 | 2 | 3 | 4 -> None
    | _ -> 
        hand
        |> sortByRank
        |> List.take 5
        |> Some

let parseHand (hand:Cards) =
    match hand with
    | StraightFlush found -> { rank = HandType.StraightFlush; hand = found }
    | FourOfaKind found -> { rank = HandType.FourOfAKind; hand = found }
    | FullHouse found -> { rank = HandType.FullHouse; hand = found }
    | Flush found -> { rank = HandType.Flush; hand = found }
    | Straight found -> { rank = HandType.Straight; hand = found }
    | ThreeOfAKind found -> { rank = HandType.ThreeOfAKind; hand = found }
    | TwoPair found -> { rank = HandType.TwoPair; hand = found }
    | Pair found -> { rank = HandType.Pair; hand = found }
    | HighCard found -> { rank = HandType.HighCard; hand = found }
    | _ -> { rank = HandType.Unknown; hand = hand }



let deck = [
    for s in 1..4 do
        for r in 2..14 do
            yield { suit = enum<Suit>s; rank = enum<Rank>r }
]

let generateSequences decksize handsize = 
    let rec _generate collected low high acc = 
        if (List.length collected = handsize)
        then 
            (List.sort collected)::acc
        else 
            List.fold(fun acc' index -> 
                _generate 
                    (index::collected) 
                    (index + 1) 
                    (high + 1) 
                    acc'
            ) acc [low..high]    
    _generate [] 0 (decksize - handsize) []

let sequences = 
    generateSequences (List.length deck) 5
    |> List.map (fun hand -> 
        hand 
        |> List.map(fun card -> deck.[card]))
    |> List.map (fun (hand:Cards) -> parseHand hand)



////sequences
////|> List.choose(fun ranking -> 
////    ranking.hand
////    |> List.choose (fun card -> 
////        match card with
////        | { rank = Rank.Ace; suit = Suit.Club } -> Some card
////        | None
////    )
////    |> Some)
//
//sequences 
//|> List.filter (fun ranked -> ranked.rank = HandType.FourOfAKind)
//|> List.length
//
//sequences 
//|> List.length


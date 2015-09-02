module Poker

open PokerTypes

let (|StraightFlush|_|) (hand:Hand) =
    hand 
    |> List.choose (fun elem -> 
        match elem.rank with
        | Rank.Ace -> Some({ rank = Rank.Ace'; suit = elem.suit })
        | _ -> None)
    |> List.append hand
    |> List.sortByDescending (fun card -> card.suit, card.rank)
    |> List.windowed 5
    |> List.tryFind (fun window -> 
        window
        |> List.pairwise
        |> List.forall (fun (a, b) -> 
            (a.suit = b.suit) && 
            ((int a.rank) - 1 = (int b.rank)) ))
    
let (|FourOfaKind|_|) (hand:Hand) =
    hand
    |> List.sortByDescending (fun card -> card.rank)
    |> List.windowed 4
    |> List.tryFind(fun window -> 
        window
        |> List.pairwise
        |> List.forall(fun (a, b) -> a.rank = b.rank))
    |> function
        | None -> None
        | Some found ->
            found
            |> List.map (fun card -> card.rank)
            |> List.head
            |> (fun rank ->
                hand
                |> List.filter (fun card -> card.rank <> rank)
                |> List.sortByDescending (fun (card:Card) -> card.rank)
                |> List.take 1
                |> (fun kicker -> 
                    Some (found@kicker)))
            
let (|FullHouse|_|) (hand:Hand) = 
    hand
    |> List.map (fun card -> card.rank)
    |> List.countBy id
    |> List.filter (fun (_, count) -> count >= 2)
    |> List.sortByDescending (fun (rank, count) -> count, rank)
    |> function
        | [] -> None
        | [_] -> None
        | (_, 2)::_ -> None
        | (highRank, _)::theKickerCards ->
            hand
            |> List.filter (fun card -> card.rank = highRank)
            |> List.take 3
            |> (fun highCards ->
                theKickerCards
                |> List.map (fun (rank, _) -> rank)
                |> List.sortByDescending id
                |> List.head
                |> (fun lowRank ->
                    hand
                    |> List.filter (fun card -> card.rank = lowRank)
                    |> List.take 2
                    |> (fun lowCards -> 
                        List.append highCards lowCards)
                        |> Some))

let (|Flush|_|) (hand:Hand) =
    hand
    |> List.sortByDescending (fun card -> card.suit, card.rank)
    |> List.windowed 5
    |> List.tryFind (fun window ->
        window
        |> List.pairwise
        |> List.forall (fun (a, b) -> a.suit = b.suit))

let (|Straight|_|) (hand:Hand) =
    hand 
    |> List.choose (fun elem -> 
        match elem.rank with
        | Rank.Ace -> Some({ rank = Rank.Ace'; suit = elem.suit })
        | _ -> None)
    |> List.append hand
    |> (fun handWithHighAces ->
        handWithHighAces
        |> List.map (fun card -> card.rank)
        |> List.distinct
        |> List.sortByDescending id
        |> List.windowed 5
        |> List.tryFind(fun window ->
            window
            |> List.pairwise
            |> List.forall (fun (a,b) -> (int a) - 1 = (int b)))
        |> function
            | None -> None
            | Some found ->
                found
                |> List.map (fun rank -> 
                    handWithHighAces
                    |> List.find (fun card -> card.rank = rank))
                |> Some)
        
let (|ThreeOfAKind|_|) (hand:Hand) =
    hand
    |> List.sortByDescending (fun card -> card.rank)
    |> List.windowed 3
    |> List.tryFind(fun window -> 
        window
        |> List.pairwise
        |> List.forall(fun (a, b) -> a.rank = b.rank))
    |> function
        | None -> None
        | Some found ->
            found
            |> List.map (fun card -> card.rank)
            |> List.head
            |> (fun rank ->
                hand
                |> List.filter (fun card -> card.rank <> rank)
                |> List.sortByDescending (fun (card:Card) -> card.rank)
                |> List.take 2
                |> (fun kicker -> 
                    Some (found@kicker)))

let (|TwoPair|_|) (hand:Hand) =
    hand
    |> List.map (fun card -> card.rank)
    |> List.countBy id
    |> List.filter (fun (_, count) -> count > 1)
    |> List.map (fun (rank, _) -> rank)
    |> List.sortByDescending id
    |> function
        | [] -> None
        | [_] -> None
        | highRank::otherPairs -> 
            otherPairs
            |> List.head
            |> (fun lowRank -> 
                hand
                |> List.filter (fun card -> card.rank <> highRank)
                |> List.filter (fun card -> card.rank <> lowRank)
                |> List.sortByDescending (fun card -> card.rank)
                |> List.tryHead
                |> function
                    | None -> None
                    | Some theKicker ->
                        hand
                        |> List.filter (fun card -> card.rank = highRank)
                        |> List.take 2
                        |> (fun highCards ->
                            hand
                            |> List.filter (fun card -> card.rank = lowRank)
                            |> List.take 2
                            |> (fun lowCards -> 
                                List.append lowCards [theKicker]
                                |> List.append highCards
                                |> Some)))
    
let (|Pair|_|) (hand:Hand) =
    hand
    |> List.sortByDescending (fun card -> card.rank)
    |> List.windowed 2
    |> List.tryFind(fun window -> 
        window
        |> List.pairwise
        |> List.forall(fun (a, b) -> a.rank = b.rank))
    |> function
        | None -> None
        | Some found ->
            found
            |> List.map (fun card -> card.rank)
            |> List.head
            |> (fun rank ->
                hand
                |> List.filter (fun card -> card.rank <> rank)
                |> List.sortByDescending (fun (card:Card) -> card.rank)
                |> List.take 3
                |> (fun kicker -> 
                    Some (found@kicker)))

let (|HighCard|_|) (hand:Hand) = 
    match hand.Length with
    | 0 | 1 | 2 | 3 | 4 -> None
    | _ -> 
        hand
        |> List.sortByDescending (fun card -> card.rank)
        |> List.take 5
        |> Some

let parseHand (hand:Hand) =
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
    |> List.map (fun (hand:Hand) -> parseHand hand)

//sequences
//|> List.choose(fun ranking -> 
//    ranking.hand
//    |> List.choose (fun card -> 
//        match card with
//        | { rank = Rank.Ace; suit = Suit.Club } -> Some card
//        | None
//    )
//    |> Some)

sequences 
|> List.filter (fun ranked -> ranked.rank = HandType.FourOfAKind)
|> List.length

sequences 
|> List.length


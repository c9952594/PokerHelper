module Poker

open PokerTypes

let (|StraightFlush|_|) (hand:Hand) =
    hand 
    |> List.choose (fun elem -> 
        match elem.rank with
        | Rank.Ace -> Some({ rank = Rank.Ace'; suit = elem.suit })
        | _ -> None)
    |> List.append hand
    |> List.sortByDescending (fun card -> card.suit, (int card.rank))
    |> List.windowed 5
    |> List.tryFind (fun window -> 
        window
        |> List.pairwise
        |> List.forall (fun (a,b) -> 
            (a.suit = b.suit) && 
            ((int a.rank) - 1 = (int b.rank))))
    
let (|FourOfaKind|_|) (hand:Hand) =
    hand
    |> List.sortByDescending (fun card -> (int card.rank))
    |> List.windowed 4
    |> List.tryFind(fun window -> 
        window
        |> List.pairwise
        |> List.forall(fun (a,b) -> a.rank = b.rank))
    |> function
        | None -> None
        | Some found ->
            let kicker = 
                Set.difference (Set.ofList hand) (Set.ofList found)
                |> Set.toList
                |> List.maxBy (fun (card:Card) -> card.rank)
            Some (found@[kicker])

let (|FullHouse|_|) (hand:Hand) = None

let (|Flush|_|) (hand:Hand) =
    hand
    |> List.sortByDescending (fun card -> card.suit, (int card.rank))
    |> List.windowed 5
    |> List.tryFind (fun window ->
        window
        |> List.pairwise
        |> List.forall (fun (a,b) -> a.suit = b.suit))

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
            |> List.sortByDescending (fun rank -> (int rank))
            |> List.windowed 5
            |> List.tryFind(fun window ->
                window
                |> List.pairwise
                |> List.forall (fun (a,b) -> (int a) - (int b) = 1))
            |> function
                | None -> None
                | Some found ->
                    found
                    |> List.map (fun rank -> 
                        handWithHighAces
                        |> List.find (fun card -> card.rank = rank))
                    |> Some)
        
let (|ThreeOfAKind|_|) (hand:Hand) = None

let (|TwoPair|_|) (hand:Hand) = None

let (|Pair|_|) (hand:Hand) = None

let (|HighCard|_|) (hand:Hand) = None

let parseHand (hand:list<Card>) =
    let ranks = List.map (fun (c:Card) -> (int c.rank))
    match hand with
    | StraightFlush found -> Some({ rank = HandType.StraightFlush; hand = found; ranking = (ranks found)})
    | FourOfaKind found -> Some({ rank = HandType.FourOfAKind; hand = found; ranking = (ranks found)})
    | FullHouse found -> Some({ rank = HandType.FullHouse; hand = found; ranking = (ranks found)})
    | Flush found -> Some({ rank = HandType.Flush; hand = found; ranking = (ranks found)})
    | Straight found -> Some({ rank = HandType.Straight; hand = found; ranking = (ranks found)})
    | ThreeOfAKind found -> Some({ rank = HandType.ThreeOfAKind; hand = found; ranking = (ranks found)})
    | TwoPair found -> Some({ rank = HandType.TwoPair; hand = found; ranking = (ranks found)})
    | Pair found -> Some({ rank = HandType.Pair; hand = found; ranking = (ranks found)})
    | HighCard found -> Some({ rank = HandType.HighCard; hand = found; ranking = (ranks found)})
    | _ -> None




//
//let hasStraight (hand:list<Card>) =
//    match hand.Length with
//    | 0 | 1 | 2 | 3 | 4 -> false
//    | _ ->
//        let sortedHand = hand |> List.sortBy (fun card -> card.rank)
//        let firstCard = List.head sortedHand
//
//        match firstCard.rank with
//        | Rank.Ace ->
//            let highAce = { rank = Rank.Ace'; suit = firstCard.suit }
//            sortedHand@[highAce]
//        | _ ->
//            sortedHand
//        |> List.map (fun card -> (int card.rank))
//        |> List.pairwise
//        |> List.map (fun (a,b) -> b - a)
//        |> List.windowed 4
//        |> List.contains [1;1;1;1]
//
//let hasFlush (hand:list<Card>) = 
//    hand
//    |> List.distinctBy (fun card -> card.suit)
//    |> List.length = 1
//
//let hasStraightFlush (hand:list<Card>) = 
//    (hasFlush hand) && (hasStraight hand)
//
//let countByRank hand =
//    hand 
//    |> List.map(fun card -> (int card.rank)) 
//    |> List.countBy id
//    |> List.map (fun (_, count) -> count)
//
//let hasFourOfAKind (hand:list<Card>) = 
//    hand
//    |> countByRank 
//    |> List.contains 4
//
//let hasFullHouse (hand:list<Card>) = 
//    let ranked = hand |> countByRank 
//    ranked
//    |> List.filter (fun item -> item = 3)
//    |> List.length
//    |> function 
//       | 0 -> false
//       | 1 -> ranked |> List.contains 2
//       | _ -> true
//
//let hasThreeOfAKind (hand:list<Card>) =
//    hand
//    |> countByRank 
//    |> List.contains 3
//
//let hasTwoPair (hand:list<Card>) = 
//    hand
//    |> countByRank
//    |> List.filter (fun item -> item = 2)
//    |> List.length
//    |> function
//       | 0 | 1 -> false
//       | _ -> true
//
//let hasPair (hand:list<Card>) = 
//    hand
//    |> countByRank 
//    |> List.contains 2
//
//let matchHand hand = 
//    match hand with
//    | _ when hand |> hasStraightFlush -> HandType.StraightFlush
//    | _ when hand |> hasFourOfAKind -> HandType.FourOfAKind
//    | _ when hand |> hasFullHouse -> HandType.FullHouse
//    | _ when hand |> hasFlush -> HandType.Flush
//    | _ when hand |> hasStraight -> HandType.Straight
//    | _ when hand |> hasThreeOfAKind -> HandType.ThreeOfAKind
//    | _ when hand |> hasTwoPair -> HandType.TwoPair
//    | _ when hand |> hasPair -> HandType.Pair
//    | _ -> HandType.HighCard
//
//
//let possibleHandsGenerator (deck:list<Card>) handsize = 
//    let rec _generate collected low high acc = 
//        if (List.length collected = handsize)
//        then 
//            let hand = 
//                collected
//                |> List.sort
//                |> List.map (fun card -> deck.[card])
//            hand::acc
//        else 
//            List.fold(fun acc' index -> 
//                _generate 
//                    (index::collected) 
//                    (index + 1) 
//                    (high + 1) 
//                    acc'
//            ) acc [low..high]    
//    _generate [] 0 (List.length deck - handsize) []
//    |> List.map (fun hand -> { handtype = matchHand hand; hand = hand })
//
//
//let deck = [ for s in [1..4] do for r in [1..13] do yield { suit = enum<Suit>s; rank = enum<Rank>r }]
//
//let possibleHands = possibleHandsGenerator deck 5



// Next steps
// 1. Rank hands
// 2. Identify handtype, hand and ranking in one list
// 3. Calculate 7 card hands with 2 card starter
// 4. Calculate remaining possibleHands with two cards removed
// 4. Calculate remaining possibleHands with five known cards


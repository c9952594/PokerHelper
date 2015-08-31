module Poker

open PokerTypes

let (|StraightFlush|_|) (hand:Hand) =
    let highAcesHand = 
        hand
        |> List.filter (fun card -> card.rank = Rank.Ace)
        |> List.map (fun card -> { rank = Rank.Ace'; suit = card.suit })
        |> List.append hand

    highAcesHand
    |> List.sortBy (fun card -> card.suit, -(int card.rank))
    |> List.windowed 5
    |> List.tryFind(fun window ->
        let firstCard = window.[0]
        let allSameSuit = window |> List.forall(fun card -> card.suit = firstCard.suit)
        if (allSameSuit)
        then
            window
            |> List.pairwise
            |> List.map(fun (a,b) -> (int a.rank) - (int b.rank))
            |> List.forall (fun diff -> diff = 1)
        else 
            false)

let (|FourOfaKind|_|) (hand:Hand) =
    let acesHighHand =
        hand
        |> List.map (fun card -> 
            match card.rank with
            | Rank.Ace -> { rank = Rank.Ace'; suit = card.suit }
            | _ -> card)

    let (fourOfaKind, notFourOfaKind) =
        acesHighHand
        |> List.map (fun card -> card.rank)
        |> List.countBy id
        |> List.partition(fun (rank, count) -> count = 4)
        
    match List.isEmpty fourOfaKind with
    | true -> None
    | false -> 
        let kickerRank = 
            notFourOfaKind
            |> List.sortBy (fun (rank, count) -> -(int rank))
            |> List.head
            |> fst

        let kickerCard = List.find (fun (card:Card) -> card.rank = kickerRank) acesHighHand

        let fourOfAKindRank = fourOfaKind |> List.head |> fst
        let fourOfAKindCards = 
            acesHighHand 
            |> List.filter (fun card -> card.rank = fourOfAKindRank)
                
        Some (fourOfAKindCards@[kickerCard])

//let (|Flush|_|) (hand:Hand) =
//    let suit = 
//        hand
//        |> List.map (fun card -> card.suit)
//        |> List.countBy id
//        |> List.sortByDescending (fun (suit,count) -> count)
//        |> List.head
//
//    if (snd suit >= 5)
//    then Some (fst suit)
//    else None

let (|Straight|_|) (hand:Hand) =
    let highAcesHand = 
        hand
        |> List.filter (fun card -> card.rank = Rank.Ace)
        |> List.map (fun card -> { rank = Rank.Ace'; suit = card.suit })
        |> List.append hand

    let foundHand = 
        highAcesHand
        |> List.map (fun card -> card.rank)
        |> List.distinct
        |> List.sortBy (fun rank -> -(int rank))
        |> List.windowed 5
        |> List.tryFind(fun window ->
            window
            |> List.pairwise
            |> List.forall (fun (a,b) -> (int a) - (int b) = 1))
            
    match foundHand with
    | None -> None
    | Some value -> 
        let findCardFromRank rank = List.find (fun (card:Card) -> card.rank = rank) highAcesHand
        let mapHand hand = List.map findCardFromRank hand
        Some(mapHand value)
        
let parseHand (hand:list<Card>) =
    let ranks = List.map (fun (c:Card) -> (int c.rank))
    match hand with
    | StraightFlush found -> Some({ rank = HandType.StraightFlush; hand = found; ranking = (ranks found)})
    | FourOfaKind found -> Some({ rank = HandType.FourOfAKind; hand = found; ranking = (ranks found)})
    | Straight found -> Some({ rank = HandType.Straight; hand = found; ranking = (ranks found)})
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


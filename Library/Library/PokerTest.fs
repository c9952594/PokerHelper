module PokerTest

open NUnit.Framework
open FsUnit
open PokerTypes
open Poker
open PokerData

type ``matchHand test`` ()=   
    [<Test>] member test.
     ``with straightFlushExample`` ()=
        testHands
        |> List.iter(fun hand ->
            let parsed =  parseHand hand.cards
            parsed.Value.rank |> should equal hand.handType
            parsed.Value.hand |> should equal hand.hand
            parsed.Value.ranking |> should equal hand.ranking
            )




//
//[<TestFixture>] 
//type ``matchHand test`` ()=   
//    [<Test>] member test.
//     ``with straightFlushExample`` ()=
//        straightFlushExamples |> List.iter (testMatchHand HandType.StraightFlush)
//    [<Test>] member test.
//     ``with fourOfAKindExample`` ()=
//        fourOfAKindExamples |> List.iter (testMatchHand HandType.FourOfAKind)
//    [<Test>] member test.
//     ``with fullHouseExample`` ()=
//        fullHouseExamples |> List.iter (testMatchHand HandType.FullHouse)
//    [<Test>] member test.
//     ``with flushExample`` ()=
//        flushExamples |> List.iter (testMatchHand HandType.Flush)
//    [<Test>] member test.
//     ``with straightExample`` ()=
//        straightExamples |> List.iter (testMatchHand HandType.Straight)
//    [<Test>] member test.
//     ``with threeOfAKindExample`` ()=
//        threeOfAKindExamples |> List.iter (testMatchHand HandType.ThreeOfAKind)
//    [<Test>] member test.
//     ``with twoPairExample`` ()=
//        twoPairExamples |> List.iter (testMatchHand HandType.TwoPair)
//    [<Test>] member test.
//     ``with pairExample`` ()=
//        pairExamples |> List.iter (testMatchHand HandType.Pair)
//    [<Test>] member test.
//     ``with highCardExample`` ()=
//        highCardExamples |> List.iter (testMatchHand HandType.HighCard)


//let countOfHands = (float (List.length possibleHands))
//possibleHands 
//|> List.countBy (fun hand -> hand.handtype) 
//|> List.map(fun (handtype, count) -> (handtype, 100.0 * ((float count)/countOfHands)))
//|> List.sortBy(fun (_, percentage) -> percentage)

module PokerTest

open NUnit.Framework
open FsUnit
open PokerTypes
open Poker
open PokerData
open Shouldly

type ``matchHand test`` ()=   
    [<Test>] member test.
     ``with straightFlushExample`` ()=
        testHands
        |> List.iter(fun hand ->
            let parsed =  parseHand hand.cards
            parsed.rank.ShouldBe hand.handType
            parsed.hand.ShouldBe hand.hand)
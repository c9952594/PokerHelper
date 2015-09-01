module PokerData

open PokerTypes

type TestHand = { cards: Hand; handType: HandType; hand: Hand; ranking:list<int>}

let testHands = [
    {handType = HandType.StraightFlush;
     cards = [{ rank = Rank.Jack; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Club };
              { rank = Rank.Queen; suit = Suit.Club };
              { rank = Rank.King; suit = Suit.Club };
              { rank = Rank.Ten; suit = Suit.Club }];
     hand = [{ rank = Rank.Ace; suit = Suit.Club };
             { rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Club };
             { rank = Rank.Jack; suit = Suit.Club };
             { rank = Rank.Ten; suit = Suit.Club }];
     ranking = [14;13;12;11;10]};

    {handType = HandType.StraightFlush;
     cards = [{ rank = Rank.Two; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Club };
              { rank = Rank.Five; suit = Suit.Club };
              { rank = Rank.Three; suit = Suit.Club };
              { rank = Rank.Four; suit = Suit.Club }];
     hand = [{ rank = Rank.Five; suit = Suit.Club };
             { rank = Rank.Four; suit = Suit.Club };
             { rank = Rank.Three; suit = Suit.Club };
             { rank = Rank.Two; suit = Suit.Club };
             { rank = Rank.Ace'; suit = Suit.Club }];
     ranking = [5;4;3;2;1]};

    {handType = HandType.FourOfAKind;
     cards = [{ rank = Rank.Ace; suit = Suit.Heart };
              { rank = Rank.Ace; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Spade };
              { rank = Rank.Ace; suit = Suit.Diamond };
              { rank = Rank.Eight; suit = Suit.Club }];
     hand = [{ rank = Rank.Ace; suit = Suit.Heart };
             { rank = Rank.Ace; suit = Suit.Club };
             { rank = Rank.Ace; suit = Suit.Spade };
             { rank = Rank.Ace; suit = Suit.Diamond };
             { rank = Rank.Eight; suit = Suit.Club }];
     ranking = [14;14;14;14;8]};

    {handType = HandType.FourOfAKind;
     cards = [{ rank = Rank.King; suit = Suit.Club };
               { rank = Rank.Ace; suit = Suit.Heart };
               { rank = Rank.Ace; suit = Suit.Spade };
               { rank = Rank.Jack; suit = Suit.Diamond };
               { rank = Rank.Ace; suit = Suit.Club };
               { rank = Rank.Queen; suit = Suit.Diamond };
               { rank = Rank.Ace; suit = Suit.Diamond }];
     hand = [{ rank = Rank.Ace; suit = Suit.Heart };
             { rank = Rank.Ace; suit = Suit.Spade };
             { rank = Rank.Ace; suit = Suit.Club };
             { rank = Rank.Ace; suit = Suit.Diamond };
             { rank = Rank.King; suit = Suit.Club }];
     ranking = [14;14;14;14;13]};

    {handType = HandType.Flush;
     cards = [{ rank = Rank.Four; suit = Suit.Diamond };
              { rank = Rank.Ace; suit = Suit.Diamond };
              { rank = Rank.Ten; suit = Suit.Diamond };
              { rank = Rank.Nine; suit = Suit.Club };
              { rank = Rank.Eight; suit = Suit.Diamond };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.Six; suit = Suit.Diamond }];
     hand = [{ rank = Rank.Ace; suit = Suit.Diamond };
             { rank = Rank.Queen; suit = Suit.Diamond };
             { rank = Rank.Ten; suit = Suit.Diamond };
             { rank = Rank.Eight; suit = Suit.Diamond };
             { rank = Rank.Six; suit = Suit.Diamond }];
     ranking = [14;12;10;8;6]};


    {handType = HandType.Straight;
     cards = [{ rank = Rank.Ace; suit = Suit.Diamond };
              { rank = Rank.Four; suit = Suit.Diamond };
              { rank = Rank.Two; suit = Suit.Club };
              { rank = Rank.Three; suit = Suit.Spade };
              { rank = Rank.Five; suit = Suit.Club }];
     hand = [{ rank = Rank.Five; suit = Suit.Club };
             { rank = Rank.Four; suit = Suit.Diamond };
             { rank = Rank.Three; suit = Suit.Spade };
             { rank = Rank.Two; suit = Suit.Club };
             { rank = Rank.Ace'; suit = Suit.Diamond }];
     ranking = [5;4;3;2;1]};

    {handType = HandType.Straight;
     cards = [{ rank = Rank.Ace; suit = Suit.Diamond };
              { rank = Rank.Ten; suit = Suit.Club };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.King; suit = Suit.Club };];
     hand = [{ rank = Rank.Ace; suit = Suit.Diamond };
             { rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Diamond };
             { rank = Rank.Jack; suit = Suit.Spade };
             { rank = Rank.Ten; suit = Suit.Club }];
     ranking = [14;13;12;11;10]};

    {handType = HandType.Straight;
     cards = [{ rank = Rank.Nine; suit = Suit.Diamond };
              { rank = Rank.Ten; suit = Suit.Club };
              { rank = Rank.Five; suit = Suit.Diamond };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.Ten; suit = Suit.Heart };
              { rank = Rank.King; suit = Suit.Club }];
     hand = [{ rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Diamond };
             { rank = Rank.Jack; suit = Suit.Spade };
             { rank = Rank.Ten; suit = Suit.Club };
             { rank = Rank.Nine; suit = Suit.Diamond }];
     ranking = [13;12;11;10;9]};

]




     //let straightExamples = [
//    [ { rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Two; suit = Suit.Club };
//     { rank = Rank.Three; suit = Suit.Spade };
//     { rank = Rank.Four; suit = Suit.Diamond };
//     { rank = Rank.Five; suit = Suit.Club }];
//
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ten; suit = Suit.Club };
//     { rank = Rank.Jack; suit = Suit.Spade };
//     { rank = Rank.Queen; suit = Suit.Diamond };
//     { rank = Rank.King; suit = Suit.Club }]
//]




//
//let fullHouseExamples = [
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ace; suit = Suit.Club };
//     { rank = Rank.Ace; suit = Suit.Spade };
//     { rank = Rank.Eight; suit = Suit.Diamond };
//     { rank = Rank.Eight; suit = Suit.Club }];
//
//     [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ace; suit = Suit.Club };
//     { rank = Rank.Ace; suit = Suit.Spade };
//     { rank = Rank.Eight; suit = Suit.Diamond };
//     { rank = Rank.Eight; suit = Suit.Club };
//     { rank = Rank.Eight; suit = Suit.Heart };
//     { rank = Rank.Seven; suit = Suit.Heart }]
//]
//
//let flushExamples = [
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ten; suit = Suit.Diamond };
//     { rank = Rank.Eight; suit = Suit.Diamond };
//     { rank = Rank.Six; suit = Suit.Diamond };
//     { rank = Rank.Four; suit = Suit.Diamond }]
//]

//let threeOfAKindExamples = [
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ace; suit = Suit.Spade };
//     { rank = Rank.Eight; suit = Suit.Diamond };
//     { rank = Rank.Ace; suit = Suit.Club };
//     { rank = Rank.Four; suit = Suit.Club }]
//]
//
//let twoPairExamples = [
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Eight; suit = Suit.Club };
//     { rank = Rank.Ace; suit = Suit.Spade };
//     { rank = Rank.Four; suit = Suit.Club };
//     { rank = Rank.Eight; suit = Suit.Diamond }]
//]
//
//let pairExamples = [
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ace; suit = Suit.Heart };
//     { rank = Rank.Eight; suit = Suit.Diamond };
//     { rank = Rank.Six; suit = Suit.Diamond };
//     { rank = Rank.Four; suit = Suit.Club }];
//
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ten; suit = Suit.Club };
//     { rank = Rank.Jack; suit = Suit.Spade };
//     { rank = Rank.Jack; suit = Suit.Diamond };
//     { rank = Rank.King; suit = Suit.Club }]
//]
//
//let highCardExamples = [
//    [{ rank = Rank.Ace; suit = Suit.Diamond };
//     { rank = Rank.Ten; suit = Suit.Diamond };
//     { rank = Rank.Eight; suit = Suit.Diamond };
//     { rank = Rank.Six; suit = Suit.Diamond };
//     { rank = Rank.Four; suit = Suit.Club }]
//]
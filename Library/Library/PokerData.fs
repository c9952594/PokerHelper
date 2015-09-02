module PokerData

open PokerTypes

type TestHand = { cards: Hand; handType: HandType; hand: Hand }

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
             { rank = Rank.Ten; suit = Suit.Club }]};

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
             { rank = Rank.Ace'; suit = Suit.Club }]};

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
             { rank = Rank.Eight; suit = Suit.Club }]};

    {handType = HandType.FourOfAKind;
     cards = [{ rank = Rank.King; suit = Suit.Club };
               { rank = Rank.Ace; suit = Suit.Heart };
               { rank = Rank.Ace; suit = Suit.Spade };
               { rank = Rank.Jack; suit = Suit.Diamond };
               { rank = Rank.King; suit = Suit.Diamond };
               { rank = Rank.Ace; suit = Suit.Club };
               { rank = Rank.Queen; suit = Suit.Diamond };
               { rank = Rank.Ace; suit = Suit.Diamond }];
     hand = [{ rank = Rank.Ace; suit = Suit.Heart };
             { rank = Rank.Ace; suit = Suit.Spade };
             { rank = Rank.Ace; suit = Suit.Club };
             { rank = Rank.Ace; suit = Suit.Diamond };
             { rank = Rank.King; suit = Suit.Club }]};

    {handType = HandType.FullHouse;
     cards = [{ rank = Rank.Seven; suit = Suit.Heart };
              { rank = Rank.Eight; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Spade };
              { rank = Rank.Eight; suit = Suit.Spade };
              { rank = Rank.Ace; suit = Suit.Diamond };
              { rank = Rank.Eight; suit = Suit.Heart };
              { rank = Rank.Seven; suit = Suit.Diamond };
              { rank = Rank.Seven; suit = Suit.Club }];
     hand = [{rank = Rank.Eight; suit = Suit.Club}; 
             {rank = Rank.Eight; suit = Suit.Spade};
             {rank = Rank.Eight; suit = Suit.Heart};
             {rank = Rank.Ace; suit = Suit.Spade};
             {rank = Rank.Ace; suit = Suit.Diamond}]};

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
             { rank = Rank.Six; suit = Suit.Diamond }]};

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
             { rank = Rank.Ace'; suit = Suit.Diamond }]};

    {handType = HandType.Straight;
     cards = [{ rank = Rank.Ace; suit = Suit.Diamond };
              { rank = Rank.Ten; suit = Suit.Club };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.King; suit = Suit.Club }];
     hand = [{ rank = Rank.Ace; suit = Suit.Diamond };
             { rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Diamond };
             { rank = Rank.Jack; suit = Suit.Spade };
             { rank = Rank.Ten; suit = Suit.Club }]};

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
             { rank = Rank.Nine; suit = Suit.Diamond }]};

    {handType = HandType.ThreeOfAKind;
     cards = [{ rank = Rank.Nine; suit = Suit.Diamond };
              { rank = Rank.Two; suit = Suit.Club };
              { rank = Rank.Nine; suit = Suit.Club };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.Nine; suit = Suit.Heart };
              { rank = Rank.King; suit = Suit.Club }];
     hand = [{ rank = Rank.Nine; suit = Suit.Diamond };
             { rank = Rank.Nine; suit = Suit.Club };
             { rank = Rank.Nine; suit = Suit.Heart };
             { rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Diamond }]};

    {handType = HandType.TwoPair;
     cards = [{ rank = Rank.Nine; suit = Suit.Diamond };
              { rank = Rank.Two; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Club };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.Nine; suit = Suit.Heart };
              { rank = Rank.Two; suit = Suit.Diamond }];
     hand = [{ rank = Rank.Nine; suit = Suit.Diamond };
             { rank = Rank.Nine; suit = Suit.Heart };
             { rank = Rank.Two; suit = Suit.Club };
             { rank = Rank.Two; suit = Suit.Diamond };
             { rank = Rank.Ace; suit = Suit.Club }]};

    {handType = HandType.Pair;
     cards = [{ rank = Rank.Nine; suit = Suit.Diamond };
              { rank = Rank.Two; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Club };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.Nine; suit = Suit.Heart };
              { rank = Rank.King; suit = Suit.Club }];
     hand = [{ rank = Rank.Nine; suit = Suit.Diamond };
             { rank = Rank.Nine; suit = Suit.Heart };
             { rank = Rank.Ace; suit = Suit.Club };
             { rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Diamond }]}

    {handType = HandType.HighCard;
     cards = [{ rank = Rank.Nine; suit = Suit.Diamond };
              { rank = Rank.Two; suit = Suit.Club };
              { rank = Rank.Ace; suit = Suit.Club };
              { rank = Rank.Jack; suit = Suit.Spade };
              { rank = Rank.Queen; suit = Suit.Diamond };
              { rank = Rank.Six; suit = Suit.Heart };
              { rank = Rank.King; suit = Suit.Club }];
     hand = [{ rank = Rank.Ace; suit = Suit.Club };
             { rank = Rank.King; suit = Suit.Club };
             { rank = Rank.Queen; suit = Suit.Diamond };
             { rank = Rank.Jack; suit = Suit.Spade };
             { rank = Rank.Nine; suit = Suit.Diamond }]}

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
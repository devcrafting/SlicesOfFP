module PokerHands

// NB : discriminated unions are sorted, i.e Two < Three
type Suit = Hearts | Clubs | Diamonds | Spades
type Rank = Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten | Jack | Queen | King | Ace
type Card = Rank * Suit
type Category = HighCard | OnePair | TwoPairs | ThreeOfAKind | Straight | Flush | FullHouse | FourOfAKind | StraightFlush | RoyalFlush

// Could use Enums to simplify this function, but then we should prefix with "Rank." every values
let charToRank = function
    | 'A' -> Ace
    | 'K' -> King
    | 'Q' -> Queen
    | 'J' -> Jack
    | 'T' -> Ten
    | '9' -> Nine
    | '8' -> Eight
    | '7' -> Seven
    | '6' -> Six
    | '5' -> Five
    | '4' -> Four
    | '3' -> Three
    | '2' -> Two
    | _ -> failwith "Invalid card rank"

let charToSuit = function
    | 'c' -> Clubs
    | 'h' -> Hearts
    | 'd' -> Diamonds
    | 's' -> Spades
    | _ -> failwith "Invalid card suit"

let card (str:string) : Card = (charToRank str.[0], charToSuit str.[1])

let cards (hand:string) = hand.Split(' ') |> Seq.map card |> Seq.toList

let rank : Card -> Rank = fst

let ranks = List.map rank >> List.sortDescending 

let groups = ranks >> List.groupBy id >> List.map snd >> List.sortByDescending List.length

let category = function

    | [[_;_;_;_];_] -> FourOfAKind
    | [[_;_;_];[_;_]] -> FullHouse
    | [[_;_;_];_;_] -> ThreeOfAKind
    | [[_;_];[_;_];_] -> TwoPairs
    | [[_;_];_;_;_] -> OnePair
    | _ -> HighCard

let straight = function
    | (HighCard, [Ace;Five;_;_;_]) -> (Straight, [Five; Four; Three; Two; Ace])
    | (HighCard, [Ace;_;_;_;Ten] as r)
    | (HighCard, [King;_;_;_;Nine] as r) 
    | (HighCard, [Queen;_;_;_;Eight] as r) 
    | (HighCard, [Jack;_;_;_;Seven] as r) 
    | (HighCard, [Ten;_;_;_;Six] as r) 
    | (HighCard, [Nine;_;_;_;Five] as r) 
    | (HighCard, [Eight;_;_;_;Four] as r) 
    | (HighCard, [Seven;_;_;_;Three] as r) 
    | (HighCard, [Six;_;_;_;Two] as r) -> (Straight, snd r)
    | r -> r

let ranking cards =
    let groups = groups cards
    straight (category groups, List.concat groups)
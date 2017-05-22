module PokerHands

// NB : discriminated unions are sorted, i.e Two < Three
type Suit = Hearts | Clubs | Diamonds | Spades
type Rank = Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten | Jack | Queen | King | Ace

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

let card (str:string) = (charToRank str.[0], charToSuit str.[1])

let cards (hand:string) = hand.Split(' ') |> Seq.map card |> Seq.toList

let rank : Rank * Suit -> Rank = fst

let ranks = List.map rank >> List.sortDescending 
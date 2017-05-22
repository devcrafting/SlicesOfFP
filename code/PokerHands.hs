module PokerHands
where
import Data.Char
import Data.List
import Data.Ord

data Suit = Hearts | Clubs | Diamonds | Spades
    deriving (Eq, Show)

data Rank = Two | Three | Four | Five | Six | Seven | Eight 
          | Nine | Ten | Jack | Queen | King | Ace
    deriving (Eq, Ord, Enum, Show)

type Card = (Rank, Suit)

data Category = HighCard | OnePair | TwoPairs | ThreeOfAKind | Straight | Flush | FullHouse | FourOfAKind | StraightFlush | RoyalFlush deriving (Eq,Ord,Show)

ranking cs = flush (isFlush cs) (promote (cat,rs)) where
    cat = category gs 
    rs = concat gs 
    gs = groups cs

promote (HighCard, [Ace, Five, _,_,_]) = (Straight, [Five, Four, Three, Two, Ace])
promote (HighCard, rs) | isStraight rs = (Straight, rs)
promote x = x

flush True (HighCard, rs) = (Flush, rs)
flush True (Straight, [Ace,_,_,_,_]) = (RoyalFlush, [Ace, King, Queen, Jack, Ten])
flush True (Straight, rs) = (StraightFlush, rs)
flush False x = x

isStraight [a,_,_,_,b] = fromEnum a == fromEnum b + 4

rank :: Card -> Rank
suit :: Card -> Suit
card :: String -> Card

rank = fst
suit = snd

card [r,s] = (charToRank r, charToSuit s)

groups = sortBy (flip (comparing length)) . group . ranks

category [[_, _, _, _], _] = FourOfAKind
category [[_, _, _], [_, _]] = FullHouse
category [[_, _, _], _, _] = ThreeOfAKind
category [[_, _], [_, _], _] = TwoPairs
category [[_, _], _, _, _] = OnePair
category _ = HighCard
 
charToRank :: Char -> Rank
charToRank 'A' = Ace
charToRank 'K' = King
charToRank 'Q' = Queen
charToRank 'J' = Jack
charToRank 'T' = Ten
charToRank c   = toEnum (digitToInt c - 2)

charToSuit :: Char -> Suit
charToSuit 's' = Spades
charToSuit 'd' = Diamonds
charToSuit 'c' = Clubs
charToSuit 'h' = Hearts

cards :: String -> [Card]
cards = map card . words

ranks :: [Card] -> [Rank]
ranks = sortBy (flip compare) . map rank

isFlush :: [Card] -> Bool
isFlush = (== 1) . length . group . map suit 

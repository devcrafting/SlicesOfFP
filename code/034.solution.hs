data Suit = Hearts | Clubs | Diamonds | Spades
    deriving (Eq, Show)

data Rank = Two | Three | Four | Five | Six | Seven | Eight 
          | Nine | Ten | Jack | Queen | King | Ace
    deriving (Eq, Ord, Enum, Show)

type Card = (Rank, Suit)
type Hand = [Card]

rank :: Card -> Rank
rank = fst

suit :: Card -> Suit
suit = snd


card :: String -> Card
card [r,s] = (charToRank r, charToSuit s)

charToRank :: Char -> Rank
charToRank 'A' = toEnum 12
charToRank 'K' = toEnum 11
charToRank 'Q' = toEnum 10
charToRank 'J' = toEnum 9
charToRank 'T' = toEnum 8
charToRank c   = toEnum $ (Data.Char.digitToInt c) - 2

charToSuit :: Char -> Suit
charToSuit 'h' = Hearts
charToSuit 'c' = Clubs
charToSuit 'd' = Diamonds
charToSuit 's' = Spades

cards :: String -> Hand
cards = (map card) . words


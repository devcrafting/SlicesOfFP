-- 034.hs
import Test.Hspec
import PokerHands

main = hspec $ 
    describe "cards" $ 
        it "should collect cards from a string" $
            cards "8d Ah Qc"  `shouldBe`
                 [(Eight,Diamonds),(Ace,Hearts),(Queen,Clubs)]

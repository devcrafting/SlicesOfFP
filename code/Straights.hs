import Test.Hspec
import PokerHands

main = hspec $
    describe "ranking" $
        it "should correctly order a list by ranking" $ do
            let s = ["7s 5c 4d 3d 2c" ,"As Kc Qd Jd 9c"
                    ,"2h 2d 5c 4c 3c" ,"Ah Ad Kc Qc Jc"
                    ,"2c 2s 3s 3c 4h" ,"Ac As Ks Kc Jh"
                    ,"2h 2d 2c 4c 3c" ,"Ah Ad Ac Qc Jc"
                    ,"5h 4s 3d 2c Ah" ,"Ah Ks Qd Jc Th"
                    ,"2h 2d 2c 3h 3c" ,"Ah Ad Ac Kh Kc"
                    ,"2c 2s 2h 2d 3c" ,"Ac As Ah Ad Jc"]
                isOrdered :: Ord(a) => [a] -> Bool
                isOrdered [_] = True
                isOrdered (x:y:xs) = x < y && isOrdered (y:xs) 
                r = map (ranking.cards) s
            isOrdered r `shouldBe` True
        
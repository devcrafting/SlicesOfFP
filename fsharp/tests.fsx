#r "../packages/xunit.abstractions/lib/net35/xunit.abstractions.dll"
#r "../packages/xunit.assert/lib/dotnet/xunit.assert.dll"
#r "../packages/xunit.extensibility.core/lib/dotnet/xunit.core.dll"
#r "../packages/xunit.extensibility.execution/lib/net45/xunit.execution.desktop.dll"
#r "../packages/FsUnit.xUnit/lib/net45/FsUnit.Xunit.dll"
#r "../packages/Unquote/lib/net45/Unquote.dll"
#load "PokerHands.fs"

open FsUnit.Xunit
open Xunit
open Swensen.Unquote

open PokerHands

let rec isOrdered = function
    | [_] -> true
    | x::y::tail -> x < y && isOrdered (y::tail) 

type ``PokerHands`` () =
    [<Fact>]
    member x.``can only contains valid cards`` () =
        test <@ cards "Ac Kh Qd Js Tc" = [(Ace, Clubs); (King, Hearts); (Queen, Diamonds); (Jack, Spades); (Ten, Clubs)]@>

    [<Fact>]
    member x.``can rank a hand`` () =
        test <@ ranks (cards "8d Ah Qc") = [Ace; Queen; Eight] @>

    [<Fact>]
    member x.``group cards by rank`` () =
        test <@ groups (cards "8d Ah Qc 8h 8s") = [[Eight;Eight;Eight];[Ace];[Queen]] @>

        test <@ groups (cards "8d Ah Qc 8h As") = [[Ace;Ace];[Eight;Eight];[Queen]] @>

    [<Fact>]
    member x.``categorize simple hands`` () =
        let hands = ["4s 5d Kc Tc 3d"
                     ;"4s Kd Kc Tc 3d"
                     ;"4s Kd Kc Tc Td"
                     ;"Ts Kd Kc Kc 8d"
                     ;"Ts Kd Kc Tc Td"
                     ;"Ts Kd Kc Kc Kd"]
        test <@ List.map (cards >> groups >> category) hands =  
                    [HighCard; OnePair; TwoPairs
                    ;ThreeOfAKind; FullHouse; FourOfAKind] @>

    [<Fact>]
    member x.``categorize hands including straight`` () =
        let s = ["7s 5c 4d 3d 2c" ;"As Kc Qd Jd 9c"
                    ;"2h 2d 5c 4c 3c" ;"Ah Ad Kc Qc Jc"
                    ;"2c 2s 3s 3c 4h" ;"Ac As Ks Kc Jh"
                    ;"2h 2d 2c 4c 3c" ;"Ah Ad Ac Qc Jc"
                    ;"5h 4s 3d 2c Ah" ;"Ah Ks Qd Jc Th"
                    ;"2h 2d 2c 3h 3c" ;"Ah Ad Ac Kh Kc"
                    ;"2c 2s 2h 2d 3c" ;"Ac As Ah Ad Jc"]
        test <@ List.map (cards >> ranking) s |> isOrdered @>

    [<Fact>]
    member x.``categorize hands including flushes`` () =
        let s = ["7s 5c 4d 3d 2c" ;"As Kc Qd Jd 9c"
                    ;"2h 2d 5c 4c 3c" ;"Ah Ad Kc Qc Jc"
                    ;"2c 2s 3s 3c 4h" ;"Ac As Ks Kc Jh"
                    ;"2h 2d 2c 4c 3c" ;"Ah Ad Ac Qc Jc"
                    ;"5h 4s 3d 2c Ah" ;"Ah Ks Qd Jc Th"
                    ;"7c 5c 4c 3c 2c" ;"Ac Kc Qc Jc 9c"
                    ;"2h 2d 2c 3h 3c" ;"Ah Ad Ac Kh Kc"
                    ;"2c 2s 2h 2d 3c" ;"Ac As Ah Ad Jc"
                    ;"5c 4c 3c 2c Ac" ;"Ah Kh Qh Jh Th"]
        test <@ List.map (cards >> ranking) s |> isOrdered @>

``PokerHands``().``can only contains valid cards``()
``PokerHands``().``can rank a hand``()
``PokerHands``().``group cards by rank``()
``PokerHands``().``categorize simple hands``()
``PokerHands``().``categorize hands including straight``()
``PokerHands``().``categorize hands including flushes``()

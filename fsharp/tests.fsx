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

type ``PokerHands`` () =
    [<Fact>]
    member x.``can only contains valid cards`` () =
        test <@ cards "Ac Kh Qd Js Tc" = [(Ace, Clubs); (King, Hearts); (Queen, Diamonds); (Jack, Spades); (Ten, Clubs)]@>

    [<Fact>]
    member x.``can rank a hand`` () =
        test <@ ranks (cards "8d Ah Qc") = [Ace; Queen; Eight] @>

``PokerHands``().``can only contains valid cards``()
``PokerHands``().``can rank a hand``()

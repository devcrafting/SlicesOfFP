# SlicesOfFP
Slices of Functional Programming

Learning FP in Haskell with the Poker Hands kata.

## NCrafts experience : learning Haskell

I ran this kata at [NCrafts 2017](http://www.ncrafts.io) with [Emilien Pecoul](http://twitter.com/ouarzy). We did not finish it at the conference, but I finished it on my own.

## A try with F#

I was curious of what would be the differences between F# and Haskell. Overall, it is very similar.
So the differences I spotted (if I am not wrong, because I still have lots of things to learn in F#):

- In F#, you can define Enum, giving explicitly a value to each case using a discriminated union-like type
    - Drawback: you need to prefix each value with the enum (type), i.e `Rank.Ace` instead of just `Ace`, not very beautiful, and moreover it leaks a bit 
    - Advantage: you can do the same as in Haskell (toEnum/fromEnum), simplifying `straight` and `charToRank` functions
- In F#, composition is done with `>>` or `<<` operators, where as Haskell proposes `.` as a mimic of `f ∘ g`, so the `>>` operator is in reverse order of `.`
- Pattern matching applied to function arguments is using the `function` keyword in F#, whereas in Haskell you directly put your patterns in function signature, repeating the function name (disturbing for lots of people at first)   
- F# does have the equivalent (as far as I know) of the `where` syntax to declare details of a function, but it is not really important in this case 
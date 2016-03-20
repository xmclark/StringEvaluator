# String Parser Solution

## Discussion
I use a couple different algorithms to implement a mathematical expression parser.
The implementation is fundamentally _extensible_ because I use interface injection
(or dependency injection). I create an interface called IAlgorithm which has a 
virtual method `Execute()`. The `Evaluator` class gets a couple of these interfaces
for cleaning, parsing, and evaluating a string expression. Since those interfaces
are implemented separately from the main class, they are loosely coupled with the
`Evaluator`.

## My Algorithm

# String Parser Solution

## Discussion
I use a couple different algorithms to implement a mathematical expression parser.
The implementation is fundamentally _extensible_ because I use interface injection
(or dependency injection). I create an interface called IAlgorithm which has a 
virtual method `Execute()`. The `Evaluator` class gets a couple of these interfaces
for cleaning, parsing, and evaluating a string expression. Since those interfaces
are implemented separately from the main class, they are loosely coupled with the
`Evaluator`. An example implementation is:
```
// parser implemenation
Parser : IAlgorithm {...}

// validator implementation
Validator : IAlgorithm {...}

// solver implementation
Solver : IAlgoritm {...}

var parser = new Parser();
var validator = new Validator();
var solver = new Solver();

var evaluator = new Evaluator(validator, parser, solver);
```

## My Algorithm
I use an alternative algorithm of my own design (but I take no credit because somebody
else probably made it already) to implement the parser. Instead of a shunting-yard
algorithm, I implement an algorithm using a linked list and a special pointer called
HEAD. Since `Evaluator` is extensible, I can easily implement a ShuntingYard class
another day! This algorithm creates a linked list and assigns a pointer to node in
linked list. The pointer holds the precedence state information. The tokens are 
iterated over in an IList and appended or inserted into the result linked list.

1. Assign the HEAD pointer to null
2. Initialize LinkedList
3. While there are tokens:
  Get token
  IF token is Left Scoping token a.k.a. left parenthesis:
    Elevate local precedence state variable
  ELSE IF token is Right Scoping token a.k.a. right parenthesis:
    Lower local precedence state variable
    Set the HEAD to null
  ELSE IF token is Value token:
    IF HEAD is null:
      Add token to end of Linked List
      Set HEAD to point at token
    ELSE:
      Add token BEFORE HEAD
  ELSE IF token is Operator token:
    IF HEAD is null:
      Add token to end of Linked List
      Set HEAD to point at token
    IF HEAD is Value token:
      Add token BEFORE the HEAD
      Set HEAT to point at token
    ELSE IF HEAD is Operator token:
      IF Current Token Precedence is greater than HEAD token Precedence:
        Add token BEFORE the HEAD
      ELSE:
        Add token AFTER the HEAD
      Set HEAD to point at token 	
	


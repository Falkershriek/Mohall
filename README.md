# Mohall

Mohall an application simulating the Monty Hall problem. Some changes have been made to simplify the original problem scenario without losing its quintescence. A brief summary of the problem as presented in this application can be found below. You can read about the Monty Hall problem in detail on [Wikipedia](https://en.wikipedia.org/wiki/Monty_Hall_problem).

## The Mohall problem (simplified Monty Hall problem)

You are presented with three doors. Behind one of the doors is a reward. There's nothing behind the other two doors.

1. First, you are asked to select one of the three doors.
2. Once you've made up your mind, one of the other two doors is opened - it's always an empty door.
3. Then, you are asked whether you'd like to change your choice, i.e. to swap the door you picked for the other door that's still closed.
4. After you choose to either sawp to keep your original choice, the remaining two doors are opened and you learn whether you correctly chose the door with the reward or an empty door.

The question is: does changing your choice in step 3. give you an advantage?

<details> 
  <summary>Answer</summary>
	While it might seem like it shouldn't matter whether you swap the doors at the end or not, swapping once one of the empty doors is opened does give you an advantage.
	
	To explain it: In the beginning, you have a 1:3 chance of picking right and a 2:3 chance of picking wrong. If you pick an empty door, which happens 2/3 of the time, there remains only one empty door that can possibly be opened in step 2., in which case the remaining closed door will be the reward door. If you then swap your choice, you choose the reward door. Thus, to summarize, if you *always* swap, you turn your 2:3 chance to lose into a 2:3 chance to win because whenever you choose the wrong door initially (which happens 2:3 of the time), the only door you can swap to is the correct door.
</details>

## Differences between Mohall and the original Monty Hall problem

Here are the key differences between the Mohall's problem and the original Monty Hall problem.
1. In the original, the problem is presented as a part of a game show lead by a show host.
2. In the game show, behind one of the doors is a car, behind each of the other two doors is a goat.
3. The original problem does not make it explicit that the show host will always open a door with a goat, but it's generally assumed that they will. since otherwise they would ruin the show by revealing the reward in step 2., meaning that the show contestant would have no reason to swap doors in step 3.
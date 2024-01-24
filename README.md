# GamedevAssignment1

**Gameplay:** [Youtube](https://youtu.be/zdX3eWxdtuk?si=A3GWWJAwf83TIdVx)
(I made few changes after publishing this video such as adding invisible walls, but the gameplay remians the same)

 
Posts of classmates I commented on
1. Nevin Garduno
2. Ahmed Ruyyashi


**Project idea**: I used the given bb8 models and the code to make this game, which reflects on a childhood memory/passtime for me. I tried to implement a basic shooter game between me and my brother, who is another bb8 here. We try to shoot balls at each other and we get the scores accordingly. Another concept which I implemented is of a parent coming in when we are playing. Here, a countdown starts and I have to hit all the target pegs before the clock reaches zero so as to avoid getting caught ny my parent, which is my Dad here in this game. Some points I would like to highlight:

1. Added textures to floor and boundries. Added a skybox from Unity Asset store.
2. Bullets in this game have no gravity, for better play style. However, their gravity is turned on once they collide so that we can see the bounce effect clearly.
3. I added a physics material to get some extra bounce as well.
4. If we get hit by a ball, we might start rotating due to physics of the impact. Press key 'Q' to stop any movement of the player.
5. I made a father figure in the Unity itself. And I added a simple animation, which makes him raise above ground and rotate to see the players.
6. I have used a bunch of coroutines for the timers and other stuff in the code.
7. I changed the bb8 player controller character so as to look around with mouse, instead of rotating with Q and E only.
8. There are invisible walls behind the players which stops the balls within bounds. however the balls are destroyed after 5 seconds each.
9. Light effect here is the red siren light which switches on when the timer goes on and vice versa.

**How to play:**
Download the files in the [Builds](Builds) folder and run the .exe file in your system

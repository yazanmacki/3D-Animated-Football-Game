# 3D-Animated-Football-Game

This was the first project I worked on in Unity. It is a football game involving turns taken
between player and goalkeeper. It involved 3D character models that are animated to shoot/save
the ball. After working on this project for a while a decided to abandon it and make a new
and simplified version, in which no 3D animations were involved. This allowed me to focus on 
the thing I enjoyed more, the game mechanics, rather than putting all my efforts into the 
animations.

Scripts:

MoveGoalkeeper.cs: Goalkeeper controller. If CPU turn, uses ball trajectory to know where ball will go, saves ball based on difficulty level
If player turn, jumps to position chosen by player. Intitiates all goalkeeper's diving/saving/moving animations.

MovePlayer.cs: Ball shooter controller. If CPU turn, kicks ball in random direction based on difficulty level. If player turn, kicks ball on a
trajectory based on arrow direction and power level. Initiates all the player's running/kicking animations.

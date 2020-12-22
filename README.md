![BouncingBall Logo](https://media.discordapp.net/attachments/335464035921428480/790986552372101140/BouncingBallBannerTransparent.png)
----

It's just a ball that bounces. The simulation is very basic, and has no drag because that'd be super complicated. Still, it looks nice, and with a small enough BasicGraphic it runs pretty smoothly.

The ball bounces around the console window, and automatically adjusts if the console is resized. The ball isn't affected by the force if it is forced up or left. We'd probably need to simulate mass to calculate the acceleration but eh woops.

![BouncingBall In Action](https://cdn.discordapp.com/attachments/335464035921428480/790987631080112168/unknown.png)

There's a few parameters available when running BouncingBall.exe from the console. All of them must be in the following format `/PARAMETERNAME=VALUE`

|Parameter Name|Description|Acceptable values|Default|
|-|-|-|-|
|GRAPHIC|Path to a file holding a BASICGRAPHIC|File ending in .DF or .HC|SquareBall, A 6x3 graphic that is the logo|
|X|Starting X Position for the ball|A double value from 0 to the Console Width|0|
|Y|Starting Y Position for the ball|A double value from 0 to the Console Height|0|
|XSpeed|Starting XSpeed for the ball|Any Double Value|3|
|YSpeed|Starting YSpeed for the ball. <br><br> **NOTE:** POSITIVE IS DOWN|Any Double Value|0|
|Speed|Starting Speed Vector for the ball|A Speed vector formatted as `SPEED:ANGLE (In Degrees)`|3:0|
|Bounce|Alias for Bounciness|Any double value <br><br>(but it should be between 0 and 1)|0.9|
|Bounciness|Percentage of speed that will be kept after a bounce.|Any Double Value <br><br>(But it should be between 0 and 1)|0.9|
|Grav|Alias for Gravity|Any double value <br><br>(But it should be smol)|0.5|
|Gravity|Acceleration per frame|Any double value <br><br>(But it should be smol)|0.5|
|FPS|*attempted* Frames Per Second. May be less if the BasicGraphic is too big.|An integer value from 1 to 100. <br><br>Don't make it too large or you won't be able to see the ball.|30|
|ShowInfo|Show the ball info or not|A boolean|True|
|StopOnZero|Stops the simulation at speed 0,0 or not|A boolean|False|

Once the simulation is in motion, you can press some keys to modify parameters:

|Key|Description|
|-|-|
|<kbd>↑</kbd>|Kicks the ball up (Subtracts 5 from YSpeed)|
|<kbd>↓</kbd>|Kicks the ball down (Adds 5 to YSpeed)|
|<kbd>←</kbd>|Kicks the ball left (Subtracts 5 from XSpeed)|
|<kbd>→</kbd>|Kicks the ball right (Adds 5 to Xspeed)|
|<kbd>+</kbd>|Increases gravity by .1|
|<kbd>-</kbd>|Decreases gravity by .1|
|<kbd>.</kbd>|Increases bounciness by .1 up to 1|
|<kbd>,</kbd>|Decreases bounciness by .1 down to 0|
|<kbd>Space</kbd>|Stops the ball|
|<kbd>Esc</kbd>|Closes the simulation|
|<kbd>I</kbd>|Toggle ShowInfo|

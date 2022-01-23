# Line-Rider
A 2D Line Rider Game made with Unity

Remember the Line Rider of old from flash gaming days? Well, this is him now, feel old yet? <br/>
I've just made a replica of what the game used to be, and adjusted its physics up to my taste. In this game, player basically draws a line and the character slides on top of them. Each drawn line gives the character its direction and speed according to its angle. 

![LineRiderSS1](https://user-images.githubusercontent.com/44427408/150669104-6886a2f6-b4c9-453d-bf43-5ff401fbe803.jpg)

So, what is this game? How to play it, what is the purpose even, you may ask. In a nutshell, There is an amazingly drawn character with his/her(?) ice sled, and that thing literally slides on the lines that the player draws. The position, rotation, angles and such do affect the cool riding dude's speed and such. Draw a horribly misarable line, and your character may just hit his/her head into the line and suffer the consequences. <br/>
Anyway, Let's move to the technical details now! <br/>

# Technical Aspects

In this game, the game managing part is controlled by 2 managers, Input Manager and Line Manager. <br/>
+ Input Manager, simply uses Unity's new input system and manages all the player inputs, either to control the game itself or the character. Since it's using the new input system, everything is event based. Line Manager and Player script are listening the events that they subscribed, and as soon as an input is given, they do receive their end of deal and do the action. <br/>

+ Line Manager, simply responsible for starting to drawing a line, ending it as well as erasing a line. It basically creates an object with line renderer and edge collider whenever the player tries to draw a line with mouse actions. 

Additional scripts are Player (where the player actions happen), CameraFollow (where the camera follows the character with a smoothdamp), Pan (where the player can slightly move the screen with mouse on edges), Zoom (where zooming with mouse scroll is handled) and finally a static Class Utils where raycasting into the scene to pick up objects (lines) are handled.

![LineRiderSS3](https://user-images.githubusercontent.com/44427408/150669472-11eee208-277e-4fc6-8e7b-225256ec68d2.jpg)

Well, that's about it! Thanks for reading I guess.

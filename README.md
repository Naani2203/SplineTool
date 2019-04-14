SplineTool

This tool was created to give platforms and camera so that the level designer of the team could use it to create movement path of objects quickly.
A smooth spline movement is achieved by using Catmull-Rom Spline.


To create path using the tool:
1. Select the GameObject "Tool" from the inspector
2. Click on "Spawn Path Object on Mouse Click" and set it to true.
3. Once "Spawn Path Object on Mouse Click" is true, ALT + left-click in the scene to create a new Path Object
4. You can Spawn a Mover object which moves along the path using the button "Spawn Mover".

You can make any GameObject a "Mover" by adding "SplineMover" component to it.
There are two movement modes:
1. Linear - The object moves along the path and the movement is not smooth at edges.
2. Catmull -The object uses Catmull-Rom Spline to smooth out the movement along the path.

These movement modes can be changed on "Mover" object.

# A tale of 3D shapes and rounding issues

DISCLAIMER: This is not meant to be offensive to any person or group, and should be read as a (somewhat) educational and (barely) entertaining write-up of my experience.

This is simply a little challenge I set myself to see if I am clever enough to put the math together in order to render a set of points in 3D space from scratch.
The short answer is, sort of.
The long answer is more of an epic tale of integers, doubles, trigonometry, aswastikas, and a key piece of math that unlocked the entire puzzle.

## Chapter 1 - Creating our little world

To start this project off, I decided to use a simple forms application as this gives me an easy way to render pixels without too much of a hassle.
The first thing I did was to create a list that would represent my pixels in 3D space.
Since the screen pixels are essentially finite whole numbers, I made the (very poor) decision to represent my data as a list of integers:

```csharp
private List<(int, int, int)> coordinates;
```

Next, I added a simple button that would add some points to the coordinates list, in the shape of a small cube:

```csharp
private void btnBox_Click(object sender, EventArgs e)
{
    coordinates = new List<(int, int, int)>();

    coordinates.Add((5, 5, 5));
    coordinates.Add((5, -5, 5));
    coordinates.Add((5, 5, -5));
    coordinates.Add((5, -5, -5));
    coordinates.Add((-5, 5, 5));
    coordinates.Add((-5, -5, 5));
    coordinates.Add((-5, 5, -5));
    coordinates.Add((-5, -5, -5));
}
```

The next thing on the agenda would be to see if I can get these points rendered on the screen.
This would happen in a simple method called `RenderCoordinates()`, that consists mostly of code copied verbatim from [this](https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-draw-a-filled-rectangle-on-a-windows-form) site.

Rendering these points would be easy, since the first step would be to use simple isometric shapes, meaning that there would be no “perspective”, I could simply ignore the Z coordinate of each point and only render the X and Y coordinates!
These coordinated are adjusted such that the (0,0) coordinate is in the middle of the screen instead of the top left corner, and since the coordinates are already int values, I did not even have to round them to the nearest integer in order to render them!

```csharp
private void RenderCoordinates()
{
    var brushSize = 10;

    SolidBrush myBrush = new SolidBrush(Color.Red);
    Graphics formGraphics;
    formGraphics = this.CreateGraphics();

    var centerX = this.Width / 2;
    var centerY = this.Height / 2;

    coordinates.ForEach(c =>
    {
        var (x, y, z) = c;
        formGraphics.FillRectangle(myBrush, new Rectangle(x + centerX, y + centerY, brushSize, brushSize));
    });

    myBrush.Dispose();
    formGraphics.Dispose();
}
```

I tested it out, and a tiny little red square consisting of 4 points appears in the middle of the screen!
So far everything is going great! With a start like this, what could possibly go wrong?

## Chapter 2 - Getting into the good stuff

I will first start with a simple challenge; rotating the cube in the Z-axis. This means that all we are expecting to see would be 4 points rotating around the centre of the screen.
So I add a button that should rotate the point by 10degrees every time it is clicked.
Turns out, the math to rotate an arbitrary point around another point by some random angle is not as straight forward as I thought.
The answer ended up requiring some clever matrix multiplications that I was at this point too lazy to try and understand and all the solutions that I have tried ended up in the points flying all over the place!
I ended up copying a solution from [this](https://stackoverflow.com/questions/22491178/how-to-rotate-a-point-around-another-point) Stack Overflow post.
Because every point will always be rotated around the centre-point of the screen I could simply ignore all the translation stuff and just plug my values into the formula given on that thread.
I was left with the following function:

```csharp
private void btnRight_Click(object sender, EventArgs e)
{
    coordinates = coordinates.Select(c =>
    {
        var (x, y, z) = c;

        var angle = ConvertToRadians(10);

        var newX = (x) * Math.Cos(angle) - (y) * Math.Sin(angle);
        var newY = (x) * Math.Sin(angle) + (y) * Math.Cos(angle);
        var newZ = z;

        return (newX, newY, newZ);
    }).ToList();

    RenderCoordinates();
}

```

Could it really be this simple? 

## Chapter 3 - Why integer was a horrible, horrible choice!

Let us pause quickly to catch up on the story so far.

At this point in our story we have plugged in this small piece of math, and we are about to run it for the first time.
But before we do that, I would like to point out two things that explains what is about to happen next.
The first is that I chose Integers instead of doubles to represent my coordinates.
The second is that I do not clear the screen before I render a new set of points.

Back to the story.

I run the code, and for the first couple of rotations everything looks good.

The next couple of clicks I realize that the lines created by the dots seem to keep straight. At this point I am starting to think that it is probably a rounding issue, and hypothesize that the points will probably collapse into the centre.

I click a bunch more times, and... oh... my... 
What have I done!?

The lines made a perfect little red swastika in the middle of my screen! Luckily I chose to rotate the shape in a direction that resulted in a reversed swastika (Close call).

I will not display it here though. Just to be safe.

[//]: # ![Accidental swastika shape](https://github.com/Hannoob/3DMaths/blob/master/images/fail.bmp "Accidental swastika shape")

This is because even though, the coordinates have to be rendered as integers, they can really be any decimal value, especially when using the trigonometric functions.
So I quickly fixed these issues, and added some styling and colour changes, and sure enough, my cube was rotating nicely.

## Chapter 4 - Rotation in other axes

Okay now that the heart rate has come down, I went on to rotate my cube around a more interesting axis. First up, the Y axis!
After looking at the existing code, I realized that I can basically ignore the Y changes, because these would remain constant if I rotate it in the Y axis, so I just need to update the X and Z coordinates.
It was basically like doing the same rotation, just looking from a different angle.
I updated the rotation code to the following:

```csharp
var newX = (x) * Math.Cos(angle) - (z) * Math.Sin(angle);
var newY = y;
var newZ = (x) * Math.Sin(angle) + (z) * Math.Cos(angle);
```

And sure enough! My cube was spinning happily in the Y axis!
This turned out to be way easier than I initially thought!
At this point I am feeling pretty chuffed with myself and decided that I am not going to any more axes as all of them would pretty much be a repetition of the one I already created.

## Chapter 5 - Some bells and whistles

One thing that has been bothering me for the longest time (Probably since I was a kid).
How does perspective work?
What is the math that makes things closer to us look bigger and move faster, and further away things look smaller and move slower?
In fact, that is probably what started me on this quest in the first place.
I had the feeling that things disappear into the distance, in the middle of where you are looking, and also that the rate at which things get smaller could not be linear, even though it sort of looks like it when you draw it out.
My instinct was that it must be hyperbolic because things would become very close to 0, but would never be completely invisible.
So I knew it had to do with the Z axis, but what really made it click for me was the realization that now the “camera” matters.
In isometric images, it would not matter at what distance I view the shape, it would always be the same size, but in perspective drawing, it maters how far I am from the object.

I decided to test the following idea.
The further away a point is from the camera, the closer it should be translated to the centre of the screen.
This would be done by the following code:

```csharp
var distance = cameraDistance + z;
var warpedX = x / (distance* warping);
var warpedY = y / (distance* warping);
formGraphics.FillRectangle(myBrush, new Rectangle((int)warpedX + centerX, (int)warpedY + centerY, brushSize, brushSize));
```

I would simply make the x and y coordinates smaller based on the z coordinates + the camera distance.
One thing that I saw almost immediately was that, if I had to divide by the distance from the camera in pixels, things would collapse into the centre way too quickly. That is why I introduced a “warping factor” to help reduce the rate at which things get smaller. I randomly picked a value of 0.002, rand the code for the first time and, remarkably, it gave great results!

![Success](https://github.com/Hannoob/3DMaths/blob/master/images/success.bmp "Success")

First try! That literally never happens!

# Chapter 6 - Conclusion

In conclusion, there are a few lessons that I learned in this experiment.

1. The actual math is not really that difficult (Except for the rotation part).
2. Always clear the screen before rendering new points.
3. Rounding issues can cause some rather surprising results.
4. Perspective is very simple to model
5. Cameras must have some sort of “warping” factor (I will keep an eye out for this in real life)

In fact, I wonder what this “warping” factor would be for human eyes? Is it the same for everyone? Is there even a “warping” factor in real life or is it just something I had to introduce to make my math work out?

Now that we are asking the “real” questions, if anyone knows I would love to hear it!

Here ends this epic tale of math and swastikas.
I hope you found it interesting.

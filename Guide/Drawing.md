# Drawing

`context.Drawing.Draw` is raised once per frame, before the game draws it. Its `Canvas` draws text, lines, rectangles, circles and images, and every drawing lasts that frame only: what should stay on the screen is drawn again at each frame.

```csharp
context.Drawing.Draw += (_, e) =>
{
    e.Canvas.DrawText("Hello!", new Point(20, 20), Font.Kooky30, new Color(255, 255, 255), Layer.Overlay);
};
```

## Layers

Every drawing goes to a layer, which decides what it covers and its units. From back to front:

| Layer | Where | Units |
|---|---|---|
| `BehindScenery` | In the level, in front of the backdrop, behind its scenery and tiles | Level units |
| `BehindRunners` | In the level, in front of its tiles, behind every runner | Level units |
| `BehindLocalRunners` | In front of the other runners, behind the ones of this computer | Level units |
| `InFrontOfRunners` | In front of every runner and item, behind the match's HUD | Level units |
| `Overlay` | Over the menus and the HUD, under popups and the mouse cursor | Pixels of the game's window |

In level units, a drawing stays where it is in the level as the camera moves and zooms: draw above a runner's `Hitbox`, and the text follows it. A drawing on a layer the game does not show this frame, such as a level layer in the menus, is dropped.

`canvas.VisibleArea(layer)` is the part of a layer the player sees: the camera's view for the level's layers, the whole window for the overlay. Use it to place things from the edges of the screen, which change with the window's size.

## Text

`canvas.MeasureText(text, position, font)` gives the area a text covers, to center it or put a background behind it. Fonts are the game's own, by family and size: `Font.Default18`, `Font.Kooky30`, `Font.Digital30`...

## Images

```csharp
var logo = context.Drawing.LoadImage("Logo.png");

context.Drawing.Draw += (_, e) =>
    e.Canvas.DrawImage(logo, new Rectangle(20, 20, logo.Width, logo.Height), Layer.Overlay);
```

- `LoadImage` reads a PNG from your mod's folder, once, in `OnLoad`. List the file as a `ModFile` in your project so that builds copy it. A file in a subfolder keeps it: `<ModFile Include="Sprites\Player.png" />` is copied to `Sprites\Player.png` in your mod's folder, which `LoadImage("Sprites/Player.png")` reads.
- `context.Drawing.GameImage(...)` gives some of the game's own images, such as its notification box and popup buttons, to draw in the game's style.
- `image.Recolored(color)` is the same image turned to a color's hue, keeping its shading.
- `DrawImage` stretches the image to the rectangle you give, and takes an opacity from 0 to 1.

See [ExampleDrawing](../Examples/ExampleDrawing).

## Online

Drawing only changes what your own game shows, so a mod that only draws can be validated for every online play, as long as what it shows gives no unfair advantage.

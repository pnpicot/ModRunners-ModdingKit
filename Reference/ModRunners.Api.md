# ModRunners.Api assembly

## ModRunners.Api namespace

| public type | description |
| --- | --- |
| interface [IGame](./ModRunners.Api/IGame.md) | The running game. |
| interface [IMod](./ModRunners.Api/IMod.md) | The entry point of a mod. A mod's assembly has exactly one public class implementing it, with a public constructor that takes no argument. |
| interface [IModContext](./ModRunners.Api/IModContext.md) | What ModRunners gives a mod: its identity, as its manifest declares it, its log, the game, drawing on the screen, its options, its messages, its items, its tile types, and whether other mods are loaded. |
| interface [IModLog](./ModRunners.Api/IModLog.md) | A mod's log. Its lines go to `ModRunners/Logs/Runtime.log`, prefixed with the mod's id. |
| struct [Point](./ModRunners.Api/Point.md) | A position, in the units of the space it belongs to. |
| struct [Rectangle](./ModRunners.Api/Rectangle.md) | An axis-aligned rectangle, in the units of the space it belongs to. |

## ModRunners.Api.Cameras namespace

| public type | description |
| --- | --- |
| class [CameraPlacingEventArgs](./ModRunners.Api.Cameras/CameraPlacingEventArgs.md) | Where the camera looks in this frame and how far it is zoomed out, which a handler can change. |
| interface [ICamera](./ModRunners.Api.Cameras/ICamera.md) | The camera that shows the level: the part of it the screen shows, and how far it is zoomed out. In a match, it frames every runner still in the race around the front runner, and zooms out as they spread. In a Solo run, it follows the runner and looks ahead of its speed. It can shake, and the game keeps its view inside the level. |

## ModRunners.Api.Drawing namespace

| public type | description |
| --- | --- |
| struct [Color](./ModRunners.Api.Drawing/Color.md) | A color, with its opacity in [`A`](./ModRunners.Api.Drawing/Color/A.md). |
| class [DrawEventArgs](./ModRunners.Api.Drawing/DrawEventArgs.md) | The canvas of the frame being drawn. |
| enum [Font](./ModRunners.Api.Drawing/Font.md) | The game's fonts, by family and size in points. A 12 point font is about 15 pixels high. The game's language can add fallback faces for its characters. |
| enum [GameImage](./ModRunners.Api.Drawing/GameImage.md) | An image of the game's menus, for drawings in the game's style. |
| interface [ICanvas](./ModRunners.Api.Drawing/ICanvas.md) | What a mod draws in the current frame. Each drawing goes to a [`Layer`](./ModRunners.Api.Drawing/Layer.md), in that layer's units, and only lasts this frame. A drawing on a layer the game does not show this frame is dropped. Within a layer, later drawings cover earlier ones. |
| interface [IDrawing](./ModRunners.Api.Drawing/IDrawing.md) | What a mod draws on the screen. |
| interface [IImage](./ModRunners.Api.Drawing/IImage.md) | An image a mod loaded from its folder with [`LoadImage`](./ModRunners.Api.Drawing/IDrawing/LoadImage.md), an image of the game's menus ([`GameImage`](./ModRunners.Api.Drawing/IDrawing/GameImage.md)), or one of those recolored. |
| interface [IShader](./ModRunners.Api.Drawing/IShader.md) | A pixel shader from the mod's folder, which colors each pixel of the drawings made with it. It is an effect compiled with `fxc /T fx_2_0`, whose technique's passes have a pixel shader and no vertex shader. |
| enum [Layer](./ModRunners.Api.Drawing/Layer.md) | Where a drawing goes among what the game draws, from back to front. The layers in the level are in level units, which scroll and zoom with the camera. |

## ModRunners.Api.Input namespace

| public type | description |
| --- | --- |
| interface [IKeyboard](./ModRunners.Api.Input/IKeyboard.md) | The keyboard, as gameplay reads it. Keys reach mods while the game's window is active, and while neither the chat, a popup, the player info nor the pause menu takes them. |
| interface [IMouse](./ModRunners.Api.Input/IMouse.md) | The mouse, as the game's menus read it. |
| enum [Key](./ModRunners.Api.Input/Key.md) | A key of the keyboard. `D0` to `D9` are the digits above the letters, `NumPad0` to `NumPad9` the numeric keypad's, and the `Oem` keys are punctuation keys whose symbols depend on the keyboard's layout. |
| class [KeyPressedEventArgs](./ModRunners.Api.Input/KeyPressedEventArgs.md) | A key the player pressed. |

## ModRunners.Api.Items namespace

| public type | description |
| --- | --- |
| interface [IBomb](./ModRunners.Api.Items/IBomb.md) | The bomb, whose explosion pushes the runners it reaches and takes the detonator away ([`IBombRules`](./ModRunners.Api.Items/IBombRules.md)). Each runner has one bomb. |
| interface [IBombRules](./ModRunners.Api.Items/IBombRules.md) | The bomb. Used, it is dropped behind its runner and falls to the ground, and the item becomes the detonator, whose use sets it off. An explosion, a shockwave or a fireball reaching it sets it off too. Its explosion pushes every runner in its reach away from it, its own included, without stunning them. |
| interface [IBox](./ModRunners.Api.Items/IBox.md) | The box, which a runner touching it breaks, stumbling ([`IBoxRules`](./ModRunners.Api.Items/IBoxRules.md)). |
| interface [IBoxRules](./ModRunners.Api.Items/IBoxRules.md) | The box, and the triple box, which gives three uses. Used, a box is dropped behind its runner and falls to the ground, where it stays until something breaks it. A runner touching it breaks it and stumbles, losing most of its speed. Explosions, shockwaves, rockets, fireballs and golden hooks break it too. |
| interface [IConfettiRules](./ModRunners.Api.Items/IConfettiRules.md) | The confetti, which only a golden character's runner can get. Used, it throws its own runner back and up, against its motion, and stuns it. |
| interface [IDroppedBox](./ModRunners.Api.Items/IDroppedBox.md) | A box its runner dropped, from the drop until it breaks. A runner's boxes are reused, so read it from [`InPlay`](./ModRunners.Api.Items/IBox/InPlay.md) when needed: once broken, one kept from before is out of date, and may be another drop of the same box. |
| interface [IFireball](./ModRunners.Api.Items/IFireball.md) | The fireball, which stuns the runners it touches ([`IFireballRules`](./ModRunners.Api.Items/IFireballRules.md)). |
| interface [IFireballRules](./ModRunners.Api.Items/IFireballRules.md) | The fireball. Used, it is thrown ahead of its runner, falls, rolls along the ground and slides along walls and ceilings, and explodes on a wall ahead. It stuns every runner it touches and goes on. Its drawing grows as it flies, its hitbox does not. |
| interface [IGoldenHook](./ModRunners.Api.Items/IGoldenHook.md) | The golden hook, which pulls the runner it locks onto toward its runner ([`IGoldenHookRules`](./ModRunners.Api.Items/IGoldenHookRules.md)). |
| interface [IGoldenHookRules](./ModRunners.Api.Items/IGoldenHookRules.md) | The golden hook. Used, it flies toward the runner most in front of its runner on the side it aims at, locks onto it and pulls it back, while its runner is drawn toward it. |
| interface [IGoldenHookShot](./ModRunners.Api.Items/IGoldenHookShot.md) | A golden hook its runner shot, from the shot until it is released. A runner's golden hook is reused, so read it from [`InPlay`](./ModRunners.Api.Items/IGoldenHook/InPlay.md) when needed: once released, one kept from before is out of date, and may be another shot of the same hook. |
| interface [IIce](./ModRunners.Api.Items/IIce.md) | The ice, which freezes the runners its beam reaches ([`IIceRules`](./ModRunners.Api.Items/IIceRules.md)). |
| interface [IIceBeam](./ModRunners.Api.Items/IIceBeam.md) | An ice beam, from when its runner uses the ice until the beam ends. Read it from [`InPlay`](./ModRunners.Api.Items/IIce/InPlay.md) when needed: once it ended, one kept from before is out of date. |
| interface [IIceRules](./ModRunners.Api.Items/IIceRules.md) | The ice. Used, it charges, then fires a horizontal beam from the top of its runner, which passes through walls and freezes every other runner it reaches, teammates included. |
| interface [IInvinciDrillRules](./ModRunners.Api.Items/IInvinciDrillRules.md) | The InvinciDrill. Used, it makes its runner invincible: nothing hits, stuns, freezes or hooks it, it boosts for free, breaks through boxes and obstacles, and tackles every runner it runs into. |
| interface [IItemPickup](./ModRunners.Api.Items/IItemPickup.md) | An item pickup of the level. Every game has its own copy, taken when that game learns of the take, and back after its own count. |
| interface [IItemPickups](./ModRunners.Api.Items/IItemPickups.md) | The item pickups of the map, and the items they roll from. Touching a pickup without an item gives the runner a random item, weighted by its place in the race, and the pickup comes back a few seconds later. |
| interface [IItemRules](./ModRunners.Api.Items/IItemRules.md) | The values items play by, one group per item. Speeds are in level units per second, and sizes are multiples of the game's, since an item's drawing and hitbox differ. |
| interface [IItems](./ModRunners.Api.Items/IItems.md) | The items: the values they play by, and the items in play. |
| interface [IModItem](./ModRunners.Api.Items/IModItem.md) | An item a mod added ([`Define`](./ModRunners.Api.Items/IModItems/Define.md)), for the whole run of the game. |
| interface [IModItems](./ModRunners.Api.Items/IModItems.md) | The items this mod adds to the game. Runners take them from item pickups, hold them with their icon, and use them with the item button, as the game's own items. |
| interface [IPlacedBomb](./ModRunners.Api.Items/IPlacedBomb.md) | A bomb placed in the level, from when its runner drops it until it explodes. Read it from [`InPlay`](./ModRunners.Api.Items/IBomb/InPlay.md) when needed: once it exploded, one kept from before is out of date, and changing it does nothing. |
| interface [IReleasedShockwave](./ModRunners.Api.Items/IReleasedShockwave.md) | A shockwave its runner released, from the release until the runner releases its shockwave again. Its values apply to this release only. |
| interface [IRocket](./ModRunners.Api.Items/IRocket.md) | A rocket, from when it is fired until it explodes. A runner's rockets are reused, so read it from [`InPlay`](./ModRunners.Api.Items/IRockets/InPlay.md) or [`Fired`](./ModRunners.Api.Items/IRockets/Fired.md) when needed: once it exploded, one kept from before is out of date, and may be another flight of the same rocket. |
| interface [IRocketRules](./ModRunners.Api.Items/IRocketRules.md) | The rockets, and the triple rockets, which give three uses. Each use fires a rocket up toward the side of the runner ahead. It homes in on the runner most in front of it, turning tighter the longer it flies, and explodes on whatever it hits. The explosion pushes every runner in its reach, its own included, and stuns them. The rockets of the map's launchers are not items and keep the game's values. |
| interface [IRockets](./ModRunners.Api.Items/IRockets.md) | The rockets, whose explosions push and stun the runners they reach ([`IRocketRules`](./ModRunners.Api.Items/IRocketRules.md)). |
| interface [IShockwave](./ModRunners.Api.Items/IShockwave.md) | The shockwave, whose blast pushes the runners it reaches ([`IShockwaveRules`](./ModRunners.Api.Items/IShockwaveRules.md)). |
| interface [IShockwaveRules](./ModRunners.Api.Items/IShockwaveRules.md) | The shockwave. Held, it charges for up to 1 second, which sends it further ahead. Released, it travels to its target, then blasts there: a square growing from 400 to 800 units wide pushes every other runner in it away from its center, harder near the center. Each runner is pushed once, keeps half its velocity and loses its hooks. The blast also sets off bombs and breaks boxes. |
| enum [Item](./ModRunners.Api.Items/Item.md) | An item a runner can hold. The triple box and the triple rockets are the box and the rockets with three uses. |
| class [ItemHittingEventArgs](./ModRunners.Api.Items/ItemHittingEventArgs.md) | An item about to hit a runner, on this game, which a handler can refuse. |
| enum [ItemMoment](./ModRunners.Api.Items/ItemMoment.md) | Something that happens with an item ([`ItemMomentEventArgs`](./ModRunners.Api.Items/ItemMomentEventArgs.md)). |
| class [ItemMomentEventArgs](./ModRunners.Api.Items/ItemMomentEventArgs.md) | Something that happened with an item, on this game. |
| class [ItemRolledEventArgs](./ModRunners.Api.Items/ItemRolledEventArgs.md) | The item a runner rolled from a pickup, which it gets once every handler ran: one of the game's items, or a mod's. |
| interface [IThrownFireball](./ModRunners.Api.Items/IThrownFireball.md) | A fireball, from when its runner throws it until it explodes. A runner's fireballs are reused, so read it from [`InPlay`](./ModRunners.Api.Items/IFireball/InPlay.md) when needed: once it exploded, one kept from before is out of date, and may be another throw of the same fireball. |
| class [ModItemUsedEventArgs](./ModRunners.Api.Items/ModItemUsedEventArgs.md) | A use of a mod item. |
| class [RocketFiredEventArgs](./ModRunners.Api.Items/RocketFiredEventArgs.md) | A rocket a use of the rockets fired. |
| enum [ShockwavePhase](./ModRunners.Api.Items/ShockwavePhase.md) | Where a released shockwave is in its life. |
| class [ShockwaveReleasedEventArgs](./ModRunners.Api.Items/ShockwaveReleasedEventArgs.md) | A shockwave a runner released. |

## ModRunners.Api.Levels namespace

| public type | description |
| --- | --- |
| interface [ILevel](./ModRunners.Api.Levels/ILevel.md) | The map being played, from when it is built at the start of a match to the end of the match. Every round of the match plays on it. |
| interface [ILevelObject](./ModRunners.Api.Levels/ILevelObject.md) | An object a mapper placed on the map in the level editor, besides its tiles: a deco, a text, a gate, a lever, a trigger zone, a checkpoint, spikes and the rest, as the map's file holds it. It is the same on every game, whatever the player's Quality option, which only leaves some decos undrawn. |
| class [LevelChangedEventArgs](./ModRunners.Api.Levels/LevelChangedEventArgs.md) | The level that was closed, and the one that was built. |
| enum [Stage](./ModRunners.Api.Levels/Stage.md) | The stage a map is set in, named by the map's file. It picks the map's backdrop, the image that fills the screen behind it, and its scenery. |

## ModRunners.Api.Lobbies namespace

| public type | description |
| --- | --- |
| enum [BotDifficulty](./ModRunners.Api.Lobbies/BotDifficulty.md) | How well a bot plays, as the Add Bot option and the practice menu offer it. |
| enum [GameMode](./ModRunners.Api.Lobbies/GameMode.md) |  |
| interface [ILobby](./ModRunners.Api.Lobbies/ILobby.md) | The players and slots of the current game, from the character selection to the end of its matches. Story levels, the tutorial and local parties have one too, without a character selection. |
| interface [IOpenLobbies](./ModRunners.Api.Lobbies/IOpenLobbies.md) | The quick match, ranked and custom lobbies other players host, as Steam lists them for this game's player. |
| interface [IOpenLobby](./ModRunners.Api.Lobbies/IOpenLobby.md) | A lobby another player hosts and a player of this game could join, as its host advertises it. The same lobby is the same object in every search of the session. |
| interface [ISlot](./ModRunners.Api.Lobbies/ISlot.md) | One of the places of a lobby, for a player or a bot. |
| interface [ITeam](./ModRunners.Api.Lobbies/ITeam.md) | A team of the lobby: slots that play together. |
| enum [LobbyDistance](./ModRunners.Api.Lobbies/LobbyDistance.md) | How far an open lobby is, by the nearest of Steam's search distances that finds it. |
| class [SlotChangedEventArgs](./ModRunners.Api.Lobbies/SlotChangedEventArgs.md) | A slot got or lost its player or bot. |
| [Flags] enum [SlotOptions](./ModRunners.Api.Lobbies/SlotOptions.md) | The options of an empty slot in the character selection of a custom match or a local party, which only the lobby's host sees. |
| class [SlotReadyChangedEventArgs](./ModRunners.Api.Lobbies/SlotReadyChangedEventArgs.md) | A slot that got ready in the character selection, or cancelled. |

## ModRunners.Api.Matches namespace

| public type | description |
| --- | --- |
| interface [IMatch](./ModRunners.Api.Matches/IMatch.md) | The match being played: its rounds, each from GO until every runner is placed for the next one. Story levels, the tutorial and local parties are matches too. A Solo run is not: it has no rounds. |
| interface [IXpLine](./ModRunners.Api.Matches/IXpLine.md) | A line of the XP screen's panel: its text, and the XP it pays in the panel's left column. |
| interface [IXpScreen](./ModRunners.Api.Matches/IXpScreen.md) | The XP screen ("Points!!!") the game shows after a match: a panel with its title, its lines and the total, then the level bar, the new unlocks and, in a ranked match, the ranked progress. Each game shows its own player's, and every game moves on when the host's timer ends. |
| class [LineUpEventArgs](./ModRunners.Api.Matches/LineUpEventArgs.md) | The runners about to start a round, from the back of the start line to its front, in an order a handler can change. |
| enum [OutCause](./ModRunners.Api.Matches/OutCause.md) | How a runner went out of a round. Every elimination is the camera's: the game keeps no cause, so it is read from the runner as it goes out. |
| class [RoundEndedEventArgs](./ModRunners.Api.Matches/RoundEndedEventArgs.md) | A round that ended: its points are given, and every runner is placed for the next one. |
| enum [RoundWinnerBanner](./ModRunners.Api.Matches/RoundWinnerBanner.md) | What the game does with its banner when a round is won: the winner's portrait slides across the screen with its points, and the new point pops in, on the banner and in the HUD. |
| class [RoundWonEventArgs](./ModRunners.Api.Matches/RoundWonEventArgs.md) | A round that was just won: its winner, and how many points the win gives. |
| class [RunnerOutEventArgs](./ModRunners.Api.Matches/RunnerOutEventArgs.md) | A runner eliminated from the round. |
| class [XpAward](./ModRunners.Api.Matches/XpAward.md) | An XP award the XP screen pays: the match's result, a feat of the match, or a daily bonus. |
| enum [XpLineKind](./ModRunners.Api.Matches/XpLineKind.md) | What a line of the XP screen's panel shows. |

## ModRunners.Api.Menus namespace

| public type | description |
| --- | --- |
| interface [IMainMenu](./ModRunners.Api.Menus/IMainMenu.md) | The main menu: the buttons on the bottom left of its first page, and the buttons of its Multiplayer and Practice pages. |
| interface [IMenuButton](./ModRunners.Api.Menus/IMenuButton.md) | A button a mod added to a menu. The game highlights it and lets the player reach it like its own buttons. |
| interface [IMenuPage&lt;TButton&gt;](./ModRunners.Api.Menus/IMenuPage-1.md) | A page of the main menu with the game's entries in a column, such as the Multiplayer page. Buttons a mod adds go into that column. |
| enum [MainMenuButton](./ModRunners.Api.Menus/MainMenuButton.md) | The game's buttons on the main menu, from top to bottom. The first three are bigger and spaced wider than the others. |
| enum [MultiplayerMenuButton](./ModRunners.Api.Menus/MultiplayerMenuButton.md) | The game's buttons on the Multiplayer page, from top to bottom. |
| enum [PracticeMenuButton](./ModRunners.Api.Menus/PracticeMenuButton.md) | The game's buttons on the Practice page, from top to bottom. |

## ModRunners.Api.Messaging namespace

| public type | description |
| --- | --- |
| interface [IMessages](./ModRunners.Api.Messaging/IMessages.md) | Messages between this mod and its copies on the other games of a modded lobby. Each game runs the mod on its own, so a message tells the others what only this game knows or decides, such as a random choice. Runners, items, eliminations and rounds already reach every game without one. |
| class [MessageReceivedEventArgs](./ModRunners.Api.Messaging/MessageReceivedEventArgs.md) | A message the mod sent from another game of the lobby. |

## ModRunners.Api.Options namespace

| public type | description |
| --- | --- |
| interface [IListField&lt;T&gt;](./ModRunners.Api.Options/IListField-1.md) | A field of a list's items, as declared with [`IListOption`](./ModRunners.Api.Options/IListOption.md). |
| interface [IListItem](./ModRunners.Api.Options/IListItem.md) | One item of a list option ([`Items`](./ModRunners.Api.Options/IListOption/Items.md)). |
| interface [IListItemValues](./ModRunners.Api.Options/IListItemValues.md) | The values of a default item being declared ([`AddDefault`](./ModRunners.Api.Options/IListOption/AddDefault.md)). |
| interface [IListOption](./ModRunners.Api.Options/IListOption.md) | An option that is a list: the player adds, removes and reorders its items, up to [`MaxItems`](./ModRunners.Api.Options/IListOption/MaxItems.md), and each item has the fields the mod declares on the list. Its fields and its default items are declared in [`OnLoad`](./ModRunners.Api/IMod/OnLoad.md), before its items are read. |
| interface [IOption&lt;T&gt;](./ModRunners.Api.Options/IOption-1.md) | One of the mod's options, as declared with [`IOptions`](./ModRunners.Api.Options/IOptions.md). |
| interface [IOptions](./ModRunners.Api.Options/IOptions.md) | The mod's options, which the player edits in the game. Each is declared in [`OnLoad`](./ModRunners.Api/IMod/OnLoad.md), in the order the options page shows them, and its value is saved between starts. |
| class [OptionChangedEventArgs&lt;T&gt;](./ModRunners.Api.Options/OptionChangedEventArgs-1.md) | An option's value, before and after a change. |

## ModRunners.Api.Ranks namespace

| public type | description |
| --- | --- |
| interface [IRanking](./ModRunners.Api.Ranks/IRanking.md) | This game's player in the ranked matches of the current season. |
| enum [League](./ModRunners.Api.Ranks/League.md) | The leagues of the ranked seasons, from the lowest. |
| struct [Rank](./ModRunners.Api.Ranks/Rank.md) | A rank of the current season: its league and its points. |

## ModRunners.Api.Runners namespace

| public type | description |
| --- | --- |
| struct [GrappleColors](./ModRunners.Api.Runners/GrappleColors.md) | The colors a runner's grapple is drawn with: the hook and its rope as the hook flies and once it locked onto a grapple ceiling, the golden hook item's hook, and the rope as it snaps. |
| interface [IAirRules](./ModRunners.Api.Runners/IAirRules.md) | How a runner falls and steers in the air. |
| interface [IBoostRules](./ModRunners.Api.Runners/IBoostRules.md) | The boost, which speeds a runner up from its meter while it holds boost and a direction, and super boosts, which level objects give. |
| interface [IGrapple](./ModRunners.Api.Runners/IGrapple.md) | The colors of the runners' grapples, the runners of other players and bots included. [`Colors`](./ModRunners.Api.Runners/IGrapple/Colors.md) is the rule for every runner, and a slot's runner can have its own colors as an exception. A change shows from the next shot, and the locked hook's from the next lock. |
| interface [IJumpRules](./ModRunners.Api.Runners/IJumpRules.md) | The jump, the air jump, the wall jump and the ledge hop. A runner has two jumps between landings, and walking off a ledge uses the first. |
| interface [IMovementRules](./ModRunners.Api.Runners/IMovementRules.md) | The values a runner moves by, grouped as players know its movement. Speeds are in level units per second, and accelerations and slowdowns in level units per second, per second. |
| interface [IRunner](./ModRunners.Api.Runners/IRunner.md) | The character a slot's player, bot or replay controls in a match. A runner is created at the match's first countdown and kept for every round, until its player leaves or the match closes. Read it from [`Runner`](./ModRunners.Api.Lobbies/ISlot/Runner.md) when needed: once its player left, a runner kept from before keeps its last values. |
| interface [IRunnerPopup](./ModRunners.Api.Runners/IRunnerPopup.md) | A text shown above a runner with [`ShowPopup`](./ModRunners.Api.Runners/IRunner/ShowPopup.md). |
| interface [IRunningRules](./ModRunners.Api.Runners/IRunningRules.md) | How a runner speeds up and slows down on the ground. A runner holding a direction accelerates by the band its speed is in: [`SlowRunAcceleration`](./ModRunners.Api.Runners/IRunningRules/SlowRunAcceleration.md) up to [`SlowRunSpeed`](./ModRunners.Api.Runners/IRunningRules/SlowRunSpeed.md), [`RunAcceleration`](./ModRunners.Api.Runners/IRunningRules/RunAcceleration.md) up to [`RunSpeed`](./ModRunners.Api.Runners/IRunningRules/RunSpeed.md), and above it, it slows down by [`OverspeedDeceleration`](./ModRunners.Api.Runners/IRunningRules/OverspeedDeceleration.md), so it settles at [`RunSpeed`](./ModRunners.Api.Runners/IRunningRules/RunSpeed.md). Boosting moves where it settles up to [`HighSpeed`](./ModRunners.Api.Runners/IRunningRules/HighSpeed.md) ([`IBoostRules`](./ModRunners.Api.Runners/IBoostRules.md)). |
| interface [ISlideRules](./ModRunners.Api.Runners/ISlideRules.md) | The slide, which lasts while its button is held and the runner moves, slowing it down. |
| interface [ISwingRules](./ModRunners.Api.Runners/ISwingRules.md) | The swing on the grapple's rope, which ignores gravity and the directions held, and lets go by itself near the top of its arc. |
| struct [RopeColors](./ModRunners.Api.Runners/RopeColors.md) | The two lines a grapple's rope is drawn with. |
| class [RunnerActedEventArgs](./ModRunners.Api.Runners/RunnerActedEventArgs.md) | Something a runner did, or that happened to it, as it moved. |
| enum [RunnerAction](./ModRunners.Api.Runners/RunnerAction.md) | Something a runner does, or that happens to it, as it moves. |
| [Flags] enum [RunnerControls](./ModRunners.Api.Runners/RunnerControls.md) | The controls a runner is played with: the keys or buttons its player presses, or a bot's choices in their place. Several are held at once. |
| struct [RunnerOutline](./ModRunners.Api.Runners/RunnerOutline.md) | A line drawn around a runner's character, behind it, following the shape of its current animation frame, not its hitbox. The game draws none. |

## ModRunners.Api.Screens namespace

| public type | description |
| --- | --- |
| enum [Screen](./ModRunners.Api.Screens/Screen.md) | A screen of the game. |
| class [ScreenChangedEventArgs](./ModRunners.Api.Screens/ScreenChangedEventArgs.md) | The screen the player left, and the one they are on. |

## ModRunners.Api.Settings namespace

| public type | description |
| --- | --- |
| enum [CustomTrails](./ModRunners.Api.Settings/CustomTrails.md) | Whose custom trails show, as the Game page's "Custom Trails" sets it. |
| enum [FrameLimitMethod](./ModRunners.Api.Settings/FrameLimitMethod.md) | How the game waits for the next frame, numbered like the launch option `+framelimitmethod`. |
| enum [GraphicsQuality](./ModRunners.Api.Settings/GraphicsQuality.md) | The Graphics page's "Quality". |
| interface [IAppliedSetting&lt;T&gt;](./ModRunners.Api.Settings/IAppliedSetting-1.md) | A saved setting that the game applies once, when it loads the saved options, and keeps in effect separately. So its value in effect can differ from its saved value: the player can apply a value without saving it, and a mod can override it for the session. |
| interface [IAudioSettings](./ModRunners.Api.Settings/IAudioSettings.md) | The options menu's Audio page. Volumes go from 0, silent, to 1. |
| interface [IGameSettings](./ModRunners.Api.Settings/IGameSettings.md) | The options menu's Game page. |
| interface [IGraphicsSettings](./ModRunners.Api.Settings/IGraphicsSettings.md) | The options menu's Graphics page. |
| interface [ILaunchSettings](./ModRunners.Api.Settings/ILaunchSettings.md) | The settings only Steam launch options set. They have no saved value: each session starts from the game's default or the launch option. |
| interface [ISavedSetting&lt;T&gt;](./ModRunners.Api.Settings/ISavedSetting-1.md) | A setting kept in the player's saved options, as the options menu sets it. |
| interface [ISessionSetting&lt;T&gt;](./ModRunners.Api.Settings/ISessionSetting-1.md) | A setting whose value in effect can be overridden for the session, like a Steam launch option overrides it. An override never reaches the player's saved options. |
| interface [ISetting&lt;T&gt;](./ModRunners.Api.Settings/ISetting-1.md) | One of the game's settings. |
| interface [ISettings](./ModRunners.Api.Settings/ISettings.md) | The game's settings, grouped like the options menu's pages, and the launch options. |
| enum [PlayerIndicator](./ModRunners.Api.Settings/PlayerIndicator.md) | When the marker above each runner shows, as the Game page's "Player Indicator" sets it. |
| enum [ProcessPriority](./ModRunners.Api.Settings/ProcessPriority.md) | Windows' priority classes for a process. The game's launch option `+priority` offers Normal, AboveNormal, High and Realtime. |
| struct [Resolution](./ModRunners.Api.Settings/Resolution.md) | A screen resolution, in pixels. |
| enum [SearchDistance](./ModRunners.Api.Settings/SearchDistance.md) | How far online matchmaking looks for other players, as the Game page's "Search Distance" sets it. |
| enum [SearchDistanceMode](./ModRunners.Api.Settings/SearchDistanceMode.md) | How matchmaking uses the [`SearchDistance`](./ModRunners.Api.Settings/SearchDistance.md), as the Game page shows it next to the distance. |
| class [SettingChangedEventArgs&lt;T&gt;](./ModRunners.Api.Settings/SettingChangedEventArgs-1.md) | A setting's value in effect, before and after a change. |
| enum [WindowMode](./ModRunners.Api.Settings/WindowMode.md) | How the game's window covers the screen, as the Graphics page's "Window Mode" sets it. |

## ModRunners.Api.Sounds namespace

| public type | description |
| --- | --- |
| enum [GameSound](./ModRunners.Api.Sounds/GameSound.md) | A sound the game plays. Each runner has its own copy of every sound. |
| interface [ISoundEffects](./ModRunners.Api.Sounds/ISoundEffects.md) | How a sound sounds: its volume, its pitch, its filters and its effects. Every value's default leaves the game's sound as it is. |
| interface [ISoundRules](./ModRunners.Api.Sounds/ISoundRules.md) | How each sound sounds: the rule for every runner ([`Rules`](./ModRunners.Api.Sounds/ISounds/Rules.md)), or one runner's own values ([`Sounds`](./ModRunners.Api.Runners/IRunner/Sounds.md)). |
| interface [ISounds](./ModRunners.Api.Sounds/ISounds.md) | The sounds the game plays, and how each one sounds. |
| struct [SoundEcho](./ModRunners.Api.Sounds/SoundEcho.md) | The sound repeated, each repeat quieter than the one before. The repeats keep the volume and the pan the sound started with, and stop once they would be quieter than a hundredth of it. |
| struct [SoundPitchRange](./ModRunners.Api.Sounds/SoundPitchRange.md) | The range the game draws the pitch of each play of a sound in, so that a sound repeated never sounds quite the same. |
| class [SoundPlayedEventArgs](./ModRunners.Api.Sounds/SoundPlayedEventArgs.md) | A sound this game started playing. |
| struct [SoundReverb](./ModRunners.Api.Sounds/SoundReverb.md) | The sound echoing off the walls of a space. A preset can be changed where it differs, as in `SoundReverb.Hall with { Mix = 0.5f }`. |

## ModRunners.Api.Tiles namespace

| public type | description |
| --- | --- |
| interface [IModTileType](./ModRunners.Api.Tiles/IModTileType.md) | A kind of tile a mod adds ([`Define`](./ModRunners.Api.Tiles/IModTileTypes/Define.md)): a look that a tile of the level can take in place of its own ([`SetModTileType`](./ModRunners.Api.Levels/ILevel/SetModTileType.md)). The tile keeps its [`TileType`](./ModRunners.Api.Tiles/TileType.md), so its form and what it does to runners and hooks. Anything more its tiles do, such as how runners move on them, is the mod's to write. |
| interface [IModTileTypes](./ModRunners.Api.Tiles/IModTileTypes.md) | The kinds of tile this mod adds. A tile takes one on this game only: nothing is sent, and each game draws the tiles as its mods set them. |
| interface [ITiles](./ModRunners.Api.Tiles/ITiles.md) | How tiles look, on every map: their palette, the thickness of their borders and their outline. Each is a rule for every tile type, and a type can have its own as an exception. A change shows from the next frame, on the map being played too. |
| struct [TileOutline](./ModRunners.Api.Tiles/TileOutline.md) | A line drawn inside a tile along its edges that touch air: an empty tile, the transparent part of a tile's drawing, such as the empty half of a slope, or the outside of the map. It runs around each cluster of tiles without a break, along faces, slopes and corners. The game's tiles have none. |
| struct [TilePalette](./ModRunners.Api.Tiles/TilePalette.md) | The two colors a tile is drawn with. The game's tiles are black, their main color, with white marks, their secondary color, on the faces that do something: a grapple ceiling's bottom edge, a wall jump face. |
| enum [TileStyle](./ModRunners.Api.Tiles/TileStyle.md) | How a tile is drawn among the level editor's tiles of its [`TileType`](./ModRunners.Api.Tiles/TileType.md). A tile's style changes nothing of what it does. |
| enum [TileType](./ModRunners.Api.Tiles/TileType.md) | What a tile does to runners and hooks. Every tile is solid. The level editor offers 16 tiles, and several of them are drawings of one type, such as the plain, stepped and checkered slopes ([`GetTileStyle`](./ModRunners.Api.Levels/ILevel/GetTileStyle.md)). |

<!-- DO NOT EDIT: generated by xmldocmd for ModRunners.Api.dll -->

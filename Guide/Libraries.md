# Libraries

A mod can use another mod's code. A library is a mod that offers public classes and methods, and the mods that need it call them as they call the Api. Players get the library with your mod: the Mod Browser installs it along with yours.

Dependencies need ModRunners 0.4.0 or newer.

## Using a library

1. **Install the library** in your game, from the Mod Browser. Your project compiles against its dll in `ModRunners/Mods/`.
2. **Add it to your project file,** by its Id:

    ```xml
    <ItemGroup>
        <ModDependency Include="SomeLibrary" />
    </ItemGroup>
    ```

    It references `ModRunners/Mods/SomeLibrary/SomeLibrary.dll` without copying it into your mod's folder. The library loads from its own folder, and a copy in yours would do nothing.
3. **List it in your manifest,** with the lowest version your mod works with:

    ```json
    "Dependencies": [
        { "Id": "SomeLibrary", "Version": "1.2.0" }
    ]
    ```

4. **Call it,** after `using SomeLibrary;`, as its documentation says.

ModRunners loads the library before your mod, so it is ready in your `OnLoad`. When the library is not installed, turned off, older than the version you ask, or fails to load, your mod does not load either. The Mods page marks it "needs a mod" and says why.

## Optional dependencies

When your mod also works without the library, add `"Optional": true`:

```json
"Dependencies": [
    { "Id": "SomeLibrary", "Version": "1.2.0", "Optional": true }
]
```

Your mod then loads whether the library is there or not. When it is there, it still loads before yours.

- **Ask whether it is loaded** with `context.IsModLoaded("SomeLibrary")`. The answer is final from your `OnLoad` on, since your dependencies load before you.
- **Keep every use of the library in a class of its own,** and mark its methods `[MethodImpl(MethodImplOptions.NoInlining)]`. .NET loads a library when it compiles a method that names one of the library's types, so a method that runs without the library must not name them, even through a small method the compiler would copy into it.
- An optional library installed in an older version than you ask keeps your mod from loading, as a required one does: your code would otherwise run against a library older than it was written for.

[ExampleDependencies](../Examples/ExampleDependencies) uses [ExampleLibrary](../Examples/ExampleLibrary) this way: it announces each round with the library's banner, and writes in the log without it. Without `"Optional": true`, it would call the library directly, with no check.

## Making a library

A library is a mod like any other: a manifest, one class implementing `IMod`, published on the Mod Browser.

- **Its public classes and methods** are what other mods use.
- **Prepare them in `OnLoad`.** Keep what they need, such as your `IModContext` or its drawing, in fields of your own. Your `OnLoad` runs before the `OnLoad` of every mod that depends on you. [ExampleLibrary](../Examples/ExampleLibrary) draws its banner with its own drawing this way.
- **A mod that wants the library to act for it** can pass its own `IModContext` to the library's methods.
- **Write its documentation.** Add `<GenerateDocumentationFile>true</GenerateDocumentationFile>` to the project's `PropertyGroup`. Each build then copies the documentation next to the dll, and the editors of the mods that use your library show your `///` comments. The compiler warns about every public member without one.
- **Never break a published version.** The mods built against your library call its public members by name. A new version may add members, but never change or remove one: the mods built against the old version would fail as soon as they reach it, possibly in the middle of a match. A change that has to break them needs a new Id.

## Publishing

- **Publish the library first.** The service refuses a mod whose required dependencies are not on the Mod Browser at the version it asks or newer. Optional ones are not checked.
- **Players get it with your mod.** Installing or updating your mod from the Mod Browser installs the libraries it needs that the player does not have, in their latest version, and turns back on a library the player turned off.
- **Online play** needs every loaded mod validated, so a player with your mod and its library needs both validated. I review your mod with what it calls in its libraries ([Publishing](Publishing.md)).

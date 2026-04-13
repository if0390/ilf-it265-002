\## Name: Ibeth Fernandez

\### Module: 9



\---



\### Date: 04/13/2026



\#### Goals for this Module

\- \[x] Choose a game engine and commit to it

\- \[x] Install Unity and set up a new project

\- \[x] Research how to build a card game in Unity

\- \[ ] Start planning the card data structure



\#### Progress

\- \*\*What I accomplished\*\*:

&#x20; - Decided to go with Unity as the engine for the digital version of Fractured Wonderland. Chose it over Godot mainly because of the larger community, more available tutorials, and am more familiar with Unity.

&#x20; - Installed Unity Hub and set up a new 2D project.

&#x20; - Spent time watching tutorials and reading about how card games are typically structured in Unity.

\- \*\*Challenges faced\*\*:

&#x20; - Never built a card game in Unity before, so it wasn't clear where to even start. A general Unity tutorial doesn't translate directly to a card game's needs.

&#x20; - Fractured Wonderland has extra complexity compared to a basic card game — cards have two effects depending on world state, and the game involves player roles, locations, and a shared game board.

\- \*\*Solutions\*\*:

&#x20; - Narrowed research focus to card-game-specific Unity tutorials rather than general ones. Looking at how simpler card games are structured in Unity first, then thinking about how to layer Fractured Wonderland's mechanics on top.



\#### Learnings

\- Building a card game in Unity is less about physics and more about data management and UI logic, which is a different mindset than a typical game project.



\#### Free Thinking

\- Each card could be a ScriptableObject with two effect fields — one for Dream, one for Nightmare. At runtime, the game just reads whichever field matches the current world state.

\- The World State itself could be a simple static bool or a GameManager variable that every card and UI element listens to. When it flips, everything updates at once.

\- Should the board locations (Forgotten Garden, Shattered Ballroom, etc.) also be ScriptableObjects? Or just hardcoded GameObjects in the scene?

\- What does the main game scene even look like? Probably a board view in the center, a hand of cards at the bottom, a world state indicator at the top, and a Consumed pile counter visible to everyone.



\#### Next Steps

\- Learn how to create a ScriptableObject for card data in Unity.

\- Build a simple test scene: one card that displays different text depending on a toggled world state — just to prove the core mechanic works in code.


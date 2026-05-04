\## Name: Ibeth Fernandez

\### Module: 11



\---



\### Date: 05/3/2026



\#### Goals for this Module

\- \[x] Complete the Game Design Document (GDD)

\- \[x] Create a ScriptableObject for card data in Unity

\- \[x] Set up the basic game scene and board layout

\- \[ ] Get a card displaying on screen with world state toggle



\#### Progress

\- \*\*What I accomplished\*\*:

&#x20; - Completed the Game Design Document using the template — filled out sections covering the game overview, target audience, core mechanics, win conditions, turn structure, and card types.

&#x20; - Created a `CardData` ScriptableObject in Unity with fields for card name, Dream effect description, and Nightmare effect description. Can now create individual card assets directly in the Unity editor.

&#x20; - Set up the main game scene with a basic board layout placeholder panels for the five locations (The Dream, Forgotten Garden, Shattered Ballroom, Hollow Library, Mirror Gate), a world state indicator area, and a Consumed pile counter.

\- \*\*Challenges faced\*\*:

&#x20; - The GDD template covers a lot of ground and it wasn't always clear what level of detail was needed for each section.

&#x20; - Figuring out how much to write vs. how much is still unknown.

\- \*\*Solutions\*\*:

&#x20; - For sections that were hard to fill out, focused on writing what is known for certain.



\#### Learnings

\- A GDD is most useful as it doesn't need to be perfect or complete on the first pass, just detailed enough to guide development decisions.

\- ScriptableObjects in Unity act like data containers that live as assets in the project folder creating a `CardData` asset for each card means card info can be edited in the Inspector without touching any code.

\- Setting up the board layout early, even as rough placeholder panels, makes it much easier to visualize how the game will actually feel to look at and play.



\#### Free Thinking

\- Now that the `CardData` ScriptableObject exists, the next logical step is a `CardDisplay` script that reads from it and updates the UI text based on the current world state.

\- The board layout made it clear that the five locations need connecting lines or arrows between them so players can see which locations are adjacent — important for the Move phase of each turn.

\- The GDD also helped clarify a few rules that were still fuzzy — writing things down forces you to be specific in a way that casual brainstorming doesn't.


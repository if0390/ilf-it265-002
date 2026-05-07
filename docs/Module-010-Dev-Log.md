\## Name: Ibeth Fernandez

\### Module: 10



\---



\### Date: 04/20/2026



\#### Goals for this Module

\- \[x] Watch card game specific tutorials for Unity

\- \[x] Research Unity UI / Canvas system

\- \[ ] Create a ScriptableObject for card data

\- \[ ] Build a basic card on screen



\#### Progress

\- \*\*What I accomplished\*\*:

&#x20; - Watched several Unity tutorials focused specifically on card games — learned how others structure their projects, organize card data, and handle hand/deck management.

&#x20; - Spent time studying Unity's UI and Canvas system since that's going to be the backbone of how cards and the game board are displayed.

&#x20; - Got a better general picture of how a card game project is laid out in Unity before jumping into building anything.

\- \*\*Challenges faced\*\*:

&#x20; - The Unity Canvas system was harder to wrap my head around than expected — concepts like Canvas Scaler, anchors, and the difference between Screen Space and World Space were confusing at first.

&#x20; - Card game tutorials vary a lot in complexity and approach, so it was hard to know which pattern to follow for a game as unique as Fractured Wonderland.

\- \*\*Solutions\*\*:

&#x20; - Slowed down and focused on understanding Canvas basics before moving on — watched a dedicated UI tutorial to get comfortable with anchors and layout before worrying about card logic.



\#### Learnings

\- Unity's Canvas system works in layers — UI elements stack on top of each other based on their order in the hierarchy, which will matter a lot for things like drawing a card from the deck or showing a card's details on hover.

\- Anchors and pivots control how UI elements reposition when the screen size changes — important to understand early so the layout doesn't break on different resolutions.



\#### Free Thinking

\- Fractured Wonderland's world state indicator could live in a persistent overlay Canvas that sits above everything else — always visible no matter what's happening on the board.

\- Should each location (Forgotten Garden, Hollow Library, etc.) be its own UI panel that activates/deactivates as players move? Or should the whole board always be visible like the physical version? 



\#### Next Steps

\- Create a `CardData` ScriptableObject with fields for card name, Dream effect description, and Nightmare effect description.

\- Build a basic test scene with one card panel on screen that swaps its displayed effect text when a world state toggle is clicked.




\## Name: Ibeth Fernandez

\### Module: 8



\---



\### Date: 04/06/2026



\#### Goals for this Module

\- \[ ] Present Fractured Wonderland to the class

\- \[ ] Choose a game engine (Unity or Godot) for digital implementation

\- \[ ] Begin planning the digital card system

\- \[ ] Research how other card games handle dual-effect mechanics in code



\#### Progress

\- \*\*Challenges faced\*\*:

&#x20; - Still haven't locked in an engine — Unity is more familiar but Godot might be lighter and easier to manage for a card game scope.

&#x20; - Translating the physical dual-effect card system into a digital format will require some thought around how the UI communicates which effect is currently active.

\- \*\*Solutions\*\*:

&#x20; - N/A — engine decision is still pending research this module.



\#### Learnings

\- Presenting the game out loud helped reinforce which mechanics are easy to explain and which ones still feel confusing to a new audience — the fragment collection rules (location + world state + card) needed the most clarification.

\- The dual-effect card system is the most innovative part of the design, but it's also the hardest to communicate quickly. A visual demo or interactive prototype would go a long way.



\#### Free Thinking

\- If this goes digital, the World State flip could be a full visual transition — the UI palette shifts from soft pastels (Dream state) to dark reds and purples (Nightmare state) so players feel the change immediately.

\- How should hidden identity work digitally? In the physical game, identities are cards dealt face-down. In a digital version, the server would need to store each player's role privately and only reveal it under the right conditions.

\- Could the Consumed pile be visualized as a decaying progress bar — like Wonderland literally crumbling as it fills? Ties into the lore of the dream dying if no Alice sustains it.

\- Unity vs Godot: Unity has more resources for card game UI (assets, tutorials), but Godot's scene system might map cleanly to individual cards as scenes with swappable components for Dream/Nightmare effects.



\#### Next Steps

\- Research Unity and Godot card game implementations to make a final engine decision.

\- Sketch out what the digital game board UI might look like — locations, world state indicator, fragment tracker, and Consumed pile counter.

\- Think about how player turns will be structured in code: Vote → Move → Play → Draw as a state machine.


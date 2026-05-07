\# Game Design Document (GDD)



\## Fractured Wonderland



\*A Utopia Works production\*



\*Author: Ibeth F.\*



\---



\## Change Log



This section tracks the major design changes that occurred between the physical prototype's playtests and the current digital prototype design.



\### Change 1 — Rebalancing the Nightmare advantage



\*\*Observed in playtest:\*\* Even when the Dreamers played well, the math seemed to favor the Nightmare team — by the time Dreamers identified a single Nightmare, eliminations had already pushed the player count too close to parity.



\*\*Change made:\*\* The Dreamer win condition is being re-examined for the digital prototype. Specifically, the original design required a fixed 3 Dream Fragments to win, but voting could happen frequently enough that Nightmares could reach parity (Dreamers ≤ Nightmares) before fragments were realistically collectible. In the digital prototype, the voting phase is being reduced from "every round" to a single triggered round, giving Dreamers more uninterrupted turns to gather fragments before being eliminated.



\*\*Why:\*\* If players go in \*expecting\* Nightmares to win, the social deduction loses tension — Dreamers stop trying, Nightmares stop bothering to be subtle. Slowing the voting cadence shifts pressure off the Dreamer team without removing the deduction element, and is the smallest change that addresses the imbalance for now. Future iterations may also adjust the role split or the fragment threshold based on more playtest.



\### Change 2 — Simplifying archetype abilities



\*\*Observed in playtest:\*\* Players found the archetypes confusing. Specifically, they weren't sure what the limits or boundaries of each archetype's ability were — when can the Warrior break locks? Can they break any lock? Does the Singer's ability cost an action? Players had to keep stopping the game to ask, which broke immersion and made the social deduction harder.



\*\*Why:\*\* Cutting the most ambiguous mechanics for this iteration lets us validate that the \*core loop\* works before reintroducing complexity. These mechanics are still part of the full design vision and may return in a later, more polished version once the foundation is solid.



\### Change 3 — Reducing the board from 4 to 3 locations



\*\*Observed in playtest:\*\* Less critical, but tied to the confusion theme — players were also a bit uncertain about which locations did what, and movement felt arbitrary when there were too many options that didn't feel mechanically distinct.



\*\*Change made:\*\* The digital prototype uses 3 locations instead of 4: The Dream (center starting location), The Garden, and The Mirror Gate. The fourth location is cut for now.



\*\*Why:\*\* Three locations is the minimum for movement to feel meaningful without overwhelming first-time players with too many choices. Each location now has a clearer mechanical identity: The Dream is the safe starting hub, The Garden is the main fragment-collection space, The Mirror Gate is the locked location that the Warrior cannot break. A fourth location can be added back once the three-location version proves the loop is fun.



\### Summary



The unifying theme of these changes is \*\*reducing complexity to validate the core loop\*\*. The physical prototype had ambitious mechanics that proved to be both individually under-specified and collectively overwhelming. The digital prototype is intentionally a smaller version of the same game — same theme, same hidden-role structure, same world-state flipping — built to answer a single question: \*\*does the core loop of move → play card → discuss → vote actually create the tense social-deduction feeling we want?\*\* Once that's proven, complexity can be added back deliberately, one piece at a time, with each addition tested against the same playtest baseline.



\---



\## 1. Introduction



\### 1.1. Scope of the document



This document is intended for the building of the digital prototype, my course instructor evaluating the work, and anyone reviewing the design including classmates giving feedback. It captures the design vision for Fractured Wonderland in its physical board game form, with notes pointing toward how that design translates into the Unity digital prototype.



\### 1.2. Elevator pitch



\*Fractured Wonderland\* is a 5-player social deduction card game set in a corrupted dark fairytale world. Dreamers must collect Dream Fragments scattered across a shifting wonderland to escape, while hidden Nightmares secretly sabotage them. With a flippable world state, archetype-based abilities, and tense voting rounds, the game turns "Among Us meets Alice in Wonderland" into a tabletop experience full of paranoia and betrayal.



\---



\## 2. Game Overview



\### 2.1. Game concept



Players take on archetype roles in a shattered fantasy realm, secretly assigned as either Dreamers or Nightmares. On each turn, a player moves to a location, plays a card, and draws back up. Periodic voting phases let the group eliminate a suspected Nightmare. The game is built around the feeling of distrust, hidden agendas, and the tension of social deduction.



\### 2.2. Audience



Target players are ages 14+, comfortable with social deduction games like \*Among Us\*, \*Werewolf\*, \*The Resistance\*, and \*Secret Hitler\*, and drawn to dark fairytale aesthetics. They enjoy strategy + storytelling combined and play in friend groups of 4–6 people. 



\### 2.3. Genre



\*\*Primary genre:\*\* Social deduction card game.



\*\*Secondary genres:\*\* Strategy (world state and card timing matter), dark fantasy (gothic wonderland setting).

\### 2.4. Setting



A fractured fairytale realm — Wonderland after a great corruption split it into two overlapping states: the pure Dream and the corrupted Nightmare. The world flips between these states based on player actions. Aesthetic blends gothic horror with classic fairytale imagery: cracked mirrors, withered gardens, crooked towers.



\### 2.5. World structure



Players navigate a small, fixed board of 4–5 named locations. Movement is location-to-location by choice. Players begin at The Dream (center) and travel outward.



\### 2.6. Player



5 players total. Each is assigned a hidden role (Dreamer or Nightmare) and an archetype (Warrior, Singer, Queen, or Twins). Twins is played by two players sharing the archetype but acting independently. Roles and archetypes are kept secret; only the player themselves knows their assignments at the start.



\### 2.7. Core loop



On each player's turn:

1\. \*\*Move\*\* to a location (or stay).

2\. \*\*Play one card\*\* from hand (Dream Fragment, World Shatter, Identity Swap, or Move card).

3\. \*\*Resolve effects\*\* (collect fragment if conditions are met, flip world state, etc.).

4\. \*\*Draw\*\* one card from the deck.

Voting is a separate phase triggered by a player action — it does not happen automatically each turn.



\---



\## 3. Gameplay



\### 3.1. Objectives



\*\*Dreamers' main objective:\*\* Collect 3 Dream Fragments as a team, unlock the Mirror Gate, and have at least one Dreamer reach it to escape Wonderland.

&#x20;

\*\*Nightmares' main objective:\*\* Fill the Consumed pile to 20 cards, or eliminate all Dreamers through triggered voting rounds.



\### 3.2. Progression



The game progresses turn by turn through a fixed loop. Tension builds as fragments are collected (visible to all) and players are eliminated. The world state flipping creates shifting opportunities — a location useful for fragments now may be locked next round. Information accumulates: who played what card, who voted for whom, who moved where. Players progress in their understanding of the social puzzle rather than gaining levels or items.



\### 3.3. Play flow



A typical game runs 20–30 minutes. After setup and role reveal, players take turns clockwise. Voting phases are triggered by a specific card or player action rather than happening on a fixed interval.

\---



\## 4. Mechanics



\### 4.1. Rules



\- Each player begins at The Dream with a hand of 3 cards.

\- On a turn, a player may move once, play one card, and draw one card.

\- Dream Fragment cards can only be successfully played at a location whose current world state matches the card's required state.

\- The world state can be flipped by World Shatter cards (and is locked for 2 rounds afterward).

\- Voting is triggered by a player action, not automatically each round. Voting requires majority; ties result in no elimination.

\- The Consumed pile fills as cards are played. When it reaches 20 cards, Nightmares win.



\### 4.2. Game universe



The world state functions as a global variable affecting all locations. When a location is "locked" by a card effect, it cannot be entered or interacted with for the duration. 



\### 4.3. Character movement



Players move between named locations on the board. One movement per turn (or stay in place). Some cards modify movement (e.g., Move cards allow an extra movement). Locked locations cannot be entered.



\### 4.5. Player interaction



Players interact through:

\- \*\*Card play\*\* with effects on the world or other players.

\- \*\*Voting\*\* during the voting phase.

\- \*\*Discussion\*\* — open table talk, accusations, claims about role.



\---



\## 5. Graphics and Audio



\### 5.1. Visual system



2D, panel-based UI. The prototype uses Unity's Canvas system with placeholder rectangles and system fonts. 



\### 5.2. Interface



The interface is the primary gameplay surface. Players see:

\- The board

\- Their current location 

\- Their hand 

\- Current world state

\- Public fragment count

\- Their secret role/archetype (only on their reveal screen, hidden during others' turns)



\---



\## 6. Game World



\### 6.1. Look \& Feel of the world



A small, claustrophobic realm — five named locations, each tied to a fairytale archetype. The world feels haunted: beautiful in the Dream state, decayed in the Nightmare state. The flip between states is the world's central tension.



\### 6.2. Locations



\#### The Dream (center)



\- \*\*Description:\*\* The starting location.

\- \*\*World state:\*\* Accessible in any state.

\- \*\*Connection to plot:\*\* All players begin here. 



\#### Forgotten Garden



\- \*\*World state:\*\* Dream only — the Garden fragment can only be collected when the world is in Dream state.

\- \*\*Connection to plot:\*\* One of the three fragment locations. Favors Dreamers.



\#### Shattered Ballroom



\- \*\*World state:\*\* Any state — the fragment here can be collected regardless of current world state.

\- \*\*Connection to plot:\*\* The most accessible fragment location; a neutral ground.



\#### Hollow Library



\- \*\*World state:\*\* Nightmare only — the Library fragment can only be collected when the world is in Nightmare state.

\- \*\*Connection to plot:\*\* One of the three fragment locations. Favors Nightmares.



\#### Mirror Gate



\- \*\*World state:\*\* Exit point — no fragment here.

\- \*\*Connection to plot:\*\* The Dreamers' escape point. Must be unlocked by playing the "Locked Door" card in Dream state. At least one Dreamer must reach it after the team collects all 3 fragments to win.


Chess: The Pre-Sequel
A 2D chess roguelite built in Unity with C#. It takes standard chess as a base and mixes in ideas from deck-builders and inventory-management games (Slay the Spire, Balatro, Backpack Battles, Teamfight Tactics) to turn a strict, solved game into something chaotic and replayable.
This is a personal learning project. The focus is on understanding Unity and C# properly, so the codebase has been through several deliberate refactoring passes rather than just being made to "work".

Concept
Standard chess is almost completely calculated and solved. The idea here is to add a chaos factor on top of it: instead of a fixed starting army, the player collects pieces, fits them onto the board within a weight capacity, and upgrades them with artifacts to build combinations that break the game in fun ways.

Planned systems, drawn from the design document:
Roguelite map — a randomly generated run of rooms (fights, elites, treasure, events) leading to a boss, in the style of Slay the Spire.
Inventory and weight — each piece has a weight; the player fits a limited army onto the board.
Artifacts — modifiers attached to pieces that interact with each other, inspired by Backpack Battles.
Sets / bundles — pieces evolve through artifact combinations and gain bonuses based on how many unique pieces of a set you own, inspired by Teamfight Tactics.
Combat — pieces fight rather than simply capturing 

Current state
The core chess systems are implemented and have been refactored into a clean architecture. What works right now:
Unified movement. All sliding and jumping pieces share a single CanReach(Tile, Vector2Int[], int maxDistance) method in the Piece base class, driven by direction sets in a static Directions class (Orthogonal, Diagonal, Universal, Knight). One algorithm covers five of the six piece types — the knight's jump falls out naturally from L-shaped vectors with maxDistance = 1, with no special-case code. This follows the "Vector Attacks" approach used by chess engines (https://www.chessprogramming.org/Vector_Attacks).
Pawn is intentionally handled separately, because its forward-move / diagonal-capture asymmetry doesn't fit the shared model.
Combat system. A dedicated CombatManager handles HP, attack power, defense, and percentage-based damage resistances. Damage types (Piercing / Slashing / Bludgeoning) form a rock-paper-scissors triangle, so type matchups matter.
Centralised input. A SelectionManager handles all piece selection and move/attack highlighting. Moving input here (and removing colliders from the piece prefabs) fixed a long-standing intermittent bug where some pieces couldn't be selected, caused by competing OnMouseDown handlers on both tiles and pieces.
Board stores tiles as a typed Tile[,] grid, with highlight clearing extracted into Board.ClearAllHighlights() and Tile.ClearHighlights().

Tech
Engine: Unity (2D)
Language: C#
Art: Aseprite (64×64 sprites, 64 PPU)
Version control: Git + GitHub

Architecture notes
A few decisions worth calling out, since the why mattered more than the what on this project:
One movement method, not six. Rook, Bishop, Queen and King differ only in their direction set and maximum distance, so they collapse into a single method plus a table of vectors. This removed three copies of the same loop.
Managers over mixed responsibilities. Combat and selection each live in their own manager class instead of being spread across Piece. This made the selection bug fixable by deleting the structure that caused it, rather than patching the symptom.
Naming conventions follow the course style guide: PascalCase for methods and classes, camelCase for variables, m_ for member fields and s_ for static fields.

Roadmap
Pawn promotion (next planned feature)
The roguelite progression layer (map, inventory, artifacts, sets)
The wider game loop tying runs together

Author
Artem Siletskyi

# SpaceWars.Console

> SpaceWars.Console is a starter client for players to interact with [SpaceWars (the server)](https://github.com/SnowSE/SpaceWars).

This project is designed to be forked (or cloned) by participants and modified to program their own custom 'macros' for their individual competitive advantage.

## Gameplay

Upon joining a game, players manage a queue of actions - movement actions, turning actions, firing actions, repairing ship/shield actions, buying weapon actions, etc.  A player can queue up as many actions as they want, but one player action is executed for each player each 'tick' of the game.  Players do have the ability to clear their queue though and refill it (so if you're busy flying toward an opponent but the opponent starts flying away you can clear your queue to change your heading and start flying in a new direction.

## Weapons

Every weapon has one or more 'ranges', which will impact the effectiveness of the weapon.  If you're too far away, the weapon has no effect (even if you're lined up).  The closer you get, the more effective the weapon is.

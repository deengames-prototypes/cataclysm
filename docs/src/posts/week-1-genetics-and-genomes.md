meta-id: af987624f8d2327b71c6b6d3a062cfcb6af2478d

meta-title: Week 1: Genetics and Genomes
meta-publishedOn: 2017-02-03

Last week, I left off with a shell roguelike (movement, field of view, darkness, random dungeon generation, player movement). This week, I started to work on modelling the core classes I need for genetics, which ultimately create a unique, randomized genome for each monster.

In simple terms, I want each run of my game (potentially) to create a unique "universe" populated with monsters. These monsters have genetic variations. Eg. if you play the game twice, given a monster (say, Ratty), you may find:

- In the first playthrough, Ratty can be ice-elemental or fire-elemental, and in terms of genetics, fire-elemental dominates.
- In the second playthrough, Ratty can be ice-elemental or thunder-elemental, and ice-elemental dominates.

While I want this to be potentially randomizable, for development and debugging, I'm going to use a fixed seed with the random number generator so I can see consistent results.

With that in mind, this week, I:

- Created the first "super genome" (set of all possible genes and variations/alleles) for all monsters, with the minimal set of stats for basic combat
- Seeded random generation so I can test with consistent results
- Wrote code to generate a unique genome, per monster, based on a subset of the super-genome genes
- Fixed a bug where some alleles are never used (the game now distributes them randomly to different monsters)

When I debug the game, I get a unique (consistent) set of randomly-generated monster types, each with unique genes and genomes.

Although I didn't reach my goal of unit-testing, next week, I can start in earnest on the actual gameplay: picking a starting creature for the player, and the real "roguelike" gameplay (exploring, fighting, etc.).

Until next week!
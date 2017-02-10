meta-id: 1ac07708dc797843cadfc2049e59797b66c92831

meta-title: Week 2: Monsters and Stats
meta-publishedOn: 2017-02-10

This week went slower than expected. I focused on three areas:

- Builds
- Tests
- Creatures

# Builds

For various reasons (which I won't go into here), I switched back from developing on Linux to developing on Windows. At the same time, I need to make sure Cataclysm keeps building and running on Linux.

To achieve that goal, I set up a Travis-CI build via GitHub, and added the build status to the `README.md` file. Travis builds Cataclysm on every check-in (on every branch) and reports immediately if the build failed.

This way, I can be sure I don't lose out on Linux support.

# Tests

I'm a big fan of test-driven development and unit-testing in general. With Cataclysm, I intentionally separated "view" concerns (the UI: displaying, receiving input, etc.) in order to try to keep the "core" code as testable as possible.

I didn't write any unit tests earlier, since I didn't have much to test (other than a proof-of-concept app in `SadConsole`). This week, I wrote unit tests for the few model classes and data-generation classes I have (that create monster genomes).

Going forward, all new code requires unit tests.

# Creatures

Initally, I used placeholder animal names (eg. `lion`, `tiger`). While this presented an educational opportunity (teach players about real animal powers), the downside is that almost every monster would have completely unique attacks.

Instead, I switched over to animal-inspired names. I also created base stats for all animals (health, strength, etc.) and started, but didn't complete, the list of attacks per creature.

My goal next week includes completing creature generation, picking a random starter, and generating a starting map.
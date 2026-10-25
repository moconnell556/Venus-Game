# VENUS ACENSION

*Survive the station. Question the voice guiding you.*

VENUS ACENSION is a 2D survival game set aboard a floating research station in 
Venus's upper atmosphere. Players must navigate the station, manage essential resources, and maintain critical life-support systems
to survive the planet's hostile environment. Throughout the experience, the player is helped by the "voice" a seemingly helpful AI whose true intentions become increasingly unclear.

## Team — Three Musketeers

- [Krish](https://github.com/KrishnaV193) — Character design, environment artwork, Unity prototyping, and documentation.
- [Marie](https://github.com/moconnell556) — Gameplay programming, player movement, station simulation, interactions, and scene development.
- [Taliyah](https://github.com/taliyahjulia) — and communication of the game's concept.

- ## About

VENUS ACENSION is a 2D survival game inspired by the scientific possibility of human habitation in Venus's upper atmosphere. Although Venus's surface is extremely hostile, certain regions of its upper atmosphere have temperatures and pressures that could theoretically support floating research habitats.

Our game places players aboard one of these fictional stations, where survival depends on maintaining essential resources and managing the station's environment.

Players can explore the habitat, interact with station systems, and work to maintain oxygen, water, and power. Throughout the experience, VOICE, the station's AI, provides voice guidance and information about the surrounding environment.

Our goal was to combine scientific concepts with interactive gameplay, allowing players to experience some of the challenges associated with surviving on another planet.

VENUS ACENSION was developed using Unity 6 and C#. Our visual assets were designed using Canva and Piskel, including the station environments and our original pixel-art character.

### External Tools and AI Usage

- **ElevenLabs:** Used to create The "voice" AI-generated voice dialogue.
- **Unity 6:** Used as the game engine, with C# for programming gameplay mechanics and interactions.
- **Canva:** Used to design the station environments and visual assets.
- **Piskel:** Used to create our original pixel-art character.
- **GitHub and Git LFS:** Used for version control, collaboration, and asset management.

### AI Disclosure

AI assistance, including claude, was used for programming guidance and portions of prototype code, including the player movement prototype. Team members reviewed and adapted AI-assisted code as part of development.

ElevenLabs was used to generate audio dialogue for The voice.

No AI-generated images or videos were used. Visual assets were designed by team members.

The project was initially created using Unity's standard 2D project template. Game-specific features and mechanics were 
developed during the hackathon.

## Prerequisites

The following tools are required to open and run VENUS ACENSION:

- **Unity Hub** — For managing and opening the Unity project.
- **Unity 6** — The game engine used to develop VENUS//30.
- **Git** — For cloning the project repository.
- **Git LFS** — For downloading large game assets, including images and audio.

The project can be opened on a computer running a compatible version of Unity 6.

Before cloning the repository, make sure Git LFS is installed and initialized:

```bash

## Build from Source

Follow these steps to download and run VENUS ACENSION.

### 1. Clone the Repository

Open a terminal and run:

```bash
git lfs install
git clone https://github.com/moconnell556/Venus-Game.git
cd Venus-Game
git lfs pull
```

### 2. Open the Project in Unity

1. Launch Unity Hub.
2. Select **Add → Add project from disk**.
3. Navigate to the downloaded `Venus-Game` folder.
4. Select the folder and open the project using Unity 6.
5. Allow Unity to import the project's assets and packages.

git lfs install


## Extras

### Scientific Inspiration

VENUS ACENSION draws inspiration from real scientific research into the possibility of human exploration and habitation within Venus's upper atmosphere.

Although Venus's surface experiences temperatures exceeding 460°C and atmospheric pressure more than 90 times that of Earth's surface, conditions approximately 50 kilometers above the surface are significantly more moderate.

NASA has explored concepts involving floating research stations and airships in this region, including the High Altitude Venus Operational Concept (HAVOC).

However, major challenges remain:

- **Breathable Air:** Venus's atmosphere consists primarily of carbon dioxide, requiring a separate oxygen supply.
- **Corrosive Clouds:** Sulfuric acid droplets would require protective materials.
- **Water Management:** Water would need to be stored and recycled.
- **Power Generation:** Station equipment and life-support systems would require reliable energy.
- **Environmental Monitoring:** Conditions inside the habitat would need to be continuously maintained.

These challenges inspired the survival mechanics and station environment in VENUS ACENSION.

Our game is a fictional, simplified interpretation of these scientific concepts rather than a demonstration of currently available colonization technology.

### Project Scope

VENUS ACENSION was developed during Geaux Hack 2026 as a survival-game prototype.

Our primary focus was creating an interactive station environment, implementing resource-management systems, and integrating VESPER's voice guidance.

Some originally envisioned features were simplified to focus on developing the core survival experience within the hackathon timeframe.

### Scientific References

- [NASA — Venus Facts](https://science.nasa.gov/venus/venus-facts/)
- [NASA — High Altitude Venus Operational Concept (HAVOC)](https://www.nasa.gov/general/havoc/)

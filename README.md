# GDIM32-Final
## Check-In


### Group Devlog
When we were working the logic for how the interact text ("[E]") would show up when the player hovered over an interactable, there were several instances where the crosshair indicated we were looking right at it, but the text wasn't showing up. And then vice versa, when we were no where near it and the text was certain now was the time to press "E". To discover the issue, we used  `Debug.DrawRay()` to check where the raycast was actually hitting, and if it was following what the camera indicated. We discovered, then, that the raycast wasn't actually checking where the player looked exactly, but rather coming perpendiclarly out of the player regardless of the up/down angle that the camera was at. Because of this, some interactables were physically impossible to hit with the raycast (either higher or lower than the sightline), so the interact text never popped up. As for the instances where the interact text would pop-up when the camera wasn't looking at the object, the `Debug.DrawRay()` confirmed (once it was working correctly) that the raycast was hitting the interactable, so we knew that the problem was in the logic. As it turned out, we had inversed the order of an event invoke and the activation of the interact text component, so they would cancel each other out or double toggle weirdly rather than as-intended. 

### Eli Gutierrez
My contributions came almost exclusively in the form of script and component work (the logic), rather than the creation of the scene, objects, and layout of the levels. This included coding the `Player` class, the abstract `Interactable` class and the subsequent child classes, the `GameController` class, and the `UIController` class. For the `Player`, I worked both the player movement and the camera movement, as well as the detection of whether or not the player was looking at an `Interactable`, and what `Interactable` they were looking at if they pressed "E". This was done using inheritance. If an object was tagged as an "Interactable", I could use `GetComponent<>()` on them and check if `Item`, `NPC`, or `Door` didn't return null. Beyond that, I also created the branching dialogue system using ScriptableObjects (`DialogueLines`), the `NPC` class, the `UIController` class, and the `GameController` class. When the Player interacts with an NPC, `SetToTalking()` is called within the `NPC` class and the `UIController` is informed of what dialogue the game should be displaying and the ways it can branch with the ScriptableObjects. However, the starting node of the branching dialogue needed to be based on whether or not a key item has been collected by the player, so I made an array of the potential starting nodes and used a boolean `hasKeyItem` to determine whether or not the Player has the item (the cup, in this case) when the dialogue is first loaded. Depending on the `hasKeyItem` state, a different index is chosen for the starting node. Creating the dialogue UI also required the use of `Buttons` and `TMP_Text`, which was scaled properly in the `Canvas` along with the crosshair and interact text I added. Overall, the project has stuck pretty strongly to the original plans that we worked out, however in hindsight, we definitely should have been clearer about what the classes would be called and the structure they would be created in (in the case of inheritance), as there was some confusion regarding that. Additionally, since we didn't know too much about branching dialogue when we began the project, I would have planned better for how the event system was structured to better support that rather than relying on a method trigger and additional member variables to store changes. 

### Team Member Name 2
Put your individual check-in Devlog here.

### Jasmine Caicedo 
I was able to contribute a lot to the creation of my groups game, especially on the visual end of everything. I was also mainly involved with sound prompts and some assistance with fixing up some errors in the code. I followed the Proposal breakdown nearly perfectly, and really didn't veer off at all. It was extremely effective when organizing the "audioController" class specifically, as I could trace back where each class connects to each other and what each contain. This was very importaint when I was tracing back the player delegate and getting the event message prompts from that class to fit, like for example when the player was walking or not it would play a sound effect. When it comes to what I contributed in terms of scene building, The wall and floor game objects were created with plains that I snapped together to make fully streamlined rooms. All of these rooms have a collider component attached to them so that the player cant fall through and go into the void. I was also able to decorate the entire room and world, and attached some light fixtures in the house with some light components to give it a realistacally "homey" feel. Another thing I added was the Sister NPC. I imported her and added a capusle collider (so the player can't phase through her), and an animator component so that she can react differently depending on how the player interacts with her. I was also able to code a simple finite state for her using the meathods of "RunState" and "NPC" state in order to dictate her changing states for when she is just idly bobbing, waving you over, or talking to you as the player. The Animation controller in unity was also set up by me, and I had to edit the transition duration to almost zero to make the transition for the amimations states much faster than they normally would. 

## Final Submission
### Group Devlog
Put your group Devlog here.


### Team Member Name 1
Put your individual final Devlog here.
### Team Member Name 2
Put your individual final Devlog here.
### Team Member Name 3
Put your individual final Devlog here.

## Open-Source Assets
- [Car Parking Jam Low Poly Env by Furquan](https://assetstore.unity.com/packages/3d/environments/car-parking-jam-low-poly-env-266619) - Pyramid backround asset
- [Footsteps - Essentials by Nox_Sound](https://assetstore.unity.com/packages/audio/sound-fx/foley/footsteps-essentials-189879) - Player walking sounds
- [Simple Toon by Dmitry Chalovskiy](https://assetstore.unity.com/packages/vfx/shaders/simple-toon-185038) - Toon shaders for various items around the entire game (mainly inside of the home) 
- [Pandazole - Farm Ranch low poly Pack by Pandazole](https://assetstore.unity.com/packages/3d/props/pandazole-farm-ranch-low-poly-pack-206756) - Fences in the main scene neighbors house along with some giant veggies (carrot and beet)
- [3D Low Poly Enviroment Assets by MochiModels](https://assetstore.unity.com/packages/3d/environments/3d-low-poly-environment-assets-299354) - Ice asset in main scene
- [Bossa Nova (Fast) by Dee Yan Key]([https://freemusicarchive.org/search/?quicksearch=bossa+nova&search-genre=](https://freemusicarchive.org/music/Dee_Yan-Key/Latin_Dance/08--Dee_Yan-Key-Bossa_Nova__fast/)) - Background music for the main open world
- [ELECTRIC BUZZ SOUND EFFECT - FREE by Sound FX](https://www.youtube.com/watch?v=r9pyitlRYpg) - Fridge background noise
- [Ultimate Interior Furniture Pack (Low Poly) – Household & Kitchen Props by Fries and Seagull](https://assetstore.unity.com/packages/3d/props/interior/ultimate-interior-furniture-pack-low-poly-household-kitchen-prop-316897) - All props for inside of the home
- [30 Stylized Textures FREE by Billion Mucks](https://assetstore.unity.com/packages/2d/textures-materials/30-stylized-textures-free-246556) - Floor textures for the inside of the house
- [Low Poly Mini Village Free by Underhill Labz](https://assetstore.unity.com/packages/3d/environments/low-poly-mini-village-free-131677) - Neighbors Home in main scene 
- [Free SkyBox Extended Shader by BOXOPHOBIC](https://assetstore.unity.com/packages/vfx/shaders/free-skybox-extended-shader-107400) - Main scene skybox 
- [FREE Stylized PBR Textures Pack by Lumo-Art 3D](https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-pbr-textures-pack-111778) - Wall textures in the home, grass textures in the main scene
- [Low Poly Nature - FREE Vegetation by Elcanetay](https://assetstore.unity.com/packages/3d/vegetation/low-poly-nature-free-vegetation-134006) - Grass on the floor in main scene along with trees
- [RPG Essentials Sound Effects - FREE! by leohpaz](https://assetstore.unity.com/packages/audio/sound-fx/rpg-essentials-sound-effects-free-227708) - pickup noises for items that have yet to be used
- [Mixamo by Adobe](https://www.mixamo.com/#/) - Sister NPC model (Michelle), along with the idle, speaking, and waving animation

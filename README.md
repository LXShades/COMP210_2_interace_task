# AR/VR Project (1707981)
GitHub Repo: http://github.com/LXShades/comp210_2_interace_task  
Note: The commits by 'Newtoto' were from me using the Amber computer, and mistakenly not logging into my own account in TortoiseGit. Sorry for my error!  

## Zombie Tag
A game where you play as a crawling zombie on the ground, chasing down fleeing humans and tagging them so they become zombies too. The player moves by crawling, ideally reducing motion sickness by restricting camera movement to snappy pulls made by the user's arms.  

The goal of the game is to convert the entire roster of fleeing humans into mindless zombies joining you in the takeover of humanity.  

### Mechanics
* Using your hands, pull yourself forwards along the ground by touching the ground and pulling the trigger  
* Pull in humans with hands and eat them by pulling both hands together and moving towards your mouth  
* Grab the skateboard to move faster  
* **When using keyboard and mouse:** Use **left click** to grab the ground or humans, **scrollwheel** to change distance of your hand, and **right click** to turn the camera.  
* Stretch: Weights to make the arms heavier and user slower and more zombie-like  
* Stretch: Pick up rocks and knives, throw them towards humans  

### User stories
Trello: https://trello.com/b/2eY6zbbQ/zombietag  

There were the **original** pre-planned user stories. See Trello for the updated list.  
**Bolded** stories are highest priority in each category, unbolded stories are ideal but less priority than the next category.  

* As a zombie, I want an area to explore
 * - **Implement a basic city street with tall box buildings either side and a forward roadway**
 * - **Implement boundary to stop humans and zombies from diverging from this road**
* As a zombie, I want to have hands so I can move and grab stuff
 * - **Implement SteamVR hands** with predefined model or hand-shape made of cubes
* As a zombie I want to be able to pull myself along the ground with my hands so I can chase humans
 * - **Implement VR hand grip by pulling a trigger**
 * - **Implement ground detection/response to being gripped**
 * - **Implement head movement in response to pulling the gripped ground**
 * - Implement visual feedback to hand gripping on a surface
 * - Implement vibration feedback to hand gripping on a surface
* As a zombie I want humans that I can chase
 * - **Implement cubemans which run away at a slower speed than the zombie**
 * - **Make cubemans zig-zag from side to side of the road**
 * - Make humans run away more slowly if they are at a distance, so the player can reasonably catch them
* As a zombie, I want to grab these humans
 * - **Implement human grabbing, they just stick to your hand when you grip them**
* As a zombie, I want to bite these humans
 * - **Implement double human grabbing**
 * - **When the human reaches the mouth, make it change to green**
* As a zombie, I want bitten humans to join me
 * - **When the human is green, it slowly chases other humans**
* As a zombie, I want to know I'm victorious when all humans are mine
 * - Implement a human counter UI that sits in the sky, so you can see how many humans are left by looking up
 * - Implement a 'you win' block that sits ahead of the player
 * - Implement a 'restart' block which reloads the scene when clicked

## Market Research
## Existing VR games with camera movement
### Sprint Vector
Developed by Survios and released in 2017, Sprint Vector is a fast-paced VR platformer that essentially breaks all the rules of 'don't move the player or they will projectile vomit'. Rather, in this case the player _is_ basically the projectile.  

Movement is achieved by swinging your arms, probably creating the level of control essential to making the player feel slightly less dizzy.  

MetaCritic gave SprintVector an 84/100. Many reviewers cited its uses as an impromptu fitness program for your aching arms and well-adjusted, motion sickness-calibrated controls. It has been described as an 'adrenaline rush' [2] and has achieved Gold for sales on Steam [1].  

### Astro Bot
Released in 2018 by JapanStudio, Astro Bot is a critically-acclaimed VR platformer, scoring a 90 on MetaCritic [3]. It seemingly uses an on-rails camera, characterised as a robot effectively canonically putting the player into the game. Attacks of goo and bullets toward this robot character encourage the player to dodge with their body, adding a level of immersion.  

This game confirms that platformers are viable in VR, making the VR-ization of the frog game reasonable especially if it incorporates some directly player-oriented gameplay. The curious thing about Astro Bot is how it successfully implements a moving view when doing so is unrecommended. Actually, the main recommendation is that acceleration should be avoided, rather than constant movement. This is shown in Astro Bot gameplay videos where camera acceleration and deceleration is quick and snappy.  

## Similar games to ZombieTag
### Climbey
Released in 2016, Climbey is a VR game centred on a climbing mechanic, where players can climb using their hands. It has 'Very positive' reviews on Steam [5], although it has not gained much market attention, perhaps due to a lack of marketing and abstract style.  

The climbing interface is similar to Zombie Tag, but with more verticality. Zombie Tag's advantage is its more specific, zombie-based style. Zombie games are still fairly popular [6, 8, 9], having their own category on Steam [7] and as you are a zombie, there is more potential for zombie-themed tag gameplay. Furthermore, climbing could be introduced as a stretch goal in a future iteration to add to the mayhem. These qualities give Zombie Tag an edge over Climbey even though the core mechanics are similar.  

## Sources
[1] https://store.steampowered.com/sale/2018_so_far_top_vr_titles/ - Best selling games accessed 13/11/2018  
[2] https://www.eurogamer.net/articles/2018-02-20-sprint-vector-review - Accessed 13/11/2018  
[3] https://www.metacritic.com/game/playstation-4/astro-bot-rescue-mission - Accessed 13/11/2018  
[4] https://vr.arvilab.com/blog/combating-vr-sickness-debunking-myths-and-learning-what-really-works - AR/VR guidelines, accessed 13/11/2018  
[5] https://store.steampowered.com/app/520010/Climbey/ - Climbey, accessed 10/12/2018  
[6] https://www.gamesradar.com/uk/best-zombie-games/ - List of the best zombie games, accessed 10/12/2018  
[7] https://store.steampowered.com/tags/en/Zombies/ - Zombie category, accessed 10/12/2018  
[8] https://www.nerdmuch.com/games/11717/best-zombie-games/ - Best zombie games, accessed 10/12/2018  
[9] https://www.ranker.com/list/most-popular-zombie-games-today/ranker-games - Best zombie games, accessed 10/12/2018  

# Earlier proposals
Ideally, my project will include: 

* An explorable area despite the challenges of movement in VR
* Fast-paced non-static gameplay
* Achievable to create along with the frog game assignment, comp220 assignment and CPD

### Giant Tag
A game where you play as a giant in a big city. It's pretty simple. Smash everything! Destroy the world! Pick up planes and throw them at buildings or other planes, smash your fist down on skyscrapers, grab the ground and pull yourself along whilst crushing everything below you.  

#### Mechanics
* Using your hands, destroy everything.
* Also you can pull yourself along the ground in a similar way to the zombie game, or perhaps take the Sprint Vector approach where you swing your arms.

### Frog Game except you're Hanging on the Tree
A VR version of the Frog game we're making in the group. It's a platformer where you play as a frog and use your tongue for sick movements and stuff. Currently, the extent of the game is not strictly defined, but a level could be defined for the VR prototype. The level could include: Dodging cannonballs, riding an updraught up the centre of a tree trunk, breakable walls that the player can break, jumping super high and tongue-swinging targets. The focus of the level could be around gaining height so at the end the player can look down upon the world.  

The player's perspective is from the heights of trees around the area, giving you a bird's eye view of the main character. Player movement only occurs via the trees, which you could teleport or pull yourself to. Alternatively, based on Astro Bot's strategy, the camera could move in straight lines across the trees at a relatively fixed speed.  

#### Mechanics
* Flinging your hand out elastically, grabbing the tree and pulling yourself toward it  
* Using the trackpad to drive the character
* Tongue-swinging (somehow)
* Jumping and double-jumping for verticality.

## Challenges
The main challenge for this is achieving freedom of movement without causing motion sickness from VIMS, visually-induced motion sensitivity. This creates a disparity between the sense of movement in the inner ear (vestibular system) and the movement the player sees themself achieving.  

Some remedies to this could include:
* Increasing the user's control and ability to stop, perhaps by using the hands to dictate movement
* Reducing acceleration time (making movement 'snappier' rather than slow and steady)
* Tilting the user virtually: perhaps associating the user's 'down vector' with their direction of movement could compensate the lack of vestibular stimili, by letting the real-life gravity provide the force.
* * i.e. because you're tilting in game as you move to the side, you can still feel the force in real life, but it's just natural gravity. Of course, the force of gravity is static, which means it won't have the feeling of added force, but it would reduce the visual-vestibular disparity slightly.
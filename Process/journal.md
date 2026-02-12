# Week 1 - Make A Thing: The Tomb of Many Ends

## Initial Brainstorm
First, I knew that I wanted to use Twine because I had never used it before, but it seemed unique and fun. Then, I tried brainstorming a theme/idea/story to try to implement in Twine. I kept going back to theme's I had already tried before and didn't like any of them because I really wanted to try something new. I ended up using a topic randomizer website until I found something that inspired me. I saw the word Mummy and instantly thought of Indiana Jones and thought of like old tomb exploration and so that is what I went with. Knowing that the goal was not to make something perfect but simply to make something complete helped me commit to an idea and move forward. 

At first, I thought about simply adding an inventory mechanic. At the start of the game, you would select 2 or 3 items out of a handful of options. Some interactions would be successful if you have the right tool(s) and some would be not even available if you didn't select the item at the start. I found a [simple inventory guide](https://twinelab.net/simple-inventory/#/) online that was pretty easy to follow along to.

However, this felt really boring. An inventory is so basic and has been done so many times before. So I kept thinking. Since I am the type of player that wants to interact everything, I thought it could be fun to play around with that. I decided to add a "curiosity level" which increases based on what you interact with and what you do. Different curiosity levels allow for different endings and interactions. 

Once I had all these main concepts down, I started planning it out on a piece of paper.I am a visual person when I brainstorm, so it allowed me to piece together my story in a way that made sense. I was able to plan out different paths and endings in a clear way for it to feel somewhat logical and coherent.

## Core Concept 
The final concept became a short interactive game set inside a tomb that reacts to the player’s curiosity. The core mechanic is a curiosity variable that increases when the player chooses to investigate objects, touch things, or explore deeper into the tomb. Rather than using combat or an inventory system, the game relies on choice-based progression and changing tone. The tomb itself becomes an observing presence that “remembers” the player’s actions and responds accordingly through text and events.

![Screenshot of Twine storygraph of The Tomb of Many Ends](Media/MakeAThing_storygraph.png)

## Curiosity Level 
One of my main design goals was to make the experience somewhat counter-intuitive for certain players. As someone who typically wants to interact with everything in games, I found it interesting to design a system where curiosity is not always rewarded. High curiosity can lead to negative endings, such as being killed by the tomb or the snake, while more restrained play can result in safer outcomes. At the same time, high curiosity can also unlock a secret ending, where surviving the snake encounter leads to a hidden tunnel and a special reward. This allowed curiosity to feel risky rather than strictly good or bad.

I was able to create different endings and interactions based on "curiosity level" with lines like this:
 <<if $curiosity > 2 and $curiosity < 7>>
 [[...->Tomb1]]
 <</if>>

## Exploration and Learning 
Because this was my first time using Twine, much of the process involved learning how the tool works, especially how to use variables, conditionals, and passage structure in SugarCube. I found Twine to be surprisingly flexible and well suited for creating branching narratives and multiple endings with minimal mechanics. It allowed me to focus on writing, pacing, and player choice without needing complex systems. 

## Stupid Struggle 
As I was experimenting with different logic statements for the curiosity level, I kept going to playtest it and I kept seeing in grey <<if>> and <</if>>. I thought there was an error in what I was doing. I couldn't figure out what I was doing wrong for so long. I was honestly really frustrated and wanted to scrap the whole idea... then I tried the normal play button and everything was fine. I now know that those lines are visible specifically for playtesting and debugging, and it isn't an error... Whoops!

## Reflection
Overall, this project helped me become more comfortable working quickly, experimenting with unfamiliar tools, and accepting imperfection in early prototypes. Working within such a short timeframe forced me to prioritize finishing something playable rather than over-polishing individual elements. This helped reduce my usual tendency to get stuck refining ideas instead of moving forward.
Using Twine for the first time also changed how I thought about game structure. Because the tool is text-based and relatively simple, it encouraged me to think more carefully about player choice, pacing, and consequence rather than relying on complex mechanics. I found that even small systems, like a single curiosity variable, could meaningfully shape the player’s experience and lead to multiple outcomes.
Moving forward, I’m interested in continuing to experiment with simple mechanics that have layered narrative consequences, as well as expanding on some of the ideas introduced in this game, such as environmental storytelling and multiple endings. 

One way I thought about improving it would be to simply add music to create a slightly more immersive experience. When I was planning out the adventure, I had also thought of adding a trap room between the stone door and the final room. In this room, the player would have to use clue to make their way across trapped tiles that could help or end their playthrough. Due to time and the actual scope of this project, I decided not to add it for now.

Anyway, I am pretty proud of the end result and I think it is an interesting Make A Thing project.

# Week 2 - Exploration Prototype 1 

Due to some small technical issues during the class, we were just told to play around with Unity and to learn to be comfortable using it. 

Since I did not know where to start and I had never used Unity before, I decided to find a basic tutorial to follow. I found that Unity offered many tutorials with [Unity Learn](https://learn.unity.com/?signup=true). I decided to follow a basic tutorial, one of the first ones I saw. 

The tutorial explained what each window does, how to move around the interface, how to create objects, apply materials and effects, set up lighting, and make simple moving objects. Overall, it felt well-rounded and gave a good overview of the engine rather than focusing on one specific feature.

![Screenshot of Unity window with tutorial in progress](Media/week2_tutorial_screenshot.png)

I did not know what to expect but turns out it is similar to Unreal Engine in many ways. In cégep, I had a few classes where we learned how to use Unreal Engine so it felt familiar. This made the learning process feel less intimidating and helped me feel more confident navigating Unity. Even though I didn’t create a full prototype this week, this exploration helped me better understand how Unity is structured and how different elements work together inside the engine. Getting familiar with the interface, hierarchy, and basic workflows felt important before attempting anything more complex. It also made me realize how much time can be saved by using tutorials as a starting point rather than trying to figure everything out on my own.

Overall, this week felt more like laying groundwork than producing something finished. While it was less creative than the previous assignment, it helped me feel more prepared to use Unity in future weeks and made the idea of building something from scratch in the engine feel more achievable.


# Week 3 - Exploration Prototype 2

In this week's class, we started with some theory on types of prototype and prototype fidelity. 
We then looked at "Gotta-Catcha-Mall" and the different scripts that handle the collisions, movement and points. 
We then explored how to transform those same basic mechanics to make Pong.

The goal this week was to think of a question you wanted to answer 
and to create a low fidelity prototype to explore this question/idea. 
I started by brainstorming ideas that I might want to explore.

## Brainstorm
I was feeling super sick all week so this is not my best but I did try. At first, I wanted something simple enough to realistically complete, but still interesting from a design perspective. 
Since we were given a Pong-style base game, I thought it made sense to build off of that rather than starting something entirely new. I kept thinking about how I could slightly disrupt a very familiar game without making it overly complicated.

I was drawn to the idea of unpredictability. Pong is normally very stable — the ball behaves exactly how you expect it to, 
and over time players can almost enter a rhythm. I became curious about what would happen if that stability slowly disappeared.

This led me to my core question:

How does unpredictability affect a player’s sense of control?
Would the game feel more exciting? More stressful? Less skill-based?

From there, the idea of the “UnpredictaPawg” emerged.

## Core Idea

The concept was to create a ball that mutates slightly every time it collides with something. Instead of behaving like a perfectly consistent physics object, the ball would gradually become harder to predict.

After each bounce, the ball randomly changes:

- speed 
- size 
- color 
- audio pitch

The goal was not to completely break the game, but to slowly add chaos while keeping it playable and true to the core of the game.

Because Pong is such a recognizable and minimal game, even small changes become very noticeable. 
I liked the idea that a tiny system adjustment could completely shift the emotional experience of playing.

## Process

Technically, the prototype was fairly straightforward, but it still involved some experimentation. 
I modified the existing ball script so that a mutation function would trigger on collision. 
This function slightly adjusts the ball’s speed within a safe range, rescales it, and randomizes its color and sound pitch.

One thing I learned very quickly is that physics can behave in unexpected ways. 
My first attempt resulted in a painfully slow ball that eventually stopped moving altogether. 
After some debugging, I realized I had mixed force-based movement with direct velocity changes, which caused the physics 
system to fight itself a little.

Switching to velocity-based movement immediately made the game feel more responsive and much closer to the classic Pong experience. 
It was a good reminder that sometimes the simplest technical approach is the strongest one, especially for arcade-style mechanics.

I also added limits to the speed and scale so the mutations would stay within playable boundaries. 
Without those constraints, the prototype could easily become frustrating rather than experimental.

![Screenshot of mutate ball script modi](Media/unpredictapawg_mutateball.png)


## Learnings

What I found interesting is that even though the change itself is small, it noticeably alters how the game feels. 
I didn’t introduce new controls or complicated mechanics, but the experience became less predictable and boring.

This exploration reminded me that experimentation does not always mean adding more features. 
Sometimes modifying a very familiar system is enough to create a different experience. 
I tend to overthink / overcomplicate projects but due to the time limitations and my illness, it forced me to think more simply for once (I doubt this will last).

One success was keeping the scope under control. 
It would have been very easy to keep adding mutations, 
but I tried to stay focused on the original question instead of feature-creeping the prototype.

The biggest struggle was definitely the physics behavior early on. When the ball stopped moving, 
it was not immediately obvious why, and debugging physics is not always very visible. Once I identified the issue, though, 
the fix was simple and the prototype became much more stable.

Another small but important addition was the visual color change. It helps communicate to the player that something has shifted, 
making the mutations feel intentional rather than like a bug.o

## Reflection

Overall, this exploration helped me better understand how low-fidelity prototypes can answer very specific design questions. 
The project was not about making a polished game, but about observing how one variable could reshape player experience.

If I were to continue exploring this idea, I might experiment with gradual instability, where the mutations become more extreme the longer a rally lasts. 
This could create a clearer arc of tension within each round. I could also imagine giving players limited ways to influence 
the chaos, such as a temporary stabilizing paddle or a mechanic that resets the ball. 
That might introduce an interesting balance between control and disorder.

Overall, I am happy with how this prototype turned out. It feels like a strong example of 
how a very small system can meaningfully transform a classic game while still remaining simple to implement. 
More importantly, it helped me become more comfortable modifying existing mechanics instead of feeling 
like I always need to invent something entirely new.
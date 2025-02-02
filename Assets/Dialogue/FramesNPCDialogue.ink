EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
VAR NPCName = "Dashar"
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
<i>Huff huff</i> #speaker:{NPCName}
+[Why are you panting?]
    Well, what else but runnin' a marathon?#speaker:{NPCName}
+[Where is this?]
    We're in the mountains! Nice view, ey?#speaker:{NPCName}
- Wanna join me for another run?#speaker:{NPCName}
+[Sorry, I'm busy looking for the designated pickup point for spaceships.]
-Oh, well that's okay.#speaker:{NPCName}
-But it's a pretty far walk from here, I'd reckon...#speaker:{NPCName}
-Maybe you could at least squeeze in some warm-ups before you go?#speaker:{NPCName}
+[Warm-ups?]
-Yea! Runnin', jumpin', stretchin'... you name it.#speaker:{NPCName}
-Just a few'll do. They'll perk you right up!#speaker:{NPCName}
+[Uh... I'm not really a sporty person...]
    Doesn't matter whether you're a fitness guy!#speaker:{NPCName}
    Exercise helps you get into the flow of the action, as they say.#speaker:{NPCName}
    If ya ever wanna warm-up, I'll be right here.#speaker:{NPCName}
    ->DONE
+[Let's do it!]
    Nice!#speaker:{NPCName}
    For this exercise, all you need to do is follow my lead.#speaker:{NPCName}
     When I walk, you walk. When I jump, you jump!#speaker:{NPCName}
    Heh, pretty simple, right?#speaker:{NPCName}
    Well, let's get to it!#speaker:{NPCName}
        ~ StartMinigame()
        ->DONE
        
==AfterMinigame==
Wow, you're pretty good at this! #speaker:{NPCName}
Getting your body into action means entering a state of flow. #speaker:{NPCName}
It's really good for psyching yourself up! In fact, you feel like an action character that way!#speaker:{NPCName}
+[Action character?]
    Yep! Y'feel like you could throw a punch and destroy anything.#speaker:{NPCName}
    Anyway, ya said something about a spaceship pickup point?#speaker:{NPCName}
    I actually saw some spaceships flying overhead from that direction.#speaker:{NPCName}
    Just go down that hole 'n follow the path.#speaker:{NPCName}
     Careful, though! There are some flying enemies on the way.#speaker:{NPCName}
    Though with your athleticism, I'd say they won't a dent in you!#speaker:{NPCName}
    ++[Thank you. I'll be on my way.]
        You're welcome! Remember the exercises I taught ya!#speaker:{NPCName}
        ->DONE

            


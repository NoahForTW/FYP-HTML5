EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Huff huff #speaker:Dashar
+Why are you panting?
    Well, what else but runnin' a marathon?#speaker:Dashar
+Where is this?
    We're in the mountains! Nice view, ey?#speaker:Dashar
- Wanna join me for another run?#speaker:Dashar
+Sorry, I'm busy looking for the designated pickup point for spaceships.
-Oh, well that's okay.#speaker:Dashar
-But it's a pretty far walk from here, I'd reckon...#speaker:Dashar
-Maybe you could at least squeeze in some warm-ups before you go?#speaker:Dashar
+Warm-ups?
-Yea! Runnin', jumpin', stretchin'... you name it.#speaker:Dashar
-Just a few'll do. They'll perk you right up!#speaker:Dashar
+Uh... I'm not really a sporty person...
    Doesn't matter whether you're a fitness guy!#speaker:Dashar
    Exercise helps you get into the flow of the action, as they say.#speaker:Dashar
    If ya ever wanna warm-up, I'll be right here.#speaker:Dashar
    ->DONE
+Let's do it!
    Nice!#speaker:Dashar
    For this exercise, all you need to do is follow my lead.#speaker:Dashar
     When I walk, you walk. When I jump, you jump!#speaker:Dashar
    Heh, pretty simple, right?#speaker:Dashar
    Well, let's get to it!#speaker:Dashar
        ~ StartMinigame()
        ->DONE
        
==AfterMinigame==
Wow, you're pretty good at this! #speaker:Dashar
Getting your body into action means entering a state of flow. #speaker:Dashar
It's really good for psyching yourself up! In fact, you feel like an action character that way!#speaker:Dashar
+Action character?
    Yep! Y'feel like you could throw a punch and destroy anything.#speaker:Dashar
    Anyway, ya said something about a spaceship pickup point?#speaker:Dashar
    I actually saw some spaceships flying overhead from that direction.#speaker:Dashar
    Just go down that hole 'n follow the path.#speaker:Dashar
     Careful, though! There are some flying enemies on the way.#speaker:Dashar
    Though with your athleticism, I'd say they won't a dent in you!#speaker:Dashar
    ++Thank you. I'll be on my way.
        You're welcome! Remember the exercises I taught ya!#speaker:Dashar
        ->DONE

            


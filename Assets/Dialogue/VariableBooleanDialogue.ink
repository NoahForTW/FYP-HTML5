EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
VAR IsMinigameFailed = false
VAR NPCName = "Mechael"
{ IsMinigameFailed : -> FailedMinigame | { IsMinigameCompleted: -> AfterMinigame | -> BeforeMinigame }}

==FailedMinigame==
Woah! Those gears popped right outta their slots!#speaker:{NPCName}
Seems this ain't our day, ey?#speaker:{NPCName}
But, there must be some sorta way for those gears to slot themselves in!#speaker:{NPCName}
Maybe we needa rethink the positions... or maybe tha gears 'emselves...#speaker:{NPCName}
Here, I'll pass ya a gear.#speaker:{NPCName}
+[I'll take it from here! #startminigame]
    ~StartMinigame()
    ->DONE
+[Uh... let me think first.]
    No problem! All engineers need time to architect a solution.#speaker:{NPCName}
    ->DONE

==BeforeMinigame==
Well, this is sure do be a conundrum.#speaker:{NPCName}
+ [What is?]
-Woah, matey! Ya scared the living daylights outta me!#speaker:{NPCName}
Welp, anyway, it's not something to worry about too much.#speaker:{NPCName}
Or maybe it is a pretty big thing to worry about.#speaker:{NPCName}
Y'see, I'm in charge of operating this room 'ere.#speaker:{NPCName}
This handles the opening and closing of all the doors on the planet.#speaker:{NPCName}
As well as any teleportation doohickies or what-have-you.#speaker:{NPCName}
But uh, the machine's started to... malfunction recently.#speaker:{NPCName}
+[Public transportation on this planet sure is something.]
    I also checked the security cameras for any mischief, an' it all seems to be the fault of the cybergoblin.#speaker:{NPCName}
    Anyway, I'm still busy fixin' it, so could ya come back later when I'm done?#speaker:{NPCName}
    ++[Maybe if I lent a hand, we could find the solution faster. #startminigame]
        Oh! That sure be a good point. Could you check around the room for anything?#speaker:{NPCName}
        In the meantime, I'll be wrenchin' away ova' here.#speaker:{NPCName}
        ~ StartMinigame()
        ->DONE
    ++[Welp, good luck.]
        Thanks, matey. S'gonna take a while, I reckon...#speaker:{NPCName}
        ->DONE
        
==AfterMinigame==
Woah, ya fixed it!#speaker:{NPCName}
You're a real prodigy, y'know? Ever considered becoming an engineer?#speaker:{NPCName}
+[Sorry, but I'm set on becoming a games creator.]
    I seez. Games also sounds pretty fun!#speaker:{NPCName}
    ++ [Anyway, do you know where is the designated pickup point for spaceships?]
+[Hmm...]
    Hah, I'll let you ponder that.#speaker:{NPCName}
    ++ A[nyway, do you know where is the designated pickup point for spaceships?]
-Oh, I do happen to know about that!#speaker:{NPCName}
 I know where all the doors and switches on this planet lead, after all.#speaker:{NPCName}
Just pass through here an' flip the switch on the other side.#speaker:{NPCName}
+[I see. Thank you, I'll be on my way.]
    Yer welcome! Thanks a whole bunch again!#speaker:{NPCName}
    Oh, also, we have an open engineer position here...#speaker:{NPCName}
    ->DONE

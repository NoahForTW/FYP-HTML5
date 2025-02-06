EXTERNAL StartMinigame()
EXTERNAL ChangeAvatar()
VAR IsMinigameCompleted = false
VAR IsMinigameFailed = false

VAR NPCName = "Praxylis"
{ IsMinigameFailed : -> FailedMinigame | { IsMinigameCompleted: -> AfterMinigame | -> BeforeMinigame }}

==FailedMinigame==
Hmph! Not straight at all!#speaker:{NPCName}
You're really quite dim at measuring things.#speaker:{NPCName}
Take a good looksie at my ruler, and then try again!#speaker:{NPCName}
+[Ugh... I'll try again. #startminigame]
    ~StartMinigame()
    ->DONE
+[Go away, old fart!]
    GO away? How about no!#speaker:{NPCName}
    I'll stay here until you measure the line to the boxes proper!#speaker:{NPCName}
    ->DONE


==BeforeMinigame==
Hrm... #speaker:{NPCName}
+[What's up?]
-Oi! Do not disturb!#speaker:{NPCName}
-I am busy measuring the approximate length of these lines to these boxes...#speaker:{NPCName}
-...Gah, no! I drew the line just a half centimeter too short again!#speaker:{NPCName}
-YOU interrupted me, right? So this is your fault!#speaker:{NPCName}
+[What?!]
+[Yes, I'm the mastermind!]
-Erase, erase...#speaker:{NPCName} 
-Here, chap! You go draw those lines instead for me!#speaker:{NPCName} 
+[No way!]
    Hmph, fine! *Grumble grumble*#speaker:{NPCName} 
    ->DONE
+[Uh, sure?]
    Very good.#speaker:{NPCName}
    In addition, be sure to match the lines to the correct boxes.#speaker:{NPCName}
     Otherwise, your line still won't count!#speaker:{NPCName}
    I care a lot about precision and correctness, you see..#speaker:{NPCName}
        ~ StartMinigame()
        ->DONE
        
==AfterMinigame==
...Hmph. Your lines are actually quite fine. And accurate to boot... #speaker:{NPCName}
Very well then! Seems I underestimated you. #speaker:{NPCName}
->LoopOption
-Heh, welcome. And remember, if you require any knowledge, I am right here to answer!#speaker:{NPCName}
            ->DONE
            
==LoopOption==
You may ask me, the brilliant Professor (NPC), anything you wish.#speaker:{NPCName}
+ [How do I reach the designated pickup point for spaceships?]
    Hm... to reach there, you will need to go over those platforms over there... #speaker:{NPCName}
    But be careful, youngster! One wrong move and you could perish. #speaker:{NPCName}
    ++ [Thank you, I will keep it in mind.]
        ->DONE
+[I have nothing to ask, you old weezer.]
    What?! Surely someone as young as yourself would have something to ask! #speaker:{NPCName}
    ->LoopOption

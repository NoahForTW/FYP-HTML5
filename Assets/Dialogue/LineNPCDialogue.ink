EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Hrm... #speaker:Praxylis
+What's up?
-Oi! Do not disturb!#speaker:Praxylis
-I am busy measuring the approximate length of these lines to these boxes...#speaker:Praxylis
-...Gah, no! I drew the line just a half centimeter too short again!#speaker:Praxylis
-YOU interrupted me, right? So this is your fault!#speaker:Praxylis
+What?!
+Yes, I'm the mastermind!
-Erase, erase...#speaker:Praxylis 
-Here, chap! You go draw those lines instead for me!#speaker:Praxylis 
+No way!
    Hmph, fine! *Grumble grumble*#speaker:Praxylis 
    ->DONE
+Uh, sure?
    Very good.#speaker:Praxylis
    In addition, be sure to match the lines to the correct boxes.#speaker:Praxylis
     Otherwise, your line still won't count!#speaker:Praxylis
    I care a lot about precision and correctness, you see..#speaker:Praxylis
        ~ StartMinigame()
        ->DONE
        
==AfterMinigame==
...Hmph. Your lines are actually quite fine. And accurate to boot... #speaker:Praxylis
Very well then! Seems I underestimated you. #speaker:Praxylis
->LoopOption
-Heh, welcome. And remember, if you require any knowledge, I am right here to answer!#speaker:Praxylis
            ->DONE
            
==LoopOption==
You may ask me, the brilliant Professor (NPC), anything you wish.#speaker:Praxylis
+ How do I reach the designated pickup point for spaceships?
    Hm... to reach there, you will need to go over those platforms over there... #speaker:Praxylis
    But be careful, youngster! One wrong move and you could perish. #speaker:Praxylis
    ++ Thank you, I will keep it in mind.
        ->DONE
+I have nothing to ask, you old weezer.
    What?! Surely someone as young as yourself would have something to ask! #speaker:Praxylis
    ->LoopOption

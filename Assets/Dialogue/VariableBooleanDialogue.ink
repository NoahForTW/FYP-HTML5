EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
<i>sighs</i>
+ Are you alright?
    It is difficult trying to figure out which gear belongs to this puzzle...
    ++ I would not mind helping you.
        Eh? 
        You have my gratitude then.
        ~ StartMinigame()
        ->DONE
    ++ Get good.
        Okay, thats mean. 
        I'll figure this out on my own then.
        ->DONE
        
==AfterMinigame==
...Hmph. Your lines are actually quite fine. And accurate to boot... #speaker:Praxylis
Very well then! Seems I underestimated you. #speaker:Praxylis
-Heh, welcome. And remember, if you require any knowledge, I am right here to answer!#speaker:Praxylis
            ->DONE

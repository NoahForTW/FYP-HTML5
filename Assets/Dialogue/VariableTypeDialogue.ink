EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Hey!
Could you help me solve some questions?
+ Why should I help you?
    I need to solve these in order to activate these platforms to get to the other side.
    Would you care to help me? 
    ++ Sure, I need to get to the other side too.
        Aw sweet! Thank you!
        ~ StartMinigame()
        ->DONE
    ++ No thanks.
        Aw shucks, I'll find someone else to ask then.
        ->DONE
        
==AfterMinigame==
...Hmph. Your lines are actually quite fine. And accurate to boot... #speaker:Praxylis
Very well then! Seems I underestimated you. #speaker:Praxylis
-Heh, welcome. And remember, if you require any knowledge, I am right here to answer!#speaker:Praxylis
            ->DONE
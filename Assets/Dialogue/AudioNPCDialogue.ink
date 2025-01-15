EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Oh no! #speaker:Sonicus
+What's wrong?
    All my audio tapes are missing their sticker tags...#speaker:Sonicus  
    ++Do you need help?
        Oh! I'll appreciate it.#speaker:Sonicus
        ~ StartMinigame()
        ->DONE
    ++Good luck on that.
        Oh... Thank you?#speaker:Sonicus
        ->DONE
        
==AfterMinigame==
Thanks for your help! #speaker:Sonicus
+Do you know where is the designated pick-up point for ships on this planet?
    Hm, I'm not sure about the area you're talking about. #speaker:Sonicus
    You can walk through this broken PC & ask the next alien about it.#speaker:Sonicus
    ->DONE
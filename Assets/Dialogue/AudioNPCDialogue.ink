EXTERNAL StartMinigame()
VAR isMinigameDone = false
{isMinigameDone: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Oh no! #speaker:Sonicus
+What's wrong?#speaker:You
    All my audio tapes are missing their sticker tags...#speaker:Sonicus  
    ++Do you need help?#speaker:You
        Oh! I'll appreciate it.#speaker:Sonicus
        ~ StartMinigame()
        ->DONE
    ++Good luck on that.#speaker:You
        Oh... Thank you?#speaker:Sonicus
        ->DONE
        
==AfterMinigame==
Thanks for your help! #speaker:Sonicus
+Do you know where is the destinated pick-up point for ship on this planet?
    Hm, I am unsure where is the area you are refering to 
    You can walk through this broken PC & ask the next alien about it.
    ->DONE
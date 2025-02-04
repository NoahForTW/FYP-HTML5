EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
VAR IsMinigameFailed = false
VAR NPCName = "Sonicus"
{IsMinigameFailed : ->FailedMinigame|}
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==FailedMinigame==
    
->DONE

==BeforeMinigame==
Listen to some sick tunes here! #speaker:{NPCName}
+[Who are you?]
    I'm {NPCName}! I do DJ sets on the streets!#speaker:{NPCName}
+[What are you doing?]
    I'm promoting some of my DJ music!#speaker:{NPCName}
-Here, let me play you some tunes...#speaker:{NPCName}
-...Oh, huh?#speaker:{NPCName}
+[What's wrong?]
    Uh, weird. The label on this CD is different from the actual music...#speaker:{NPCName}
    Grah! Did the cybergoblin do this again?#speaker:{NPCName}
    ++[Cybergoblin?]
-Oh, that's a creature that likes to corrupt and glitch out our devices. So annoying!.#speaker:{NPCName}
-Sorry, could you help me place labels on the correct music?#speaker:{NPCName}
+[Leave it to me!]#StartMinigame
    Ah, thank you! You're a lifesaver.#speaker:{NPCName} 
    ~ StartMinigame()
        ->DONE
+[Uh, I'm kinda busy at the moment.]
        Oh, no worries. But it'll take a while for me to label them...#speaker:{NPCName}
        ->DONE
        
==AfterMinigame==
Thank you! Now they're labelled properly. #speaker:{NPCName}
In the meantime, how do you find the music? Cool, right?#speaker:{NPCName}
Enjoy it for as long as you'd like!#speaker:{NPCName}
+ [Thank you, but I have to hurry somewhere.]
    Oh? Where would that be?#speaker:{NPCName}
    ++[Do you know where is the designated pickup point for spaceships?]
        Ah, I think it's past this TV here. Just walk right through!#speaker:{NPCName}
        My music is being blasted all over the planet, so you can enjoy it while walking! #speaker:{NPCName}
        +++[Thank you, and goodbye.]
            ->DONE
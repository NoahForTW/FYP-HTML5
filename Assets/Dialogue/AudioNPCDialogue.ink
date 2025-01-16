EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Listen to some sick tunes here! #speaker:Sonicus
+Who are you?
    I'm Sonicus! I do DJ sets on the streets!#speaker:Sonicus
+What are you doing?
    I'm promoting some of my DJ music!#speaker:Sonicus  
-Here, let me play you some tunes...#speaker:Sonicus
-...Oh, huh?#speaker:Sonicus
+What's wrong?
    Uh, weird. The label on this CD is different from the actual music...#speaker:Sonicus
    Grah! Did the cybergoblin do this again?#speaker:Sonicus
    ++Cybergoblin?
-Oh, that's a creature that likes to corrupt and glitch out our devices. So annoying!.#speaker:Sonicus
-Sorry, could you help me place labels on the correct music?#speaker:Sonicus
+Leave it to me!
    Ah, thank you! You're a lifesaver.#speaker:Sonicus
    ~ StartMinigame()
        ->DONE
+Uh, I'm kinda busy at the moment.
        Oh, no worries. But it'll take a while for me to label them...#speaker:Sonicus
        ->DONE
        
==AfterMinigame==
Thank you! Now they're labelled properly. #speaker:Sonicus
In the meantime, how do you find the music? Cool, right?#speaker:Sonicus
Enjoy it for as long as you'd like!#speaker:Sonicus
+ Thank you, but I have to hurry somewhere.
    Oh? Where would that be?#speaker:Sonicus
    ++Do you know where is the designated pickup point for spaceships?
        Ah, I think it's past this TV here. Just walk right through!#speaker:Sonicus
        My music is being blasted all over the planet, so you can enjoy it while walking! #speaker:Sonicus
        +++Thank you, and goodbye.
            ->DONE
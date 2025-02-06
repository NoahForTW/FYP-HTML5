EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
VAR IsMinigameFailed = false
VAR NPCName = "Pain-Ter"
{ IsMinigameFailed : -> FailedMinigame | { IsMinigameCompleted: -> AfterMinigame | -> BeforeMinigame }}

==FailedMinigame==
Hm... Did I overestimate your artistic eye...?#speaker:{NPCName}
No, no! This won't do.#speaker:{NPCName}
I believe you can do better than this.#speaker:{NPCName}
Prove me wrong!#speaker:{NPCName}
+[Yessir! #startminigame]
    ~StartMinigame()
    ->DONE
+[Nah, I don't think I will.]
So you will give up that easily?#speaker:{NPCName}
Then that is really the sign of a failing artist.#speaker:{NPCName}
How pitiful...#speaker:{NPCName}  
    ->DONE

==BeforeMinigame==
Yes, yes... this sculpture... magnifique! #speaker:{NPCName}
+[What are you doing?]
    I am practising my colour magic!#speaker:{NPCName}
    And I am using it to paint my beloved sculptures.#speaker:{NPCName}
    ++[That's an interesting brush you have there.]
+[What is this place?]
    This is my magic workshop! Where art meets magic!#speaker:{NPCName}
    I am currently colouring some sculptures with colour magic.#speaker:{NPCName}
    ++[That's an interesting brush you have there.]
-Precisely! This brush-shaped wand allows me to cast colours on any area I so choose!#speaker:{NPCName}
Though, recently there has been a slight problem...#speaker:{NPCName}
A fellow by the name of the Cybergoblin has been repainting all my sculptures to a boring gray again!#speaker:{NPCName}
Ah, woe is me! Would a fellow like you be able to help me?#speaker:{NPCName}
All you need to do is repaint the sculpture based on the reference I give you.#speaker:{NPCName}
+[Leave this to me! #startminigame]
    You have my eternal gratitude!#speaker:{NPCName}
    ~ StartMinigame()
        ->DONE
+[Sorry, I'm a bit busy at the moment.]
        <i>Sniff</i> Y-you weally won't...?#speaker:{NPCName}
        <i>Sniff</i> Alright... But, if you ever feel so inclined... I will be waiting <i>sniff</i>
        ->DONE
        
==AfterMinigame==
Ah, yes! My sculpture... has regained its vibrant hues! #speaker:{NPCName}
Oh, what could I ever do to repay a fellow like yourself?#speaker:{NPCName}
+ [Can I know where the designated pickup point for spaceships is?]
    Ah! You just have to go past here and flip the lever on the other side.#speaker:{NPCName}
    Are you in a hurry anywhere, perchance?#speaker:{NPCName}
    ++[I need to get to an art school.]
        An art school? Why, magnifique!#speaker:{NPCName}
        I hope you learn lots of artistic techniques there!#speaker:{NPCName}
        +++[Thank you, and goodbye.]
    ++[I need to get to a game development school.]
        Ooh, game development? Fanciful stuff! #speaker:{NPCName}
        Game development is a lot like magic, I'd say. Lots of things you can do with tech! #speaker:{NPCName}
        I hope you learn lots of game development techniques there! #speaker:{NPCName}
        +++[Thank you, and goodbye.]
- So long! And good luck on whatever endeavours you may have!
->DONE
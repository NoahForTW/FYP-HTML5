EXTERNAL StartMinigame()
VAR IsMobilePlatform = false    
VAR NPCName = "Shiftix"
Hm? Oh, hi there!#speaker:{NPCName}
+ Hi... Whats is that thing behind you?
-This old thing? It's a device that allows users to <color=yellow>teleport</color> to another side of this planet.#speaker:{NPCName}
+ Does it lead me to a spaceship pickup point?
    Yep! But you need to find these along the way, which would lead you right to it.#speaker:{NPCName}
    ->LeverGuide


== LeverGuide==
{IsMobilePlatform: ->MobileLeverGuide |->PCLeverGuide }
->END


==PCLeverGuide==
To interact with it, simply use your <color=yellow>interaction</color> key #speaker:{NPCName}
You don't remember your interaction key? #speaker:{NPCName}
It's the <color=yellow>'F'</color> key, dummy.#speaker:{NPCName}
->Questioning
        
==MobileLeverGuide==
To interact with it, simply use your <color=yellow>interaction</color> button #speaker:{NPCName}
You don't remember your interaction button? #speaker:{NPCName}
It's the <color=yellow>yellow</color> button, dummy.#speaker:{NPCName}
->Questioning

==Questioning==
Do you need me to repeat?#speaker:{NPCName}
+Yes please.
    ->LeverGuide
+Nah, I've got it
    Nice, make sure you remember what I've said to you.#speaker:{NPCName}
    ->DONE
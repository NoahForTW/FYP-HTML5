EXTERNAL StartMinigame()
VAR IsMobilePlatform = false
VAR NPCName = "Axisar"
Hi! Do you need a guide on how to control your character?#speaker:{NPCName}
+ [Yes, I need a guide]
    ->ControlsGuide
    ->DONE
+ [No thank you]
    ->DONE


== ControlsGuide==
{IsMobilePlatform: ->MobileControlsGuide |->PCControlsGuide }
->END


==PCControlsGuide==
Hold on either <color=yellow>'A' or 'D'</color>  keys to walk left or right respectively  #speaker:{NPCName}
Press <color=yellow>'Space'</color> key to jump.#speaker:{NPCName}
To interact, press on the <color=yellow>'F'</color> key #speaker:{NPCName}
->Questioning
        
==MobileControlsGuide==
Hold on either the <color=yellow>left or right buttons</color> to walk left and right respectively  #speaker:{NPCName}
Press the <color=yellow>up button</color> to jump.#speaker:{NPCName}
To interact, press on the <color=yellow>yellow button</color> #speaker:{NPCName}
->Questioning

==Questioning==
Do you need me to repeat?#speaker:{NPCName}
+[Yes please.]
    ->ControlsGuide
+[Nah, I've got it]
    Oh okay! Make sure you remember these tips!#speaker:{NPCName}
    ->DONE
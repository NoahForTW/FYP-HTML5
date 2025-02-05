EXTERNAL StartMinigame()
VAR IsMinigameCompleted = false
VAR IsMinigameFailed = false
VAR NPCName = "Yarnok"
{ IsMinigameFailed : -> FailedMinigame | { IsMinigameCompleted: -> AfterMinigame | -> BeforeMinigame }}

==FailedMinigame==
Ack! The bridge snapped apart!#speaker:{NPCName}
The line you laid out was too wobbly, I suppose...#speaker:{NPCName}
The line you laid out was too wobbly, I suppose...#speaker:{NPCName}
Want to try that again?#speaker:{NPCName}
+ [Yes! #startminigame]
    ~StartMinigame()
    ->DONE
+ [I'll pass for now... My hands are shaky.]
    That's no problem. Just tell me when you're ready and steady to go.#speaker:{NPCName}
    ->DONE


==BeforeMinigame==
Hrm... #speaker:{NPCName}
+[What are you up to?]
    Figuring out how to fix up a bridge...#speaker:{NPCName}
    ++ [And you plan to use that yarn ball to...?]
+[What is this?]
    Ah, just a big old gap over here.#speaker:{NPCName}
    ++ [And you plan to use that yarn ball to...?]
-Oh, this isn't just any ordinary yarn ball, no no.#speaker:{NPCName}
In fact, all I need is a single thread to make a workable bridge!#speaker:{NPCName}
+[Oh, cool.]
    Right? Really practical.#speaker:{NPCName}
+[What sorcery is this?!]
    Sorcery? This is just alien tech, as far as I'm concerned.#speaker:{NPCName}
-Though, there are some caveats to using this yarn as an approach.#speaker:{NPCName}
You need to lay out the yarn really straight. Any crookedness, and the yarn just falls apart.#speaker:{NPCName}
It's fragile when it's rolled up all over, but with enough precision, it becomes sturdy as wood.#speaker:{NPCName}
Wanna give it a try? I can tell you're pretty interested.#speaker:{NPCName}
+[Yes, let me try! #startminigame]
    Here you go. Remember, lay it out straight!#speaker:{NPCName}
    ~ StartMinigame()
    ->DONE
+[Uh... it looks kinda freaky...]
    Hrmm? Well, as long as it's functional...#speaker:{NPCName}
    Just let me know if you wanna try it.#speaker:{NPCName}
    ->DONE

        
==AfterMinigame==
Yes, see? It became a bridge!#speaker:{NPCName}
Just remember, when crafting something, a practised hand leads to the best results.#speaker:{NPCName}
Anyway, you seem like you wanna cross this bridge real bad.#speaker:{NPCName}
Do you have anywhere you need to be right now?#speaker:{NPCName}
+[How do I get to the designated pickup point for spaceships?]
    Oh! Well, it's just straight on past here then.#speaker:{NPCName}
    There's some nasty enemies and platforms on the other side though, so...#speaker:{NPCName}
    Beware!#speaker:{NPCName}
    ++[Thank you, and goodbye.]
        Goodbye! Remember, stay on the straight and steady!#speaker:{NPCName}
        ->DONE

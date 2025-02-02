EXTERNAL StartMinigame()
EXTERNAL ChangeAvatar()
VAR IsMinigameCompleted = false
VAR NPCName = "Codexor"
{IsMinigameCompleted: ->AfterMinigame|->BeforeMinigame }

==BeforeMinigame==
Hm? #speaker:{NPCName}
Oh, my! #speaker:{NPCName}
+ [Who are you?]
    I am {NPCName}, a professor at this school.#speaker:{NPCName}
+ [Where am I?]
    You are in one of the practical labs at this school. #speaker:{NPCName}
- You came at quite a bad timing... I am doing a practical demonstration right now.#speaker:{NPCName}
+ [Of what?]
-Of, uh... Highly reactive explosives.#speaker:{NPCName}
+ [What?!]
- Well, since you have only just arrived and do not know the safety protocols, I will halt the demonstration for now.#speaker:{NPCName}
Allow me to activate the stopping mechanisms...#speaker:{NPCName}
...Erm, huh?#speaker:{NPCName}
Why won't it work?!#speaker:{NPCName}
Urgh, apologies, let me at least try to activate the emergency exit.#speaker:{NPCName}
+ [Oh no, oh no, am I gonna die?]
    Apologies, please give me a bit more time...#speaker:{NPCName}
    ++[! There's some kind of panel here to input some passcodes.]
        ~StartMinigame()
        ->DONE
+ [I can't just sit around and do nothing! I should try to help.]
    ...?! What are you doing?!#speaker:{NPCName}
    ->DONE

==AfterMinigame==
.The mechanism... stopped? #speaker:{NPCName}
Oh, right! My goodness! I had to enter the passcodes before I could activate the safety mechanisms!#speaker:{NPCName}
Hmph, good job...#speaker:{NPCName}
~ChangeAvatar()
...You little maggot.
+ [Who are you?!]
-I am the cybergoblin. I wreak havoc on this planet for fun.
I have been observing you for some time, and found your problem-solving to be interesting...
So I decided to set up a little test for you here.
But as per usual, you passed it with aplomb, you persistent screwhead.
So, allow me to applaud you on that.
+ [Ugh, you're annoying.]
-Yeah, I get that a lot.
Well, the pickup point is right after this place. Go on ahead.
You're going to an actual school from there, right?
Well, don't tell anyone else I said this, but...
...Good luck. I think you'll do pretty darn well there.
+[Wow, uh... thanks.]
    (hums)
    ~ChangeAvatar()
    ->DONE
+[Go screw yourself.]
    You too!
    ~ChangeAvatar()
    ->DONE

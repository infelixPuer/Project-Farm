Greetings newcomer! Nice to meet you!

* Hello! Nice to meet you too!
    -> answer_confident
* I don't have time for this. Bye!
    -> answer_bye
    
== answer_bye ==
Farewell then.
-> END

== answer_confident ==
I'll hope, you will like this place!
How are you doing?

* Fine, thanks! And you?
    -> good_mood
* Not so well..
    -> bad_mood

== good_mood ==
Me too!

Do you like our village?

* Yeah, it's great!
    -> good_village
    
* Meh, not so much.
    -> bad_village

== bad_mood ==
Oh, I'm sorry.
-> END

== good_village ==
Thank you!
We've all been working really hard to make this place nice!
Well, good day for you!
-> END

== bad_village ==
Wow, now I'm upset :(
-> END
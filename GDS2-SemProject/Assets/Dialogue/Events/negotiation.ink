INCLUDE globals.ink

VAR knot_name = -> negotiation

-> knot_name

=== negotiation ===

~ morale = 0
~ gold = 0

You and Lieutenant Edwards are approached by a foreign messenger. He mentions that his Deputy is asking for some assistance in exchange for payment. #speaker:Lieutenant Edwards #portrait:LieutenantEdwards #background: clearing

Lieutenant Edwards thinks this could be a good opportunity to gather some resources and take a rest.

What do you do? #0:gold:-200
+ [Agree to help and send reinforcements (Gold - 200, Morale + 150]
    You agree to send some reinforcements in exchange for payment. 
    ~ morale += 150
    ~ gold -= 200
    -> END
+ [Decline to help.]
    You turn down the offer for help and continue on your way. -> END
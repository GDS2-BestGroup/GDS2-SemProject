INCLUDE globals.ink

VAR knot_name = -> negotiation

-> knot_name

=== negotiation ===

~ morale = 0

You and Lieutenant Edwards are approached by a foreign messenger. He mentions that his Deputy is asking for some assistance in exchange for payment. #speaker:Lieutenant Edwards #portrait:LieutenantEdwards #background: clearing

Lieutenant Edwards thinks this could be a good opportunity to gather some resources and take a rest.

What do you do?
+ [Agree to help and send reinforcements (Gold + 300, Morale + 50, Troop Damage - 5%)]
    You agree to send some reinforcements in exchange for payment. 
    ~ morale += 50
    -> END
+ [Decline to help.]
    You turn down the offer for help and continue on your way. -> END
+ [Avoid them and do not attack (Morale + 150)]
    You order your men to stand down. 
    ~ morale += 150
    -> END
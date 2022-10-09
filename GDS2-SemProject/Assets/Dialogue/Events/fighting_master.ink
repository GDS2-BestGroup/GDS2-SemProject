INCLUDE globals.ink

-> fighting_master

=== fighting_master ===

~ morale = 0
~ gold = 0

A fighting master approaches you and Lieutenant Edwards with an offer to train your units in hand-to-hand combat for a fee. #background:castle_ruins_clearing #speaker: Lieutenant Edwards #portrait:LieutenantEdwards

What do you do?
+ [Pay the fighting master to train your troops (Gold - 200, Attack + 5%)]
    You accept his offer and inform your troops of the training plan. 
    ~ gold -= 200
    -> END
+ [Refuse the offer.]
    You politely refuse and continue on your way. -> END
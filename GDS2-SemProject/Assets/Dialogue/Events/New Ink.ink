INCLUDE globals.ink

~ morale = 0
~ gold = 0

A fighting master approaches you and Lieutenant Edwards with an offer to train your units in hand-to-hand combat for a fee. #background:castle_ruins_clearing #speaker:Lieutenant Edwards #portrait:LieutenantEdwards
What do you do? #0:gold:-200
+ [Pay the fighting master to train your troops (Gold - 200, Attack +5%)]
    You accept his offer and inform your troops of the training plan.
    ~ gold -= 200
+ [Refuse the offer.]
    You politely refuse and continue on your way.
INCLUDE globals.ink

VAR knot_name = -> enemy_camp

-> knot_name

=== enemy_camp ===

~ morale = 0
~ gold = 0

You are approached by your lieutenant who informs you of a nearby enemy camp. He advises that attacking them at night will be the best option however you understand that this could lead to soldier morale lowering. #speaker:Lieutenant Edwards #portrait:LieutenantEdwards #background:castle_ruins_clearing

What do you do? #0:morale:-600
+ [Attack them in the night (Troop Damage +10%, Morale - 150)]
    You order an attack during the night. 
    ~ morale -= 600
    -> END
+ [Attack during the day]
    You order an attack during the day. -> END
+ [Avoid them and do not attack (Morale + 150)]
    You order your men to stand down. 
    ~ morale += 150
    -> END
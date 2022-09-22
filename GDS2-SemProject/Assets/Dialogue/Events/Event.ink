INCLUDE globals.ink

VAR knot_name = -> enemy_camp

-> knot_name

=== enemy_camp ===

You are approached by your lieutenant who informs you of a nearby enemy camp. He advises that attacking them at night will be the best option however you understand that this could lead to soldier morale lowering. #speaker:Lieutenant Edwards #portrait:LieutenantEdwards

What do you do?
+ [Attack them in the night (Troop Damage +10%, Morale - 150)]
    ~ morale -= 150
    You order an attack during the night. -> END
+ [Attack during the day]
    You order an attack during the day. -> END
// + [Avoid them and do not attack (Morale + 150)]
//     ~ morale -= 150
//     You order your men to stand down. -> END
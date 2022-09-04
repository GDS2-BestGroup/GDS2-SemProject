INCLUDE globals.ink

VAR knot_name = -> enemy_camp

-> knot_name

=== enemy_camp ===

You are approached by your lieutenant who informs you of a nearby enemy camp. He advises that attacking them at night will be the best option however you understand that this could lead to soldier morale lowering. #speaker:Lieutenant Adam #portrait:Lieutenant Adam

What do you do?
+ [Attack them in the night (Troop Damage +10%, Morale - 150)]
    ~ morale = 150
    You order an attack during the night. -> END
+ [Attack during the day]
    You order an attack during the day. -> END
+ [Avoid them and do not attack(Enemy Camp +1 next battle, Morale + 150)]
    You order your men to stand down. -> END

=== cave ===

Your army arrives at a cave entrance which you know connects to the next settlement however none of your lieutenants are familiar with this particular cave system. #background:Cave

What do you do?
+ [Enter the cave system anyway (Morale - 150)]
    You enter the cave system blindly. Somehow you make it out but the stressful experience lowers troop morale. -> END
+ [Look for a local cave guide (Gold - 200)]
    You offer payment for a local cave guide to navigate the cave system and your troops make it swiftly and safely to the exit. -> END
    

=== market ===

You enter a bustling market. The fragant smell of sizzling street food envelops your senses.
You wander the streets curiously until the cravings become overwhelming. Noticing an enticing stall, you make your way towards it.
The stall offers an assortment of delicious snacks and you are inclined to try one. The vendor has her back turned towards you. What do you do?
+ [Get the vendor's attention and buy a snack]
    You call out to the vendor. She turns around with a beaming smile and asks what you would like. You pick and pay for a delicious snack then make your way back towards the camp. -> END
+ [Attempt to steal a snack]
    You try to steal a snack but are caught by a wandering guard. He is disgusted by your actions and tries to arrest you. You resist. Prepare for battle! -> END
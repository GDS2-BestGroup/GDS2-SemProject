INCLUDE globals.ink

-> cave

=== cave ===

~ morale = 0
~ gold = 0

Your army arrives at a cave entrance which you know connects to the next settlement however none of your lieutenants are familiar with this particular cave system. #background:cave #speaker: 

What do you do? #0:morale:-150
+ [Enter the cave system anyway (Morale - 150)]
    You enter the cave system blindly. Somehow you make it out but the stressful experience lowers troop morale. 
    ~ morale -= 150
    -> END
+ [Look for a local cave guide (Troop Movement Speed -10%)]
    You offer payment for a local cave guide to navigate the cave system and your troops make it swiftly and safely to the exit. -> END
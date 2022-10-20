INCLUDE globals.ink

-> blacksmith

=== blacksmith ===

~ morale = 0
~ gold = 0

You pass a large settlement that encompasses multiple skilled blacksmiths. #background:blacksmith #speaker: Lieutenant Serena #portrait:LieutenantSerena
The village chief approaches both you and Lieutenant Serena with an offer to repair and refine your army's weapons.

What do you do?
+ [Pay the smith settlement to work on your army's weapons (Gold - 100, Attack + 5%)] #gold: -200
    You ask Lieutenant Serena  to gather funds and pay for weapon improvements for your army. 
    ~ gold -= 100
    -> END
+ [Refuse the offer.]
    You politely refuse the village chief and continue on your way. -> END
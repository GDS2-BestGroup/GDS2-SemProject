INCLUDE globals.ink

-> mage_to_armoredknight

=== mage_to_armoredknight ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:castle_ruins_clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do? #0:gold:-50
+ [Accept his offer of gold for information (Gold -50)]
     ~ gold -= 50
    "A few hundred years ago, a ruthless king built an army of armoured knights.
    Many tried to rebel but their impenetrable armour made their weaponry useless.
    That was until a famous guild of mages heard of the king’s atrocities and decided to join the ensuing rebel army.
    <color=\#37A63F>Their magic easily pierced through the heavy armour and defeated the armoured knights</color>, bringing peace back to the land."
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
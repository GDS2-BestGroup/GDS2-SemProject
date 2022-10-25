INCLUDE globals.ink

-> armoredknight_to_wolf

=== armoredknight_to_wolf ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do? #0:gold:-50
+ [Accept his offer of gold for information (Gold -50)]
    "A foolish Mage once arrogantly decreed that no man would be able to defeat her, and entered into a tournament to prove it.
    The numerous competitors could not overcome her power as she rapidly recited her spells before they could reach her to stop her.
    Until she met an archer during the final duel. Her barrage of arrows forced the Mage to focus on dodging and she could not send forth her spells, eventually being struck by a stray arrow.
    Henceforth, <color=\#37A63F>the Mage became wary of archers</color> and would hide from them in the battlefield."
    ~ gold -= 50
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
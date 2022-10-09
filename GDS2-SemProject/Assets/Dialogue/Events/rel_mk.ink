INCLUDE globals.ink

-> mage_to_armoredknight

=== mage_to_armoredknight ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do?
+ [Accept his offer of gold for information (Gold -50)]
    "Those with heavy protection can only be protected from those that are of a physical dimension."
    ~ gold -= 50
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
INCLUDE globals.ink

-> wolf_to_archer

=== wolf_to_archer ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do?
+ [Accept his offer of gold for information (Gold -100)]
    "Swift beasts will fleetly hunt anchored souls that wield feathered artifacts."
    ~ gold -= 100
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
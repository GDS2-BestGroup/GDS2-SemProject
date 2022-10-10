INCLUDE globals.ink

-> archer_to_mage

=== archer_to_mage ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do?
+ [Accept his offer of gold for information (Gold -50)]
    "Nimble robins with their keen ears can swiftly seek out those that use words as their weapons."
    ~ gold -= 50
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
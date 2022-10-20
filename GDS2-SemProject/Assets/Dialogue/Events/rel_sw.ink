INCLUDE globals.ink

-> swordman_to_wolf

=== swordman_to_wolf ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do?
+ [Accept his offer of gold for information (Gold -50)]
    "Listen carefully, while man may be scared of beast, only an agile one with a tool of silver can overwhelm the onslaught."
    ~ gold -= 50
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
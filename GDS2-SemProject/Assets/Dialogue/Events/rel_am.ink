INCLUDE globals.ink

-> archer_to_mage

=== archer_to_mage ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:waterfall #speaker: Wise Old Man #portrait: WiseOldMan

What do you do? #0:gold:-50
+ [Accept his offer of gold for information (Gold -50)]
    ~ gold -= 50
    "A long time ago, the wolves of this land were peaceful and would not attack humans without provocation.
    One day, an exiled Mage decided to create a wolf army however the new wolves were filled with such extreme bloodthirst that he could not control them.
    The wolves killed the Mage in the ensuing chaos and set about ravaging the land, but the King's army quickly discovered that these wolves were vulnerable to silver weaponry.
    Thus, they began <color=\#37A63F>equipping all swordsman with silver swords</color> to efficiently control this new species."
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
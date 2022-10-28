INCLUDE globals.ink

-> swordman_to_wolf

=== swordman_to_wolf ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:blacksmith #speaker: Wise Old Man #portrait: WiseOldMan

What do you do? #0:gold:-50
+ [Accept his offer of gold for information (Gold -50)]
    ~ gold -= 50
    "I used to be the quartermaster for many brilliant young soldiers.
    However certain knowledge can only be learnt through practical trials. The boys learned a valuable lesson when I asked them to compete, one in lighter armour and one in heavy armour.
    <color=\#37A63F>You can dance around a heavily armoured Knight as much as you want, but in the end, you won't be able to emerge the victor.</color>"
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
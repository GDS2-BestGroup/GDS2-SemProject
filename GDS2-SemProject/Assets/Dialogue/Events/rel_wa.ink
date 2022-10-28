INCLUDE globals.ink

-> wolf_to_archer

=== wolf_to_archer ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do? #0:gold:-50
+ [Accept his offer of gold for information (Gold -50)]
    ~ gold -= 50
    "The ancient art of archery has always been closely aligned with the natural world.
    That has not changed since, and young archers are still taught to respect all wildlife.
    However, the <color=\#37A63F>wolves</color> of the current world do not share this connection and will ruthlessly hunt those standing in its way, <color=\#37A63F>especially those archers that so foolishly attempt to appease them</color>."
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
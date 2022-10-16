INCLUDE globals.ink

-> mage_to_armoredknight

=== mage_to_armoredknight ===

~ morale = 0
~ gold = 0

A wise old man requests an audience with you. He is offering to provide some war insights in exchange for some gold. #background:clearing #speaker: Wise Old Man #portrait: WiseOldMan

What do you do?
<<<<<<< Updated upstream
+ [Accept his offer of gold for information (Gold -100)]
    "Those with heavy protection can only be protected from those that are of a physical dimension."
    ~ gold -= 100
=======
+ [Accept his offer of gold for information (Gold -50)]
    "A few hundred years ago, a ruthless king built an army of armoured knights. 
    They conquered and ravaged every castle they passed and slaughtered anyone who disobeyed them. 
    No man had the ability to stop them. 
    Until a group of mages from a far away land heard of the king‘s atrocities. 
    They gathered their own army of mages and challenged the king’s army for the freedom of the land. 
    Emboldened by his continuous success, the king faced them head on but that was a fatal mistake. 
    Their magic easily pierced through the heavy armour and defeated the king."
    ~ gold -= 50
>>>>>>> Stashed changes
    -> END
+ [Refuse the offer.]
    You politely refuse and the wise old man leaves. -> END
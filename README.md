# manager

Manager est un logiciel de gestion de parc informatique, pour les systèmes d'exploitation Windows.
Il est basé sur une architecture client-serveur.

# Client 

Le client installé sur les machines va régulièrement récuperer différentes informations pour les envoyer au serveur qui va ainsi traiter celles-ci.

### Réseau
- IPv4
- IPv6
- addr MAC

### Système

- OS version
- architecture
- username
- nom de la machine
- Uptime
- Disque système (C:)
- BIOS (pour des raisons de sécurité je regarde si le BIOS est celui par défaut, ça semble bénin, mais c'est pour prévenir en cas de backdoor ou bootkit)

### Disques durs

- Nom du disque (sa lettre correspondante)
- Temperature du disque dur
- Son type (amovible ou non)
- Le serial number
- Son label (DATA ou OS) 
- Type de File System (NTFS ou autre)
- La taille du disque (GO)
- L'espace dispo (Go et %)

### CPU

- Type de CPU
- Température de chaque core CPU

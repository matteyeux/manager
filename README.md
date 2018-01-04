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

## Serveur

Le serveur reçoit les informations pour les rediriger dans un fichier XML qui envoyer celles-ci vers une base de donnée

## Documentation

La Documentation a été générée avec [Doxygen](http://www.stack.nl/~dimitri/doxygen/). 
Si vous souhaitez ajouter des options lors de la génération de la documentation vous avez deux options :
- Utiliser l'interface graphique qui generera un fichier de configuration automatiquement.
- Via CLI en modifiant le `Doxyfile` manuellement et en le generant comme ceci : `doxygen <path_to_Doxyfile>`.

J'ai ajouté un batch file pour executer automatiquement cette commande, le fichier est dans le repertoire [doxygen](https://github.com/matteyeux/manager/tree/master/doxygen).


## Contribution

- Cloner le repository ainsi que le git submodule server : `git clone https://github.com/matteyeux/manager`
- Initialiser et cloner les sous modules: `git submodule init && git submodule update`

Si lorsque vous clonez ou que souhaitez contribuer au projet mais que vous avez cette erreur : <br>
`fatal: unable to access 'https://github.com/matteyeux/manager/': Couldn't resolve host 'github.com'`
Il faut configurer `git` pour qu'il passe par votre proxy : <br>
`git config --global http.proxy https://user:passwd@proxy:port`


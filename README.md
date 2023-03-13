# Le POC de la refonte ECARE GP

## Installer Redis dans Windows (Version 7.0.9)

Premièrement il faut installer le WSL (Windows Subsystem for Linux). Pour faire, il avoir Windows 10 version 2004 et supérieur (Build 19041 et supérieur) ou Windows 11
- Ouvrer Panneau de configuration -> Programmes
- Dans la section Programmes et fonctionnalités, cliquer sur Activer ou désactiver des fonctionnalités Windows
- Vers le bas, on trouve Sous-système Linux pour Windows. Cochez la case et redémarrer la machine.
- Après le redémarrage, ouvrer Microsoft Store et chercher dans la zone de recherche "Ubuntu", et installer-le.
- C'est bon, vous aurez un nouveau type dans le Windows Powershell qui s'appelle Ubuntu pour ouvrir le terminal.

Maintenant, commençant l'installation Redis. Dans le terminal Ubuntu exécutant les commandes suivantes :

```bash
 #Installation Redis
  $ curl -fsSL https://packages.redis.io/gpg | sudo gpg --dearmor -o /usr/share/keyrings/redis-archive-keyring.gpg
  $ echo "deb [signed-by=/usr/share/keyrings/redis-archive-keyring.gpg] https://packages.redis.io/deb $(lsb_release -cs) main" | sudo tee /etc/apt/sources.list.d/redis.list
  $ sudo apt-get update
  $ sudo apt-get install redis
  $ sudo service redis-server start
  $ sudo chmod -R 755 /etc/redis/
  $ sudo chmod -R 755 /var/log/redis/
  $ sudo chmod -R 755 /var/lib/redis/
  $ redis-cli
  127.0.0.1:6379> config set stop-writes-on-bgsave-error no
  OK
 #Si vous voulez voir l'ensemble des Keys enregistrés dans Redis :
  127.0.0.1:6379> KEYS <Keyname | * pour tous les keys>
 #Exemple d'affichage d'un key
  1) "Client-589082d8-c724-459b-9f6b-124e819d2781" 
 #Vider la base de données de cache de Redis
  127.0.0.1:6379> FLUSHALL
  OK
 #Récupérer une valeur via le Key
  127.0.0.1:6379> GET <Keyname>
 #Pour sortir du CLI entrer exit
  127.0.0.1:6379> exit
```

## Contact :

EL MABROUK Marouane : [elmabroukmarouane@gmail.com](elmabroukmarouane@gmail.com)

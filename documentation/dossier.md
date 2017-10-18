# Dossier - Ochsendorf Marcel

* DWBT WS1718
* http://github.com/RBEGamer/dwbt_praktikum_ws1718


## MILESTONE 0

### TASKS
* I. Erstellen des Dossiers
* II. Installation von VS17 inkl. ASPNET CORE und .NET
* III. Installation von MariaDB und testen der DB Verbindung via VS17
* IV. Installation von HeidiSQL und einrichten der DB Verbindung


#### TASK MS0 II.
Bei der Installation von VS17 habe ich zusaetzlich das .NET Core 2.0 Framework installiert.
So kann das Projekt auch auf Linux/MacOSX Rechnern ausgeführt werden.
Die initiale Projektmappe habe ich ueber VS17 auf einem PC erstellt um die kompatibilitaet von VS17 und Visual Studio for Mac zu gewaehrleisten.

#### TASK M0 III.
Um das System clean zu halten, habe ich MariaDB in einem Docker Container installiert, so kann die Datenbank auch einfach auf zB EC2 Container instanzen verteilt werden. Das ASPNET.NET CORE Projekt basiert ebenfalls auf Docker basis. Seit VS17 ist es moeglich direkt DockerProjekte anzulegen, welche auch über Docker(Compose) gestartet und verteilt werden können.

#### TASK MS0 IV.
Da mein Hauptsystem für dieses Praktikum ein MacOSX System ist, muss ich ein anderes DBMS verwenden.
Also habe ich zusaetzlich zu HeidiSQL, SequelPro (http://sequelpro.com) installiert und dor die DB Verbindung eingerichtet.

### QUESTIONS M0

#### Lizenzen
* VS17 - EULA (Endbenutzer-Lizenzvereinbarung) | nicht offen
* MariaDB - GPL (General Public License ) | offen, bearbeiten, kopieren, rechte beibehalten
* SequelPro - MIT (SPDX) | ohne limitierung
* HeidiSQL - GPL

#### Web Development IDE
Hier liste ich einige IDEs die ich bereits verwende. Dabei finde ich die Produktpalette von JetBrains sehr gelungen. Bootstrapstudio ist zwar keine direkte IDE für Webentwicklung, aber sie macht den Umgang und das erstellen von Templates mit Bootstrap sehr einfach.

* JetBrains WebStorm - https://jetbrains.com/webstorm
* Adobe Dreamweaver CS6 -  http://adobe.com/de/products/dreamweaver.html
* Bootstrap Studio -  https://bootstrapstudio.io



#### DB PORTS
Da MariaDB ein Fork von MySQL ist, wurde auch der StandartPort beibehalten. Dieser ist TCP-Port 3306

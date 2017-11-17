# Dossier - Ochsendorf Marcel | 3120232

* DWBT WS1718
* http://github.com/RBEGamer/dwbt_praktikum_ws1718


## MILESTONE 0

#### TASKS MS0
* I. Erstellen des Dossiers
* II. Installation von VS17 inkl. ASPNET CORE und .NET
* III. Installation von MariaDB und testen der DB Verbindung via VS17
* IV. Installation von HeidiSQL und einrichten der DB Verbindung

Da ich gerne das neue .net core Framework benzutzen moechte sind die installationschritte etwas anders.
.NET CORE laeuft nicht nur auf der Windows Plattform, sondern auch unter Linux und MacOSX.



##### TASK MS0 I.
Bei der Installation von VS17 habe ich zusaetzlich das .NET Core 2.0 Framework installiert.
So kann das Projekt auch auf Linux/MacOSX Rechnern ausgeführt werden.
Die initiale Projektmappe habe ich ueber VS17 auf einem PC erstellt um die kompatibilitaet von VS17 und Visual Studio for Mac zu gewaehrleisten.

##### TASK M0 II.
Um das System clean zu halten, habe ich MariaDB in einem Docker Container installiert, so kann die Datenbank auch einfach auf zB EC2 Container instanzen verteilt werden. Das ASPNET.NET CORE Projekt basiert ebenfalls auf Docker basis. Seit VS17 ist es moeglich direkt DockerProjekte anzulegen, welche auch über Docker(Compose) gestartet und verteilt werden können.

##### TASK MS0 III.
Da mein Hauptsystem für dieses Praktikum ein MacOSX System ist, muss ich ein anderes DBMS verwenden.
Also habe ich zusaetzlich zu HeidiSQL, SequelPro (http://sequelpro.com) installiert und dor die DB Verbindung eingerichtet.

#### QUESTIONS M0

##### Lizenzen
* VS17 - Licence Terms | nicht offen
* MariaDB - GPL (General Public License ) | offen, bearbeiten, kopieren, rechte beibehalten
* SequelPro - MIT (SPDX) | ohne limitierung
* HeidiSQL - GPL

##### Web Development IDE
Hier liste ich einige IDEs die ich bereits verwende. Dabei finde ich die Produktpalette von JetBrains sehr gelungen. Bootstrapstudio ist zwar keine direkte IDE für Webentwicklung, aber sie macht den Umgang und das erstellen von Templates mit Bootstrap sehr einfach. Adobe Dreamweaver ist die Allroundlösung und bietet gut Integration für andere Adobe Produkte.

* JetBrains WebStorm - https://jetbrains.com/webstorm
* Adobe Dreamweaver CS6 - http://adobe.com/de/products/dreamweaver.html
* Bootstrap Studio - https://bootstrapstudio.io

##### DB PORTS
Da MariaDB ein Fork von MySQL ist, wurde auch der StandartPort beibehalten. Dieser ist TCP-Port 3306.
Bei benutzung des MariaDB Docker images, kann der Port variieren, dazu den EXPOSE Parameter in der Dockerfile anpassen.



## MILETONE 1


### HTML - PAKET 1

### #TASKS MS1
* Erstellen der statischen HTML seiten von dem Mockups. Dabei die umsetzung eines Shops mit 10 Produkten
* Recherchieren der Fragen

#### TASK MS1 TASK I ANSWER I.
Um den Shop zumzusetzen ohne dynamische Inhalte, muss für jedes Produkt eine eigene Seite angelegt weren. Hinzu kommen noch die Startseite, Auswahlseite, Impressum,..
Dabei ist jede Profuktseite identisch aufgebaut, wie auch in unserem Beispiel (Details.html).
Mit dem Produktnamen, Foto, Preis, usw. Diese Inhalte müssen aber für jedes Produkt manuell angepasst werden und als neue HTML Seite gespeichert werden.

* `produkt_brot.html`
* `profukt_wurst.html`

Dies ist für einen Shop mit nur 10 Produkten zwar machbar, aber nur wenn keine weiteren Produkte hinzukommen.
Dafür würde erst einmal ein Layout für die Detail Ansicht erstellt werden und für jedes Produkt neu kopiert werden, die Inhalte eingeügt und zum schluss als neue Datei gespeichert werden und natürlich auch in der Übersicht per Hand verlinkt werden.


#### TASK MS1 TASK II ANSWER I.
Eine normale ComboBox besteht bei normalen HTML nur aus `<select>`und den passenden `<option>` Attribut. Um nun einige Einträge zu Bündeln gibt es zusätzlich die Option der `<optiongroup>`.
Diese gruppiert alle `<option>` in ihrem Scope und stellt zusätzlich ein Label bereit um die Gruppe zu benenen.


`<select>
    <optgroup label="Category 1">
        <option>Item 1</option>
        <option>Item 2</option>
    </optgroup>
    <optgroup label="Category 2">
        <option>Item 3</option>
        <option>Item 4</option>
    </optgroup>`



#### TASK MS1 TASK II ANSWER II.
Um gezielt Elemente in einer ComboBox zu deaktivieren gibt es das `disabled` Attribut innerhalb eines Eintrages/Option. Wenn das Falg gesetzt ist, wird der Eintrag ausgegraut angezeigt und lässt sich nicht mehr selektieren.

 *  NORMAL      `<option>Item 3</option>`
 *  DEAKTIVIERT `<option disabled>Item 3</option>`


#### TASK MS1 TASK II ANSWER III.
Ein weiteres nützliches Attribut, ist das `value=` Attribut innerhalb eines Eintrages.
Dadurch ist es möglich Items mit einem alternativen namen zu versehen, anstatt des angezeigen Namens.
Dies hat den Vorteil, das man einfach Items eine ID zuweisen kann ohne das man dies im Backend machen muss.

* `<option value="1">Volvo</option>` Hier wird die ID 1 übergeben anstatt der in der ComboBox angezeigte name.



### SQL - PAKET 2

#### Was bewirkt das Semikolon am Ende der Anweisung? Dokumentieren Sie die kurzen Antworten im Dossier.

Das Semikolon ist das Zeichen für SQL, dass der Befehl/die Zeile zu ende ist. Dies ist vorallem bei mehrzeiligen Statemens wichtig, damit klar unterschieden werden kann wann welches Statement zuende ist. Somit werden auch Statement Blöcke getrennt um die Ausführungsfluss zu steuern.
Zusätzlich können auch die Statement `BEGIN` und `END` verwendet werden.





#### Was müssen Sie ändern, um diese besondere Beziehung abzudecken? Welche Vor-/Nachteile hat die von Ihnen ausgewählte Abbildung?

Hier gibt es verschiene Ansätze, im Grunde ist die Beziehung zwischen FE-Nutzer und Student/Gast/Mitarbeiter eine `1:1`. Ein FE-Nutzer kann entwieder Student, Mitarbeiter oder Gast sein, aber nicht Gast und Student zugleich. Eine möglichkeit wäre es beim `FE-Nutzer` flags oder ENUMS. Um ganz deutlich zu kennzeichnen dass es sich um diese Bentuzerrolle handelt. Danach kann in der passenden Tabelle nach den weiteren Daten gesucht werden.  Dies hat den Nachteil, dass zusätzlich zu dem eintragen der Daten in der Tabelle `Student/Gast/Mitarbeiter` auch das passende Falg gesetzt werden muss.

Eine weiter möglichkeit wäre es passende zusätzliche `FK_Student`, `FK_Mitarbeiter`, `FK_Gast` Attribute zu erstellen und diese als optional deklarieren. So muss nur geschaut werden welche FK oprion nicht null ist.


#### wie Sie die binären Relationstypen (1:N, N:M) abgebildet haben

Bei der `1:N` (es gibt einen Lehrer, welcher mehrere Klassen betreut). Diese Relation kann einfach durch ein `FK_LEHRER_ID` (-> zeigt auf die id des Lehrers) gelösst werden.

* `LEHRER: ID, NAME, Anlegedatum`
* `Klasse, ID, NAME, JAHR, FK_LEHRER`


Bei einer `N:M` Relation kann dies nicht mehr durch einfaches hinzufügen eines weiteren Attributes, sondern durch hinzufügen einer weiteren Tabelle. Diese enthält nur die `ID` Schlüssel der beiden zu verbindenen Tabellen und dadurch die jeweiligen IDs. Als beispiel kann man hier `Schüler <-> Kurse` nehmen. Da alle Schüler auch in allen Kursen sein können.



* `SCHÜLER ID, Name, Jahr`
* `KURSE, ID, NAME, Dauer`

Nun kommt die junction Table hinzu:

* `SCHÜLER_KURSE FK_SCHÜLER_ID, FK_KURS_ID`

Um nun zb alle Schüler zu bekommen die in einem Kurs sind muss nur eine Abfrage mit der `KursID` in der `SCHÜLER_KURSE` Tabelle gemacht werden.




#### welche Constraints in MariaDBes gibt und welchem Zweck sie diene
Constraints dienen dazu zu verhindern Daten in die Datenbank einzufügen die die Integrität der Datenbank beeinflussen würden. Eine möglichkeit ist zb der `Primary Key` wenn dieser für eine Attribut gesetzt ist muss der Wert des Attributes in der Tabelle einzigartig sein. Dies wird häufig bei der `ID` spalte einer Tabelle verwendet. Wenn es schon eine `ID` mit dem Wert 1 gibt kann kein weiteres `Insert ID 1` ausgeführt werden. Der PRIMARY KEY ist aber nur für ein Attribut der Tabelle gültig und indetifiziert die Reihe als eindeutig. Anders als beim `UNIQUE` Constraint.

Dies macht auch der andere Constraint `UNIQUE`. Dieser sagt nur dass dieser wert im Attribut gleich sein soll. Dies kann man auch für mehrere Attribute setzten.


`FOREIGN KEY` zeigt auf einen `PRIMARY KEY` einer anderen Tabelle. Dieser wird verwendet wenn ein Attribut auf eine andere Tabelle verweist. Wie im `LEHRER <-> KLASSE` beispiel. Dort wird ein FK Verwendet und genau auf eine Reihe einer anderen Tabelle zu verwiesen. Welche aber bereits existieren muss, andernfalls wird der INSERT Befhelt nicht ausgeführt.

Das `CHECK` Constraint ist wie ein If in anderen sprachen, hier kann eine Bedingung eingefügt werden. Welche den einzufügenden Datensatz auf korrektheit überprüft.

#### wie Sie den Aufzählungsdatentyp ENUM
Ein enum ist ein Platzhalter für Vordefinierte werte. zb Farbe= ROT,GRÜN,GELB
Das Attribut Farbe kann somit entwieder Rot, Gelb oder Grün sein.
In SQL wird ein ENUM so angelegt:

* `ENUM('rot','gelb','grün')`

Es können aber nicht nur Zeichenketten sein, sondern auch:

* ""
* null

Ebenfall kann auf einen Enum eintrag auch per ID zugegriffen werden. Im Beispiel oben steht Rot für die ID 1, gelb für 2 usw.



#### per CHECK Constraint auch in anderen DBMS nachbilden könnten•… was Sie tun mussten, um die Spezialisierung in der Datenbank abbilden zu können (welche INSERT Befehle, welche Reihenfolge)

Um das `CHECK` Constraint nachzubilden, würde ich vorher überprüfen ob es die Spalte schon gibt, Dies kann über zuerst über einen `SELECT` Befehl geschehen. So könnte man alle abhängigen Tabellen zuerst überprüfen. Auch kann man die Reihenfolge der `INSERT` Befehle so ändern, dass zuerst du super tabelle befüllt werden. In unserem Beispiel muss die `FE-Nutzer` Tabelle zuerst befüllt werden bevor die `Gast/Student/Mitarbeiter` Tabelle befüllt werden kann, da diese auf die bereits existierende Elemente in der `FE-Nutzer` Tabelle zurückgreifen.

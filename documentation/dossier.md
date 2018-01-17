# Dossier

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


#### Wie Sie die binären Relationstypen (1:N, N:M) abgebildet haben

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





### Sakila Datenbank SQL Abfragen - PAKET 3


#### Wozu dienen die SQL Funktionen COALESCE, IFNULL und NULLIF?

`Coalesce` wertet die übergebenen Argumente in Reihenfolge aus und gibt von den übergebenen Argumenten das erste von `Null` verschiedene Element zurück
`IFNULL` ermöglicht es einen Ausdruck auszuwerten und festzulegen, welcher Wert an Stelle des Ausdrucks angegeben werden soll wenn dieser `NULL` ergibt.
`NULLIF(Ausdruck1,Ausdruck2)` gibt es Wert NULL zurück wenn die beiden Ausdrücke gleich sind.


#### Wozu dienen die Schlüsselwörter ALL und ANY bei Subqueries und wie kann man sie einsetzen?

`ANY` und `ALL` dienen dazu in einer Unterabfrage eine Bedingung zu Formulieren welche Daten ausgewählt werden sollen indem man ein Element mit einer Liste von Elementen vergleicht. Bei `ALL` werden jene Resultate zurückgegeben bei denen das Element gleich jedem der einzelnen Listenelemente ist. Bei `ANY` werden jene Daten ausgewählt bei denen der Element mindestens gleich einem der Listenelemente ist.


#### Welche Kunden (customer) haben Nachnamen, die mit A, B oder C beginnen? Geben Sie nur die Nachnamen aus.
`select `last_name` from `customer` where `last_name like` 'a%' or `last_name` like 'b%' or `last_name` like 'c%'`


#### 2 Welche Kunden (customer) sind als aktiv geführt (active)? Geben Sie die Vor- und Nachnamen der Kunden in einer Spalte zusammengefasst zurück. (In der Form: Vorname Nachname)
`select concat(`first_name`, ' ', `last_name`) from `customer` where `active`='1'`


#### 3 Welche Kunden (customer) haben die kürzeste E-Mail-Adresse (email)? Geben Sie die zehn kürzesten E-Mail-Adressen und die zugehörigen Kunden-IDs aus. Sortieren Sie aufsteigend nach der Länge der E-Mail-Adresse.
`select `customer_id`,`email` from `customer` ORDER BY length(`email`) DESC LIMIT 10`


#### 4 Geben Sie die Anzahl der Kunden (customer) pro Geschäft (gespeichert in store_id) aus. Das Ergebnis muss sowohl die Store-ID als auch die Anzahl der zu dieser ID gefundenen Kunden enthalten. Benennen Sie diese Spalte Anzahl der Kunden.
`select count(`customer_id`) as `Anzahl Kunden`,store_id from `customer` group by `store_id``


#### 5 Welche Kinderfilme (film) sind günstig ausleihbar? Nennen Sie die Film-ID, den Titel und die Leihkosten (rental_rate) aller Filme für unter $1 ausgeliehen werden können und das Rating G aufweisen. (Ignorieren Sie vorerst Rentals)
select `film_id`,`title`,`rental_rate` from `film where rating` = 'G' and `rental_rate`<1.0


#### 6 Führen Sie nun nur noch die Filme auf, die laut Inventar (inventory) in Store 1 vorliegen. Geben Sie zu jedem Film zusätzlich die Anzahl der im Inventar geführten Kopien aus.
`select COUNT(`inventory_id`) as 'Anzahl Kopien' , `film_id`   from `inventory` where `store_id`='1' group by `film_id``


#### 7 Listen Sie pro Store-ID und pro Rating die Anzahl der im Inventar geführten Kopien von Filmen auf. Das Ergebnis soll zuerst für Store 1 und dann für Store 2 die Ratings alphabetisch sortiert enthalten.
`select `rating`,`store_id` ,count(`inventory_id`) from `film` join `inventory` on `film`.`film_id` = `inventory`.`film_id` group by `store_id`,`rating` order by `store_id` , `rating` asc`


#### 8 Welche Filme sind in der Kategorie (category) Children aufgeführt, haben jedoch ein überhaupt nicht für Kinder geeignetes Rating? (Rating R oder NC-17). Stellen Sie den Bezug zwischen dem Namen Children und der Filme her. Geben Sie Film_id, Rating und Titel der Filme aus.
`select `film`.`film_id`,`rating`,`title`  from `film` join `film_category` on `film`.`film_id` = `film_category`.`film_id` join `category` on `film_category`.`category_id` = `category`.`category_id` where `category`.`name` = 'Children' and `film`.`rating` in ('R','NC-17')`


#### 9 Listen Sie Kundendaten ID, Vorname, Nachname und E-MailAdresse, Anschrift, PLZ und Stadt für alle Kunden aus Deutschland aus.
`select `customer_id` , `first_name`, `last_name`, `email`, `address`.`address`, `district`, `postal_code`,`city`.`city`  from `customer` join `address` on `customer`.`address_id` = `address`.`address_id` join `city` on `address`.`city_id` = `city`.`city_id` where `city`.`country_id` = (select `country_id` from `country` where `country`.country` = 'Germany')`


#### 10 Geben Sie die Anzahl der Kunden pro Land aus. Die Liste soll nur die Länder mit mindestens 30 Kunden enthalten und den Landesnamen und die Anzahl der Kunden absteigend sortiert enthalten.
`select count(`customer_id`),`country`.`country`  from `customer` join `address` on `customer`.`address_id`=`address`.`address_id` join `city` on `address`.`city_id` = `city`.`city_id` join `country` on `city`.`country_id` = `country`.`country_id` group by `country`.`country` having count(`customer_id`)>=30`


#### 11 Geben Sie die Summe der eingenommenen Ausleihgebühren (payment) pro Kunde pro Monat aus. Berücksichtigen Sie nur Zahlungen (amount) im ersten Halbjahr 2005 (payment_date). Geben Sie Vor- und Nachnamen der Kunden, den Namen des Monats und die Summe der Ausleihgebühren in diesem Monat aus. Die Monate sollen chronologisch korrekt gelistet und je Monat sollen die Summen abwärts sortiert werden. Tipp: Nutzen Sie MONTHNAME().
`select `first_name`,`last_name`, monthname(`payment_date`),sum(`amount`) as 'Ausleihgebühren' from `payment`
join `rental` on `payment`.`rental_id`=`rental`.`rental_id`
join `customer` on `rental`.`customer_id` = `customer`.`customer_id`
where month(`payment_date`)<='6' and year(`payment_date`)='2005'
group by `first_name`,`last_name`,monthname(`payment_date`)
order by month(`payment_date`) asc, Ausleihgebühren desc`


#### 12 Geben Sie den umsatzstärksten Monat samt Summe (amount) aus. Der Monat soll wieder als Namen vorkommen, die Spalte soll Monat heißen. Die Spalte für die Summe soll Umsatz heißen.
`select max(`Summe`) as 'Umsatz' ,`Monat` from(
select sum(`amount`) as 'Summe', monthname(`rental_date`) as 'Monat' from `payment`
join `rental` on `payment`.`rental_id`=`rental`.`rental_id`
)`

#### 13 Geben Sie den Umsatz je Kategorie aus je Store ID (inventory.store_id) zurück. Die Spalten sollen Kategorie, Store und Umsatz heißen.
`select `name` as 'Kategorie',`store_id` as 'Store', sum(`amount`) as 'Umsatz' from `payment`
join `rental` on `payment`.`rental_id` = `rental`.`rental_id`
join `inventory` on `rental`.`inventory_id` = `inventory`.`inventory_id`
join `film` on `inventory`.`film_id` = `film`.`film_id`
join `film_category` on `film`.`film_id` = `film_category`.`film_id`
join `category` on `film_category`.`category_id` = `category`.`category_id`
group by `store_id`,`name``


#### 14 Geben Sie alle Vorgesetzten (supervisor_id) für den Mitarbeiter (staff) mit der ID 6 (staff_id) aus. Es genügt, wenn Sie die Vor- und Nachnamen der Vorgesetzten in der richtigen Reihenfolge (von oberster Hierarchieebene bis herunter auf den Mitarbeiter selbst) zurückgeben. Tipp: Setzen Sie dies mittels CTE um (siehe [4]).
`WITH RECURSIVE 'StaffAndSuperVisor' AS
 ( SELECT `staff_id`,`first_name`,`last_name`,`supervisor_id` FROM `staff`
   WHERE `staff_id`=6
   UNION ALL
   SELECT `f`.`staff_id`,`f`.`first_name`,`f`.`last_name`,`f`.`supervisor_id`
   FROM `staff` AS 'f', 'StaffAndSuperVisor' AS 'a'
   WHERE `f`.`staff_id` = `a`.`supervisor_id` )
SELECT * FROM `StaffAndSuperVisor`;`

#### 15. Lassen Sie sich die Detail-Informationen zu allen Spalten der Mitarbeiter-Tabelle anzeigen (staff). Zeigen Sie nur die Spalten ohne NOT NULL-Constraint an (Null).
`SHOW COLUMNS FROM `staff` where `columns`.`null` = 'YES'`

#### Erzeugen Sie für die Datenbank Praktikum aus Meilenstein 1 einen Benutzer:
• Ein Nutzer namens webapp, mit dem Ihre Anwendung in Paket 4
auf die Datenbank zugreifen wird. Das Kennwort vergeben Sie
selbst. Der Nutzer webapp soll vollständige Rechte über die
Datenbank erhalten (optional exklusive GRANT).

• Notieren Sie die notwendigen SQL-Befehle für die Erstellung in
jedem Fall in Ihrem Dossier. (Die Benutzerverwaltung in HeidiSQL
erzeugt die erforderlichen SQL-Befehle für Sie – beachten Sie den
Query Log).


`CREATE USER 'webapp'@'localhost' IDENTIFIED BY 'webapp'';
GRANT USAGE ON *.* TO 'webapp'@'127.0.0.1';
GRANT SELECT, EXECUTE, SHOW VIEW, ALTER, ALTER ROUTINE, CREATE, CREATE ROUTINE, CREATE TEMPORARY TABLES, CREATE VIEW, DELETE, DROP, EVENT, INDEX, INSERT, REFERENCES, TRIGGER, UPDATE, LOCK TABLES  ON `dbwt_praktikum`.* TO 'webapp'@'127.0.0.1' WITH GRANT OPTION;
FLUSH PRIVILEGES;``

#### Dokumentieren Sie, was das GRANT Recht ermöglicht und ob Sie es für sinnvoll erachten. Stellen Sie auch sicher, dass der Nutzer webapp ausschließlich die Rechte auf der oben angesprochenen Datenbank besitzt!

Grant erlaubt es dem Benutzer die Rechte anderer Nutzen der Datenbank zu ändern. Dies ist im vorliegenden Fall nicht sinnvoll, es wäre dann von Nutzen wenn man einen ‘Verwaltungsuser’ schaffen wollte dessen Aufgabe explizit die Verwaltung anderer User ist.









### MCV- PAKET 5
Es ist möglich Anmledeversuche zu Loggen in dem eine weitere Tabelle in der Datenbank angelegt wird. In dieser Tabelle wird für die User Id geloggt wann versucht worden ist sich anzumelden. Dort können auch weitere DAten wie IP geloggt werden. Um anmeldeversuche dann zu Zählen kann ein einfaches Count verwendet werden. Um fehlgeschlagene versuche zu Zählen könnte noch ein weiteres Flag erstellt werden, welches besagt dass die Anmeldung ungültig ist.




### LINQ- PAKET 6

#### 6.2 Die Kriterien für Anmeldungen der Art "Backend" sind nicht fest vorgegeben gewesen, daher wählen Sie einen der genannten Vorschläge oder machen einen eigenen.


Ich habe mich in für keine spezialisierung des Admin-Users entschieden. Da ich es für am einfachsten fand den User nur mit einem weiteren Flag auszustatten, da es im jetzigen Zeitpunkt keine weiteren nötig sind.

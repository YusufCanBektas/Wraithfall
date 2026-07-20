# Wraithfall

Ein 2.5D Shoot-'em-up (Shmup), entwickelt in Unity mit C#.

**Entwickelt von:** Yusuf
**Repository:** https://github.com/YusufCanBektas/Wraithfall

## Konzept

Wraithfall ist ein Side-Scrolling Shoot-'em-up im Sci-Fi-/Alien-Setting. Der
Spieler steuert ein selbst modelliertes Raumschiff (X-Wing-Style) durch eine
außerirdische Landschaft, bekämpft mehrere Wellen von Gegnern und tritt am
Ende gegen einen Boss an. Während des Spiels können Items eingesammelt
werden, die Punkte, Extra-Leben, ein temporäres Schild oder ein
Waffen-Upgrade gewähren.

## Steuerung

- Bewegung: WASD / linker Stick
- Boost: Shift
- Schießen: Leertaste

## Spielablauf

- Der Spieler bekämpft in Level 1 fünf Gegnerwellen mit ansteigender
  Schwierigkeit
- Nach der letzten Welle erscheint ein Boss
- Wird dieser erste Boss besiegt, folgt ein Kamera-Zoom- und Blend-Übergang
  in Level 2, das den Planeten des Aliens zeigt
- In Level 2 kämpft der Spieler gegen weitere Gegnerwellen und einen
  zweiten, stärkeren Boss
- Wird auch dieser Boss besiegt, endet das Spiel mit einem Sieg-Bildschirm
- Verliert der Spieler in einem der beiden Level alle Leben, erscheint ein
  Game-Over-Bildschirm
- Beide Bildschirme zeigen den erreichten Punktestand sowie den
  gespeicherten Highscore an
- Im Hauptmenü lässt sich die Lautstärke über ein Optionsmenü einstellen

## Wahlpflichtpunkte

### PRG
- **Boss Gegner** — eigener `BossController` mit Patrouillen-Bewegung,
  gezieltem Beschuss, Healthbar-Anzeige und eigener Win-Condition
- **Extramanöver (Items)** — Sammel-Item-System mit vier Typen (Punkte,
  Extra-Leben, temporäres Schild, temporäres Waffen-Upgrade), inklusive
  Drop-Chance bei besiegten Gegnern, unabhängigem zeitgesteuertem
  Item-Spawner und einem Magnet-Effekt, der Items in Spielernähe anzieht

### ALD
- **Audio** — eigenständiges Musik- und Soundeffekt-System
  (`AudioManager`) mit Lautstärkeregelung, das über Szenenwechsel hinweg
  bestehen bleibt
- **Weitere Modelle** — über die Mindestanforderung hinaus wurden 6
  zusätzliche eigene Blender-Modelle erstellt: zwei Projektile (Spieler-
  und Gegner-Geschoss) sowie vier Sammel-Item-Modelle (Punkte,
  Extra-Leben, Schild, Waffen-Upgrade)
- **Eigene Materialien** — alle 8 eigenen Blender-Modelle verwenden
  selbst erstellte PBR-Materialien (individuelle Farb- und
  Materialwerte je Modell, per Python-Skript definiert)

## Eigene 3D-Modelle (Blender)

Alle folgenden Modelle wurden selbst in Blender erstellt (Python-Skript-Workflow,
FBX-Export). Die ersten beiden erfüllen die Mindestanforderung an eigenen
Modellen, alle weiteren sind zusätzliche Modelle (siehe Wahlpflicht ALD).

1. **Spieler-Schiff** (`player.blend`) — Low-Poly X-Wing-Style-Raumschiff
2. **Alien Fighter** (`gegner.blend`) — Low-Poly-Gegner-Raumschiff mit
   PBR-Materialien
3. **Spieler-Projektil** (`shooter.blend`) — Plasma-Torpedo-Geschoss des
   Spielers
4. **Gegner-Projektil** (`gegnerlaser.blend`) — Laser-Geschoss der Gegner
5. **Item: Punkte** (`item_point.blend`) — Sammel-Item für den Punktestand
6. **Item: Extra-Leben** (`item_extraLive.blend`) — Sammel-Item für ein
   zusätzliches Leben
7. **Item: Schild** (`item_shield.blend`) — Sammel-Item für ein
   temporäres Schild
8. **Item: Waffen-Upgrade** (`item_WeaponUpgrade.blend`) — Sammel-Item
   für ein temporäres Waffen-Upgrade

## Externe Assets

Die folgenden Assets stammen nicht aus eigener Erstellung und werden hier
gemäß ihrer jeweiligen Lizenz aufgeführt:

### 3D-Modelle

**UFO (erster Boss)**
UFO by marcusnikira [CC-BY] (https://creativecommons.org/licenses/by/3.0/) via Poly Pizza (https://poly.pizza/m/4nCAC4jP0a)

**Alien (zweiter Boss, Level 2)**
Alien by Quaternius (https://poly.pizza/m/sUTLXji0aL)
Lizenz: CC0 / Public Domain (keine Attribution erforderlich)

### Sound-Effekte

**Sci-Fi Sounds** und **Interface Sounds** von Kenney
Quelle: https://kenney.nl
Lizenz: CC0 1.0 Universal (keine Attribution erforderlich)

### Musik

**Sci-Fi Music Pack Vol. 3** von alkakrab
Quelle: https://alkakrab.itch.io/sci-fi-music-pack-vol-3
Lizenz: siehe im Paket enthaltene Lizenz-Datei
Verwendet für: Hauptmenü-Musik sowie eigene, unterschiedliche
Hintergrundmusik für Level 1 und Level 2

### UI-Assets

**Free UI Hologram Interface** von Wenrexa
Quelle: https://wenrexa.itch.io/holoui
Lizenz: CC0 1.0 Universal (keine Attribution erforderlich)
Verwendet für: Sammel-Item-Icons (Punkte, Extra-Leben, Schild,
Waffen-Upgrade) sowie UI-Buttons

### Level-Hintergrund

KI-generiertes Bild, selbst erstellt.

## Technische Systeme

- **Damageable-Basisklasse** — gemeinsame Schaden-/Sterbe-Logik für
  Gegner und Boss (Polymorphie), reduziert doppelten Code
- **WaveData (ScriptableObject)** — Wellen-Konfiguration als
  eigenständige Assets statt fest im Code verankerter Werte
- **HUD** — Punkteanzeige, Wellenzähler, Boss-Healthbar und
  Power-Up-Statusanzeige mit Cooldown-Darstellung
- **Highscore-System** — der höchste erreichte Punktestand wird
  dauerhaft gespeichert und bei Sieg/Niederlage angezeigt
- **Item-System** — vier Sammel-Item-Typen mit Drop-Mechanik,
  zeitgesteuertem unabhängigem Spawner und Magnet-Einzugseffekt

## Technische Hinweise

- Unity-Version: 6 (6000.3.13f1)
- Render Pipeline: Universal Render Pipeline (URP)
- Input-System: Unity Input System Package (Move + Boost + Shoot Actions)

## Starten des Projekts

1. Projekt in Unity 6 öffnen
2. Szene `MainMenu` öffnen und ausführen
3. Über den "Spiel starten"-Button gelangt man in `Level 1`
4. Nach Sieg über den ersten Boss erfolgt automatisch der Übergang zu
   `Level 2`
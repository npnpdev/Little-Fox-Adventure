# Little Fox Adventure

[English](#english-version) | [Polski](#wersja-polska)

---

## English Version

### Project Description

Little Fox Adventure is a 2D retro-style platformer built in Unity 2022.3.10f1 using the 2D Universal Render Pipeline. Guide a clever fox through two challenging levels riddled with puzzles and treasures.

### Technologies & Tools

* **Engine:** Unity 2022.3.10f1 (2D URP)
* **Language:** C#
* **Rendering:** Sprite Renderer, Tilemap (Tile Palette, Composite Collider 2D)
* **Physics:** Rigidbody2D, Collider2D (Box, Circle), collision and trigger detection
* **UI:** Canvas & TextMeshPro for HUD (points, lives, keys)
* **Version Control:** Git & GitHub

### Features

* **Movement & Jumping:** WASD/arrow keys + Space
* **Collectibles:** Keys and bonuses via trigger interactions
* **Life System:** 3 lives, level reset upon losing all
* **Scene Management:** Two distinct levels (`Level1`, `Level2`) handled by GameManager
* **HUD:** Real-time display of score, keys, and lives

### Repository Structure

```
Little-Fox_Adventure/
├── Assets/
│   ├── Scenes/       # Unity scenes (Level1, Level2)
│   ├── Scripts/      # C# scripts (movement, logic, GameManager)
│   ├── Sprites/      # 2D art assets (characters, backgrounds, icons)
│   ├── Prefabs/      # Prefabs: player, obstacles, pickups
│   └── UI/           # UI elements (Canvas, HUD)
├── ProjectSettings/  # Unity project settings
├── .gitignore
└── README.md         # This file
```

### Challenges & Solutions

* **Collision Detection:** Used Composite Collider 2D and Layers for optimized platform collisions.
* **Game State Management:** Simple GameManager for scene loading and restart logic—no external patterns.
* **Dynamic HUD:** TextMeshPro text updates on state change events.

### Project Status

Complete and no longer in active development.

---

## Wersja polska

### Opis projektu

Little Fox Adventure to dwuwymiarowa gra platformowa w stylu retro, stworzona w Unity 2022.3.10f1 z wykorzystaniem 2D Universal Render Pipeline. Steruj sprytnym liskiem przez dwa poziomy pełne zagadek i skarbów.

### Technologie i narzędzia

* **Silnik:** Unity 2022.3.10f1 (2D URP)
* **Język:** C#
* **Renderowanie:** Sprite Renderer, Tilemap (Tile Palette, Composite Collider 2D)
* **Fizyka:** Rigidbody2D, Collider2D (Box, Circle), detekcja kolizji i triggerów
* **Interfejs:** Canvas i TextMeshPro dla HUD (punkty, życie, klucze)
* **Kontrola wersji:** Git & GitHub

### Funkcjonalności

* **Ruch i skoki:** WSAD/strzałki + Spacja
* **Zbieranie przedmiotów:** Klucze i bonusy poprzez wejście w trigger
* **System życia:** 3 życia, reset poziomu po utracie wszystkich
* **Zarządzanie scenami:** Dwa poziomy (`Level1`, `Level2`) zarządzane przez GameManager
* **HUD:** Dynamiczne wyświetlanie punktów, kluczy i życia

### Struktura repozytorium

```
Little-Fox_Adventure/
├── Assets/
│   ├── Scenes/       # Sceny Unity (Level1, Level2)
│   ├── Scripts/      # Skrypty C# (ruch, logika, GameManager)
│   ├── Sprites/      # Grafiki 2D (postacie, tła, ikony)
│   ├── Prefabs/      # Prefaby: gracz, przeszkody, bonusy
│   └── UI/           # Elementy interfejsu (Canvas, HUD)
├── ProjectSettings/  # Ustawienia projektu Unity
├── .gitignore
└── README.md         # Ten plik
```

### Wyzwania i rozwiązania

* **Detekcja kolizji:** Zastosowanie Composite Collider 2D oraz warstw dla optymalizacji.
* **Zarządzanie stanem gry:** Prosty GameManager odpowiadający za ładowanie scen i reset rozgrywki.
* **Dynamiczny HUD:** Aktualizacja TextMeshPro przy zmianach stanu gry.

### Status projektu

Projekt ukończony, brak dalszego rozwoju.

### Author / Autor

* **Author:** Igor Tomkowicz
* **GitHub:** [npnpdev](https://github.com/npnpdev)
* **LinkedIn:** [Igor Tomkowicz](https://www.linkedin.com/in/igor-tomkowicz-a5760b358/)
* **Email:** [npnpdev@gmail.com](mailto:npnpdev@gmail.com)

### License / Licencja

MIT

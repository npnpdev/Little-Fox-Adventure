# Little Fox Adventure

> Odkryj uroki platformówki w stylu retro, prowadząc sprytnego lisa przez niebezpieczne poziomy pełne zagadek i skarbów!

---

## Spis treści

* [Opis projektu](#opis-projektu)
* [Technologie i narzędzia](#technologie-i-narz%C4%99dzia)
* [Funkcjonalności](#funkcjonalno%C5%9Bci)
* [Instalacja i uruchomienie](#instalacja-i-uruchomienie)
* [Jak grać](#jak-gra%C4%87)
* [Struktura repozytorium](#struktura-repozytorium)
* [Wyzwania i rozwiązania](#wyzwania-i-rozwiazania)
* [Status projektu](#status-projektu)
* [Autor / Kontakt](#autor--kontakt)
* [Licencja](#licencja)

---

## Opis projektu

Little Fox Adventure to dwuwymiarowa gra platformowa stworzona w Unity 2022.3.10f1 z wykorzystaniem 2D Universal Render Pipeline. Gracz steruje liskiem, porusza się, skacze i zbiera klucze, by otworzyć przejścia i ukończyć dwa zróżnicowane poziomy.

---

## Technologie i narzędzia

* **Silnik**: Unity 2022.3.10f1 (2D URP)
* **Język**: C#
* **Renderowanie**: Sprite Renderer, Tilemap (Tile Palette, Composite Collider 2D)
* **Fizyka**: Rigidbody2D, Collider2D (Box, Circle), detekcja kolizji i triggerów
* **Interfejs**: Canvas + TextMeshPro dla HUD (punkty, życia, klucze)
* **Kontrola wersji**: Git & GitHub

---

## Funkcjonalności

* **Poruszanie i skoki**: sterowanie klawiszami WSAD / strzałkami i Spacją
* **Zbieranie przedmiotów**: klucze oraz inne bonusy wyzwalane podczas wejścia w trigger
* **System życia**: początkowo 3 życia; reset poziomu po utracie wszystkich
* **Zarządzanie scenami**: dwa poziomy (`Level1`, `Level2`) załadowywane przez GameManager
* **HUD**: dynamiczne wyświetlanie stanu punktów, kluczy i żyć

---

## Struktura repozytorium

```
Little-Fox_Adventure/
├── Assets/
│   ├── Scenes/         # Sceny Unity (Level1, Level2)
│   ├── Scripts/        # Skrypty C# (ruch, logika, GameManager)
│   ├── Sprites/        # Grafiki 2D (postacie, tła, ikony)
│   ├── Prefabs/        # Prefaby: gracz, przeszkody, bonusy
│   └── UI/             # Elementy interfejsu (Canvas, HUD)
├── ProjectSettings/    # Konfiguracja projektu Unity
├── .gitignore
└── README.md
```

---

## Wyzwania i rozwiązania

* **Detekcja kolizji z platformami**: skorzystałem z `Composite Collider 2D` i warstw (`Layers`) dla optymalizacji.
* **Zarządzanie stanem gry**: prosty `GameManager` bez zewnętrznych wzorców, odpowiadający za ładowanie scen i reset rozgrywki.
* **Dynamiczny HUD**: użycie TextMeshPro i aktualizacja tekstów w skryptach przy każdej zmianie stanu.

---

## Status projektu

Projekt jest ukończony i nie jest dalej rozwijany.

---

## Autor / Kontakt

* **Igor Tomkowicz**
* GitHub: [npnpdev](https://github.com/npnpdev)
* LinkedIn: [Igor Tomkowicz](https://www.linkedin.com/in/igor-tomkowicz-a5760b358/)
* E-mail: npnpdev@gmail.com

---

## Licencja

Ten projekt jest dostępny na licencji MIT. Zobacz plik [LICENSE](LICENSE) po szczegóły.

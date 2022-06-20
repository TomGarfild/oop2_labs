# Object Oriented Programming 2 year

## Lab 1
1. (**) Stack, queue, deque (double-ended queue). Реалізації на основі зв’язних списків, масивів, бібліотечних засобів. Стандартні операції для кожної структури. Deque має реалізовувати інтерфейси stack та queue.
2. (****) Графи на основі списку суміжності, матриці суміжності (збереження даних у вершинах та ребрах графів). Додавання та видалення вершин/ребер. Перевірка на зв’язність графу. Визначення відстані між двома вершинами графу.
3. (**) Адреси IPv4 (a1.a2.a3.a4, 0<=ai<=255) та IPv6 (b1:b2:b3:b4:b5:b6:b7:b8, кожна з bi – набір від 1 до 4 шістнадцяткових цифр 0..9a..f). Адреси підмереж у форматі CIDR: address/subnet_bits, де address – адреса в IPv4 форматі, subnet_bits – кількість бітів маски підмережі.

    >(*) за реалізацію пошуку вільного діапазону адрес заданого розміру (підмережі);
    >(*) parsing.

#### Additional features:
    * Unit tests,
    * Documentation.
---
## Lab 2
### Timer
    * Using Blazor WebAssembly
    * Can edit timers
    * Timers are saved in storage
    * Is deployed at
   [Timer](https://d3jl2hjpzvti0j.cloudfront.net/)

---
## Project
## Rock paper scissors game
    * Using asp .net core project for web server
    * Using console application for client
    * Data saved in sql db
   
## Project 2
## Crypto Helper
    Open folder Crypto Helper

## Lab3
Took idea from [video](https://www.youtube.com/watch?v=2moh18sh5p4/)

    * Created wrapper for web client(could not test without it).
    * Downloader for downloading data and formatting it
    * DownloadService can download data synchronously, asynchronously, asynchronously parallel
    * User interface with 3 buttons with sync call(blocks interface), async call(not blocking interface but time is same as sync), async parallel call(fast async)
    * Unit tests

Data(RAM:8GB, Processor:Intel(R) Core(TM) i5-9300H CPU @ 2.40GHz):

    * Sync: 00:00:03.039
    * Async: 00:00:03.3
    * Async parallel: 00:00:00.879
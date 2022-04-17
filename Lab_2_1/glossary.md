# Glossary
### Server
+ JsonWorker       - worker for reading and writin json data
---
+ Account          - model with user's login and password
+ Series           - model with information about users who play and status
+ PrivateSeries    - series that has code
+ TrainingSeries   - series with computer
+ Round            - model with players' moves
---
+ AccountStorage   - storage that works with accounts
+ AuthService      - service for user authentication
+ RoundService     - service for managing rounds
+ SeriesService    - series manager

### Client
+ Menu             - base class for menus
+ RegistrationMenu - menu with registration and login options
+ GameMenu         - menu with with options in what room play
+ RoundMenu        - in private room show create/enter room, and then/in public show move options
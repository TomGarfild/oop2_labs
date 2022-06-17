# TelegramBot Crypto Helper
## About

Telegram bot that gets info about trending cryptocurrency, gainers and losers.
@manage_crypto_bot

## Patterns

1.  **Strategy** - for handling telegram bot's Updates
2.  **Factory** - handles creating telegram bot client
3.  **Command** - objects with request info for handlers
4.  **Mediator** - self written mediator object for managing communication
5.  **Proxy** - cached wrapper for clients
6.  **Template Method** - clients inherit from base client
7.  **Prototype** - cloning strategy, record entities
8.  **State** - in what state is bot, depends on this value what strategies use
9.  **Facade** - services wrappers of managers with BL
10. **Builder** - in tests for models use builders
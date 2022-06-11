# TelegramBot Crypto Helper
## About

Telegram bot that gets info about trending cryptocurrency, gainers and losers.
@manage_crypto_bot

## Patterns

- **Strategy** - for handling telegram bot's Updates
- **Factory** - handles creating telegram bot client
- **Command** - objects with request info for handlers
- **Mediator** - self written mediator object for managing communication
- **Proxy** - cached wrapper for clients
- **Template Method** - clients inherit from base client
- **Prototype** - cloning strategy, record entities
- **State** - in what state is bot, depends on this value what strategies use
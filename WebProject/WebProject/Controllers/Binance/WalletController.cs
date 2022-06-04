using Kernel.Clients;
using Microsoft.AspNetCore.Mvc;

namespace WebProject.Controllers.Binance;

[Route("binance/wallet")]
public class WalletController : Controller
{
    private readonly WalletClient _walletClient;
    public WalletController(WalletClient walletClient)
    {
        _walletClient = walletClient;
    }
}
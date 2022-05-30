using Kernel;
using Kernel.Clients;
using Kernel.Extensions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
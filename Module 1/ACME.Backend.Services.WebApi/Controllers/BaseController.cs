using ACME.Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACME.Backend.Services.WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected readonly IShopUnitOfWork _unitOfWork;

    public BaseController(IShopUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

}

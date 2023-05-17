using ASP.Models;
using ASP.Models.Requests;
using ASP.Models.Responces;

using Authentication.JWT;

using AutoMapper;

using Domain.Services;

using Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApp.Controllers.Abstractions;

using carrierSlot = ASP.Models.Views.CarrierSlot;
using shipperSlot = ASP.Models.Views.ShipperSlot;
using user = ASP.Models.Views.User;

namespace WebApp.Controllers
{
    [ApiController,
        Route("api/[controller]")]
    public sealed class ShipperController : BaseController
    {
        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="AuthorizationService"/>.
        /// </summary>
        public Lazy<AuthorizationService> Authorization { private get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="CarrierSlotService"/>.
        /// </summary>
        public Lazy<CarrierSlotService> CarrierSlot { private get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="ShipperSlotService"/>.
        /// </summary>
        public Lazy<ShipperSlotService> ShipperSlot { private get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CarrierController"/>.
        /// </summary>
        public ShipperController(Lazy<UserManager<AppUser>> users,
                                  Lazy<CarrierService> carrierService,
                                  Lazy<ShipperService> shipperService,
                                  Lazy<IMapper> mapper,
                                  Lazy<AuthorizationService> authorization,
                                  Lazy<CarrierSlotService> carrierSlot,
                                  Lazy<ShipperSlotService> shipperSlot)
            : base(users, mapper, carrierService, shipperService)
        {
            Authorization = authorization;
            CarrierSlot = carrierSlot;
            ShipperSlot = shipperSlot;
        }

        // GET api/<ShipperController>
        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var data =
                await ShipperService
                          .Value
                          .AllAsync();

            if (!data.Any())
            {
                return NotFound<IEnumerable<user.Item>>();
            }

            var list = Mapper.Value.Map<IEnumerable<user.Item>>(data);

            return Ok(list);
        }

        // GET api/<ShipperController>/5/carrier-slot
        [HttpGet("{id}/carrier-slot")]
        public async Task<IActionResult> GetCarrierSlotsAsync(int id)
        {
            var data =
                await CarrierSlot
                          .Value
                          .AllByShipperAsync(id);

            if (data == null)
            {
                return NotFound<carrierSlot.Details>();
            }

            var item = Mapper.Value.Map<carrierSlot.Details>(data);

            return Ok(item);
        }

        // GET api/<ShipperController>/5/carrier-slot/in-work
        [HttpGet("{id}/carrier-slot/in-work")]
        public async Task<IActionResult> GetCarrierInWorkAsync(int id)
        {
            var data =
                await CarrierSlot
                          .Value
                          .AllInWorkByShipperAsync(id);

            if (data == null)
            {
                return NotFound<carrierSlot.Details>();
            }

            var item = Mapper.Value.Map<carrierSlot.Details>(data);

            return Ok(item);
        }

        // GET api/<ShipperController>/5/carrier-slot/complete
        [HttpGet("{id}/carrier-slot/complete")]
        public async Task<IActionResult> GetCarrierCompleteSlotsAsync(int id)
        {
            var data =
                await CarrierSlot
                          .Value
                          .AllCompleteByShipperAsync(id);

            if (data == null)
            {
                return NotFound<carrierSlot.Details>();
            }

            var item = Mapper.Value.Map<carrierSlot.Details>(data);

            return Ok(item);
        }

        // GET api/<ShipperController>/5/shipper-slot
        [HttpGet("{id}/shipper-slot")]
        public async Task<IActionResult> GetShipperSlotsAsync(int id)
        {
            var data =
                await ShipperSlot
                          .Value
                          .AllByShipperAsync(id);

            if (data == null)
            {
                return NotFound<shipperSlot.Details>();
            }

            var item = Mapper.Value.Map<shipperSlot.Details>(data);

            return Ok(item);
        }

        // GET api/<ShipperController>/5/shipper-slot/in-work
        [HttpGet("{id}/shipper-slot/in-work")]
        public async Task<IActionResult> GetShipperInWorkAsync(int id)
        {
            var data =
                await ShipperSlot
                          .Value
                          .AllInWorkByShipperAsync(id);

            if (data == null)
            {
                return NotFound<shipperSlot.Details>();
            }

            var item = Mapper.Value.Map<shipperSlot.Details>(data);

            return Ok(item);
        }

        // GET api/<ShipperController>/5/shipper-slot/complete
        [HttpGet("{id}/shipper-slot/complete")]
        public async Task<IActionResult> GetShipperCompleteAsync(int id)
        {
            var data =
                await ShipperSlot
                          .Value
                          .AllCompleteByShipperAsync(id);

            if (data == null)
            {
                return NotFound<shipperSlot.Details>();
            }

            var item = Mapper.Value.Map<shipperSlot.Details>(data);

            return Ok(item);
        }

        // GET api/<ShipperController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data =
                await ShipperService
                          .Value
                          .GetAsync(id);

            if (data == null)
            {
                return NotFound<user.Details>();
            }

            var item = Mapper.Value.Map<user.Details>(data);

            return Ok(item);
        }

        // POST api/<ShipperController>/register
        [HttpPost("register"),
            AllowAnonymous]
        public async Task<Responce<LoginResponce>> RegisterAsync([FromBody] user.New customer)
        {
            await ShipperService.Value.RegisterAsync(customer);

            var login =
                new LoginRequest
                {
                    UserName = customer.UserName,
                    Password = customer.Password
                };

            var data = await Authorization.Value.MakeAttemptAsync(login);

            return Ok(data);
        }

        // POST api/<ShipperController>/login
        [HttpPost("login"),
            AllowAnonymous]
        public async Task<Responce<LoginResponce>> LoginAsync([FromBody] LoginRequest request)
            => Ok(
                await Authorization.Value.MakeAttemptAsync(request));
    }
}
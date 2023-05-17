using Authentication.JWT;

using AutoMapper;

using Contracts.Enums;

using Domain.Services;

using Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApp.Controllers.Abstractions;

using carrierSlot = ASP.Models.Views.CarrierSlot;

namespace WebApp.Controllers
{
    [ApiController,
        Route("api/[controller]")]
    public sealed class CarrierSlotController : BaseController
    {
        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="DijkstraService"/>.
        /// </summary>
        public Lazy<DijkstraService> DijkstraService { private get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="AuthorizationService"/>.
        /// </summary>
        public Lazy<CarrierSlotService> CarrierSlotService { private get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CarrierController"/>.
        /// </summary>
        public CarrierSlotController(Lazy<UserManager<AppUser>> users,
                                  Lazy<CarrierService> carrierService,
                                  Lazy<ShipperService> shipperService,
                                  Lazy<IMapper> mapper,
                                  Lazy<DijkstraService> dijkstraService,
                                  Lazy<CarrierSlotService> carrierSlotService)
            : base(users, mapper, carrierService, shipperService)
        {
            DijkstraService = dijkstraService;
            CarrierSlotService = carrierSlotService;
        }

        // GET api/<CarrierSlotController>/free
        [HttpGet("free")]
        public async Task<IActionResult> GetFreeAsync()
        {
            var data =
                await CarrierSlotService
                          .Value
                          .AllFreeAsync();

            if (!data.Any())
            {
                return NotFound<IEnumerable<carrierSlot.Item>>();
            }

            var list = Mapper.Value.Map<IEnumerable<carrierSlot.Item>>(data);

            return Ok(list);
        }

        // GET api/<CarrierSlotController>/5/path
        [HttpGet("{id}/path")]
        public async Task<IActionResult> GetPathAsync(int id)
        {
            var data =
                await CarrierSlotService
                          .Value
                          .GetAsync(id);
            var path =
                DijkstraService
                    .Value
                    .FindShortestPath(data);

            if (path == null)
            {
                return NotFound<string>();
            }

            return Ok(path);
        }

        // GET api/<CarrierSlotController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data =
                await CarrierSlotService
                          .Value
                          .GetAsync(id);

            if (data == null)
            {
                return NotFound<carrierSlot.Details>();
            }

            var item = Mapper.Value.Map<carrierSlot.Details>(data);

            return Ok(item);
        }

        // POST api/<CarrierSlotController>
        [HttpPost("")]
        public async Task<IActionResult> RegisterAsync([FromBody] carrierSlot.New customer)
        {
            var user = await this.AppUser;
            
            if (user.Type != UserType.Carrier)
            {
                return this.BadRequest("Вы не можете создавать заявки данного типа.");
            }

            var id = await CarrierSlotService.Value.CreateAsync(customer, await this.GetAuthorizedUserIdAsync());

            return Ok(id);
        }

        // PATCH api/<CarrierSlotController>/5/accept
        [HttpPatch("{id}/accept")]
        public async Task<IActionResult> AcceptAsync(int id)
        {
            var user = await this.AppUser;

            if (user.Type != UserType.Shipper)
            {
                return this.BadRequest("Вы не можете принимать заявки данного типа.");
            }

            await this.CarrierSlotService.Value.AcceptAsync(id, await this.GetAuthorizedUserIdAsync());

            return Ok();
        }

        // PATCH api/<CarrierSlotController>/5/in-work
        [HttpPatch("{id}/in-work")]
        public async Task<IActionResult> InWorkAsync(int id)
        {
            await CarrierSlotService.Value.InWorkAsync(id);

            return Ok();
        }

        // PATCH api/<CarrierSlotController>/5/complete
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await CarrierSlotService.Value.CompleteAsync(id);

            return Ok();
        }
    }
}
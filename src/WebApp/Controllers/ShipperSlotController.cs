using AutoMapper;

using Contracts.Enums;

using Domain.Services;

using Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebApp.Controllers.Abstractions;

using shipperSlot = ASP.Models.Views.ShipperSlot;

namespace WebApp.Controllers
{
    [ApiController,
        Route("api/[controller]")]
    public sealed class ShipperSlotController : BaseController
    {
        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="DijkstraService"/>.
        /// </summary>
        public Lazy<DijkstraService> DijkstraService { private get; init; } = null!;

        /// <summary>
        /// Возвращает экземпляр сервиса <see cref="ShipperSlotService"/>.
        /// </summary>
        public Lazy<ShipperSlotService> ShipperSlotService { private get; init; } = null!;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ShipperSlotController"/>.
        /// </summary>
        public ShipperSlotController(Lazy<UserManager<AppUser>> users,
                                  Lazy<CarrierService> carrierService,
                                  Lazy<ShipperService> shipperService,
                                  Lazy<IMapper> mapper,
                                  Lazy<DijkstraService> dijkstraService,
                                  Lazy<ShipperSlotService> shipperSlotService)
            : base(users, mapper, carrierService, shipperService)
        {
            DijkstraService = dijkstraService;
            ShipperSlotService = shipperSlotService;
        }

        // GET api/<ShipperSlotController>/free
        [HttpGet("free")]
        public async Task<IActionResult> GetFreeAsync()
        {
            var data =
                await ShipperSlotService
                          .Value
                          .AllFreeAsync();

            if (!data.Any())
            {
                return NotFound<IEnumerable<shipperSlot.Item>>();
            }

            var list = Mapper.Value.Map<IEnumerable<shipperSlot.Item>>(data);

            return Ok(list);
        }

        // GET api/<ShipperSlotController>/5/path
        [HttpGet("{id}/path")]
        public async Task<IActionResult> GetPathAsync(int id)
        {
            var data =
                await ShipperSlotService
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
        
        // GET api/<ShipperSlotController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data =
                await ShipperSlotService
                          .Value
                          .GetAsync(id);

            if (data == null)
            {
                return NotFound<shipperSlot.Details>();
            }

            var item = Mapper.Value.Map<shipperSlot.Details>(data);

            return Ok(item);
        }

        // POST api/<ShipperSlotController>
        [HttpPost("")]
        public async Task<IActionResult> RegisterAsync([FromBody] shipperSlot.New customer)
        {
            var user = await this.AppUser;
            
            if (user.Type != UserType.Shipper)
            {
                return this.BadRequest("Вы не можете создавать заявки данного типа.");
            }

            var id = await ShipperSlotService.Value.CreateAsync(customer, await this.GetAuthorizedUserIdAsync());

            return Ok(id);
        }

        // PATCH api/<ShipperSlotController>/5/accept
        [HttpPatch("{id}/accept")]
        public async Task<IActionResult> AcceptAsync(int id)
        {
            var user = await this.AppUser;

            if (user.Type != UserType.Carrier)
            {
                return this.BadRequest("Вы не можете принимать заявки данного типа.");
            }

            await this.ShipperSlotService.Value.AcceptAsync(id, await this.GetAuthorizedUserIdAsync());

            return Ok();
        }

        // PATCH api/<ShipperSlotController>/5/in-work
        [HttpPatch("{id}/in-work")]
        public async Task<IActionResult> InWorkAsync(int id)
        {
            await ShipperSlotService.Value.InWorkAsync(id);

            return Ok();
        }

        // PATCH api/<ShipperSlotController>/5/complete
        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await ShipperSlotService.Value.CompleteAsync(id);

            return Ok();
        }
    }
}
using AutoMapper;

using Common.Exceptions;

using Contracts;

using Domain.Services.Abstractions;

using Models.DataBase;

using Store.Decorators;
using Store.Extensions;

using shipperSlot = ASP.Models.Views.ShipperSlot;

namespace Domain.Services
{
    /// <summary>
    /// Сервис работы с заявками грузоперевозчика.
    /// </summary>
    public sealed class ShipperSlotService : BaseService<ShipperSlot>
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ShipperSlotService"/>.
        /// </summary>
        public ShipperSlotService(IAsyncRepository<ShipperSlot> repository,
                               Lazy<IMapper> mapper)
            : base(repository, mapper)
        {}

        /// <summary>
        /// Возвращает все доступные заявки грозоотправителей в асинхронном режиме.
        /// </summary>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявкок грозоотправителей.</returns>
        public Task<List<ShipperSlot>> AllFreeAsync()
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => !x.IsInWork);

        /// <summary>
        /// Возвращает все заявки грозоотправителя по его идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоотправителя.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грозоотправителя.</returns>
        public Task<List<ShipperSlot>> AllByShipperAsync(int userId)
             => Repository
                    .MutateQuery(x => x.IncludeAll())
                    .ManyAsync(x => x.ShipperId == userId);

        /// <summary>
        /// Возвращает все выполненые заявки грозоотправителя по его идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоотправителя.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грозоотправителя.</returns>
        public Task<List<ShipperSlot>> AllCompleteByShipperAsync(int userId)
           => Repository
                  .MutateQuery(x => x.IncludeAll())
                  .ManyAsync(x => x.IsComplete && x.ShipperId == userId);

        /// <summary>
        /// Возвращает все заявки грозоотправителя в работе по его идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоотправителя.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грозоотправителя.</returns>
        public Task<List<ShipperSlot>> AllInWorkByShipperAsync(int userId)
           => Repository
                  .MutateQuery(x => x.IncludeAll())
                  .ManyAsync(x => x.IsInWork && x.ShipperId == userId);

        /// <summary>
        /// Возвращает все заявки грозоотправителя по идентификатору грузоперевозчика в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоперевозчика.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грозоотправителя.</returns>
        public Task<List<ShipperSlot>> AllByCarrierAsync(int userId)
           => Repository
                  .MutateQuery(x => x.IncludeAll())
                  .ManyAsync(x => x.CarrierId == userId);

        /// <summary>
        /// Возвращает все выполненые заявки грозоотправителей по идентификатору грузоперевозчика в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоперевозчика.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грозоотправителей.</returns>
        public Task<List<ShipperSlot>> AllCompleteByCarrierAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.IsComplete && x.CarrierId == userId);

        /// <summary>
        /// Возвращает все заявки грозоотправителей в работе по идентификатору грузоперевозчика в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоперевозчика.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грозоотправителей.</returns>
        public Task<List<ShipperSlot>> AllInWorkByCarrierAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.IsInWork && x.CarrierId == userId);

        /// <summary>
        /// Возвращает заявку грозоотправителея по ключу в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит заявку грозоотправителея.</returns>
        public async Task<ShipperSlot> GetAsync(int id)
        {
            var slot =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            return slot;
        }

        /// <summary>
        /// Создает заявку грозоотправителея в асинхронном режиме.
        /// </summary>
        /// <param name="shipperSlot">Создаваемая заявка.</param>
        /// <param name="shipperId">Идентификатор грозоотправителея.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор созданой заявки.</returns>
        public async Task<int> CreateAsync(shipperSlot.New shipperSlot, int shipperId)
        {
            var entity           = this.Mapper.Value.Map<ShipperSlot>(shipperSlot);
                entity.ShipperId = shipperId;

            var id = await Repository.CreateAsync(entity);

            return id;
        }

        /// <summary>
        /// Принимает заявку грозоотправителея отправителем в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <param name="shipperId">Идентификатор грузоотправителя.</param>
        public async Task AcceptAsync(int id, int carrierId)
        {
            var slot =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            slot.CarrierId = carrierId;

            await this.Repository.UpdateAsync(slot);
        }

        /// <summary>
        /// Помечает заявку как в работе в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        public async Task InWorkAsync(int id)
        {
            var slot =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            if (slot.IsComplete)
            {
                throw
                  new Exception("Заказ уже завершен");
            }

            slot.IsInWork = true;

            await this.Repository.UpdateAsync(slot);
        }

        /// <summary>
        /// Помечает заявку как выполненую в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        public async Task CompleteAsync(int id)
        {
            var slot =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            if (!slot.IsInWork)
            {
                throw
                  new Exception("Заказ еще не выполнен");
            }

            slot.IsInWork = false;

            slot.IsComplete = true;

            await this.Repository.UpdateAsync(slot);
        }
    }
}

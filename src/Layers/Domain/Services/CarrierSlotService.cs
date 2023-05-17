using AutoMapper;

using Common.Exceptions;

using Contracts;

using Domain.Services.Abstractions;

using Models.DataBase;

using Store.Decorators;
using Store.Extensions;

using carrierSlot = ASP.Models.Views.CarrierSlot;

namespace Domain.Services
{
    /// <summary>
    /// Сервис работы с заявками грузоперевозчика.
    /// </summary>
    public sealed class CarrierSlotService : BaseService<CarrierSlot>
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="CarrierSlotService"/>.
        /// </summary>
        public CarrierSlotService(IAsyncRepository<CarrierSlot> repository,
                               Lazy<IMapper> mapper)
            : base(repository, mapper)
        {}

        /// <summary>
        /// Возвращает все доступные заявки грузоперевозчиков в асинхронном режиме.
        /// </summary>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявкок грузоперевозчиков.</returns>
        public Task<List<CarrierSlot>> AllFreeAsync()
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => !x.IsInWork);

        /// <summary>
        /// Возвращает все заявки грузоперевозчика по его идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоперевозчика..</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грузоперевозчика.</returns>
        public Task<List<CarrierSlot>> AllByCarrierAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.CarrierId == userId);

        /// <summary>
        /// Возвращает все выполненые заявки грузоперевозчика по его идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоперевозчика..</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грузоперевозчика.</returns>
        public Task<List<CarrierSlot>> AllCompleteByCarrierAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.IsComplete && x.CarrierId == userId);

        /// <summary>
        /// Возвращает все заявки грузоперевозчика в работе по его идентификатору в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоперевозчика..</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грузоперевозчика.</returns>
        public Task<List<CarrierSlot>> AllInWorkByCarrierAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.IsInWork && x.CarrierId == userId);

        /// <summary>
        /// Возвращает все заявки грузоперевозчиков по идентификатору грузоотправителя в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоотправителя.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грузоперевозчиков.</returns>
        public Task<List<CarrierSlot>> AllByShipperAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.ShipperId == userId);

        /// <summary>
        /// Возвращает все выполненые заявки грузоперевозчиков по идентификатору грузоотправителя в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоотправителя.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грузоперевозчиков.</returns>
        public Task<List<CarrierSlot>> AllCompleteByShipperAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.IsComplete && x.ShipperId == userId);

        /// <summary>
        /// Возвращает все заявки грузоперевозчиков в работе по идентификатору грузоотправителя в асинхронном режиме.
        /// </summary>
        /// <param name="userId">Идентификатор грузоотправителя.</param>
        /// <returns> Задача, представляющая асинхронную операцию. Результат задачи содержит список заявок грузоперевозчиков.</returns>
        public Task<List<CarrierSlot>> AllInWorkByShipperAsync(int userId)
            => Repository
                   .MutateQuery(x => x.IncludeAll())
                   .ManyAsync(x => x.IsInWork && x.ShipperId == userId);

        /// <summary>
        /// Возвращает заявку грузоперевозчика по ключу в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит заявку грузоперевозчика.</returns>
        public async Task<CarrierSlot> GetAsync(int id)
        {
            var slot =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            return slot;
        }

        /// <summary>
        /// Создает заявку грузоперевозчика в асинхронном режиме.
        /// </summary>
        /// <param name="carrierSlot">Создаваемая заявка.</param>
        /// <param name="carrierId">Идентификатор грузоперевозчика.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит идентификатор созданой заявки.</returns>
        public async Task<int> CreateAsync(carrierSlot.New carrierSlot, int carrierId)
        {
            var entity       = this.Mapper.Value.Map<CarrierSlot>(carrierSlot);
            entity.CarrierId = carrierId;

            var id = await Repository.CreateAsync(entity);

            return id;
        }

        /// <summary>
        /// Принимает заявку грузоперевозчика отправителем в асинхронном режиме.
        /// </summary>
        /// <param name="id">Идентификатор заявки.</param>
        /// <param name="shipperId">Идентификатор грузоотправителя.</param>
        public async Task AcceptAsync(int id, int shipperId)
        {
            var slot =
                await Repository
                          .MutateQuery(x => x.IncludeAll())
                          .GetAsync(id)
                ?? throw new NotFoundException(id);

            slot.ShipperId = shipperId;

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

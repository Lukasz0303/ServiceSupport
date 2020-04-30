using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NLog;
using ServiceSupport.Core.Domain;
using ServiceSupport.Core.Repositories;
using ServiceSupport.Infrastructure.Services.DeviceGroup;
using ServiceSupport.Infrastructure.Services.ServiceOrderGroup;
using ServiceSupport.Infrastructure.Services.ShopGroup;
using ServiceSupport.Infrastructure.Services.UserGroup;

namespace ServiceSupport.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;

        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IShopRepository _shopRepository;

        public DataInitializer(IUserService userService, IUserRepository userRepository,
            IServiceOrderRepository serviceOrderRepository, IDeviceRepository deviceRepository, 
            IShopRepository shopRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            _deviceRepository = deviceRepository;
            _shopRepository = shopRepository;
            _serviceOrderRepository = serviceOrderRepository;
        }

    public async Task SeedAsync()
        {
            Logger.Info("Initializing data...");
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "l.zzzielinski@gmail.com", "lukasz", "test", "admin"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "m.kowalski@gmail.com", "marek", "test", "serviceman"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "p.jankowski@gmail.com", "pawel", "test", "servicecoordinator"));
            Logger.Info("Created users: admin, serviceman, servicecoordinator");

            List<Device> devices = new List<Device>()
            {
                new Device(Guid.NewGuid(), "ab12", DateTime.Now, DateTime.Now,
                    new Shop(Guid.NewGuid(), "Toruñ Kwiatowa 18/4",
                        new Person("£ukasz", "Zieliñski", "l.zzzielinski@gmail.com", "530230655"),
                            "001", "520640330")),
                new Device(Guid.NewGuid(), "bc34", DateTime.Now, DateTime.Now.AddDays(-1),
                    new Shop(Guid.NewGuid(), "Bydgoszcz Polna 22/4",
                        new Person("Marek", "Kowalski", "m.kowalski@gmail.com", "540270695"),
                            "002", "524740360")),
                new Device(Guid.NewGuid(), "b69", DateTime.Now.AddDays(-1), DateTime.Now,
                    new Shop(Guid.NewGuid(), "Warszwa Jana III Sobieskiego 12/3",
                        new Person("Pawe³", "Wiœniewski", "p.wisniewski@gmail.com", "560260895"),
                            "003", "524745360"))
            };

            List<ServiceOrder> serviceOrders = new List<ServiceOrder>()
            {
                new ServiceOrder(Guid.NewGuid(),
                new Person("£ukasz", "Zieliñski", "l.zzzielinski@gmail.com", "530230655"),
                new Person("Pawe³", "Wiœniewski", "p.wisniewski@gmail.com", "560360895")),
                new ServiceOrder(Guid.NewGuid(),
                new Person("£ukasz", "Zieliñski", "l.zzzielinski@gmail.com", "530230655"),
                new Person("Marek", "Kowalski", "m.kowalski@gmail.com", "540270495")),
                new ServiceOrder(Guid.NewGuid(),
                new Person("£ukasz", "Zieliñski", "l.zzzielinski@gmail.com", "53023655"),
                new Person("Marcin", "Wolski", "m.wolski@gmail.com", "560230895")),
                                new ServiceOrder(Guid.NewGuid(),
                new Person("£ukasz", "Zieliñski", "l.zzzielinski@gmail.com", "53023655")),
                new ServiceOrder(Guid.NewGuid(),
                new Person("£ukasz", "Zieliñski", "l.zzzielinski@gmail.com", "53023655"))
            };

            serviceOrders[0].AddServiceOrderDescription("Naprawa Wilanoska 36", "Urz¹dznie mia³o problem z po³aczeniem internetowym. " +
                "Problem ze statycznymi adresami IP zosta³ rozwi¹zany. Wina po stronie klinta.",
                new Person("Pawe³", "Wiœniewski", "p.wisniewski@gmail.com", "560360895"),"true");
            serviceOrders[1].AddServiceOrderDescription("Brak danych", "Urz¹dznie nie wysy³a³o danych. " +
                "Problem z niepoprawn¹ konfiguracj¹ urz¹dzenia. Wina po naszej stronie.",
                new Person("Marek", "Kowalski", "m.kowalski@gmail.com", "540270495"),"true");
            serviceOrders[2].AddServiceOrderDescription("Warszawa PL35", "Uszkodzona karta SD. " +
                "Nowa karta zostanie wys³ana do klinta i zamontowana. Naprawa w raczej ramach gwarncji, " +
                "aczkolwiek karta mog³abyæ naruszona przez klinta.",
                new Person("Marek", "Kowalski", "m.kowalski@gmail.com", "540270495"),"false");
            //serviceOrders[2].AddServiceOrderDescription("Warszawa PL35", "Uszkodzona karta SD. " +
            //    "Klient dosta³ i wymini³ kartê. Urzadzenie dzia³a poprawnie.",
            //    new Person("Marek", "Kowalski", "m.kowalski@gmail.com", "540270495"),true);

            devices[0].AddServiceOrder(serviceOrders[0]);
            devices[1].AddServiceOrder(serviceOrders[1]);
            devices[2].AddServiceOrder(serviceOrders[2]);
            devices[2].AddServiceOrder(serviceOrders[2]);

            foreach (var serviceOrder in serviceOrders)
            {
                _serviceOrderRepository.AddAsync(serviceOrder);
            }

            foreach (var device in devices)
            {
                _deviceRepository.AddAsync(device);
            }

            Logger.Info("Add serviceOrders with ServiceOrderDescriptions");

            foreach (var device in await _deviceRepository.GetAllAsync())
            {
                _shopRepository.AddAsync(device.Shop);
                _shopRepository.AddShopTime(device.Shop.Id,DayOfWeek.Monday, "10:00", "18:00");
                _shopRepository.AddShopTime(device.Shop.Id, DayOfWeek.Tuesday, "10:00", "18:00");
                _shopRepository.AddShopTime(device.Shop.Id, DayOfWeek.Wednesday, "10:00", "18:00");
                _shopRepository.AddShopTime(device.Shop.Id, DayOfWeek.Thursday, "10:00", "18:00");
                _shopRepository.AddShopTime(device.Shop.Id, DayOfWeek.Friday, "10:00", "18:00");
                _shopRepository.AddShopTime(device.Shop.Id, DayOfWeek.Saturday, "10:00", "18:00");
                _shopRepository.AddShopTime(device.Shop.Id, DayOfWeek.Saturday, "10:00", "18:00");
            }
            Logger.Info("Add shops and set shopTimes");

            await Task.WhenAll(tasks);
            Logger.Info("Data was initialized.");
        }
    }
}
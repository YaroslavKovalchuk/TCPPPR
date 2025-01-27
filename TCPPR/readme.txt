TCPPR_Server                     // Коренева папка проекту сервера
├── Program.cs                    // Точка входу в додаток, конфігурація служб і запуск сервера
├── appsettings.json              // Файл конфігурації (з’єднання з базою даних, параметри TCP-сервера та ін.)
│
├── Data                          // Робота з базою даних
│   ├── DbContext
│   │   └── AppDbContext.cs       // Контекст бази даних для взаємодії з SQL Server
│   └── Entities                  // Моделі даних для бази даних
│       ├── User.cs               // Модель користувача (логін, пароль, посада, рівень доступу)
│       ├── Equipment.cs          // Модель обладнання (інвентарний номер, QR-код, історія обслуговування)
│       ├── SparePart.cs          // Модель запасної частини (штрих-код, постачальник, наявність)
│       ├── MaintenanceRequest.cs // Модель заявки на ремонт (опис, статус, фото, модуль)
│       └── StorageLocation.cs    // Модель зберігання (місце, QR-код, тип матеріалу)
│
├── Services                      // Логіка бізнес-процесів
│   ├── Interfaces
│   │   ├── IUserService.cs       // Інтерфейс для управління користувачами (авторизація, реєстрація)
│   │   ├── IEquipmentService.cs  // Інтерфейс для обліку та управління обладнанням
│   │   ├── IInventoryService.cs  // Інтерфейс для управління запасами (доступ, списання)
│   │   └── IMaintenanceService.cs// Інтерфейс для обробки заявок на ремонт
│   └── Services
│       ├── UserService.cs        // Реалізація IUserService, управління користувачами
│       ├── EquipmentService.cs   // Реалізація IEquipmentService, управління обладнанням
│       ├── InventoryService.cs   // Реалізація IInventoryService, управління запасами
│       └── MaintenanceService.cs // Реалізація IMaintenanceService, обробка заявок на ремонт
│
├── Server                        // Реалізація TCP-сервера для обробки клієнтських запитів
│   ├── TcpServer.cs              // Основний клас TCP-сервера для підключення та обробки запитів клієнтів
│   └── ConnectionHandler.cs      // Клас для обробки підключень (асинхронна обробка кожного клієнта)
│
├── Handlers                      // Обробка запитів, отриманих від клієнтів
│   ├── RequestHandler.cs         // Головний обробник запитів (авторизація, управління заявками)
│   ├── MaintenanceRequestHandler.cs // Обробник запитів, пов’язаних із заявками на ремонт
│   ├── InventoryRequestHandler.cs // Обробник запитів, пов’язаних із запасами
│   ├── EquipmentRequestHandler.cs // Обробник запитів, пов’язаних із обладнанням
│   └── UserRequestHandler.cs      // Обробник запитів, пов’язаних із користувачами
│
├── Helpers                       // Допоміжні класи
│   ├── PasswordHelper.cs         // Хешування та перевірка паролів
│   ├── QRCodeHelper.cs           // Генерація та обробка QR-кодів для обладнання і місць зберігання
│   ├── FileHelper.cs             // Обробка фото та файлів (збереження прикріплених до заявок фото)
│   └── VoiceInputHelper.cs       // Обробка голосового вводу тексту для заявок
│
├── Files                         // Папка для зберігання файлів
│   ├── MaintenancePhotos         // Папка для зберігання фото, прикріплених до заявок на ремонт
│   └── OtherFiles                // Інші файли, які можуть використовуватись у програмі
│
└── Utilities                     // Загальні утиліти для підтримки роботи сервера
    ├── Logger.cs                 // Логування запитів, помилок та важливих подій
    └── ConfigurationManager.cs   // Керування конфігурацією програми (завантаження з appsettings.json)
Опис основних компонентів
Data: Ця папка містить усі моделі бази даних, які відображають структуру таблиць SQL Server, і контекст бази даних AppDbContext.
Тут знаходяться класи, які визначають структуру таких сутностей, як User (користувачі), Equipment (обладнання),
SparePart (запасні частини), MaintenanceRequest (заявки на ремонт) та StorageLocation (місця зберігання).

Services: Папка містить інтерфейси та реалізації сервісів, що обробляють бізнес-логіку програми.
Вона включає сервіси для управління користувачами (UserService), обладнанням (EquipmentService),
інвентарем (InventoryService) і заявками на ремонт (MaintenanceService).

Server: Основна реалізація TCP-сервера (TcpServer.cs) та клас для асинхронної обробки підключень (ConnectionHandler.cs).
TCP-сервер отримує запити від клієнтів, створює нові підключення і передає їх до відповідних обробників у папці Handlers.

Handlers: Ця папка містить різні обробники для обробки запитів, що надходять від клієнтів.
Головний RequestHandler розпізнає тип запиту та передає його до відповідного обробника 
(наприклад, UserRequestHandler для запитів про користувачів або MaintenanceRequestHandler для заявок на ремонт).

Helpers: Допоміжні класи для загальних завдань, таких як хешування паролів (PasswordHelper), обробка QR-кодів (QRCodeHelper),
обробка прикріплених фото (FileHelper) і голосового вводу (VoiceInputHelper).

Files: Ця папка організована для зберігання файлів, які надходять від клієнтів, таких як фото, прикріплені до заявок на ремонт,
та інші файли, які можуть бути необхідними для обробки запитів.

Utilities: Загальні утиліти для підтримки сервера, включаючи Logger для логування всіх подій, помилок і запитів,
а також ConfigurationManager для зручного доступу до налаштувань, що зберігаються в appsettings.json.
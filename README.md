# Test
Тестовое задание
1) Winforms клиент на .net C# - окошко с полями ввода:
  - адрес URL веб-сервиса, к которому будем обращаться
  - API-key (для авторизации вызова)
  - кнопка - ВЫЗВАТЬ МЕТОД Hello (это будет единственный метод)
  - поле вывода для того, чтобы вывести полученный ответ, либо ошибку вызова
2) Сервис Web api (желательно на .net core, но можно и на любом .net C#)
  - единственный веб-метод - Hello! (куда передаётся единственный параметр строка)
  - у сервиса есть мини БД SQL (обращение к ней через entity framework, БД желательно делать через code first), где лежат -
    а. API-ключ для авторизации запроса (его сверяем при вызове, если не совпадает - даём ошибку, например, unauthorized)
    б. Что отвечать на какой запрос, например, если передали строку "Request", то ответить "Response"
  - Предусмотреть логирование всех запросов и ответов в текстовый лог-файл
При получении запроса сервис должен:
  а) проверить API-key - убедиться, что запрос авторизован
  б) Просмотреть в БД, есть ли для переданного значения запроса нужный ответ. Если есть - отдать его в ответ, если нет - в ответ выдать unknown request

# "Сервис уведомлений" 
### *Master*: [![master status](http://stp-bck.tomsknipi.ru/TDDM/NotificationsService/badges/master/pipeline.svg)](http://stp-bck.tomsknipi.ru/TDDM/NotificationsService/commits/master) *Develop*: [![develop status](http://stp-bck.tomsknipi.ru/TDDM/NotificationsService/badges/develop/pipeline.svg)](http://stp-bck.tomsknipi.ru/TDDM/NotificationsService/commits/develop)   

## Минимальные системные требования:

|                              |                       |
|------------------------------|:---------------------:|
| *Процессор*                  |  1 ГГц                |
| *ОЗУ*                        |  256 МБ               |
| *Место на диске (минимум)*   |  100 МБ               |
| *Поддерживаемые ОС*          |  MS Windows 7 и старше|
| *.NET*                       |  4.6.1 и выше         |

## Анализ кода
Анализ кода выполняется при помощи SonarQube:
* [Для ветки develop](http://nipi-code.tomsknipi.ru:9000/dashboard?id=ORPPO.NotificationsService.Develop). Запускается автоматически при слиянии любой ветки в ветку `develop`.
* [Для всех остальных веток](http://nipi-code.tomsknipi.ru:9000/dashboard?id=ORPPO.NotificationsService.Branches). Запускается вручную через CI - например, можно применять для проверки кода перед отправкой merge request'ов.

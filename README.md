# Computer Working Status Notification

Merhaba, SignalR ve RabbitMQ teknolojilerini kullanmak için geliştirdiğim basit bir proje.

## Ne İşe Yarar?
Bir ve birden fazla bilgisayarı ortak bir sunucuya bağlayarak sunucunun bilgisayarları kontrol etmesini sağlar. Belirli bir süre sinyal alınamayan bilgisayar olursa mail gönderilir.

Not: Email gönderme işlemi için RabbitMq kullanılmıştır. Bildirim türü değiştirlirse RabbitMq'ya gerek kalmaz.

### Projede Kullanılanlar
- .Net 5 Framework
- C#
- Katmanlı Mimari
- SignalR
- RabbitMq

### Konfigürasyonlar

Bilgisayarın Sunucuya kaç saniyede bir sinyal göndereceğini Client/HubManager dizini altında SendSignalLoop methodu signalTime değişkeninden saniye olarak belirtebilirsiniz.

Mail Gönderme için Email/GmailEmailSender dizini altında ayarlara erişebilirsiniz. Varsayılan olarak Gmail eklenmiştir. 

Mailde yazan yazıyı düzenlemek için Notificator/EmailNotificator dizininin altında Inform methodundan erişebilirsiniz.

### Demo Video
[![Alt text](https://raw.githubusercontent.com/ozanercan/ComputerWorkingStatusNotification/master/Preview/Thumbnail.JPG)](https://www.youtube.com/watch?v=pggAZKURz5c)


### License
MIT

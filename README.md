## Rehber Mikro Servis Uygulaması

### Prerequisites
```
`[.Net 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)`
`[Git](https://git-scm.com/downloads)`
`[MongoDb](https://hub.docker.com/_/mongo)`
`[RabbitMQ](https://hub.docker.com/_/rabbitmq)`
```

### Projenin Klonlanması
Projeyi `git https://github.com/onselaydin/GuidMs.git .` komutu ile klonlayabiliriz.

### Servislerin Çalıştırılması
- Öncelikle Guide servisi çalıştırılır. Guide servisi 5000 portundan hizmet verir. ´dotnet GuideService.Guide.dll`
- Web servisi 5001 portundan hizmet vermekte. `dotnet Guide.Web.dll`
- Rapor servisi için `dotnet GuideService.Report.dll` servisi çalıştırılmalıdır. Bu servis 5002 den hizmet verir.


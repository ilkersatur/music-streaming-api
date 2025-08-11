# MusicStreamingApi

MusicStreamingApi, CSV dosyasından müzik oynatma loglarını okuyup, bu veriler üzerinde çeşitli analizler yapan basit bir ASP.NET Core Web API projesidir.

![MusicStreamingApi](https://github.com/ilkersatur/music-streaming-api/blob/main/1.png)

---

## Özellikler

- CSV dosyasından oynatma verilerini okuma
- Belirli tarihe göre kullanıcıların farklı şarkı çalma dağılımını alma
- Belirli tarihte, belirli sayıda farklı şarkı çalan kullanıcı sayısını sorgulama
- Belirli tarihte en çok farklı şarkı çalan kullanıcı sayısını öğrenme
- Tüm oynatma loglarını listeleme

---

## Proje Yapısı ve Ana Bileşenler

### Controller: `PlayLogsController`

API uç noktaları (endpoints) burada tanımlanmıştır:

| Endpoint                        | HTTP Metodu | Açıklama                                                                                     |
|--------------------------------|-------------|----------------------------------------------------------------------------------------------|
| `/api/playlogs`                | GET         | Tüm oynatma loglarını listeler.                                                             |
| `/api/playlogs/distribution`   | GET         | Verilen tarihte kullanıcıların farklı şarkı çalma sayılarının dağılımını döner.              |
| `/api/playlogs/usercount`      | GET         | Verilen tarihte, belirli sayıda farklı şarkı çalan kullanıcı sayısını döner.                  |
| `/api/playlogs/maxdistinctsongcount` | GET  | Verilen tarihte en çok farklı şarkı çalan kullanıcı sayısını döner.                          |

**Query parametreleri** (varsayılan değerler ile):
- `day` (int): Gün, varsayılan 10
- `month` (int): Ay, varsayılan 8
- `year` (int): Yıl, varsayılan 2016
- `distinctSongCount` (int, sadece usercount için): Farklı şarkı sayısı, varsayılan 346

---

### Servis: `PlayLogsService`

İş mantığı ve veri işleme metodları burada bulunur:

- `GetPlayLogs()`  
  Tüm oynatma loglarını CSV dosyasından okuyup listeler.

- `GetDistinctPlayDistribution(DateTime date)`  
  Verilen tarihteki oynatma kayıtlarını filtreleyerek, kullanıcıların kaç farklı şarkı çaldığını ve bu sayıların dağılımını döner.

  ![MusicStreamingApi](https://github.com/ilkersatur/music-streaming-api/blob/main/2.png)

- `GetUserCountByDistinctSongCount(DateTime date, int distinctSongCount)`  
  Verilen tarihte, tam olarak `distinctSongCount` farklı şarkı çalan kullanıcı sayısını döner.

  ![MusicStreamingApi](https://github.com/ilkersatur/music-streaming-api/blob/main/3.png)

- `GetMaxDistinctSongCount(DateTime date)`  
  Verilen tarihte en çok farklı şarkı çalan kullanıcının farklı şarkı sayısını döner.

    ![MusicStreamingApi](https://github.com/ilkersatur/music-streaming-api/blob/main/4.png)
---

### Yardımcı Sınıf: `CsvReaderHelper`

CSV dosyasından oynatma verilerini okur. Dosya yolu `Data/exhibitA-input.csv` olarak sabittir.

- `ReadCsvFile()`  
  CSV dosyasını okur, her satırı `PlayLogsModel` nesnesine dönüştürür ve liste olarak döner.

---

## Model Sınıfları

- `PlayLogsModel`  
  Oynatma kaydı (PlayId, SongId, ClientId, PlayTimeSpan)

- `PlayCountDistributionModel`  
  Farklı şarkı çalma sayısı ve bu sayıya sahip kullanıcı sayısı

---

## Kullanım Örnekleri

- Tüm oynatma loglarını almak için:  
  `GET /api/playlogs`

- Belirli bir tarih için oynatma dağılımı almak:  
  `GET /api/playlogs/distribution?day=10&month=8&year=2016`

- Belirli sayıda farklı şarkı çalan kullanıcı sayısını almak:  
  `GET /api/playlogs/usercount?distinctSongCount=100&day=10&month=8&year=2016`

- Belirli tarih için en çok farklı şarkı çalan kullanıcı sayısı:  
  `GET /api/playlogs/maxdistinctsongcount?day=10&month=8&year=2016`

---

## Hata Yönetimi

- Geçersiz tarih parametreleri gönderilirse 400 Bad Request döner.
- `distinctSongCount` 0 veya negatif ise 400 Bad Request döner.
- CSV dosyası bulunamazsa istisna fırlatılır.

---

## Gereksinimler

- .NET 6+ veya üstü
- CSV dosyasının `Data/exhibitA-input.csv` konumunda bulunması gerekir.

---

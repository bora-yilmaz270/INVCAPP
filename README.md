# INVCAPP API

## Proje Hakkında
INVCAPP, faturaların yönetimi ve işlenmesi için tasarlanmış bir RESTful API'dir. Bu proje, fatura yükleme, listeleme ve detay sorgulama gibi işlevleri sağlayarak frontend uygulamalarının ihtiyaçlarını karşılamak üzere tasarlanmıştır.
Ayrıca Emailjob ile işlenmemiş faturalar için müşterilere bilgilendirme maili göndermektedir.

## Özellikler
- Yeni belge yükleme
- Belge başlıklarının listelenmesi
- Belge detaylarının sorgulanması
- Emailjob ile bilgilendirme maili gönderilmesi 

## Kurulum
1. Bu repoyu klonlayın: `git clone https://github.com/bora-yilmaz270/INVCAPP.git`
2. Visual Studio'da açın.
3. MSSQL veritabanı bağlantınızı yapılandırın.
4. Uygulamayı derleyin ve çalıştırın.
5. Emailjob uygulamasını bilgisayarınıza kurun.

## API Endpointleri
- Yeni Fatura Yükleme: `POST /api/Invoice`
- Fatura Başlıklarını Listeleme: `GET /api/Invoice/headers`
- Fatura Detaylarını Sorgulama: `GET /api/Invoice/{invoiceId}`

## Modeller
### InvoiceCreateDto
- `invoiceHeader`: InvoiceHeaderCreateDto
- `invoiceLines`: Array of InvoiceLineCreateDto

### InvoiceHeaderCreateDto
- `invoiceId`: string
- `senderTitle`: string
- `receiverTitle`: string
- `date`: string
- `email`: string

### InvoiceLineCreateDto
- `name`: string
- `quantity`: integer
- `unitCode`: string
- `unitPrice`: double

## Geliştirme Ortamı
- Visual Studio
- MSSQL

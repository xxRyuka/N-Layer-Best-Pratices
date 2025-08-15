using Microsoft.AspNetCore.Mvc;
using N_LayerBestPratice.Services.Results;

namespace N_LayerBestPratice.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
// CustomControllerBase, tüm API denetleyicilerinizin temelini oluşturan soyutlayıcı bir sınıftır.
// Bu sınıf, ortak HTTP yanıtlarını standartlaştırmak ve tekrarı önlemek için kullanılır.
// Böylece her controller kendi iş mantığını yazarken, standart yanıt işleme mantığını bu sınıftan miras alır.
public class CustomControllerBase : ControllerBase
{
    /// <summary>
    /// Generic tipte bir Result<T> alır ve ona göre uygun HTTP yanıtını döner.
    /// </summary>
    /// <typeparam name="T">Result nesnesinin taşıdığı veri tipi (örneğin ProductDto, List<ProductDto> vb.)</typeparam>
    /// <param name="result">İşlem sonucu bilgisi ve varsa veriyi içeren Result nesnesi</param>
    /// <returns>HTTP durumu ve gerekirse veri veya hata mesajı içeren IActionResult</returns>
    /// <remarks>
    /// Bu metot, API metodlarından dönen Result<T> nesnelerini ortak şekilde işler:
    /// - Başarı durumunda 200 OK ve data döner.
    /// - İçerik yoksa 204 NoContent.
    /// - Hata durumlarında uygun status code ve hata mesajlarını içeren cevaplar oluşturur.
    /// </remarks>
    [NonAction] // Bu metot doğrudan HTTP endpoint olarak kullanılamaz, sadece diğer metotlar tarafından çağrılır.
    public IActionResult CreateActionResult<T>(Result<T> result)
    {
        if (result == null)
        {
            // Result nesnesi null ise, bu bir yanlış kullanım veya beklenmeyen durumdur.
            // Bu durumda 400 BadRequest ile hata bildiriyoruz.
            return BadRequest("Result cannot be null.");
        }

        // ResultStatus durumuna göre HTTP yanıtı oluşturuluyor.
        // switch ifadesiyle okunabilirlik artırılıyor.
        return result.Status switch
        {
            ResultStatus.Success => Ok(result), // Başarı: 200 OK + data
            ResultStatus.Error => BadRequest(result), // Hata: 400 BadRequest + hata detayları
            ResultStatus.NotFound => NotFound(result), // Bulunamadı: 404 NotFound + hata mesajları
            ResultStatus.ValidationError => BadRequest(result), // Doğrulama hatası: 400 BadRequest
            ResultStatus.UnAuthorized => Unauthorized(result), // Yetkisiz: 401 Unauthorized
            ResultStatus.NoContent => NoContent(), // İçerik yok: 204 No Content
            _ => StatusCode(500, result) // Diğer tüm durumlar için: 500 Internal Server Error
        };
    }

    /// <summary>
    /// Generic olmayan Result nesneleri için uygun HTTP yanıtı oluşturur(ama ResultStatus enumu uzerinden yonetiyoruz ilerde mobil desktop için).
    /// </summary>
    /// <param name="result">İşlem sonucu bilgisi içeren Result nesnesi</param>
    /// <returns>HTTP durumu ve gerekirse hata mesajı içeren IActionResult</returns>
    [NonAction]
    public IActionResult CreateActionResult(Result result)
    {
        if (result == null)
        {
            return BadRequest("Result cannot be null.");
        }

        // ResultStatus durumuna göre HTTP yanıtı döndürülür.
        return result.Status switch
        {
            ResultStatus.Success => Ok(), // Başarı: 200 OK (veri yok)
            ResultStatus.Error => BadRequest(result),
            ResultStatus.NotFound => NotFound(result),
            ResultStatus.ValidationError => BadRequest(result),
            ResultStatus.UnAuthorized => Unauthorized(result),
            ResultStatus.NoContent => NoContent(),
            _ => StatusCode(500, result)
        };
    }
}

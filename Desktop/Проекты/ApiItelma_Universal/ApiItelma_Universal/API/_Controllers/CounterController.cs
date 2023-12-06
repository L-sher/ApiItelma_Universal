using Microsoft.AspNetCore.Mvc;
using ApiItelma_Universal.ModelsForCounter.Cherry;
using ApiItelma_Universal.ModelsForCounter.Aveos;
using ApiItelma_Universal.ModelsForCounter.Lizing;
using ApiItelma_Universal.ModelsForCounter.Crafter;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiItelma_Universal._Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {     
        // POST api/<CheryMailController>
        [HttpPost("Chery")]
        public async Task<string> PostChery(ModelForChery value)
        {
            try
            {
                var send = new SendMailChery(value);
                if (!send.saveForFile())
                {
                    return false.ToString();
                }
                var result = send.SendMail();
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }                
        }

        [HttpPost("Aveos")]
        public async Task<string> PostAveos(ModelForAveos value)
        {
            try
            {
                var send = new SendMailAveos(value);
                if (!send.saveForFile())
                {
                    return false.ToString();
                }
                var result = send.SendMail();
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        [HttpPost("Kirill")]
        public async Task<string> PostKirill(ModelForLizing value)
        {
            try
            {
                var send = new SendMailLizing(value);
                if (!send.saveForFile())
                {
                    return false.ToString();
                }
                var result = send.SendMail();
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost("Lizing")]
        public async Task<string> PostLizing(ModelForLizing value)
        {
            try
            {
                var send = new SendMailLizing(value);
                if (!send.saveForFile())
                {
                    return false.ToString();
                }
                var result = send.SendMail();
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [HttpPost("Crafter")]
        public async Task<string> PostCrafter(ModelForCrafter value)
        {
            try
            {
                var send = new SendMailCrafter(value);
                if (!send.saveForFile())
                {
                    return false.ToString();
                }
                var result = send.SendMail();
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}

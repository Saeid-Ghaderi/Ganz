using Ganz.API.gRPC;
using Ganz.Application.Dtos.gRPC;
using Microsoft.AspNetCore.Mvc;
using static Ganz.API.gRPC.SMS;
namespace Ganz.API.Controllers.gRPCClient
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly SMSClient sMSClient;

        public NotificationController(SMSClient sMSClient)
        {
            this.sMSClient = sMSClient;
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(SMSInfoDto model)
        {
            //GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7044");
            //var client = new SMSClient(channel);
            var smsResult = await sMSClient.SendAsync(
                 new SendSMSRequest { Mobile = model.Mobile, Message = model.Message }
                );

            if (!smsResult.IsSent)
            {
                return BadRequest("is not send sms!!!");
            }

            return Ok("Send sms succ...");

        }
    }
}

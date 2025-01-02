using Ganz.Application.gRPC.Proto;
using Grpc.Core;

namespace Ganz.Application.Services.SMS_gRPC
{
    public class SMSService : SMS.SMSBase
    {
        public async override Task<SendSMSResponse> Send(SendSMSRequest request, ServerCallContext context)
        {
            //send sms
            var smsResponse = new SendSMSResponse { IsSent = true };
            return await Task.FromResult(smsResponse);
        }
    }
}

using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.API.Application.Commands
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, string>
    {
        public Task<string> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Email successfully sent!");
        }
    }
}

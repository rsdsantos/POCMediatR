using Communication.API.Domain.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Communication.API.Application.Commands
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, string>
    {
        public async Task<string> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            throw new DomainException();
        }
    }
}

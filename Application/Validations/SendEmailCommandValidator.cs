using Communication.API.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Communication.API.Application.Validations
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator(ILogger<SendEmailCommandValidator> logger)
        {
            RuleFor(command => command.Body).NotEmpty();
            RuleFor(command => command.Subject).NotEmpty();
            RuleFor(command => command.To).NotEmpty().Must(BeValidEmail).WithMessage("Please specify a valid e-mail address");

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        private bool BeValidEmail(IList<string> emails)
        {
            foreach (var email in emails)
            {
                var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var match = regex.Match(email);

                if (!match.Success)
                    return false;                
            }

            return true;
        }
    }
}

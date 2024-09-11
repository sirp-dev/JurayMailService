using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailListCommands
{
    public sealed class UpdateEmailListCommand : IRequest
    {
        public UpdateEmailListCommand(EmailList emailList)
        {
            EmailList = emailList;
        }

        public EmailList EmailList { get; set; }


    }

    public class UpdateEmailListCommandHandler : IRequestHandler<UpdateEmailListCommand>
    {
        private readonly IEmailListRepository _emailListRepository;

        public UpdateEmailListCommandHandler(IEmailListRepository emailListRepository)
        {
            _emailListRepository = emailListRepository;
        }

        public async Task Handle(UpdateEmailListCommand request, CancellationToken cancellationToken)
        {

            await _emailListRepository.UpdateAsync(request.EmailList);
        }
    }
}

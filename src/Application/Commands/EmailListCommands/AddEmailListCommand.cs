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
    public sealed class AddEmailListCommand : IRequest
    {
        public AddEmailListCommand(EmailList emailList)
        {
            EmailList = emailList;
        }

        public EmailList EmailList { get; set; }


    }

    public class AddEmailListCommandHandler : IRequestHandler<AddEmailListCommand>
    {
        private readonly IEmailListRepository _emailListRepository;

        public AddEmailListCommandHandler(IEmailListRepository emailListRepository)
        {
            _emailListRepository = emailListRepository;
        }

        public async Task Handle(AddEmailListCommand request, CancellationToken cancellationToken)
        {

            await _emailListRepository.AddAsync(request.EmailList);


        }
    }
}

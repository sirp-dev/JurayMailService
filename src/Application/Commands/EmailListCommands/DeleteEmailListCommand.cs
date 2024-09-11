using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.EmailListCommands
{
    public sealed class DeleteEmailListCommand : IRequest
    {
        public DeleteEmailListCommand(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }

    public class DeleteEmailListCommandHandler : IRequestHandler<DeleteEmailListCommand>
    {
        private readonly IEmailListRepository _emailListRepository;

        public DeleteEmailListCommandHandler(IEmailListRepository emailListRepository)
        {
            _emailListRepository = emailListRepository;
        }

        public async Task Handle(DeleteEmailListCommand request, CancellationToken cancellationToken)
        {

            var emailList = await _emailListRepository.GetByIdAsync(request.Id);

            await _emailListRepository.RemoveAsync(emailList);

        }
    }
}

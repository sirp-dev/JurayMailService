using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace Application.Commands.EmailListCommands
{
      public sealed class UploadEmailsToGroup : IRequest
    {
        public UploadEmailsToGroup(IFormFile excelFile, long groupId, string userId)
        {
            ExcelFile = excelFile;
            GroupId = groupId;
            UserId = userId;
        }

        public IFormFile ExcelFile { get; set; }
        public long GroupId { get; set; }
        public string UserId { get; set; }

    }

    public class UploadEmailsToGroupHandler : IRequestHandler<UploadEmailsToGroup>
    {
        private readonly IEmailListRepository _emailListRepository;

        public UploadEmailsToGroupHandler(IEmailListRepository emailListRepository)
        {
            _emailListRepository = emailListRepository;
        }

        public async Task Handle(UploadEmailsToGroup request, CancellationToken cancellationToken)
        {
            List<EmailList> emailLists = new List<EmailList>();

            // Set the license context before using EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var stream = request.ExcelFile.OpenReadStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    // Assuming the first sheet contains the data
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                    if (worksheet != null)
                    {
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Start from 2 to skip header row
                        {
                            var emailList = new EmailList
                            {
                                Email = worksheet.Cells[row, 1].Value?.ToString(),
                                Name = worksheet.Cells[row, 2].Value?.ToString(),
                                PhoneNumber = worksheet.Cells[row, 3].Value?.ToString(),
                                AppUserId = request.UserId,
                                EmailGroupId = request.GroupId
                                // Assign other properties accordingly
                            };
                            emailLists.Add(emailList);
                        }
                    }
                }
            }
     
            
            
            await _emailListRepository.AddMany(emailLists);
            }
    }
}

using Application.Commands.EmailGroupCommands;
using Application.Commands.EmailListCommands;
using Application.Commands.EmailProjectCommands;
using Application.Commands.EmailResponseStatusCommands;
using Application.Commands.EmailSendingStatusCommands;
using Application.Commands.GroupSendingProjectCommands;
using Application.Commands.ServerCommands;
using Application.DTO;
using Application.Queries.DashboardQueries;
using Application.Queries.EmailGroupQueries;
using Application.Queries.EmailListQueries;
using Application.Queries.EmailProjectQueries;
using Application.Queries.EmailResponseStatusQueries;
using Application.Queries.EmailSendingStatusQueries;
using Application.Queries.GroupSendingProjectQueries;
using Application.Queries.ServerQueries;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationCustomServices(this IServiceCollection services)
        {

            //
            services.AddTransient<IEmailListRepository, EmailListRepository>();
            services.AddTransient<IEmailProjectRepository, EmailProjectRepository>();
            services.AddTransient<IEmailResponseStatusRepository, EmailResponseStatusRepository>();
            services.AddTransient<IEmailSendingStatusRepository, EmailSendingStatusRepository>();
            services.AddTransient<IServerRepository, ServerRepository>();
            services.AddTransient<IEmailGroupRepository, EmailGroupRepository>();
            services.AddTransient<IGroupSendingProjectRepository, GroupSendingProjectRepository>();


            //Register Queries and Handlers
            services.AddTransient<IRequestHandler<GetByIdServerQuery, Server>, GetByIdServerQuery.GetByIdServerQueryHandler>();
            services.AddTransient<IRequestHandler<GetDashboardQuery, DashboardDto>, GetDashboardQuery.GetDashboardQueryHandler>();
            services.AddTransient<IRequestHandler<ListProjectByUserId, List<EmailProject>>, ListProjectByUserId.ListProjectByUserIdHandler>();
            services.AddTransient<IRequestHandler<GetByIdEmailProjectQuery, EmailProject>, GetByIdEmailProjectQuery.GetByIdEmailProjectQueryHandler>();

            services.AddTransient<IRequestHandler<GetByIdEmailGroupQuery, EmailGroup>, GetByIdEmailGroupQuery.GetByIdEmailGroupQueryHandler>();
            services.AddTransient<IRequestHandler<ListAllByUserIdEmailGroupQuery, List<EmailGroup>>, ListAllByUserIdEmailGroupQuery.ListAllByUserIdEmailGroupQueryHandler>();
           
            
            services.AddTransient<IRequestHandler<ListAllByUserIdEmailListQuery, List<EmailList>>, ListAllByUserIdEmailListQuery.ListAllByUserIdEmailListQueryHandler>();
            services.AddTransient<IRequestHandler<ListByGroupIdEmailListQuery, List<EmailList>>, ListByGroupIdEmailListQuery.ListByGroupIdEmailListQueryHandler>();
            services.AddTransient<IRequestHandler<ListByUserIdServerQuery, List<Server>>, ListByUserIdServerQuery.ListByUserIdServerQueryHandler>();
            services.AddTransient<IRequestHandler<ListGroupSendingProjectByProjectIdQuery, List<GroupSendingProject>>, ListGroupSendingProjectByProjectIdQuery.ListGroupSendingProjectByProjectIdQueryHandler>();
            services.AddTransient<IRequestHandler<ListByUserIdEmailSendingStatusQuery, IEnumerable<EmailSendingStatus>>, ListByUserIdEmailSendingStatusQuery.ListByUserIdEmailSendingStatusQueryHandler>();
            services.AddTransient<IRequestHandler<ListByQueryEmailResponseStatusQuery, IEnumerable<EmailResponseStatus>>, ListByQueryEmailResponseStatusQuery.ListByQueryEmailResponseStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetTotalCountEmailSendingStatusQuery, int>, GetTotalCountEmailSendingStatusQuery.GetTotalCountEmailSendingStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetTotalCountEmailResponseStatusQuery, int>, GetTotalCountEmailResponseStatusQuery.GetTotalCountEmailResponseStatusQueryHandler>();
            services.AddTransient<IRequestHandler<GetByIdEmailSendingStatusQuery, EmailSendingStatus>, GetByIdEmailSendingStatusQuery.GetByIdEmailSendingStatusQueryHandler>();
             services.AddTransient<IRequestHandler<GetEmailListIdbyMessageIdQuery, WebHookUpdateIds>, GetEmailListIdbyMessageIdQuery.GetEmailListIdbyMessageIdQueryHandler>();
             services.AddTransient<IRequestHandler<ListAllEmailResponseByMessageIdQuery, MailInfoDto>, ListAllEmailResponseByMessageIdQuery.ListAllEmailResponseByMessageIdQueryHandler>();

            
            services.AddScoped<IRequestHandler<AddEmailResponseStatusCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IEmailResponseStatusRepository>();
                return new AddEmailResponseStatusCommandHandler(repository);
            });
            services.AddScoped<IRequestHandler<UpdateMailWebhookCommand>>(sp =>
             {
                 // Resolve  
                 var repository = sp.GetRequiredService<IMediator>();
                 return new UpdateMailWebhookCommandHandler(repository);
             });

            services.AddScoped<IRequestHandler<UploadEmailsToGroup>>(sp =>
             {
                 // Resolve  
                 var repository = sp.GetRequiredService<IEmailListRepository>();
                 return new UploadEmailsToGroupHandler(repository);
             });
            services.AddScoped<IRequestHandler<UpdateEmailGroupCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IEmailGroupRepository>();
                return new UpdateEmailGroupCommandHandler(repository);
            });

            services.AddScoped<IRequestHandler<AddEmailGroupCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IEmailGroupRepository>();
                return new AddEmailGroupCommandHandler(repository);
            });



            // Register Command and Handler
            services.AddScoped<IRequestHandler<UpdateEmailProjectCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IEmailProjectRepository>();
                return new UpdateEmailProjectCommandHandler(repository);
            });

            services.AddScoped<IRequestHandler<AddEmailProjectCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IEmailProjectRepository>();
                return new AddEmailProjectCommandHandler(repository);
            });
            //
            services.AddScoped<IRequestHandler<AddServerCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IServerRepository>();
                return new AddServerCommandHandler(repository);
            });

            services.AddScoped<IRequestHandler<DeleteServerCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IServerRepository>();
                return new DeleteServerCommandHandler(repository);
            });
            services.AddScoped<IRequestHandler<UpdateServerCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IServerRepository>();
                return new UpdateServerCommandHandler(repository);
            });
            //
            services.AddScoped<IRequestHandler<AddGroupSendingProjectCommand>>(sp =>
            {
                // Resolve  
                var repository = sp.GetRequiredService<IGroupSendingProjectRepository>();
                return new AddGroupSendingProjectCommandHandler(repository);
            });
            services.AddScoped<IRequestHandler<AddEmailSendingStatusCommand, RegisterGroupEmailsDto>, AddEmailSendingStatusCommandHandler>();
            //
            //services.AddScoped<IRequestHandler<AddEmailSendingStatusCommand>>(sp =>
            //{
            //    // Resolve  
            //    var repository = sp.GetRequiredService<IEmailSendingStatusRepository>();
            //    return new AddEmailSendingStatusCommandHandler(repository);
            //});
            return services;
        }
    }

}

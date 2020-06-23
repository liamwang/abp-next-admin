﻿using LINGYUN.Abp.MessageService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace LINGYUN.Abp.MessageService.Notifications
{
    public class EfCoreNotificationRepository : EfCoreRepository<MessageServiceDbContext, Notification, long>,
        INotificationRepository, ITransientDependency
    {
        public EfCoreNotificationRepository(
            IDbContextProvider<MessageServiceDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task DeleteExpritionAsync(int batchCount)
        {
            var batchDeleteNoticeWithIds = await DbSet
                .Where(x => x.ExpirationTime <= DateTime.Now)
                .Take(batchCount)
                .Select(x => new Notification(x.Id))
                .AsNoTracking()
                .ToArrayAsync();

            DbSet.AttachRange(batchDeleteNoticeWithIds);
            DbSet.RemoveRange(batchDeleteNoticeWithIds);
        }

        public async Task<Notification> GetByIdAsync(long notificationId)
        {
            return await DbSet.Where(x => x.NotificationId.Equals(notificationId)).FirstOrDefaultAsync();
        }
    }
}

﻿using Hazel.Core;
using Hazel.Core.Domain.ApplicationUsers;
using Hazel.Core.Domain.Logging;
using System;
using System.Collections.Generic;

namespace Hazel.Services.Logging
{
    /// <summary>
    /// ApplicationUser activity service interface.
    /// </summary>
    public partial interface IApplicationUserActivityService
    {
        /// <summary>
        /// Inserts an activity log type item.
        /// </summary>
        /// <param name="activityLogType">Activity log type item.</param>
        void InsertActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Updates an activity log type item.
        /// </summary>
        /// <param name="activityLogType">Activity log type item.</param>
        void UpdateActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Deletes an activity log type item.
        /// </summary>
        /// <param name="activityLogType">Activity log type.</param>
        void DeleteActivityType(ActivityLogType activityLogType);

        /// <summary>
        /// Gets all activity log type items.
        /// </summary>
        /// <returns>Activity log type items.</returns>
        IList<ActivityLogType> GetAllActivityTypes();

        /// <summary>
        /// Gets an activity log type item.
        /// </summary>
        /// <param name="activityLogTypeId">Activity log type identifier.</param>
        /// <returns>Activity log type item.</returns>
        ActivityLogType GetActivityTypeById(int activityLogTypeId);

        /// <summary>
        /// Inserts an activity log item.
        /// </summary>
        /// <param name="systemKeyword">System keyword.</param>
        /// <param name="comment">Comment.</param>
        /// <param name="entity">Entity.</param>
        /// <returns>Activity log item.</returns>
        ActivityLog InsertActivity(string systemKeyword, string comment, BaseEntity entity = null);

        /// <summary>
        /// Inserts an activity log item.
        /// </summary>
        /// <param name="ApplicationUser">ApplicationUser.</param>
        /// <param name="systemKeyword">System keyword.</param>
        /// <param name="comment">Comment.</param>
        /// <param name="entity">Entity.</param>
        /// <returns>Activity log item.</returns>
        ActivityLog InsertActivity(ApplicationUser ApplicationUser, string systemKeyword, string comment, BaseEntity entity = null);

        /// <summary>
        /// Deletes an activity log item.
        /// </summary>
        /// <param name="activityLog">Activity log.</param>
        void DeleteActivity(ActivityLog activityLog);

        /// <summary>
        /// Gets all activity log items.
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; pass null to load all records.</param>
        /// <param name="createdOnTo">Log item creation to; pass null to load all records.</param>
        /// <param name="ApplicationUserId">ApplicationUser identifier; pass null to load all records.</param>
        /// <param name="activityLogTypeId">Activity log type identifier; pass null to load all records.</param>
        /// <param name="ipAddress">IP address; pass null or empty to load all records.</param>
        /// <param name="entityName">Entity name; pass null to load all records.</param>
        /// <param name="entityId">Entity identifier; pass null to load all records.</param>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>Activity log items.</returns>
        IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null, DateTime? createdOnTo = null,
            int? ApplicationUserId = null, int? activityLogTypeId = null, string ipAddress = null, string entityName = null, int? entityId = null,
            int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Gets an activity log item.
        /// </summary>
        /// <param name="activityLogId">Activity log identifier.</param>
        /// <returns>Activity log item.</returns>
        ActivityLog GetActivityById(int activityLogId);

        /// <summary>
        /// Clears activity log.
        /// </summary>
        void ClearAllActivities();
    }
}

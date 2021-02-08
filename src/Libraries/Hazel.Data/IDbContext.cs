using Hazel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Hazel.Data
{
    /// <summary>
    /// Defines the <see cref="IDbContext" />.
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>DbSet.</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <returns>.</returns>
        int SaveChanges();

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// The Entry.
        /// </summary>
        /// <param name="entity">The entity<see cref="object"/>.</param>
        /// <returns>The <see cref="EntityEntry"/>.</returns>
        EntityEntry Entry([NotNull] object entity);

        /// <summary>
        /// The GenerateCreateScript.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string GenerateCreateScript();

        /// <summary>
        /// Executes the given SQL against the database
        /// </summary>
        /// <param name="sql">The SQL to execute</param>
        /// <param name="doNotEnsureTransaction">true - the transaction creation is not ensured; false - the transaction creation is ensured.</param>
        /// <param name="timeout">The timeout to use for command. Note that the command timeout is distinct from the connection timeout, which is commonly set on the database connection string</param>
        /// <param name="parameters">Parameters to use with the SQL</param>
        /// <returns>The number of rows affected</returns>
        [System.Obsolete]
        int ExecuteSqlCommand(RawSqlString sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);
        /// <summary>
        /// Detach an entity from the context.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entity">Entity.</param>
        void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// The AttachRange.
        /// </summary>
        /// <param name="entities">The entities<see cref="IEnumerable{object}"/>.</param>
        void AttachRange([NotNullAttribute] IEnumerable<object> entities);
    }
}

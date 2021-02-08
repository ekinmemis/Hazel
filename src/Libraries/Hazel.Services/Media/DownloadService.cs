using Hazel.Core.Domain.Media;
using Hazel.Data;
using Hazel.Services.Events;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace Hazel.Services.Media
{
    /// <summary>
    /// Download service.
    /// </summary>
    public partial class DownloadService : IDownloadService
    {
        /// <summary>
        /// Defines the _eventPubisher.
        /// </summary>
        private readonly IEventPublisher _eventPubisher;

        /// <summary>
        /// Defines the _downloadRepository.
        /// </summary>
        private readonly IRepository<Download> _downloadRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DownloadService"/> class.
        /// </summary>
        /// <param name="eventPubisher">The eventPubisher<see cref="IEventPublisher"/>.</param>
        /// <param name="downloadRepository">The downloadRepository<see cref="IRepository{Download}"/>.</param>
        public DownloadService(IEventPublisher eventPubisher,
            IRepository<Download> downloadRepository)
        {
            _eventPubisher = eventPubisher;
            _downloadRepository = downloadRepository;
        }

        /// <summary>
        /// Gets a download.
        /// </summary>
        /// <param name="downloadId">Download identifier.</param>
        /// <returns>Download.</returns>
        public virtual Download GetDownloadById(int downloadId)
        {
            if (downloadId == 0)
                return null;

            return _downloadRepository.GetById(downloadId);
        }

        /// <summary>
        /// Gets a download by GUID.
        /// </summary>
        /// <param name="downloadGuid">Download GUID.</param>
        /// <returns>Download.</returns>
        public virtual Download GetDownloadByGuid(Guid downloadGuid)
        {
            if (downloadGuid == Guid.Empty)
                return null;

            var query = from o in _downloadRepository.Table
                        where o.DownloadGuid == downloadGuid
                        select o;

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Deletes a download.
        /// </summary>
        /// <param name="download">Download.</param>
        public virtual void DeleteDownload(Download download)
        {
            if (download == null)
                throw new ArgumentNullException(nameof(download));

            _downloadRepository.Delete(download);

            _eventPubisher.EntityDeleted(download);
        }

        /// <summary>
        /// Inserts a download.
        /// </summary>
        /// <param name="download">Download.</param>
        public virtual void InsertDownload(Download download)
        {
            if (download == null)
                throw new ArgumentNullException(nameof(download));

            _downloadRepository.Insert(download);

            _eventPubisher.EntityInserted(download);
        }

        /// <summary>
        /// Updates the download.
        /// </summary>
        /// <param name="download">Download.</param>
        public virtual void UpdateDownload(Download download)
        {
            if (download == null)
                throw new ArgumentNullException(nameof(download));

            _downloadRepository.Update(download);

            _eventPubisher.EntityUpdated(download);
        }

        /// <summary>
        /// Gets the download binary array.
        /// </summary>
        /// <param name="file">File.</param>
        /// <returns>Download binary array.</returns>
        public virtual byte[] GetDownloadBits(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
        }
    }
}

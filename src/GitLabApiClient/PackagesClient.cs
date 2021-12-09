using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Http;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Internal.Utilities;
using GitLabApiClient.Models.Packages.Responses;
using GitLabApiClient.Models.Packages.Requests;
using GitLabApiClient.Internal.Queries;
using GitLabApiClient.Models.Uploads.Requests;

namespace GitLabApiClient
{
    public sealed class PackagesClient : IPackagesClient
    {
        private readonly GitLabHttpFacade _httpFacade;
        private readonly PackagesQueryBuilder _queryBuilder;


        internal PackagesClient(
            GitLabHttpFacade httpFacade,
            PackagesQueryBuilder queryBuilder
        )
        {
            _httpFacade = httpFacade;
            _queryBuilder = queryBuilder;
        }


        /// <summary>
        /// Pyblish a generic package file. When you publish a package file, if the package does not exist, it is created.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        /// <param name="uploadRequest">The upload request containing the filename and stream to be uploaded</param>
        public async Task UploadFileAsync(ProjectId projectId, string packageName, string packageVersion, string fileName, CreateUploadRequest uploadRequest, bool hidden = false)
        {
            string status = hidden ? "hidden" : "default";
            string url = $"projects/{projectId}/packages/generic/{packageName}/{packageVersion}/{fileName}?status={status}";

            await _httpFacade.PutFileBody(url, uploadRequest);
        }

        /// <summary>
        /// Download a generic package file.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="outputPath">The filename that should contain the contents of the download after the download completes</param>
        /// <returns>Status of the export</returns>
        public async Task DownloadFileAsync(ProjectId projectId, string packageName, string packageVersion, string fileName, string outputPath)
        {
            string url = $"projects/{projectId}/packages/generic/{packageName}/{packageVersion}/{fileName}";

            await _httpFacade.GetFile(url, outputPath ?? fileName);
        }

        /// <summary>
        /// Retrieves project issue.
        /// </summary>
        public async Task<Package> GetAsync(ProjectId projectId, int packageId)
        {
            return await _httpFacade.Get<Package>($"projects/{projectId}/packages/{packageId}");
        }


        /// <summary>
        /// List package files
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        public async Task<IList<PackageFile>> GetPackageFilesAsync(ProjectId projectId, int packageId) {
            return await _httpFacade.Get<IList<PackageFile>>($"projects/{projectId}/packages/{packageId}/package_files");
        }


        /// <summary>
        /// Delete a package.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        public async Task DeletePackageAsync(ProjectId projectId, int packageId) =>
            await _httpFacade.Delete($"projects/{projectId}/packages/{packageId}");

        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Packages retrieval options.</param>
        /// <returns>Packages satisfying options.</returns>
        public async Task<IList<Package>> GetAllAsync(ProjectId projectId = null, GroupId groupId = null,
            Action<PackagesQueryOptions> options = null)
        {
            var queryOptions = new PackagesQueryOptions();
            options?.Invoke(queryOptions);

            string path = "packages";
            if (projectId != null)
            {
                path = $"projects/{projectId}/packages";
            }
            else if (groupId != null)
            {
                path = $"groups/{groupId}/packages";
            }

            string url = _queryBuilder.Build(path, queryOptions);

            return await _httpFacade.GetPagedList<Package>(url);
        }
    }
}

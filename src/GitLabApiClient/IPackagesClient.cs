using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Packages.Responses;
using GitLabApiClient.Models.Packages.Requests;
using GitLabApiClient.Models.Uploads.Requests;

namespace GitLabApiClient
{
    public interface IPackagesClient
    {

        /// <summary>
        /// Pyblish a generic package file. When you publish a package file, if the package does not exist, it is created.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        /// <param name="uploadRequest">The upload request containing the filename and stream to be uploaded</param>
        Task UploadFileAsync(ProjectId projectId, string packageName, string packageVersion, string fileName, CreateUploadRequest uploadRequest, bool hidden = false);


        /// <summary>
        /// Download a generic package file.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="outputPath">The filename that should contain the contents of the download after the download completes</param>
        /// <returns>Status of the export</returns>
        Task DownloadFileAsync(ProjectId projectId, string packageName, string packageVersion, string fileName, string outputPath);

        /// <summary>
        /// Retrieves project package.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        Task<Package> GetAsync(ProjectId projectId, int packageId);


        /// <summary>
        /// List package files
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        Task<IList<PackageFile>> GetPackageFilesAsync(ProjectId projectId, int packageId);

        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="groupId">The ID, path or <see cref="Group"/> of the group.</param>
        /// <param name="options">Packages retrieval options.</param>
        /// <returns>Packages satisfying options.</returns>
        Task<IList<Package>> GetAllAsync(ProjectId projectId = null, GroupId groupId = null, Action<PackagesQueryOptions> options = null);


        /// <summary>
        /// Delete a package.
        /// </summary>
        /// <param name="projectId">The ID, path or <see cref="Project"/> of the project.</param>
        /// <param name="packageId">The ID, path or <see cref="Package"/> of the package.</param>
        Task DeletePackageAsync(ProjectId projectId, int packageId);

    }
}

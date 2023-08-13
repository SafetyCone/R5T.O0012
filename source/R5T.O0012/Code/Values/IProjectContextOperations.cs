using System;
using System.Threading.Tasks;

using R5T.F0000;
using R5T.L0031.Extensions;
using R5T.L0040.T000;
using R5T.T0131;
using R5T.T0172;
using R5T.T0187;
using R5T.T0198;
using R5T.T0201;


namespace R5T.O0012
{
    /// <summary>
    /// Generation project context operations.
    /// </summary>
    [ValuesMarker]
    public partial interface IProjectContextOperations : IValuesMarker
    {
        public Func<IProjectContext, Task> Create_WebServerForBlazorClient(
            IProjectDescription projectDescription,
            IsSet<IRepositoryUrl> repositoryUrl,
            Func<IProjectFilePath> clientProjectFilePathProvider,
            Func<IProjectFilePath, Task> projectFilePathHandler)
        {
            return Instances.ProjectContextOperations_L0040.Create_New_Project(
                Instances.ProjectFileContextOperations.Setup_WebServerForBlazorClientProjectFile(
                    projectDescription,
                    repositoryUrl,
                    clientProjectFilePathProvider
                ),
                Instances.ProjectContextOperations_L0040.Setup_WebServerForBlazorClient(
                    projectDescription
                ),
                projectFilePathHandler
            );
        }

        public Func<IProjectContext, Task> Create_ConstructionProject(
            IProjectDescription projectDescription,
            IsSet<IRepositoryUrl> repositoryUrl,
            IHasConstructionProjectFilePath hasConstructionProjectFilePath,
            IHasLibraryProjectFilePath hasLibraryProjectFilePath)
        {
            async Task Internal(IProjectContext projectContext)
            {
                await projectContext.Run(
                    Instances.ProjectContextOperations_O0011.Create_ConsoleProject(
                        projectDescription,
                        repositoryUrl,
                        Instances.FilePathHandlers.Set_ConstructionProjectFilePath(hasConstructionProjectFilePath)
                    ),
                    Instances.ProjectContextOperations_L0040.Add_ProjectFileReference(
                        hasLibraryProjectFilePath.LibraryProjectFilePath
                    )
                );
            }

            return Internal;
        }

        public Func<IProjectContext, Task> Create_LibraryProject(
            IProjectDescription projectDescription,
            IsSet<IRepositoryUrl> repositoryUrl,
            IHasLibraryProjectFilePath hasLibraryProjectFilePath)
        {
            return Instances.ProjectContextOperations_O0011.Create_LibraryProject(
                projectDescription,
                repositoryUrl,
                Instances.FilePathHandlers.Set_LibraryProjectFilePath(hasLibraryProjectFilePath)
            );
        }
    }
}

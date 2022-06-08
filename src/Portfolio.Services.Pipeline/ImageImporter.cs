using RPGCore.FileTree;
using RPGCore.Projects;
using RPGCore.Projects.Pipeline;
using System;
using System.Collections.Generic;

namespace Portfolio.Services.Pipeline;

public class ImageImporter : IArchiveFileImporter
{
	public bool CanImport(IArchiveFile archiveFile)
	{
		return string.Equals(archiveFile.Extension, ".png", StringComparison.OrdinalIgnoreCase)
			|| string.Equals(archiveFile.Extension, ".jpg", StringComparison.OrdinalIgnoreCase)
			|| string.Equals(archiveFile.Extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
	}

	public IEnumerable<ProjectResourceUpdate> ImportFile(ArchiveFileImporterContext context, IArchiveFile archiveFile)
	{
		var update = context.AuthorUpdate(archiveFile.FullName)
			.WithContent(archiveFile);

		yield return update;
	}
}

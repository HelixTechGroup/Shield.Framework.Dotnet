using System;
using System.Collections.Generic;
using System.IO;

namespace Shield.Framework.Platform.IO
{
	public interface IPlatformStorage: IDispose, IEquatable<IPlatformStorage>
	{
		#region Properties
		Guid Id { get; }

		bool ReadOnly { get; }

		string RootDirectory { get; }
		#endregion

		#region Methods
		bool FileExists(string fileName, string path = "");

		bool DirectoryExists(string directoryName, string path = "");

		bool FileInUse(string fileName, string path = "");

		FileInfo GetFile(string fileName, string path = "");

		IEnumerable<FileInfo> GetFiles(string path = "");

		DirectoryInfo GetDirectory(string directoryName, string path = "");

		IEnumerable<DirectoryInfo> GetDirectories(string path = "");

		Stream OpenFile(string fileName, string path = "");

		void CloseFile(Stream stream);

		Stream CreateFileStream(string fileName, string path = "");

		void CreateFile(string fileName, string path = "");

		void CreateDirectory(string directoryName, string path = "");

		void DeleteFile(string fileName, string path = "");

		void DeleteDirectory(string directoryName, string path = "");

		void MoveFile(string fileName, string sourcePath, string destinationPath);

		void MoveDirectory(string directoryName, string sourcePath, string destinationPath);

		void CopyFile(string fileName, string sourcePath, string destinationPath, bool overwrite = false);

		long GetFileLength(string fileName, string path = "");

		FileAttributes GetAttributes(string name, string path);

		void SetAttributes(string name, string path, FileAttributes attributes);
		#endregion
	}
}
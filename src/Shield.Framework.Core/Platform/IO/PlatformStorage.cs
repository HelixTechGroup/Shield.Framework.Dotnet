using System;
using System.Collections.Generic;
using System.IO;
using Shield.Framework.Collections;

namespace Shield.Framework.Platform.IO
{
	public abstract class PlatformStorage : IPlatformStorage, IEquatable<PlatformStorage>
	{
		public event Action<IDispose> OnDispose;

		private readonly Guid m_id;
		protected bool m_disposed;
		protected string m_name;
		protected bool m_readOnly;
		protected string m_rootDirectory;
		protected ConcurrentList<Stream> m_streams;

		public Guid Id
		{
		    get { return m_id; }
		}

	    public bool Disposed
	    {
	        get { return m_disposed; }
	    }

	    public bool ReadOnly
	    {
	        get { return m_readOnly; }
	    }

	    public string RootDirectory
	    {
	        get { return m_rootDirectory; }
	    }

	    protected PlatformStorage() : this("") { }

		protected PlatformStorage(string rootDirectory)
		{
			m_id = Guid.NewGuid();
			m_rootDirectory = rootDirectory;
			m_streams = new ConcurrentList<Stream>();
		}

		~PlatformStorage()
		{
			Dispose(false);
		}

		public static bool operator ==(PlatformStorage left, PlatformStorage right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(PlatformStorage left, PlatformStorage right)
		{
			return !Equals(left, right);
		}

		public abstract bool FileExists(string fileName, string path = "");

		public abstract bool DirectoryExists(string directoryName, string path = "");

		public abstract bool FileInUse(string fileName, string path = "");

		public abstract FileInfo GetFile(string fileName, string path = "");

		public abstract IEnumerable<FileInfo> GetFiles(string path = "");

		public abstract DirectoryInfo GetDirectory(string directoryName, string path = "");

		public abstract IEnumerable<DirectoryInfo> GetDirectories(string path = "");

		public abstract Stream OpenFile(string fileName, string path = "");

		public abstract void CloseFile(Stream stream);

		public abstract Stream CreateFileStream(string fileName, string path = "");

		public abstract void CreateFile(string fileName, string path = "");

		public abstract void CreateDirectory(string directoryName, string path = "");

		public abstract void DeleteFile(string fileName, string path = "");

		public abstract void DeleteDirectory(string directoryName, string path = "");

		public abstract void MoveFile(string fileName, string sourcePath, string destinationPath);

		public abstract void MoveDirectory(string directoryName, string sourcePath, string destinationPath);

		public abstract void CopyFile(string fileName, string sourcePath, string destinationPath, bool overwrite = false);

		public abstract long GetFileLength(string fileName, string path = "");

		public abstract FileAttributes GetAttributes(string name, string path);

		public abstract void SetAttributes(string name, string path, FileAttributes attributes);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (m_disposed)
				return;

			if (disposing)
			{
				foreach (var s in m_streams)
				{
					m_streams.Remove(s);
					s.Close();
					s.Dispose();
				}
			}

			if (OnDispose != null)
				OnDispose(this);
			m_disposed = true;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (m_id.GetHashCode() * 397);
			}
		}

		public static bool Equals(IPlatformStorage left, IPlatformStorage right)
		{
			return left.Equals(right);
		}

		public static bool Equals(PlatformStorage left, PlatformStorage right)
		{
			return left.Equals(right);

		}

		public bool Equals(PlatformStorage other)
		{
			if (other == null)				
				return false;

			return m_id == other.Id;
		}

		public bool Equals(IPlatformStorage other)
		{
			if (other == null)
				return false;

			return m_id == other.Id;
		}

		public override bool Equals(object obj)
		{
			var p = obj as PlatformStorage;
			return p != null && Equals(p);
		}		
	}
}
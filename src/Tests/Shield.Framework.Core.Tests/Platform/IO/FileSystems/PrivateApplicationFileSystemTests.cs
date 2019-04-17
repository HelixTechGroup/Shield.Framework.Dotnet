using NSubstitute;
using NUnit.Framework;
using Shield.Framework.Platform.IO.FileSystems;
using System;

namespace Shield.Framework.Core.Tests.Platform.IO.FileSystems
{
    [TestFixture]
    public class PrivateApplicationFileSystemTests
    {


        [SetUp]
        public void SetUp()
        {

        }


        private PrivateApplicationFileSystem CreatePrivateApplicationFileSystem()
        {
            return new PrivateApplicationFileSystem();
        }

        [Test]
        public void Equals_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            PrivateApplicationFileSystem other = TODO;

            // Act
            var result = unitUnderTest.Equals(
                other);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CopyDirectory_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string sourcePath = TODO;
            string destinationPath = TODO;
            bool overwrite = TODO;
            bool isRecursive = TODO;

            // Act
            unitUnderTest.CopyDirectory(
                sourcePath,
                destinationPath,
                overwrite,
                isRecursive);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ReplaceFile_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string srcPath = TODO;
            string destPath = TODO;
            string destBackupPath = TODO;
            bool ignoreMetadataErrors = TODO;

            // Act
            unitUnderTest.ReplaceFile(
                srcPath,
                destPath,
                destBackupPath,
                ignoreMetadataErrors);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetFileLength_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.GetFileLength(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetAttributes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.GetAttributes(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SetAttributes_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;
            FileAttributes attributes = TODO;

            // Act
            unitUnderTest.SetAttributes(
                path,
                attributes);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetCreationTime_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.GetCreationTime(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SetCreationTime_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;
            DateTime time = TODO;

            // Act
            unitUnderTest.SetCreationTime(
                path,
                time);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetLastAccessTime_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.GetLastAccessTime(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SetLastAccessTime_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;
            DateTime time = TODO;

            // Act
            unitUnderTest.SetLastAccessTime(
                path,
                time);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void GetLastWriteTime_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.GetLastWriteTime(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void SetLastWriteTime_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;
            DateTime time = TODO;

            // Act
            unitUnderTest.SetLastWriteTime(
                path,
                time);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CanWatch_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.CanWatch(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Watch_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.Watch(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ConvertPathToInternal_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string path = TODO;

            // Act
            var result = unitUnderTest.ConvertPathToInternal(
                path);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void ConvertFromInternal_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            string fileSystemPath = TODO;

            // Act
            var result = unitUnderTest.ConvertFromInternal(
                fileSystemPath);

            // Assert
            Assert.Fail();
        }

        [Test]
        public void Equals_StateUnderTest_ExpectedBehavior1()
        {
            // Arrange
            var unitUnderTest = this.CreatePrivateApplicationFileSystem();
            IPrivateApplicationFileSystem other = TODO;

            // Act
            var result = unitUnderTest.Equals(
                other);

            // Assert
            Assert.Fail();
        }
    }
}

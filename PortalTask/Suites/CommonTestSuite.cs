using NUnit.Framework;
using PortalTask.Base;
using PortalTask.Tests;

namespace PortalTask.Suites
{
    [TestFixture]
    [Parallelizable]
    public class CommonTestSuite : BaseSuite
    {
        [Test]
        [Parallelizable]
        public void AddingPostsTest() => RunTest<AddingPostTest>();

        [Test]
        [Parallelizable]
        public void UpdatePutTest() => RunTest<UpdatingPostTest>();

        [Test]
        [Parallelizable]
        public void DeletePostTest() => RunTest<DeletePostTest>();

        [Test]
        [Parallelizable]
        public void CommentsEmailTest() => RunTest<CommentsEmailTest>();

        [Test]
        [Parallelizable]
        public void PostTitleTest() => RunTest<PostTitleTest>();

        [Test]
        [Parallelizable]
        public void PhotoWithTitleTest() => RunTest<PhotoWithTitleTest>();

        [Test]
        [Parallelizable]
        public void TODOsTest() => RunTest<TODOsTest>();

        [Test]
        [Parallelizable]
        public void ImageBinaryComparisonTest() => RunTest<ImageBinaryComparisonTest>();
    }
}

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieLibrary.Service.IServices;
using MovieLibrary.Service.ServicesImpl;
using MovieLibrary.Models;
using MovieLibrary.Service;
using System.Xml;
using Rhino.Mocks;
using MovieLibrary.Helpers;
using Rhino.Mocks.Constraints;

namespace MovieLibrary.Test.Services
{
    [TestClass]
    public class AWSImageServiceTest
    {
        protected static XmlDocument docVideoNormal;
        protected static XmlDocument docVideoNoMatches;
        protected static XmlDocument docVideoOnlyLargeImage;

        protected static XmlDocument docBookNormal;
        protected static XmlDocument docBookNoMatches;
        protected static XmlDocument docBookOnlyLargeImage;

        protected static MockRepository mocks = new MockRepository();
        protected static AWSRequestHelper requestHelper;
        protected static Movie movie;
        protected static Book book;

        protected static IDictionary<string, Image> testImagesVideo;
        protected static IDictionary<string, Image> testImagesBook;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            docVideoNormal = TestUtil.GetXmlDocFromFile(@"\MovieLibrary.Test\TestData\AWSImageServiceTest\VideoImages_allImageTypes.xml");
            docVideoNoMatches = TestUtil.GetXmlDocFromFile(@"\MovieLibrary.Test\TestData\AWSImageServiceTest\VideoImages_noMatches.xml");
            docVideoOnlyLargeImage = TestUtil.GetXmlDocFromFile(@"\MovieLibrary.Test\TestData\AWSImageServiceTest\VideoImages_onlyLargeImage.xml");

            docBookNormal = TestUtil.GetXmlDocFromFile(@"\MovieLibrary.Test\TestData\AWSImageServiceTest\BookImages_allImageTypes.xml");
            docBookNoMatches = TestUtil.GetXmlDocFromFile(@"\MovieLibrary.Test\TestData\AWSImageServiceTest\BookImages_noMatches.xml");
            docBookOnlyLargeImage = TestUtil.GetXmlDocFromFile(@"\MovieLibrary.Test\TestData\AWSImageServiceTest\BookImages_onlyLargeImage.xml");

            testImagesVideo = new Dictionary<string, Image>();
            testImagesVideo.Add(AWSImageService.SWATCHIMAGE, new Image("http://ecx.images-amazon.com/images/I/41EZqxTO1-L._SL30_.jpg", 30, 22));
            testImagesVideo.Add(AWSImageService.SMALLIMAGE, new Image("http://ecx.images-amazon.com/images/I/41EZqxTO1-L._SL75_.jpg", 75, 55));
            testImagesVideo.Add(AWSImageService.THUMBNAILIMAGE, new Image("http://ecx.images-amazon.com/images/I/41EZqxTO1-L._SL75_.jpg", 75, 55));
            testImagesVideo.Add(AWSImageService.TINYIMAGE, new Image("http://ecx.images-amazon.com/images/I/41EZqxTO1-L._SL110_.jpg", 110, 81));
            testImagesVideo.Add(AWSImageService.MEDIUMIMAGE, new Image("http://ecx.images-amazon.com/images/I/41EZqxTO1-L._SL160_.jpg", 160, 117));
            testImagesVideo.Add(AWSImageService.LARGEIMAGE, new Image("http://ecx.images-amazon.com/images/I/41EZqxTO1-L.jpg", 500, 366));

            testImagesBook = new Dictionary<string, Image>();
            testImagesBook.Add(AWSImageService.SWATCHIMAGE, new Image("http://ecx.images-amazon.com/images/I/51cpi7TV31L._SL30_.jpg", 30, 19));
            testImagesBook.Add(AWSImageService.SMALLIMAGE, new Image("http://ecx.images-amazon.com/images/I/51cpi7TV31L._SL75_.jpg", 75, 47));
            testImagesBook.Add(AWSImageService.THUMBNAILIMAGE, new Image("http://ecx.images-amazon.com/images/I/51cpi7TV31L._SL75_.jpg", 75, 47));
            testImagesBook.Add(AWSImageService.TINYIMAGE, new Image("http://ecx.images-amazon.com/images/I/51cpi7TV31L._SL110_.jpg", 110, 69));
            testImagesBook.Add(AWSImageService.MEDIUMIMAGE, new Image("http://ecx.images-amazon.com/images/I/51cpi7TV31L._SL160_.jpg", 160, 101));
            testImagesBook.Add(AWSImageService.LARGEIMAGE, new Image("http://ecx.images-amazon.com/images/I/51cpi7TV31L.jpg", 500, 315));

            movie = new Movie();
            movie.Title = "Der Terminator";

            Actor schwarzenegger = new Actor();
            schwarzenegger.LastName = "Schwarzenegger";

            Actor biehn = new Actor();
            biehn.LastName = "Biehn";

            movie.Actor.Add(schwarzenegger);
            movie.Actor.Add(biehn);

            book = new Book();
            book.Title = "In einer kleinen Stadt";

            Author king = new Author();
            king.LastName = "King";

            book.Author.Add(king);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            requestHelper = mocks.StrictMock<AWSRequestHelper>();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            mocks.VerifyAll();
        }

        protected void GetImagesForVideoReturnsCorrectImage(string imageType)
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docVideoNormal);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForVideo(movie);
            Image image = images[imageType];

            Assert.AreEqual(testImagesVideo[imageType], image);
        }

        protected void GetImagesForBookReturnsCorrectImage(string imageType)
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docBookNormal);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForBook(book);
            Image image = images[imageType];

            Assert.AreEqual(testImagesBook[imageType], image);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsAllImageTypes()
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docVideoNormal);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForVideo(movie);

            Assert.IsNotNull(images[AWSImageService.SWATCHIMAGE]);
            Assert.IsNotNull(images[AWSImageService.SMALLIMAGE]);
            Assert.IsNotNull(images[AWSImageService.THUMBNAILIMAGE]);
            Assert.IsNotNull(images[AWSImageService.TINYIMAGE]);
            Assert.IsNotNull(images[AWSImageService.MEDIUMIMAGE]);
            Assert.IsNotNull(images[AWSImageService.LARGEIMAGE]);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsNoImage()
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docVideoNoMatches);
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docVideoNoMatches);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForVideo(movie);

            Assert.IsNull(images[AWSImageService.SWATCHIMAGE]);
            Assert.IsNull(images[AWSImageService.SMALLIMAGE]);
            Assert.IsNull(images[AWSImageService.THUMBNAILIMAGE]);
            Assert.IsNull(images[AWSImageService.TINYIMAGE]);
            Assert.IsNull(images[AWSImageService.MEDIUMIMAGE]);
            Assert.IsNull(images[AWSImageService.LARGEIMAGE]);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsOnlyLargeImage()
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docVideoOnlyLargeImage);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForVideo(movie);

            Assert.IsNull(images[AWSImageService.SWATCHIMAGE]);
            Assert.IsNull(images[AWSImageService.SMALLIMAGE]);
            Assert.IsNull(images[AWSImageService.THUMBNAILIMAGE]);
            Assert.IsNull(images[AWSImageService.TINYIMAGE]);
            Assert.IsNull(images[AWSImageService.MEDIUMIMAGE]);
            Assert.IsNotNull(images[AWSImageService.LARGEIMAGE]);
        }

        [TestMethod]
        public void GetImagesForBookReturnsAllImageTypes()
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docBookNormal);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForBook(book);

            Assert.IsNotNull(images[AWSImageService.SWATCHIMAGE]);
            Assert.IsNotNull(images[AWSImageService.SMALLIMAGE]);
            Assert.IsNotNull(images[AWSImageService.THUMBNAILIMAGE]);
            Assert.IsNotNull(images[AWSImageService.TINYIMAGE]);
            Assert.IsNotNull(images[AWSImageService.MEDIUMIMAGE]);
            Assert.IsNotNull(images[AWSImageService.LARGEIMAGE]);
        }

        [TestMethod]
        public void GetImagesForBookReturnsNoImage()
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docBookNoMatches);
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docBookNoMatches);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForBook(book);

            Assert.IsNull(images[AWSImageService.SWATCHIMAGE]);
            Assert.IsNull(images[AWSImageService.SMALLIMAGE]);
            Assert.IsNull(images[AWSImageService.THUMBNAILIMAGE]);
            Assert.IsNull(images[AWSImageService.TINYIMAGE]);
            Assert.IsNull(images[AWSImageService.MEDIUMIMAGE]);
            Assert.IsNull(images[AWSImageService.LARGEIMAGE]);
        }

        [TestMethod]
        public void GetImagesForBookReturnsOnlyLargeImage()
        {
            Expect.Call(requestHelper.GetResponse(null)).
                IgnoreArguments().
                Constraints(Is.TypeOf<List<string>>()).
                Return(docBookOnlyLargeImage);
            mocks.ReplayAll();

            IImageService imageService = new AWSImageService(requestHelper);

            IDictionary<string, Image> images = imageService.GetImagesForBook(book);

            Assert.IsNull(images[AWSImageService.SWATCHIMAGE]);
            Assert.IsNull(images[AWSImageService.SMALLIMAGE]);
            Assert.IsNull(images[AWSImageService.THUMBNAILIMAGE]);
            Assert.IsNull(images[AWSImageService.TINYIMAGE]);
            Assert.IsNull(images[AWSImageService.MEDIUMIMAGE]);
            Assert.IsNotNull(images[AWSImageService.LARGEIMAGE]);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsCorrectSwatchImage()
        {
            GetImagesForVideoReturnsCorrectImage(AWSImageService.SWATCHIMAGE);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsCorrectSmallImage()
        {
            GetImagesForVideoReturnsCorrectImage(AWSImageService.SMALLIMAGE);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsCorrectThumbnailImage()
        {
            GetImagesForVideoReturnsCorrectImage(AWSImageService.THUMBNAILIMAGE);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsCorrectTinyImage()
        {
            GetImagesForVideoReturnsCorrectImage(AWSImageService.TINYIMAGE);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsCorrectMediumImage()
        {
            GetImagesForVideoReturnsCorrectImage(AWSImageService.MEDIUMIMAGE);
        }

        [TestMethod]
        public void GetImagesForVideoReturnsCorrectLargeImage()
        {
            GetImagesForVideoReturnsCorrectImage(AWSImageService.LARGEIMAGE);
        }

        [TestMethod]
        public void GetImagesForBookReturnsCorrectSwatchImage()
        {
            GetImagesForBookReturnsCorrectImage(AWSImageService.SWATCHIMAGE);
        }

        [TestMethod]
        public void GetImagesForBookReturnsCorrectSmallImage()
        {
            GetImagesForBookReturnsCorrectImage(AWSImageService.SMALLIMAGE);
        }

        [TestMethod]
        public void GetImagesForBookReturnsCorrectThumbnailImage()
        {
            GetImagesForBookReturnsCorrectImage(AWSImageService.THUMBNAILIMAGE);
        }

        [TestMethod]
        public void GetImagesForBookReturnsCorrectTinyImage()
        {
            GetImagesForBookReturnsCorrectImage(AWSImageService.TINYIMAGE);
        }

        [TestMethod]
        public void GetImagesForBookReturnsCorrectMediumImage()
        {
            GetImagesForBookReturnsCorrectImage(AWSImageService.MEDIUMIMAGE);
        }

        [TestMethod]
        public void GetImagesForBookReturnsCorrectLargeImage()
        {
            GetImagesForBookReturnsCorrectImage(AWSImageService.LARGEIMAGE);
        }
    }
}

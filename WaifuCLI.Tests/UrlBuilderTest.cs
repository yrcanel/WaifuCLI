using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using WaifuCLI.Core.Interfaces;
using WaifuCLI.Infrastructure.UrlBuilder;

namespace WaifuCLI.Tests
{

    public class UrlBuilderTest
    {
        [Fact]
        public void BuildUrlWithTags_AllArgumentsGivenAndAmmountOmited_ShouldReturnStr()
        {
            string expectedUrl = "https://api.waifu.im/images?IncludedTags=waifu&IncludedTags=ero&IsNsfw=True&pageSize=1";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string resultUrl = builder.BuildUrlWithTags(["waifu", "ero"], true, null);
            Assert.Equal(expectedUrl, resultUrl);
        }
        [Fact]
        public void BuildUrlWithTags_AllArgumentsGiven_ShouldReturnStr()
        {
            string expectedUrl = "https://api.waifu.im/images?IncludedTags=waifu&IncludedTags=ero&IsNsfw=True&pageSize=15";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string resultUrl = builder.BuildUrlWithTags(["waifu", "ero"], true, 15);
            Assert.Equal(expectedUrl, resultUrl);
        }
        [Fact]
        public void BuildUrlWithTags_AllArgumentsOmited_ShouldReturnStr()
        {
            string expectedUrl = "https://api.waifu.im/images?IsNsfw=All&pageSize=1";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string resultUrl = builder.BuildUrlWithTags(null, null, null);
            Assert.Equal(expectedUrl, resultUrl);
        }
        [Fact]
        public void BuildUrlWithTags_TagsOmited_ShouldReturnStr()
        {
            string expectedUrl = "https://api.waifu.im/images?IsNsfw=False&pageSize=1";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string resultUrl = builder.BuildUrlWithTags(null, false, null);
            Assert.Equal(expectedUrl, resultUrl);
        }
        [Fact]
        public void BuildUrlWithTags_IsNsfwOmited_ShouldReturnStr()
        {
            string expectedUrl = "https://api.waifu.im/images?IncludedTags=waifu&IncludedTags=ero&IsNsfw=All&pageSize=1";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string resultUrl = builder.BuildUrlWithTags(["waifu", "ero"], null, null);
            Assert.Equal(expectedUrl, resultUrl);
        }
        [Fact]
        public void BuildUrlForTags_CleanQuery_ShouldReturnSrt()
        {
            string expected = "https://api.waifu.im/tags";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string result = builder.BuildUrlForTags();
            Assert.Equal(expected, result);
        }
        [Fact]
        public void BuildUrlForTags_FullQuery_ShouldReturnSrt()
        {
            string expected = "https://api.waifu.im/tags";
            UriBuilder uriBuilder = new UriBuilder("https://api.waifu.im/images?IncludedTags=waifu&isNsfw=True");
            UrlBuilder builder = new UrlBuilder(uriBuilder);
            string result = builder.BuildUrlForTags();
            Assert.Equal(expected, result);
        }

    }
}

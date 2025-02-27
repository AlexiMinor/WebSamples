using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WebApp.Data;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;
using WebApp.Services.Implementations;
using WebApp.Services.Mappers;

namespace WebApp.Services.Tests
{
    public class ArticleServiceTests
    {
        private readonly Article[] _positiveArticlesCollection = new[]
        {
            new Article
            {
                PositivityRate = 1,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }
            },
            new Article
            {
                PositivityRate = 2,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }
            },
            new Article
            {
                PositivityRate = 3,
                Source = new Source()
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Another Source"
                }
            },
            new Article
            {
                PositivityRate = 4,
                Source = new Source() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "New Source" }
            },
            new Article
            {
                PositivityRate = 5,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }
            }, 
            new Article
            {
                PositivityRate = 6,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }

            },
            new Article
            {
                PositivityRate = 7,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }
            },
            new Article
            {
                PositivityRate = 8,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }

            },
            new Article
            {
                PositivityRate = 9,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }
            },
            new Article
            {
                PositivityRate = 10,
                Source = new Source()
                {
                    //create guid which contains only 1
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Example Source"
                }
            }
        };

        private IMediator mediatorMock;
        private ILogger<ArticleService> loggerMock;
        private ArticleService articleService;


        private void PrepareArticleService()
        {
            mediatorMock = Substitute.For<IMediator>();
            loggerMock = Substitute.For<ILogger<ArticleService>>();
            articleService = new ArticleService(loggerMock, mediatorMock, new ArticleMapper());
        }



        [Fact]
        public async Task GetAllPositiveAsync_CorrectPageAndPageSize_ReturnCollection()
        {
            //arrange
            PrepareArticleService();

            mediatorMock.Send(Arg.Any<GetPositiveArticlesWithPaginationQuery>(),
                    Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(_positiveArticlesCollection));


            var minRate = 1;
            var pageSize = 10;
            var pageNumber = 2;

            //act
            var result = await articleService.GetAllPositiveAsync(minRate, pageSize, pageNumber);

            //assert
            Assert.NotNull(result);
            Assert.Equal(10, result.Length);
            Assert.True(result[0].PositivityRate >= 1);
            Assert.True(result[0].PositivityRate <= 10);
        }

        [Fact]
        public async Task GetAllPositiveAsync_InCorrectPageAndCorrectPageSize_ThrowException()
        {
            //arrange
            PrepareArticleService();

            var minRate = 1;
            var pageSize = -10;
            var pageNumber = 2;


            //assert
            await Assert.ThrowsAnyAsync<Exception>(
                ()=>articleService.GetAllPositiveAsync(minRate, pageSize, pageNumber));
        }
    }
}
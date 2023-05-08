using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Net.Http.Formatting;
using WebApiPeriferia.Handlers;
using Infraestructure.Interfaces;
using Infraestructure.Settings;
using Microsoft.Extensions.Options;
using WebApiPeriferia.Command;
using Microsoft.Extensions.Configuration;
using Infraestructure.Dtos.Response;
using System;

namespace Test
{
    public class MutantTest
    {
        IOptions<InfraestructureSettings> _config;
        public void GlobalPrepare()
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", false)
                    .Build();

            _config = Options.Create(configuration.Get<InfraestructureSettings>());
        }

        [Fact]
        public async void GetStatsSuccess()
        {
            //Arange
            GlobalPrepare();
            GetStatsQueryResponse getStatsQueryResponse = new();
            var _statrepo = new Mock<IStatsRepository>();

            _statrepo.Setup(x => x.GetStats()).ReturnsAsync(getStatsQueryResponse);
            //Act
            var result = await _statrepo.Object.GetStats();
            // Asser
            Assert.NotNull(result);
            Assert.IsType<GetStatsQueryResponse>(result);
            Assert.Equal(getStatsQueryResponse, result);
        }

        [Fact]
        public async void CreateStatsSuccess()
        {
            string stat = "";
            var _statrepo = new Mock<IStatsRepository>();
            bool response = false;
            //Arange
            GlobalPrepare();
            _statrepo.Setup(x => x.InsertStat(stat)).ReturnsAsync(response);
            //Act
            var result = await _statrepo.Object.InsertStat(stat);

            // Assert
            Assert.IsType<bool>(result);
            Assert.Equal(response, result);
        }

        [Fact]
        public async void PostMutantDnaPatternVerticalSuccess()
        {

            //Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTATGT",
                                    "AGAAGG",
                                    "CCCCTA",
                                    "TCACTG"
                                    };


            var _statsRepository = new Mock<IStatsRepository>();


            //
            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);
            //
            PostMutantDnaCommand postMutantDnaCommand = new PostMutantDnaCommand
            {
                dna = dnaRequest
            };

            var response = await resumeBusinessNameCommandHandler.Handle(postMutantDnaCommand, CancellationToken.None);

            // Asert
            response.Should().BeTrue();
        }
        [Fact]
        public async void PostMutantDnaPatternHorizontalSuccess()
        {//Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTATGT",
                                    "AGAAGG",
                                    "CCCCTA",
                                    "TCACTG"
                                    };

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();

            //
            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);
            //
            PostMutantDnaCommand postMutantDnaCommand = new PostMutantDnaCommand
            {
                dna = dnaRequest
            };

            var response = await resumeBusinessNameCommandHandler.Handle(postMutantDnaCommand, CancellationToken.None);
            // Asert
            response.Should().BeTrue();
        }
        [Fact]
        public async void PostMutantDnaPatternObliquousSuccess()
        {//Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTATGT",
                                    "AGAACG",
                                    "CCCATA",
                                    "TCACTG"
                                    };

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();
            //
            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);
            //
            PostMutantDnaCommand postMutantDnaCommand = new PostMutantDnaCommand
            {
                dna = dnaRequest
            };

            var response = await resumeBusinessNameCommandHandler.Handle(postMutantDnaCommand, CancellationToken.None);
            // Asert
            response.Should().BeTrue();

        }

        [Fact]
        public async void PostInvalidDnaError()
        {
            //Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTTATT",
                                    "AGAACG",
                                    "CCCATA",
                                    "TCACTG"
                                    };

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();
            var _configurations = new Mock<IOptions<InfraestructureSettings>>();

            //
            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);
            //
            PostMutantDnaCommand postMutantDnaCommand = new PostMutantDnaCommand
            {
                dna = dnaRequest
            };

            var response = await resumeBusinessNameCommandHandler.Handle(postMutantDnaCommand, CancellationToken.None);
            // Asert
            response.Should().BeFalse();
            //var carga = this.TestClient.PostAsync("/mutant", dnaRequest, new JsonMediaTypeFormatter()).Result;
            //carga.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }

        [Fact]
        public void IsValidDnaSuccess()
        {
            //Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTATGT",
                                    "AGAACG",
                                    "CCCATA",
                                    "TCACTG"
            };

            string[,] matriz = new string[dnaRequest.Length, dnaRequest.First().Length];
            for (int i = 0; i < dnaRequest.Length; i++)
            {
                char[] splitedWord = dnaRequest[i].ToCharArray();
                for (int j = 0; j < splitedWord.Length; j++)
                {
                    matriz[i, j] = splitedWord[j].ToString();
                }
            }

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();

            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);


            var response = resumeBusinessNameCommandHandler.IsValidDna(matriz);
            // Asert
            response.Should().BeTrue();
        }

        [Fact]
        public void IsValidDnaError()
        {
            //Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTTATT",
                                    "AGAACG",
                                    "CCCATA",
                                    "TCACTG"
                                    };

            string[,] matriz = new string[dnaRequest.Length, dnaRequest.First().Length];
            for (int i = 0; i < dnaRequest.Length; i++)
            {
                char[] splitedWord = dnaRequest[i].ToCharArray();
                for (int j = 0; j < splitedWord.Length; j++)
                {
                    matriz[i, j] = splitedWord[j].ToString();
                }
            }

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();

            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);


            var response = resumeBusinessNameCommandHandler.IsValidDna(matriz);
            // Asert
            response.Should().BeFalse();
        }

        [Fact]
        public async void ConvertArrayToMatrizSuccess()
        {
            //Arrange
            GlobalPrepare();
            string[] dnaRequest = {
                                    "ATGCGA",
                                    "CAGTGC",
                                    "TTTATT",
                                    "AGAACG",
                                    "CCCATA",
                                    "TCACTG"
                                    };

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();

            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);


            var response = resumeBusinessNameCommandHandler.ConvertArrayToMatriz(dnaRequest);
            // Asert
            response.Should().NotBeNull();
            response.Should().BeOfType<string[,]>();
        }

        [Fact]
        public async void ConvertArrayToMatrizError()
        {
            //Arrange
            GlobalPrepare();
            string[] dnaRequest = { };

            // Arrange
            var _statsRepository = new Mock<IStatsRepository>();

            var resumeBusinessNameCommandHandler = new PostMutantDnaCommandHandler(_statsRepository.Object, _config);

            // Asert
            Assert.Throws<InvalidOperationException>(() => resumeBusinessNameCommandHandler.ConvertArrayToMatriz(dnaRequest));
        }

    }
}
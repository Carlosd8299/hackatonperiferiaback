using Infraestructure.Interfaces;
using Infraestructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using WebApiPeriferia.Command;

namespace WebApiPeriferia.Handlers
{
    public class PostMutantDnaCommandHandler : IRequestHandler<PostMutantDnaCommand, bool>
    {
        private IStatsRepository _statsRepository;
        private string _mutantDnaType, _humanDnaType;

        public PostMutantDnaCommandHandler(IStatsRepository statsRepository, IOptions<InfraestructureSettings> settings)
        {
            this._statsRepository = statsRepository;
            _mutantDnaType = settings.Value.Constants.MutantDnaType;
            _humanDnaType = settings.Value.Constants.HumanDnaType;
        }

        public async Task<bool> Handle(PostMutantDnaCommand request, CancellationToken cancellationToken)
        {
            var array = request.dna;
            string[,] matriz = ConvertArrayToMatriz(array);
            if (IsValidDna(matriz))
            {
                await _statsRepository.InsertStat(_mutantDnaType);
                return true;
            }
            await _statsRepository.InsertStat(_humanDnaType);
            return false;

        }
        public bool IsValidDna(string[,] matriz)
        {
            bool response = false;
            int horizontalCountSecuency, verticalCountSecuency, diagonalCountSecuency = 1;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                horizontalCountSecuency = 1;
                verticalCountSecuency = 1;

                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    // Para verificar que sea despues de la primera posicion
                    if (j > 0)
                    {
                        // Horizontal
                        if (matriz[i, j].Equals(matriz[i, j - 1]))
                        {
                            horizontalCountSecuency++;
                            if (horizontalCountSecuency == 4)
                            {
                                response = true;
                            }
                        }
                        else
                        {
                            horizontalCountSecuency = 1;
                        }

                        // Vertical
                        if (matriz[j, i].Equals(matriz[j - 1, i]))
                        {
                            verticalCountSecuency++;
                            if (verticalCountSecuency == 4)
                            {
                                response = true;
                            }
                        }
                        else
                        {
                            verticalCountSecuency = 1;
                        }
                        // Diagonal principal
                        if (i == j && i > 0 && j > 0)
                        {
                            if (matriz[j, i].Equals(matriz[j - 1, i - 1]))
                            {
                                diagonalCountSecuency++;
                                if (diagonalCountSecuency == 4)
                                {
                                    response = true;
                                }
                            }
                            else
                            {
                                diagonalCountSecuency = 1;
                            }
                        }
                    }

                    if (response)
                        break;
                }
                if (response)
                    break;
            }
            return response;
        }
        public string[,] ConvertArrayToMatriz(string[] array)
        {
            string[,] matriz = new string[array.Length, array.First().Length];
            for (int i = 0; i < array.Length; i++)
            {
                char[] splitedWord = array[i].ToCharArray();
                for (int j = 0; j < splitedWord.Length; j++)
                {
                    matriz[i, j] = splitedWord[j].ToString();
                }
            }

            return matriz;
        }

    }
}

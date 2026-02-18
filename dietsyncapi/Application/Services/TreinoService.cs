using dietsync.Domain.Entities;
using dietsync.DTOs;

namespace dietsync.Application.Services
{
    public class TreinoService : ITreinoService
    {
        private readonly ITreinoRepository _repository;

        public TreinoService(ITreinoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TreinoResponseDTO>> GetAllAsync(ulong userId)
        {
            var treinos = await _repository.GetAllByUserAsync(userId);

            return treinos.Select(t => new TreinoResponseDTO
            {
                Id = t.Id,
                Data = t.Data,
                Tipo = t.Tipo,
                Exercicios = t.Exercicios,
                Repeticoes = t.Repeticoes,
                Series = t.Series,
                Objetivo = t.Objetivo,
                Duracao = t.Duracao,
                Frequencia = t.Frequencia,
                NomeTreino = t.NomeTreino,
                DiaTreino = t.DiaTreino
            }).ToList();
        }

        public async Task<TreinoResponseDTO?> GetByIdAsync(ulong id, ulong userId)
        {
            var treino = await _repository.GetByIdAsync(id, userId);
            if (treino == null) return null;

            return new TreinoResponseDTO
            {
                Id = treino.Id,
                Data = treino.Data,
                Tipo = treino.Tipo,
                Exercicios = treino.Exercicios,
                Repeticoes = treino.Repeticoes,
                Series = treino.Series,
                Objetivo = treino.Objetivo,
                Duracao = treino.Duracao,
                Frequencia = treino.Frequencia,
                NomeTreino = treino.NomeTreino,
                DiaTreino = treino.DiaTreino
            };
        }

        public async Task<TreinoResponseDTO> CreateAsync(CreateTreinoDTO dto, ulong userId)
        {
            var treino = new Treino
            {
                Data = dto.Data,
                Tipo = dto.Tipo,
                Exercicios = dto.Exercicios,
                Repeticoes = dto.Repeticoes,
                Series = dto.Series,
                Objetivo = dto.Objetivo,
                Duracao = dto.Duracao,
                Frequencia = dto.Frequencia,
                NomeTreino = dto.NomeTreino,
                DiaTreino = dto.DiaTreino,
                FkIdUsuarioTreino = userId
            };

            await _repository.AddAsync(treino);

            return new TreinoResponseDTO
            {
                Id = treino.Id,
                Data = treino.Data,
                Tipo = treino.Tipo,
                Exercicios = treino.Exercicios,
                Repeticoes = treino.Repeticoes,
                Series = treino.Series,
                Objetivo = treino.Objetivo,
                Duracao = treino.Duracao,
                Frequencia = treino.Frequencia,
                NomeTreino = treino.NomeTreino,
                DiaTreino = treino.DiaTreino
            };
        }

        public async Task<bool> UpdateAsync(ulong id, UpdateTreinoDto dto, ulong userId)
        {
            var treino = await _repository.GetByIdAsync(id, userId);
            if (treino == null) return false;

            treino.Data = dto.Data;
            treino.Tipo = dto.Tipo;
            treino.Exercicios = dto.Exercicios;
            treino.Repeticoes = dto.Repeticoes;
            treino.Series = dto.Series;
            treino.Objetivo = dto.Objetivo;
            treino.Duracao = dto.Duracao;
            treino.Frequencia = dto.Frequencia;
            treino.NomeTreino = dto.NomeTreino;
            treino.DiaTreino = dto.DiaTreino;

            await _repository.UpdateAsync(treino);

            return true;
        }

        public async Task<bool> DeleteAsync(ulong id, ulong userId)
        {
            var treino = await _repository.GetByIdAsync(id, userId);
            if (treino == null) return false;

            await _repository.DeleteAsync(treino);
            return true;
        }
    }
}

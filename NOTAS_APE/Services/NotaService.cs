using NOTAS_APE.DTOs;
using NOTAS_APE.Repositories;

namespace NOTAS_APE.Services
{
    public class NotaService
    {
        private readonly INotaRepository _repository;

        public NotaService(INotaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<NotaDTO>> GetNotasDTOAsync()
        {
            var notas = await _repository.GetAllNotasAsync();

            return notas.Select(n => new NotaDTO
            {
                Id = n.Id, // ✅ AGREGA ESTO
                Cedula = n.Estudiante.Cedula,
                Nombre = n.Estudiante.Nombre,
                Apellido = n.Estudiante.Apellido,
                Curso = n.Curso.Nombre,
                CursoId = n.Curso.Id,
                Nota = n.Valor,
                FechaRegistro = n.FechaRegistro
            });
        }



        public async Task<NotaDTO> GetNotaByIdAsync(int id)
        {
            var nota = await _repository.GetNotaByIdAsync(id);
            if (nota == null) return null;

            if (nota.Curso == null)
            {
                Console.WriteLine($"⚠️ Curso null para nota id={id} con curso_id={nota.CursoId}");
            }

            return new NotaDTO
            {
                Id = nota.Id,
                Cedula = nota.Estudiante.Cedula,
                Nombre = nota.Estudiante.Nombre,
                Apellido = nota.Estudiante.Apellido,
                CursoId = nota.Curso?.Id ?? 0,
                Curso = nota.Curso?.Nombre ?? "No asignado",
                Nota = nota.Valor,
                FechaRegistro = nota.FechaRegistro
            };
        }


        public async Task<NotaDTO> CrearNotaAsync(NotaCreateDTO dto)
        {
            var nuevaNota = new Models.Nota
            {
                CedulaEstudiante = dto.Cedula_Es,
                CursoId = dto.Curso_Id,
                Valor = dto.Nota,
                FechaRegistro = DateTime.Now
            };

            var notaCreada = await _repository.CrearNotaAsync(nuevaNota);

            return new NotaDTO
            {
                Id = notaCreada.Id,
                Cedula = notaCreada.CedulaEstudiante,
                CursoId = notaCreada.CursoId,
                Nota = notaCreada.Valor,
                FechaRegistro = notaCreada.FechaRegistro
            };
        }


        public async Task<bool> ActualizarNotaAsync(int id, NotaUpdateDTO dto)
        {
            return await _repository.ActualizarNotaAsync(id, dto.Nota);
        }



    }
}

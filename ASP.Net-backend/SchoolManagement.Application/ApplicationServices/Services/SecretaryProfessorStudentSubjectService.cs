using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.SecretaryProfessorStudentSubject;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class SecretaryProfessorStudentSubjectService : ISecretaryProfessorStudentSubjectService
    {
        private readonly ISecretaryProfessorStudentSubjectRepository _secretaryProfessorStudentSubjectRepository;
        private readonly ISecretaryRepository _secretaryRepository;
        private readonly IProfessorStudentSubjectRepository _professorStudentSubjectRepository;
        private readonly IMapper _mapper;

        public SecretaryProfessorStudentSubjectService(ISecretaryRepository secretaryRepository, ISecretaryProfessorStudentSubjectRepository secretaryProfessorStudentSubjectRepository, IProfessorStudentSubjectRepository professorStudentSubjectRepository, IMapper mapper)
        {
            _secretaryRepository = secretaryRepository;
            _secretaryProfessorStudentSubjectRepository = secretaryProfessorStudentSubjectRepository;
            _professorStudentSubjectRepository = professorStudentSubjectRepository;
            _mapper = mapper;
        }

        public async Task<SecretaryProfessorStudentSubjectResponseDto> CreateSecretaryProfessorStudentSubjectAsync(SecretaryProfessorStudentSubjectDto secretaryProfessorStudentSubjectDto)
        {
            // Mapea el DTO de la relación secretaria-profesor-estudiante-asignatura a la entidad de dominio correspondiente
            var secretaryProfessorStudentSubject = _mapper.Map<Domain.Relations.SecretaryProfessorStudentSubject>(secretaryProfessorStudentSubjectDto);

            // Obtiene la secretaria correspondiente utilizando el repositorio
            secretaryProfessorStudentSubject.Secretary = await _secretaryRepository.GetByIdAsync(secretaryProfessorStudentSubject.IdSec);

            // Obtiene la evaluación correspondiente utilizando el repositorio
            secretaryProfessorStudentSubject.Evaluation = await _professorStudentSubjectRepository.GetByIdAsync(secretaryProfessorStudentSubject.IdProfStudSub);

            // Crea la relación en la base de datos y guarda el resultado
            var savedSecretaryProfessorStudentSubject = await _secretaryProfessorStudentSubjectRepository.CreateAsync(secretaryProfessorStudentSubject);

            // Mapea la entidad guardada de vuelta a un DTO y lo retorna
            return _mapper.Map<SecretaryProfessorStudentSubjectResponseDto>(savedSecretaryProfessorStudentSubject);
        }

        public async Task<IEnumerable<SecretaryProfessorStudentSubjectResponseDto>> ListSecretariesProfessorStudentSubjectsAsync()
        {
            // Obtiene todas las relaciones entre secretarias, profesores, estudiantes y asignaturas desde el repositorio
            var secretaryProfessorStudentSubjects = await _secretaryProfessorStudentSubjectRepository.ListAsync();

            var list = secretaryProfessorStudentSubjects.ToList(); // Convierte a lista para su manipulación
            List<SecretaryProfessorStudentSubjectResponseDto> secretaryProfessorStudentSubjectList = new(); // Inicializa una lista para almacenar los DTOs

            // Itera sobre cada relación y mapea a DTOs
            for (int i = 0; i < list.Count; i++)
            {
                secretaryProfessorStudentSubjectList.Add(_mapper.Map<SecretaryProfessorStudentSubjectResponseDto>(list[i])); // Agrega el DTO a la lista
            }

            return secretaryProfessorStudentSubjectList; // Retorna la lista de DTOs de relaciones entre secretarias, profesores, estudiantes y asignaturas
        }

        public async Task<SecretaryProfessorStudentSubjectResponseDto> UpdateSecretaryProfessorStudentSubjectAsync(SecretaryProfessorStudentSubjectResponseDto secretaryProfessorStudentSubjectDto)
        {
            // Obtiene la relación secretaria-profesor-estudiante-asignatura por su ID desde el DTO
            var secretaryProfessorStudentSubject = await _secretaryRepository.GetByIdAsync(secretaryProfessorStudentSubjectDto.IdSecProfStudSub);

            // Mapea los cambios desde el DTO a la entidad existente
            _mapper.Map(secretaryProfessorStudentSubjectDto, secretaryProfessorStudentSubject);

            // Actualiza la entidad en la base de datos
            await _secretaryRepository.UpdateAsync(secretaryProfessorStudentSubject);

            // Mapea y retorna la entidad actualizada como DTO
            return _mapper.Map<SecretaryProfessorStudentSubjectResponseDto>(secretaryProfessorStudentSubject);
        }


    }
}

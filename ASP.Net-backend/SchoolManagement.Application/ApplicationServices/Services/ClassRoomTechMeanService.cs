using AutoMapper;
using SchoolManagement.Application.ApplicationServices.IServices;
using SchoolManagement.Application.ApplicationServices.Maps_Dto.ClassRoomTechMean;
using SchoolManagement.Infrastructure;
using SchoolManagement.Infrastructure.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.ApplicationServices.Services
{
    public class ClassRoomTechMeanService : IClassRoomTechMeanService
    {
        private readonly IClassRoomTechMeanRepository _classRoomTechMeanRepository;
        private readonly Context _context;
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly ITechnologicalMeansRepository _technologicalMeansRepository;
        private readonly IMapper _mapper;

        public ClassRoomTechMeanService(Context contex, IClassRoomTechMeanRepository classRoomTechMeanRepository, IClassRoomRepository classRoomRepository, ITechnologicalMeansRepository technologicalMeansRepository,IMapper mapper)
        {
            _classRoomTechMeanRepository = classRoomTechMeanRepository;
            _context = contex;
            _classRoomRepository = classRoomRepository;
            _technologicalMeansRepository = technologicalMeansRepository;
            _mapper = mapper;
        }

        public async Task<ClassRoomTechMeanResponseDto> CreateClassRoomTechMeanAsync(ClassRoomTechMeanDto classRoomTechMeanDto)
        {
            var classRoomTechMean = _mapper.Map<Domain.Relations.ClassRoomTechMean>(classRoomTechMeanDto);
            classRoomTechMean.TechnologicalMeans = await _technologicalMeansRepository.GetByIdAsync(classRoomTechMean.IdTechMean);
            classRoomTechMean.ClassRoom = await _classRoomRepository.GetByIdAsync(classRoomTechMean.IdClassRoom);
            var savedClassR = await _classRoomTechMeanRepository.CreateAsync(classRoomTechMean);
            return _mapper.Map<ClassRoomTechMeanResponseDto>(savedClassR);
        }

        public async Task<ClassRoomTechMeanResponseDto> DeleteClassRoomTechMeanByIdAsync(int id)
        {
            var classRoomTeachMean = _classRoomTechMeanRepository.GetById(id);
            var classRoomTechMeanDto = _mapper.Map<ClassRoomTechMeanResponseDto>(classRoomTeachMean);
            await _classRoomTechMeanRepository.DeleteByIdAsync(id);
            return classRoomTechMeanDto;
        }

        public async Task<IEnumerable<ClassRoomTechMeanResponseDto>> ListClassRoomTechMeansAsync()
        {
            var classRoomTechMeans = await _classRoomTechMeanRepository.ListAsync();
            var list = classRoomTechMeans.ToList();
            List<ClassRoomTechMeanResponseDto> classRoomTechMeansList = new();

            for (int i = 0; i < list.Count; i++)
            {
                var temp = _mapper.Map<ClassRoomTechMeanResponseDto>(list[i]);
                temp.TechName = _context.TechnologicalMeans.Where(tm => tm.IdMean == list[i].IdTechMean).FirstOrDefault().NameMean;
                classRoomTechMeansList.Add(temp);
            }

            return classRoomTechMeansList;
        }

        public async Task<ClassRoomTechMeanResponseDto> UpdateClassRoomTechMeanAsync(ClassRoomTechMeanResponseDto classRoomTechMeanDto)
        {
            var classRoomTechMean = await _classRoomTechMeanRepository.GetByIdAsync(classRoomTechMeanDto.IdClassRoomTech);
            _mapper.Map(classRoomTechMeanDto, classRoomTechMean);
            await _classRoomTechMeanRepository.UpdateAsync(classRoomTechMean);
            return _mapper.Map<ClassRoomTechMeanResponseDto>(classRoomTechMean);
        }
    }

}

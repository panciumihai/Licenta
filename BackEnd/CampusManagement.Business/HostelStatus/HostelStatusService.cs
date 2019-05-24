using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Generics;
using CampusManagement.Business.HostelStatus.Models;

namespace CampusManagement.Business.HostelStatus
{
    public class HostelStatusService : IHostelStatusService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;

        private readonly DetailsService<Domain.Entities.HostelStatus, HostelStatusDetailsModel> _detailsService;
        private readonly CreateService<Domain.Entities.HostelStatus, HostelStatusCreateModel> _createService;

        public HostelStatusService(IGenericRepository genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;

            _detailsService = new DetailsService<Domain.Entities.HostelStatus, HostelStatusDetailsModel>
                (genericRepository, mapper);
            _createService = new CreateService<Domain.Entities.HostelStatus, HostelStatusCreateModel>
                (genericRepository, mapper);
        }

        public async Task<HostelStatusDetailsModel> GetAsync(Guid id, params string[] includes)
        {
            return await _detailsService.GetAsync(id, includes);
        }

        public async Task<IEnumerable<HostelStatusDetailsModel>> GetAllAsync(params string[] includes)
        {
            return await _detailsService.GetAllAsync(includes);
        }

        public async Task<Guid> AddAsync(HostelStatusCreateModel entity)
        {
            return await _createService.AddAsync(entity);
        }

        public async Task<IEnumerable<Guid>> AddAsync(IEnumerable<HostelStatusCreateModel> entities)
        {
            return await _createService.AddAsync(entities);
        }

        public async Task<Guid> UpdateAsync(Guid id, HostelStatusCreateModel entity)
        {
            return await _createService.UpdateAsync(id, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _createService.DeleteAsync(id);
        }

        public async Task DeleteAsync(IEnumerable<Guid> ids)
        {
            await _createService.DeleteAsync(ids);
        }

        public async Task<Guid> AddOrUpdate(HostelStatusCreateModel hostelStatusCreateModel)
        {
            var status = await _genericRepository.GetAllAsync<Domain.Entities.HostelStatus>("Hostel");

            var selectedStatus = status.FirstOrDefault(s => s.HostelId == hostelStatusCreateModel.HostelId);

            Guid result;

            if (selectedStatus == null)
            {
                result = await _createService.AddAsync(hostelStatusCreateModel);
            }
            else
            {
                if (hostelStatusCreateModel.MaleSeats != 0)
                {
                    hostelStatusCreateModel.FemaleSeats = selectedStatus.FemaleSeats;
                    hostelStatusCreateModel.ReservedFemaleSeats = selectedStatus.ReservedFemaleSeats;
                }
                else
                {
                    hostelStatusCreateModel.MaleSeats = selectedStatus.MaleSeats;
                    hostelStatusCreateModel.ReservedMaleSeats = selectedStatus.ReservedMaleSeats;
                }

                result = await _createService.UpdateAsync(selectedStatus.Id, hostelStatusCreateModel);
            }

            return result;
        }

        public async Task<IEnumerable<Guid>> AddOrUpdate(IEnumerable<HostelStatusCreateModel> hostelStatusCreateModels)
        {
            var results = new List<Guid>();
            foreach (var status in hostelStatusCreateModels)
            {
                results.Append(await AddOrUpdate(status));
            }
            return results;
        }

        public async Task SeatsDistribution(Guid id)
        {
            //var hostelStatus = await _genericRepository.GetAsync<Domain.Entities.HostelStatus>(id,"Hostel");

            var applications = await _genericRepository.GetAllAsync<Domain.Entities.Application>("Student","Student.Person");

            var students = await _genericRepository.GetAllAsync<Domain.Entities.Student>("Person");

            IList<StudentsYearDistribution> yearDistributions = new List<StudentsYearDistribution>();

            int totalStudents = 0;
            int totalApplications = 0;

            var gender = "F";
            for (int i = 1; i <= 5; ++i)
            {
                var yearDistribution = new StudentsYearDistribution()
                {
                    Gender = gender,
                    Year = i,
                    YearStudentsNumber = students.Count(s => s.Year == i && s.Person.Gender == gender),
                    YearApplicationsNumber = applications.Count(a=>a.Student.Year == i && a.Student.Person.Gender == gender)
                };
                totalStudents += yearDistribution.YearStudentsNumber;
                totalApplications += yearDistribution.YearApplicationsNumber;

                yearDistributions.Add(yearDistribution);
            }

            for (int i = 0; i < 5; ++i)
            {
                yearDistributions[i].YearStudentsPercentage = 
                    (double)yearDistributions[i].YearStudentsNumber / (double)totalStudents;

                yearDistributions[i].YearApplicationsPercentage = 
                    (double)yearDistributions[i].YearApplicationsNumber / (double)totalApplications;

                yearDistributions[i].YearPercentageMean =
                    (yearDistributions[i].YearStudentsPercentage + yearDistributions[i].YearApplicationsPercentage) / 2;
            }

            totalStudents = 0;
            totalApplications = 0;

            gender = "M";
            for (int i = 1; i <= 5; ++i)
            {
                var yearDistribution = new StudentsYearDistribution()
                {
                    Gender = gender,
                    Year = i,
                    YearStudentsNumber = students.Count(s => s.Year == i && s.Person.Gender == gender),
                    YearApplicationsNumber = applications.Count(a => a.Student.Year == i && a.Student.Person.Gender == gender)
                };
                totalStudents += yearDistribution.YearStudentsNumber;
                totalApplications += yearDistribution.YearApplicationsNumber;

                yearDistributions.Add(yearDistribution);
            }

            for (int i = 5; i < 10; ++i)
            {
                yearDistributions[i].YearStudentsPercentage =
                    (double) yearDistributions[i].YearStudentsNumber / (double) totalStudents;

                yearDistributions[i].YearApplicationsPercentage =
                    (double) yearDistributions[i].YearApplicationsNumber / (double) totalApplications;

                yearDistributions[i].YearPercentageMean =
                    (yearDistributions[i].YearStudentsPercentage + yearDistributions[i].YearApplicationsPercentage) / 2;
            }

            gender = "done";

        }

        public IEnumerable<Domain.Entities.Application> SelectApplications(IEnumerable<Domain.Entities.Application> applications, 
            string gender, int year)
        {
            var filteredApplications =
                applications.Where(a => a.Student.Year == year && a.Student.Person.Gender == gender);

            filteredApplications = filteredApplications.OrderByDescending(a=>a.Student.Score).ThenByDescending(a=>a.Student.SecondScore);

            return filteredApplications;
        }
    }
}

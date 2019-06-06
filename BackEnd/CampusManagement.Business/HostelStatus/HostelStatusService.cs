using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CampusManagement.Business.Application;
using CampusManagement.Business.Application.Models;
using CampusManagement.Business.Generics;
using CampusManagement.Business.HostelStatus.Models;
using CampusManagement.Business.Student;
using CampusManagement.Domain.Entities;

namespace CampusManagement.Business.HostelStatus
{
    public class HostelStatusService : IHostelStatusService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IMapper _mapper;
        private readonly IApplicationService _applicationService;
        private readonly IStudentService _studentService;

        private readonly DetailsService<Domain.Entities.HostelStatus, HostelStatusDetailsModel> _detailsService;
        private readonly CreateService<Domain.Entities.HostelStatus, HostelStatusCreateModel> _createService;

        public HostelStatusService(IGenericRepository genericRepository, IMapper mapper, 
            IApplicationService applicationService, IStudentService studentService)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _applicationService = applicationService;
            _studentService = studentService;

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

        public async Task<Guid> UpdateAsync(Guid id, HostelStatusCreateModel entity, params string[] includes)
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
            var status = await _genericRepository.GetAllAsync<Domain.Entities.HostelStatus>("StudentsGroups");

            var selectedStatus = status.FirstOrDefault(s => s.HostelId == hostelStatusCreateModel.HostelId);

            Guid result;

            if (selectedStatus == null)
            {
                result = await _createService.AddAsync(hostelStatusCreateModel);
            }
            else
            {
                if (hostelStatusCreateModel.StudentsGroups == null)
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
                }
                else
                {
                    for (int j=0; j< hostelStatusCreateModel.StudentsGroups.Count; j++)
                    {
                        var group = StudentsGroup.Create(
                            selectedStatus.Id,
                            hostelStatusCreateModel.StudentsGroups.ElementAt(j).Gender,
                            hostelStatusCreateModel.StudentsGroups.ElementAt(j).Year,
                            hostelStatusCreateModel.StudentsGroups.ElementAt(j).Seats,
                            new List<Domain.Entities.Student>());

                        if (hostelStatusCreateModel.StudentsGroups.ElementAt(j).Id == Guid.Empty)
                        {

                            hostelStatusCreateModel.StudentsGroups.ElementAt(j).Id =
                                await _genericRepository.AddAsync(group);
                        }
                        else
                        {
                            group.Id = hostelStatusCreateModel.StudentsGroups.ElementAt(j).Id;
                            hostelStatusCreateModel.StudentsGroups.ElementAt(j).Id =
                                await _genericRepository.UpdateAsync(group);
                        }
                    }
                }

                result = await _createService.UpdateAsync(selectedStatus.Id, hostelStatusCreateModel, "StudentsGroups");
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

        public async Task<IEnumerable<HostelStatusDetailsModel>> GetSeats()
        {
            var hostelsStatus = await _detailsService.GetAllAsync("StudentsGroups");

            var seatsDistributions = await _applicationService.SeatsDistribution();

            for (int i = 0; i < hostelsStatus.Count(); ++i)
            {
                if (hostelsStatus.ElementAt(i).StudentsGroups.Count > 0) continue;

                foreach (var seatsDistribution in seatsDistributions)
                {
                    int seats;
                    if (seatsDistribution.Gender == "M")
                        seats = (int)Math.Round(seatsDistribution.YearPercentageMean * hostelsStatus.ElementAt(i).MaleSeats);
                    else
                        seats = (int)Math.Round(seatsDistribution.YearPercentageMean * hostelsStatus.ElementAt(i).FemaleSeats);

                    var group = StudentsGroup.Create(hostelsStatus.ElementAt(i).Id
                        ,seatsDistribution.Gender, seatsDistribution.Year, seats,
                        new List<Domain.Entities.Student>());

                    group.Id = Guid.Empty;

                    hostelsStatus.ElementAt(i).StudentsGroups.Add(_mapper.Map<StudentsGroupDetailsModel>(group));
                }
            }

            return hostelsStatus;
        }

        public async Task<IEnumerable<HostelStatusDetailsModel>> SeatsAllocationPreview()
        {
            var hostelsStatus = await _detailsService.GetAllAsync("StudentsGroups");

            var applications = await _applicationService.GetAllAsync("Student", "Student.Person", "HostelPreferences");

            var genders = new[] {"F", "M"};
            foreach (var gender in genders)
            {
                for (int year = 1; year <= 5; ++year)
                {
                    var specificApplications = SelectApplications(applications, gender, year).ToList();


                    var availableHostels = hostelsStatus.Select(h => h.HostelId).ToList();

                    foreach (var app in specificApplications)
                    {
                        app.HostelPreferences.RemoveAll(p => !availableHostels.Contains(p.HostelId));

                        foreach (var preference in app.HostelPreferences)
                        {
                            try
                            {
                                var group = hostelsStatus.FirstOrDefault(s => s.HostelId == preference.HostelId)
                                    .StudentsGroups.FirstOrDefault(g => g.Gender == gender && g.Year == year);

                                if (group.Seats <= group.Students.Count)
                                {
                                    availableHostels.Remove(preference.HostelId);
                                    continue;
                                }

                                app.Student.StudentsGroupId = group.Id;

                                hostelsStatus.FirstOrDefault(s => s.HostelId == preference.HostelId)
                                    .StudentsGroups.FirstOrDefault(g => g.Gender == gender && g.Year == year && g.Seats > g.Students.Count).Students.Add(app.Student);

                            }
                            catch (Exception e)
                            {
                                app.Student.StudentsGroupId = null;
                                continue;
                            }

                            break;

                            //var student = _mapper.Map<Domain.Entities.Student>(app.Student);
                            //await _genericRepository.UpdateAsync(student);
                        }
                        if (app.Student.StudentsGroupId == null)
                            break;
                    }
                }
            }

            return hostelsStatus;
        }

        public IOrderedEnumerable<ApplicationDetailsModel> SelectApplications(IEnumerable<ApplicationDetailsModel> applications, 
            string gender, int year)
        {
            var filteredApplications =
                applications.Where(a => a.Student.Year == year && a.Student.Gender == gender);

           return filteredApplications.OrderByDescending(a=>a.Student.Score).ThenByDescending(a=>a.Student.SecondScore);
        }
    }
}

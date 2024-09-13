using EasyResume.API.Models;

namespace EasyResume.API.Abstractions;

public interface IEasyResumeRepository
{
    bool AddPerson(Person person);
    bool AddJob(Person person, Job job, Compagny compagny);
    List<Person> GetPeople();
    List<Person> GetEmployee(Compagny compagny);
    List<Job> GetJobsBetweenDates(Person person, DateTime startDate, DateTime endDate);
}
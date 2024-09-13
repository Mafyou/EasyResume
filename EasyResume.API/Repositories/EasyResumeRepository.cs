using EasyResume.API.Abstractions;
using EasyResume.API.Context;
using EasyResume.API.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EasyResume.API.Repositories;

public class EasyResumeRepository : IEasyResumeRepository
{
    private readonly EasyResumeContext _context;
    private const bool Success = true;
    private const bool Failure = false;
    public EasyResumeRepository(EasyResumeContext context)
    {
        _context = context;
    }
    public bool AddJob(Person person, Job job, Compagny compagny)
    {
        try
        {
            _context.People.Add(person);
            _context.Jobs.Add(job);
            _context.Compagnies.Add(compagny);
            _context.SaveChanges();

            return Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Failure;
    }

    public bool AddPerson(Person person)
    {
        try
        {
            _context.People.Add(person);
            _context.SaveChanges();

            return Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return Failure;
    }

    public List<Person> GetEmployee(Compagny compagny)
    {
        try
        {
            return
                _context.
                People.
                Include(a => a.Jobs).
                ThenInclude(b => b.Compagny).
            Where(x => 
                    x.Jobs.FirstOrDefault(x => x.Compagny.Name == compagny.Name) != null).
                    ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return [Person.Empty];
    }

    public List<Job> GetJobsBetweenDates(Person person, DateTime startDate, DateTime endDate)
    {
        try
        {
            var list = new List<Job>();
            var jobs = _context.Jobs.Where(x => x.StartDate >= startDate && x.EndDate <= endDate).ToList();
            foreach (var job in jobs)
            {
                var personJob = _context.People.Where(j => j.Jobs.Contains(job) != null);
                list.Add(job);
            }
            return list;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return [Job.Empty];
    }

    public List<Person> GetPeople()
    {
        try
        {
            return _context.People.Include(x => x.Jobs).ThenInclude(y => y.Compagny).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return [Person.Empty];
    }
}
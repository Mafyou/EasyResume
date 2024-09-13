using EasyResume.API.Abstractions;
using EasyResume.API.Context;
using EasyResume.API.Models;
using EasyResume.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlite<EasyResumeContext>(builder.Configuration.GetConnectionString("MainConnection"));
builder.Services.AddScoped<IEasyResumeRepository, EasyResumeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// <summary>
/// Sauvegardent une nouvelle personne. Attention, seules les personnes de moins de 150 ans peuvent être enregistrées. Sinon, renvoyez une erreur.
/// </summary>
app.MapPost("/AddPerson", (Person person, IEasyResumeRepository repo) =>
{
    var maxAllowedBirthDate = DateTime.Now.AddYears(-150);
    if (person.BirthDate <= maxAllowedBirthDate)
        return Results.BadRequest("Trop vieux");
    return Results.Ok(repo.AddPerson(person));
});

/// <summary>
/// Permettent d'ajouter un emploi à une personne avec une date de début et de fin d'emploi. Pour le poste actuellement occupé, la date de fin n'est pas obligatoire. Une personne peut avoir plusieurs emplois aux dates qui se chevauchent.
/// </summary>
app.MapPost("/AddJob", async (AddPersonJobWrapper wrapper, IEasyResumeRepository repo) =>
{
    return Results.Ok(repo.AddJob(wrapper.Person, wrapper.Job, wrapper.Compagny));
});

/// <summary>
/// Renvoient toutes les personnes enregistrées par ordre alphabétique, et indiquent également leur âge et leur(s) emploi(s) actuel(s).
/// </summary>
app.MapPost("/GetPersonsWithJobs", (Job job, IEasyResumeRepository repo) =>
{
    var data = repo.GetPeople();
    if (data.FirstOrDefault() == Person.Empty)
        return Results.BadRequest("Personne ici");
    return Results.Ok(data);
});

/// <summary>
/// Renvoient toutes les personnes ayant travaillé pour une entreprise donnée.
/// </summary>
app.MapPost("/GetPersonsByCompany", (Compagny compagny, IEasyResumeRepository repo) =>
{
    var data = repo.GetEmployee(compagny);
    if (data.FirstOrDefault() == Person.Empty)
        return Results.BadRequest("Personne ici");
    return Results.Ok(data);
});

/// <summary>
/// Renvoient tous les emplois d'une personne entre deux plages de dates.
/// </summary>
app.MapPost("/GetPersonJobBetweenDates", (PersonBetween pb, IEasyResumeRepository repo) =>
{
    var data = repo.GetJobsBetweenDates(pb.Person, pb.StartDate, pb.EndDate);
    if (data.FirstOrDefault() == Job.Empty)
        return Results.BadRequest("Personne ici");
    return Results.Ok(data);
});

app.Run();

public record AddPersonJobWrapper(Person Person, Job Job, Compagny Compagny);
public record PersonBetween(Person Person, DateTime StartDate, DateTime EndDate);
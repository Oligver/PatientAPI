using PatientApiConsoleApp.PatientApiConsoleApp;

Console.WriteLine("Welcome to PatientApi console manager");
Console.WriteLine("Would you like to create some test data? Enter 'Y' for starting process");
var key  = Console.ReadLine();
;
if (!string.IsNullOrEmpty(key) && key.ToUpper().Equals("Y"))
{
	var client = new Client("http://localhost:8088/", new HttpClient());
	foreach (var iteration in Enumerable.Range(0, 100))
	{
		await client.CreatePatient(new Patient
		{
			Active = iteration % 2 == 0,
			Gender = iteration % 2 == 0 ? "male" : "female",
			BirthDate = DateTime.SpecifyKind(DateTime.Now.AddDays(-iteration), DateTimeKind.Utc),
			Name = new PatientName
			{
				Family = $"Family{iteration}",
				Given = new[] { $"Patient{iteration}" },
				Use = "official"
			}
		});
	}
    Console.WriteLine("Patients created!");
}

Console.WriteLine("Press any key for exit");
Console.ReadKey();
Add-Migration Init -context Mc2Context -OutputDir Migrations -StartupProject Presentation\Mc2.CrudTest.MVC
Update-Database -StartupProject Presentation\Mc2.CrudTest.MVC
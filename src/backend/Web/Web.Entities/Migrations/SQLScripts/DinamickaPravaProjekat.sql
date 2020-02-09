insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'projekat', 'Projekat', 'Upravljanje projektima', 0)

insert into Moduli ( Sifra, Opis, Naziv, IsDeleted) values (  'projekat', 'Projekat modul', 'Projekat', 0)

insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'projekat', 'Projekat', (select id from PravoGrupe where sifra ='projekat'), (select id from Moduli where sifra ='projekat'), 0)

INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_projekti', N'Projekti', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Projekti', 0)
GO

/*INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
FROM [dbo].[PravoAkcije];*/
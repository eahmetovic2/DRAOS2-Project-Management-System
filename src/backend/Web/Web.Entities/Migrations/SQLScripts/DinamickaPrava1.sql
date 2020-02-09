﻿insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'postavke', 'Postavke sistema', 'Postavke', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'sifarnik', 'Upravljanje šifarnicima sistema', 'Šifarnik', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'logiranje', 'Logiranje akcija u sistemu', 'Logiranje', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'korisnik', 'Upravljanje korisnicima i ulogama', 'Korisnik', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'izvjestaj', 'Izvještaji', 'Izvještaj', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'glavni_meni', 'Glavni meni', 'Glavni meni', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'gornji_meni', 'Gornji meni', 'Gornji meni', 0)
insert into PravoGrupe ( Sifra, Opis, Naziv, IsDeleted) values (  'osnovno', 'Osnovno', 'Osnovno', 0)

insert into Moduli ( Sifra, Opis, Naziv, IsDeleted) values (  'osnovno', 'Osnovni modul', 'Osnovno', 0)
insert into Moduli ( Sifra, Opis, Naziv, IsDeleted) values (  'korisnik', 'Modul za upravljanje Korisnicima', 'Korisnik', 0)
insert into Moduli ( Sifra, Opis, Naziv, IsDeleted) values (  'sifarnik', 'Šifarnik modul', 'Šifarnik', 0)

insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'korisnik', 'Korisnik', (select id from PravoGrupe where sifra ='korisnik'), (select id from Moduli where sifra ='korisnik'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'uloga', 'Uloga', (select id from PravoGrupe where sifra ='korisnik'), (select id from Moduli where sifra ='korisnik'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'dashboard', 'Dashboard', (select id from PravoGrupe where sifra ='osnovno'), (select id from Moduli where sifra ='osnovno'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'izvjestaj', 'Izvještaj', (select id from PravoGrupe where sifra ='izvjestaj'), (select id from Moduli where sifra ='osnovno'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'log', 'Log', (select id from PravoGrupe where sifra ='logiranje'), (select id from Moduli where sifra ='osnovno'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'postavke', 'Postavke', (select id from PravoGrupe where sifra ='postavke'), (select id from Moduli where sifra ='osnovno'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'sifarnik', 'Šifarnik', (select id from PravoGrupe where sifra ='sifarnik'), (select id from Moduli where sifra ='sifarnik'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'glavni_meni_osnovno', 'Osnovno', (select id from PravoGrupe where sifra ='glavni_meni'), (select id from Moduli where sifra ='osnovno'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'gornji_meni', 'Gornji meni', (select id from PravoGrupe where sifra ='gornji_meni'), (select id from Moduli where sifra ='osnovno'), 0)
insert into PravoObjekti ( Sifra, Naziv, PravoGrupaId, ModulId, IsDeleted) values (  'pravo_upravljanja_korisnikom', 'Pravo dodavanja i izmjene korisnika', (select id from PravoGrupe where sifra ='korisnik'), (select id from Moduli where sifra ='osnovno'), 0)

declare @administrator int = (select id from Uloge where sifra = 'administrator')
declare @neodredeno int = (select id from Uloge where sifra = 'neodredeno')

insert into PravaUpravljanjaKorisnicima ( UlogaUpraviteljaId, UlogaUpravljanogId) values (  @administrator, @administrator)
insert into PravaUpravljanjaKorisnicima ( UlogaUpraviteljaId, UlogaUpravljanogId) values (  @administrator, @neodredeno)
insert into PravaUpravljanjaKorisnicima ( UlogaUpraviteljaId, UlogaUpravljanogId) values (  @neodredeno, @neodredeno)

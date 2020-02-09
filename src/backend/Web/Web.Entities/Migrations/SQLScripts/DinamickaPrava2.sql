delete from [dbo].[PravoAkcije]

GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0100000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_dashboard', N'Radna ploèa', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Radna ploèa', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0130000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0130000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_postavke', N'Postavke', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Postavke', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.0170000' AS DateTime2), CAST(N'2018-09-26T11:34:52.0170000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_postavke_sistema', N'Postavke sistema', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Postavke sistema', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1000000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1000000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_sifarnici', N'Šifarnici', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Šifarnici', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1030000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1030000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_log', N'Log', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Log', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1070000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1070000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_log_akcija', N'Akcija', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Akcija', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1070000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1070000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_log_arhiva_promjena', N'Arhiva promjena', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Arhiva promjena', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1100000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1100000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_korisnici', N'Korisnici', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Korisnici', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1130000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1130000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_dodaj_korisnika', N'Dodaj korisnika', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Dodaj korisnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1130000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1130000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_uloge', N'Uloge', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Uloge', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.1500000' AS DateTime2), CAST(N'2018-09-26T11:34:52.1500000' AS DateTime2), N'base', N'base', N'glavni_meni_osnovno_sidebar_izvjestaji', N'Izvještaji', (SELECT Id from PravoObjekti WHERE Sifra='glavni_meni_osnovno'), N'Izvještaji', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.2270000' AS DateTime2), CAST(N'2018-09-26T11:34:52.2270000' AS DateTime2), N'base', N'base',  N'postavke_postavke_pregled', N'Pregled postavki sistema', (SELECT Id from PravoObjekti WHERE Sifra='postavke'), N'Pregled postavki sistema', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.2270000' AS DateTime2), CAST(N'2018-09-26T11:34:52.2270000' AS DateTime2), N'base', N'base',  N'postavke_postavke_izmjena', N'Izmjena postavki sistema', (SELECT Id from PravoObjekti WHERE Sifra='postavke'), N'Izmjena postavki sistema', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.4170000' AS DateTime2), CAST(N'2018-09-26T11:34:52.4170000' AS DateTime2), N'base', N'base',  N'sifarnik_sifarnik_lista', N'Lista elemenata šifarnika', (SELECT Id from PravoObjekti WHERE Sifra='sifarnik'), N'Lista elemenata šifarnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.4200000' AS DateTime2), CAST(N'2018-09-26T11:34:52.4200000' AS DateTime2), N'base', N'base',  N'sifarnik_sifarnik_dodavanje', N'Dodavanje elemenata šifarnika', (SELECT Id from PravoObjekti WHERE Sifra='sifarnik'), N'Dodavanje elemenata šifarnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.4200000' AS DateTime2), CAST(N'2018-09-26T11:34:52.4200000' AS DateTime2), N'base', N'base',  N'sifarnik_sifarnik_izmjena', N'Izmjena elemenata šifarnika', (SELECT Id from PravoObjekti WHERE Sifra='sifarnik'), N'Izmjena elemenata šifarnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.4230000' AS DateTime2), CAST(N'2018-09-26T11:34:52.4230000' AS DateTime2), N'base', N'base',  N'sifarnik_sifarnik_brisanje', N'Brisanje elementa šifarnika', (SELECT Id from PravoObjekti WHERE Sifra='sifarnik'), N'Brisanje elementa šifarnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.4270000' AS DateTime2), CAST(N'2018-09-26T11:34:52.4270000' AS DateTime2), N'base', N'base',  N'sifarnik_sifarnik_pregled', N'Pregled elementa šifarnika', (SELECT Id from PravoObjekti WHERE Sifra='sifarnik'), N'Pregled elementa šifarnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.7930000' AS DateTime2), CAST(N'2018-09-26T11:34:52.7930000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_izmjena_licnih_podataka', N'Izmjena liènih podataka', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Izmjena liènih podataka', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.7970000' AS DateTime2), CAST(N'2018-09-26T11:34:52.7970000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_pregled_licni_podaci', N'Pregled liènih podataka', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Pregled liènih podataka', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8030000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8030000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_promjena_lozinke', N'Promjena lozinke', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Promjena lozinke', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8070000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8070000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_pregled_prijava', N'Pregled prijava na sistem', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Pregled prijava na sistem', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8170000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8170000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_lista', N'Lista korisnika', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Lista korisnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8200000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8200000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_pregled', N'Pregled korisnika', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Pregled korisnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8270000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8270000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_dodavanje', N'Dodavanje korisnika', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Dodavanje korisnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8300000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8300000' AS DateTime2), N'base', N'base',  N'korisnik_korisnik_izmjena', N'Izmjena korisnika', (SELECT Id from PravoObjekti WHERE Sifra='korisnik'), N'Izmjena korisnika', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8470000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8470000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_lista', N'Lista uloga', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Lista uloga', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8500000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8500000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_izmjena', N'Izmjena uloge', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Izmjena uloge', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8290000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8290000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_dodavanje', N'Dodavanje uloge', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Dodavanje uloge', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8600000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8600000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_pregled', N'Pregled uloga', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Pregled uloga', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8630000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8630000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_lista_dozvoljenih_akcija', N'Lista dozvoljenih akcija', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Lista dozvoljenih akcija', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8670000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8670000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_izmjena_dozvoljenih_akcija', N'Izmjena dozvoljenih akcija', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Izmjena dozvoljenih akcija', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8700000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8700000' AS DateTime2), N'base', N'base',  N'korisnik_uloga_lista_grupa_prava', N'Lista grupa prava', (SELECT Id from PravoObjekti WHERE Sifra='uloga'), N'Lista grupa prava', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:52.8800000' AS DateTime2), CAST(N'2018-09-26T11:34:52.8800000' AS DateTime2), N'base', N'base',  N'izvjestaj_izvjestaj_lista', N'Lista izvještaja', (SELECT Id from PravoObjekti WHERE Sifra='izvjestaj'), N'Lista izvještaja', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0000000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0000000' AS DateTime2), N'base', N'base',  N'logiranje_log_lista_akcija', N'Lista log akcija', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Lista log akcija', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0070000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0070000' AS DateTime2), N'base', N'base',  N'logiranje_log_lista_entiteta', N'Lista log entiteta', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Lista log entiteta', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0100000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0100000' AS DateTime2), N'base', N'base',  N'logiranje_log_pregled_entiteta', N'Pregled entiteta', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Pregled entiteta', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0130000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0130000' AS DateTime2), N'base', N'base',  N'logiranje_log_pregled_verzije_entiteta', N'Pregled verzije entiteta', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Pregled verzije entiteta', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0170000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0170000' AS DateTime2), N'base', N'base',  N'logiranje_log_lista_log_level', N'Lista log levela', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Lista log levela', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0200000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0200000' AS DateTime2), N'base', N'base',  N'logiranje_log_lista_log_kategorije', N'Lista log kategorija', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Lista log kategorija', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0230000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0230000' AS DateTime2), N'base', N'base',  N'logiranje_log_lista_vrsta_akcija', N'Lista vrsta akcija', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Lista vrsta akcija', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T11:34:29.0270000' AS DateTime2), CAST(N'2018-09-26T11:34:29.0270000' AS DateTime2), N'base', N'base',  N'logiranje_log_lista_vrsta_entiteta', N'Lista vrsta log entiteta', (SELECT Id from PravoObjekti WHERE Sifra='log'), N'Lista vrsta log entiteta', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T12:56:54.6770000' AS DateTime2), CAST(N'2018-09-26T12:56:54.6770000' AS DateTime2), N'base', N'base',  N'gornji_meni_gornji_meni_promjena_lozinke', N'Promjena lozinke', (SELECT Id from PravoObjekti WHERE Sifra='gornji_meni'), N'Promjena lozinke', 0)
GO
INSERT [dbo].[PravoAkcije] ([DatumIzmjene], [DatumKreiranja], [CreatedBy], [ModifiedBy], [Sifra], [Opis], [PravoObjektId], [Naziv], [IsDeleted]) VALUES (CAST(N'2018-09-26T12:56:54.8570000' AS DateTime2), CAST(N'2018-09-26T12:56:54.8570000' AS DateTime2), N'base', N'base',  N'korisnik_pravo_upravljanja_korisnikom_lista', N'Lista prava upravljanja korisnicima', (SELECT Id from PravoObjekti WHERE Sifra='pravo_upravljanja_korisnikom'), N'Lista prava upravljanja korisnicima', 0)
GO
INSERT INTO [dbo].[PravoAkcijaUloge] ([PravoAkcijaId], [UlogaId] )
SELECT Id, (SELECT Id FROM [dbo].[Uloge] WHERE Sifra = N'administrator')
FROM [dbo].[PravoAkcije];
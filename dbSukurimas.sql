DROP TABLE IF EXISTS Papildoma_paslauga;
DROP TABLE IF EXISTS Apmokejimas;
DROP TABLE IF EXISTS Saskaita;
DROP TABLE IF EXISTS Rezervacija;
DROP TABLE IF EXISTS Iskvietimas;
DROP TABLE IF EXISTS Darbuotojo_atliktas_darbas;
DROP TABLE IF EXISTS Darbo_valandu_fiksavimas;
DROP TABLE IF EXISTS Darbo_grafikas;
DROP TABLE IF EXISTS Registraturos_darbuotojas;
DROP TABLE IF EXISTS Klientas;
DROP TABLE IF EXISTS Kambarys;
DROP TABLE IF EXISTS Darbuotojas;
DROP TABLE IF EXISTS Saskaitos_busenos;
DROP TABLE IF EXISTS Rezervacijos_busena;
DROP TABLE IF EXISTS Kambario_statusai;
DROP TABLE IF EXISTS Darbo_valandos;
DROP TABLE IF EXISTS Apmokejimo_budai;
DROP TABLE IF EXISTS Naudotojas;
DROP TABLE IF EXISTS Darbas;
CREATE TABLE Darbas
(
	pavadinimas varchar (255),
	id_Darbas integer,
	PRIMARY KEY(id_Darbas)
);

CREATE TABLE Naudotojas
(
	prisijungimo_vardas varchar (255),
	slaptazodis varchar (255),
	el_pastas varchar (255),
	id_Naudotojas integer,
	PRIMARY KEY(id_Naudotojas)
);

CREATE TABLE Apmokejimo_budai
(
	id_Apmokejimo_budai integer,
	name char (7) NOT NULL,
	PRIMARY KEY(id_Apmokejimo_budai)
);
INSERT INTO Apmokejimo_budai(id_Apmokejimo_budai, name) VALUES(1, 'kortele');
INSERT INTO Apmokejimo_budai(id_Apmokejimo_budai, name) VALUES(2, 'grynais');

CREATE TABLE Darbo_valandos
(
	id_Darbo_valandos integer,
	name char (11) NOT NULL,
	PRIMARY KEY(id_Darbo_valandos)
);
INSERT INTO Darbo_valandos(id_Darbo_valandos, name) VALUES(1, '6:00-14:00');
INSERT INTO Darbo_valandos(id_Darbo_valandos, name) VALUES(2, '14:00-22:00');
INSERT INTO Darbo_valandos(id_Darbo_valandos, name) VALUES(3, '22:00-6:00');

CREATE TABLE Kambario_statusai
(
	id_Kambario_statusai integer,
	name char (9) NOT NULL,
	PRIMARY KEY(id_Kambario_statusai)
);
INSERT INTO Kambario_statusai(id_Kambario_statusai, name) VALUES(1, 'laisvas');
INSERT INTO Kambario_statusai(id_Kambario_statusai, name) VALUES(2, 'tvarkomas');
INSERT INTO Kambario_statusai(id_Kambario_statusai, name) VALUES(3, 'uzimtas');

CREATE TABLE Rezervacijos_busena
(
	id_Rezervacijos_busena integer,
	name char (11) NOT NULL,
	PRIMARY KEY(id_Rezervacijos_busena)
);
INSERT INTO Rezervacijos_busena(id_Rezervacijos_busena, name) VALUES(1, 'laukiama');
INSERT INTO Rezervacijos_busena(id_Rezervacijos_busena, name) VALUES(2, 'atsaukta');
INSERT INTO Rezervacijos_busena(id_Rezervacijos_busena, name) VALUES(3, 'patvirtinta');

CREATE TABLE Saskaitos_busenos
(
	id_Saskaitos_busenos integer,
	name char (10) NOT NULL,
	PRIMARY KEY(id_Saskaitos_busenos)
);
INSERT INTO Saskaitos_busenos(id_Saskaitos_busenos, name) VALUES(1, 'neapmoketa');
INSERT INTO Saskaitos_busenos(id_Saskaitos_busenos, name) VALUES(2, 'apmoketa');

CREATE TABLE Darbuotojas
(
	vardas varchar (255),
	pavarde varchar (255),
	gimimo_data date,
	id_Naudotojas integer,
	PRIMARY KEY(id_Naudotojas),
	FOREIGN KEY(id_Naudotojas) REFERENCES Naudotojas (id_Naudotojas)
);

CREATE TABLE Kambarys
(
	nr integer,
	statusas integer,
	PRIMARY KEY(nr),
	FOREIGN KEY(statusas) REFERENCES Kambario_statusai (id_Kambario_statusai)
);

CREATE TABLE Klientas
(
	vardas varchar (255),
	pavarde varchar (255),
	tel_nr varchar (255),
	gimimo_data date,
	id_Naudotojas integer,
	PRIMARY KEY(id_Naudotojas),
	FOREIGN KEY(id_Naudotojas) REFERENCES Naudotojas (id_Naudotojas)
);

CREATE TABLE Registraturos_darbuotojas
(
	vardas varchar (255),
	pavarde varchar (255),
	gimimo_data date,
	id_Naudotojas integer,
	PRIMARY KEY(id_Naudotojas),
	FOREIGN KEY(id_Naudotojas) REFERENCES Naudotojas (id_Naudotojas)
);

CREATE TABLE Darbo_grafikas
(
	nuo date,
	iki date,
	pamaina integer,
	id_Darbo_grafikas integer,
	fk_Darbuotojasid_Naudotojas integer NOT NULL,
	PRIMARY KEY(id_Darbo_grafikas),
	FOREIGN KEY(pamaina) REFERENCES Darbo_valandos (id_Darbo_valandos),
	CONSTRAINT Priskirtas FOREIGN KEY(fk_Darbuotojasid_Naudotojas) REFERENCES Darbuotojas (id_Naudotojas)
);

CREATE TABLE Darbo_valandu_fiksavimas
(
	data date,
	atvykimo_laikas time,
	isvykimo_laikas time,
	id_Darbo_valandu_fiksavimas integer,
	fk_Darbuotojasid_Naudotojas integer NOT NULL,
	PRIMARY KEY(id_Darbo_valandu_fiksavimas),
	CONSTRAINT Atvyksta FOREIGN KEY(fk_Darbuotojasid_Naudotojas) REFERENCES Darbuotojas (id_Naudotojas)
);

CREATE TABLE Darbuotojo_atliktas_darbas
(
	atlikimo_data date,
	id_Darbuotojo_atliktas_darbas integer,
	fk_Darbuotojasid_Naudotojas integer NOT NULL,
	fk_Darbasid_Darbas integer NOT NULL,
	PRIMARY KEY(id_Darbuotojo_atliktas_darbas),
	CONSTRAINT Atlieka FOREIGN KEY(fk_Darbuotojasid_Naudotojas) REFERENCES Darbuotojas (id_Naudotojas),
	CONSTRAINT yra FOREIGN KEY(fk_Darbasid_Darbas) REFERENCES Darbas (id_Darbas)
);

CREATE TABLE Iskvietimas
(
	pranesimo_tekstas varchar (255),
	priimtas tinyint,
	id_Iskvietimas integer,
	fk_Registraturos_darbuotojasid_Naudotojas integer NOT NULL,
	fk_darbuotojas integer NULL,
	PRIMARY KEY(id_Iskvietimas),
	CONSTRAINT Issiuncia FOREIGN KEY(fk_Registraturos_darbuotojasid_Naudotojas) REFERENCES Registraturos_darbuotojas (id_Naudotojas),
	CONSTRAINT Gauna FOREIGN KEY(fk_darbuotojas) REFERENCES Darbuotojas (id_Naudotojas)
);

CREATE TABLE Rezervacija
(
	pradzia date,
	pabaiga date,
	rezervacijos_busena integer,
	id_Rezervacija integer,
	fk_Kambarysnr integer NOT NULL,
	fk_Klientasid_Naudotojas integer NOT NULL,
	PRIMARY KEY(id_Rezervacija),
	FOREIGN KEY(rezervacijos_busena) REFERENCES Rezervacijos_busena (id_Rezervacijos_busena),
	CONSTRAINT Rezervuoja FOREIGN KEY(fk_Kambarysnr) REFERENCES Kambarys (nr),
	CONSTRAINT Sukuria FOREIGN KEY(fk_Klientasid_Naudotojas) REFERENCES Klientas (id_Naudotojas)
);

CREATE TABLE Saskaita
(
	saskaitos_nr integer,
	sudarymo_data date,
	suma decimal,
	busena integer,
	fk_Registraturos_darbuotojasid_Naudotojas integer NOT NULL,
	fk_Rezervacijaid_Rezervacija integer NOT NULL,
	fk_Klientasid_Naudotojas integer NOT NULL,
	PRIMARY KEY(saskaitos_nr),
	UNIQUE(fk_Rezervacijaid_Rezervacija),
	FOREIGN KEY(busena) REFERENCES Saskaitos_busenos (id_Saskaitos_busenos),
	CONSTRAINT Sudaro FOREIGN KEY(fk_Registraturos_darbuotojasid_Naudotojas) REFERENCES Registraturos_darbuotojas (id_Naudotojas),
	CONSTRAINT Itraukia FOREIGN KEY(fk_Rezervacijaid_Rezervacija) REFERENCES Rezervacija (id_Rezervacija),
	CONSTRAINT Atitenka FOREIGN KEY(fk_Klientasid_Naudotojas) REFERENCES Klientas (id_Naudotojas)
);

CREATE TABLE Apmokejimas
(
	data date,
	suma decimal,
	apmokejimo_paskirtis varchar (255),
	apmokejimo_budas integer,
	id_Apmokejimas integer,
	fk_Saskaitasaskaitos_nr integer NOT NULL,
	PRIMARY KEY(id_Apmokejimas),
	UNIQUE(fk_Saskaitasaskaitos_nr),
	FOREIGN KEY(apmokejimo_budas) REFERENCES Apmokejimo_budai (id_Apmokejimo_budai),
	CONSTRAINT Priklauso FOREIGN KEY(fk_Saskaitasaskaitos_nr) REFERENCES Saskaita (saskaitos_nr)
);

CREATE TABLE Papildoma_paslauga
(
	paslaugos_pavadinimas varchar (255),
	uzsakymo_data date,
	kaina decimal,
	paslaugos_busena varchar (255),
	id_Papildoma_paslauga integer,
	fk_Saskaitasaskaitos_nr integer NOT NULL,
	fk_Darbuotojasid_Naudotojas integer NOT NULL,
	fk_klientas integer,
	PRIMARY KEY(id_Papildoma_paslauga),
	CONSTRAINT Turi FOREIGN KEY(fk_Saskaitasaskaitos_nr) REFERENCES Saskaita (saskaitos_nr),
	CONSTRAINT Ivykdo FOREIGN KEY(fk_Darbuotojasid_Naudotojas) REFERENCES Darbuotojas (id_Naudotojas),
	CONSTRAINT Uzsakyta FOREIGN KEY(fk_klientas) REFERENCES Klientas (id_Naudotojas)
);

create table kitoblar (
	id SERIAL NOT NULL PRIMARY KEY,
	nomi VARCHAR(60) NOT NULL,
	muallif_id int NOT NULL,
    bolim_id int NOT NULL,
    til_id int NOT NULL,
	narxi int NOT NULL
);
create table mualliflar (
	id SERIAL NOT NULL PRIMARY KEY,
	nomi VARCHAR(60) NOT NULL
);
create table bolimlar (
	id SERIAL NOT NULL PRIMARY KEY,
	nomi VARCHAR(60) NOT NULL
);
create table tillar (
	id SERIAL NOT NULL PRIMARY KEY,
	nomi VARCHAR(60) NOT NULL
);
create table kirim (
	id SERIAL NOT NULL PRIMARY KEY,
	kitob_id int NOT NULL,
	olish_narxi int NOT NULL,
	sana timestamp default current_timestamp,
	soni int NOT NULL
);
create table chiqim (
	id SERIAL NOT NULL PRIMARY KEY,
	kitob_id int NOT NULL,
	sotish_narxi int NOT NULL,
	sana timestamp default current_timestamp,
	soni int NOT NULL
);
create table hisob (
	kitob_id int NOT NULL,
	soni int NOT NULL
);
insert into kirim (kitob_id, olish_narxi, sana, soni) values (1, 20000, '2/24/2023', 10);
insert into kirim (kitob_id, olish_narxi, sana, soni) values (2, 20000, '2/24/2023', 10);
insert into kirim (kitob_id, olish_narxi, sana, soni) values (3, 20000, '2/24/2023', 10);
insert into mualliflar (nomi) values ('Abdulla Qodiriy');
insert into mualliflar (nomi) values ('Pirimqul Qodirov');
insert into bolimlar (nomi) values ('Baadiy adabiyot');
insert into bolimlar (nomi) values ('Tarixiy');
insert into tillar (nomi) values ('Ozbek');
insert into kitoblar (nomi, muallif_id, bolim_id, til_id, Narxi) values ('Mehrobdan chayon', 1, 1, 1, 30000);
insert into kitoblar (nomi, muallif_id, bolim_id, til_id, Narxi) values ('Yulduzli tunlar', 2, 2, 1, 20000);
insert into kitoblar (nomi, muallif_id, bolim_id, til_id, Narxi) values ('Otkan kunlar', 1, 1, 1, 25000);
insert into hisob (kitob_id, soni) values (1, 10);
insert into hisob (kitob_id, soni) values (2, 10);
insert into hisob (kitob_id, soni) values (3, 10);
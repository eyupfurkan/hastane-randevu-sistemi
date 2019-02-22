create database Hastane_Yonetim_ve_Randevu_Sistemi
use Hastane_Yonetim_ve_Randevu_Sistemi

create table Tbl_Yoneticiler(
Yoneticiid tinyint primary key identity(1,1) not null,
YoneticiTC varchar(11),
YoneticiAd varchar(30),
YoneticiSoyad varchar(20),
YoneticiSifre varchar(10)
)

create table Tbl_Branslar(
Bransid tinyint primary key identity(1,1) not null,
Yoneticiid tinyint foreign key references Tbl_Yoneticiler(Yoneticiid),
BransAd varchar(50)
)

create table Tbl_Doktorlar(
Doktorid tinyint primary key identity(1,1) not null,
Bransid tinyint foreign key references Tbl_Branslar(Bransid),
Yoneticiid tinyint foreign key references Tbl_Yoneticiler(Yoneticiid),
DoktorAd varchar(15),
DoktorSoyad varchar(15),
DoktorTC varchar(11),
DoktorSifre varchar(10),
)

create table Tbl_Sekreterler(
Sekreterid tinyint primary key identity(1,1) not null,
SekreterAd varchar(30),
SekreterSoyad varchar(20),
SekreterTC varchar(11),
SekreterSifre varchar(10),
)

create table Tbl_Duyurular(
Duyuruid smallint primary key identity(1,1) not null,
Yoneticiid tinyint foreign key references Tbl_Yoneticiler(Yoneticiid),
Doktorid tinyint foreign key references Tbl_Doktorlar(Doktorid),
Sekreterid tinyint foreign key references Tbl_Sekreterler(Sekreterid),
Duyuru varchar(255)
)

create table Tbl_Hastalar(
Hastaid smallint primary key identity(1,1) not null,
HastaAd varchar(30),
HastaSoyad varchar(20),
HastaTC varchar(11),
HastaTelefon varchar(15),
HastaSifre varchar(10),
HastaCinsiyet varchar(5)
)

create table Tbl_Musteri_Hizmetleri(
MusteriHizmetleriid smallint primary key identity(1,1) not null,
MusteriHizmetleriAd varchar(30),
MusteriHizmetleriSoyad varchar(20),
MusteriHizmetleriTC varchar(11),
MusteriHizmetleriSifre varchar(10)
)

create table Tbl_Randevular(
Randevuid int primary key identity(1,1) not null,
Bransid tinyint foreign key references Tbl_Branslar(Bransid),
Doktorid tinyint foreign key references Tbl_Doktorlar(Doktorid),
Hastaid smallint foreign key references Tbl_Hastalar(Hastaid),
MusteriHizmetleriid smallint foreign key references Tbl_Musteri_Hizmetleri(MusteriHizmetleriid),
RandevuTarih varchar(10),
RandevuSaat varchar(5),
RandevuDurum bit,
Sikayet varchar(255)
)
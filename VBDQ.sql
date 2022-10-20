create database VBDQEntityDB
go
use VBDQEntityDB
go
create table QuyenHan
(
	MaQH int identity(1,1) primary key,
	TenQH nvarchar(max)
)

create table NguoiDung
(
	MaND int identity(1,1) primary key,
	TenND nvarchar(max),
	TenDangNhap nvarchar(max),
	MatKhau nvarchar(max),
	MaQH int not null

	foreign key(MaQH) references QuyenHan(MaQH)
)

create table LoaiSanPham
(
	MaLoaiSP int identity(1,1) primary key,
	TenLoaiSP nvarchar(max),
	LoiNhuan decimal(2,2)
)

create table KhachHang
(
	MaKH int identity(1,1) primary key,
	TenKH nvarchar(max)
)
create table NhaCungCap
(
	MaNCC int identity(1,1) primary key,
	TenNCC nvarchar(max),
	DiaChi nvarchar(max),
	SoDT nvarchar(10)
)
create table SanPham
(
	MaSP nvarchar(128) primary key,
	TenSP nvarchar(max),
	DonGia money default 0,
	MaLoaiSP int not null,

	foreign key(MaLoaiSP) references LoaiSanPham(MaLoaiSP)
)
create table PhieuMua
(
	MaPhieu nvarchar(128) primary key,
	NgayLap DateTime,
	MaNCC int not null

	foreign key (MaNCC) references NhaCungCap(MaNCC)
)

create table ChiTietPhieuMua
(
	MaChiTietPhieu nvarchar(128) primary key,
	MaPhieu nvarchar(128) not null,
	MaSP nvarchar(128) not null,
	SoLuong int,
	
	foreign key (MaPhieu) references PhieuMua(MaPhieu),
	foreign key (MaSP) references SanPham(MaSP)
)
create table PhieuBan
(
	MaPhieu nvarchar(128) primary key,
	NgayLap DateTime,
	MaKH int not null

	foreign key (MaKH) references KhachHang(MaKH)
)

create table ChiTietPhieuBan
(
	MaChiTietPhieu nvarchar(128) primary key,
	MaPhieu nvarchar(128) not null,
	MaSP nvarchar(128) not null,
	SoLuong int,
	
	foreign key (MaPhieu) references PhieuBan(MaPhieu),
	foreign key (MaSP) references SanPham(MaSP)
)
create table DonVi
(
	MaDV int identity(1,1) primary key,
	TenDV nvarchar(max)
)

go
insert into QuyenHan(TenQH) values(N'Admin')
insert into QuyenHan(TenQH) values(N'Nhân viên')

insert into NguoiDung(TenND, TenDangNhap, MatKhau, MaQH) values (N'ADMIN', N'admin', N'db69fc039dcbd2962cb4d28f5891aae1',1)
insert into NguoiDung(TenND, TenDangNhap, MatKhau, MaQH) values (N'An Bùi', N'builehoaian', N'3f9522b5b2bf39683f32e7dd1c4fcca3',2)


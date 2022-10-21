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

create table DonVi
(
	MaDV int identity(1,1) primary key,
	TenDV nvarchar(max)
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
	MaDV int not null,

	foreign key(MaLoaiSP) references LoaiSanPham(MaLoaiSP),
	foreign key(MaDV) references DonVi(MaDV)
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


go
insert into QuyenHan(TenQH) values(N'Admin')
insert into QuyenHan(TenQH) values(N'Nhân viên')

insert into NguoiDung(TenND, TenDangNhap, MatKhau, MaQH) values (N'ADMIN', N'admin', N'db69fc039dcbd2962cb4d28f5891aae1',1)
insert into NguoiDung(TenND, TenDangNhap, MatKhau, MaQH) values (N'An Bùi', N'builehoaian', N'3f9522b5b2bf39683f32e7dd1c4fcca3',2)

insert into LoaiSanPham(TenLoaiSP,LoiNhuan) values (N'Nhẫn',0.22)
insert into LoaiSanPham(TenLoaiSP,LoiNhuan) values (N'Vòng tay',0.3)
insert into LoaiSanPham(TenLoaiSP,LoiNhuan) values (N'Vòng cổ',0.17)
insert into LoaiSanPham(TenLoaiSP,LoiNhuan) values (N'Vàng miếng',0.13)
insert into LoaiSanPham(TenLoaiSP,LoiNhuan) values (N'Đá quý',0.33)

insert into DonVi(TenDV) values (N'cái')
insert into DonVi(TenDV) values (N'chỉ')
insert into DonVi(TenDV) values (N'lượng')
insert into DonVi(TenDV) values (N'carat')

insert into SanPham values (N'1',N'Nhẫn 101',1232122.7,1,1)
insert into SanPham values (N'2',N'Nhẫn 102',322.2,1,1)
insert into SanPham values (N'3',N'Vòng 101',7289.7,2,1)
insert into SanPham values (N'4',N'Vòng 102',32802.2,2,1)
insert into SanPham values (N'5',N'Vòng 103',12122.7,3,1)
insert into SanPham values (N'6',N'Vòng 104',902.2,3,1)
insert into SanPham values (N'7',N'Vàng tây 95',1123,4,2)
insert into SanPham values (N'8',N'Vòng ta 99',1200.2,4,3)
insert into SanPham values (N'9',N'Nhẫn saphire',2313.22,5,4)
insert into SanPham values (N'10',N'Nhẫn ruby',912302.2,5,4)

insert into KhachHang values (N'An Bùi')
insert into KhachHang values (N'Vẫn là An Bùi')
insert into KhachHang values (N'Vẫn là An bùi nhưng mà chữ b không ghi hoa')

insert into NhaCungCap values (N'Bùi An',N'Thủ Đức','0123321123')
insert into NhaCungCap values (N'Vẫn là Bùi An',N'Bình Dương','0321321123')
insert into NhaCungCap values (N'Vẫn là Bùi Ann nhưng có 2 chữ n',N'Đà Nẵng','0423234432')





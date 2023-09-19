Use master
Drop Database QLBanSach
Create Database QLBanSach
Go
Use QLBanSach
Go
--Drop table KhachHang
--Go
Create Table	KhachHang
(
	MaKH	Int	Identity(1,1),
	HoTen	Nvarchar(50)	Not Null,
	TaiKhoan	Varchar(50)	Unique,
	MatKhau	Varchar(50)	Not Null,
	Email	Varchar(100)	Unique,
	DiachiKH	Nvarchar(200),
	DienThoaiKH	Varchar(50),
	NgaySinh	DateTime,
	Constraint	Pk_KhachHang	Primary Key(MaKH)
)
Go
--Drop table ChuDe
--Go
Create Table	ChuDe
(
	MaCD	Int	Identity(1,1),
	TenChuDe	Nvarchar(50)	Not Null,
	Constraint Pk_ChuDe Primary Key(MaCD)
)
Go
--Drop table NhaXuatBan
--Go
Create Table	NhaXuatBan
(
	MaNXB	Int Identity(1,1),
	TenNXB	Nvarchar(50)	Not Null,
	DiaChi	Nvarchar(200),
	DienThoai	Varchar(50),
	Constraint	Pk_NhaXuatBan Primary Key(MaNXB),
)
Go
--Drop table Sach
--Go
Create Table	Sach
(
	MaSach	Int	Identity(1,1),
	TenSach	Nvarchar(100)	Not Null,
	GiaBan	Decimal(18,0)	Check(GiaBan>=0),
	MoTa	Nvarchar(Max),
	AnhBia	Varchar(50),
	NgayCapNhat	DateTime,
	SoLuongTon	Int,
	MaCD	Int,
	MaNXB	Int,
	Constraint	Pk_Sach	Primary Key(MaSach),
	Constraint	Fk_ChuDe	Foreign	Key(MaCD) References	ChuDe(MaCD),
	Constraint	Fk_NhaXuatBan	Foreign Key(MaNXB) References	NhaXuatBan(MaNXB)
)
Go
--Drop table TacGia
--Go
Create Table	TacGia
(
	MaTG	Int Identity(1,1),
	TenTG	Nvarchar(50)	Not Null,
	DiaChi	Nvarchar(100),
	TieuSu	Nvarchar(Max),
	DienThoai Varchar(50),
	Constraint	Pk_TacGia Primary Key(MaTG)
)
Go
--Drop table VietSach
--Go
Create Table	VietSach
(
	MaTG	Int Identity(1,1),
	MaSach	Int,
	VaiTro	Nvarchar(50),
	ViTri	Nvarchar(50),
	Constraint	Fk_TacGia	Foreign	Key(MaTG)	References	TacGia(MaTG)
)
Go
--Drop table DonDatHang
--Go
Create Table	DonDatHang
(
	SoDH	Int Identity(1,1),
	MaKH	Int,
	NgayDH	DateTime,
	NgayGiao	DateTime,
	DaThanhToan	Bit,
	TinhTrangGiaoHang	Bit,
	Constraint	Pk_DonDatHang	Primary Key(SoDH),
	Constraint	Fk_KhachHang	Foreign	Key(MaKH)	References	KhachHang(MaKH)
)
Go
--Drop table ChiTietDatHang
--Go
Create Table	ChiTietDatHang
(
	SoDH	Int,
	MaSach	Int,
	SoLuong	Int	Check(SoLuong>0),
	DonGia Decimal(18,0)	Check(DonGia>=0),
	Constraint	Pk_ChiTietDatHang	Primary Key(SoDH,MaSach),
	Constraint	Fk_DonHang	Foreign	Key(SoDH)	References	DonDatHang(SoDH),
	Constraint	Fk_Sach	Foreign Key(MaSach)	References	Sach(MaSach)
)
Go
--Thêm dữ liệu:
---Chủ đề
	Insert into ChuDe Values (N' Khoa học')
	Insert into ChuDe Values (N' Giáo dục')
	Insert into ChuDe values (N' Chiêm tinh học')
	Insert into ChuDe values (N' Tâm lý con người')
	Insert into ChuDe values (N' Sinh học con người')
	Insert into ChuDe values (N' Công nghệ thông tin')
	Insert into ChuDe values (N' Kỹ năng')
	Insert into ChuDe values (N' Kinh doanh')
	Insert into ChuDe values (N' Phật học')
select *from ChuDe
---Nhà xuất bản
	Insert into NhaXuatBan values (N' Nhà xuất bản trẻ',N' 161 B Lý Chính Thắng - Phường 7 - Quận 3 - Thành phố Hồ Chí Minh','84839316289')
	Insert into NhaXuatBan values (N' Nhà xuất bản văn hóa thông tin',N' ','')
	Insert into NhaXuatBan values (N' Nhà xuất bản đồng nai',N' ','')
	Insert into NhaXuatBan values (N' Nhà xuất bản thế giới',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản khoa học kỹ thuật',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản hồng đức',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản thời đại',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản lao động - xã hội',N' ','')
	Insert into NhaXuatBan values (N' Nhà xuất bản tổng hợp TP. HCM',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản phương đông',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản lao động',N'','')
	Insert into NhaXuatBan values (N' Nhà xuất bản tri thức',N'','')
select *from NhaXuatBan
---Sách
	Insert into Sach values (N' Cái vô hạn trong lòng bàn tay - Từ Bigbang đến giác ngộ',25000,N' Cái Vô Hạn Trong Lòng Bàn Tay là nội dung toàn bộ cuộc nói chuyện giữa nhà vật lý thiên văn nổi tiếng Trịnh Xuân Thuận và một nhà sư, vốn là một nhà khoa học Mỹ, Matthieu Ricard, về bản chất của hiện thực và ý thức qua lăng kính của vật lý cùng các ngành khoa học tự nhiên và Phật giáo.','KH_CVHTLBT.png','01/04/2016',15,1,1)
	Insert into Sach values (N' Một vũ trụ lạ thường',15000,N' Nhà vật lý đoạt giải Nobel Robert B. Laughlin đã lập luận rằng, ta vẫn chưa chạm được đến hồi kết của khoa học, mà thậm chí còn chưa tiến gần được đến đó. Ta mới chỉ đi tới cuối con đường của một lối suy nghĩ nào đấy theo quy giản luận mà thôi. Nếu thay cho việc tìm kiếm những lý thuyết tối hậu, ta hãy xem xét thế giới của những đặc tính đột sinh – có nghĩa là những đặc tính kiểu như tính rắn và hình dạng của một tinh thể, kết quả có được từ sự tổ chức của một số lượng lớn các nguyên tử – thì đột nhiên những điều huyền bí nhất sẽ trở nên gần gũi dễ hiểu như một cục nước đá hay một hạt muối vậy thôi. Và rồi Laughlin còn đi xa hơn nữa: những định luật cơ bản nhất của vật lý học – như các định luật chuyển động của Newton hay cơ học lượng tử – hẳn sẽ phải đột sinh. Các định luật này là những đặc tính của những tập hợp vật chất rộng lớn, và khi độ chính xác của chúng được nghiên cứu một cách thật gần cận, chúng sẽ tan biến thành hư không.','KH_MVTLT.jpg','01/04/2016',17,1,1)
	Insert into Sach values (N' 100 nhà khoa học có ảnh hưởng nhất trên Thế Giới',35000,N' Các nhà khoa học được giới thiệu trong cuốn sách này đều là những nhân vật hàng đầu trong lĩnh vực của mình, họ có ảnh hưởng sâu sắc đối với sự phát triển của thế giới hiện nay. Họ đã mô tả các quy luật vận động, phát hiện ra nguyên lý hoạt động của điện và mô tả kết cấu của nguyên tử. Họ phân giải hóa chất đến nguyên tố, đồng thời đã phát hiện ra sự tồn tại của nguyên tố trên mặt trời, trên mặt trăng, trên các tinh thể và cả nơi sâu nhất trong tâm trái đất.','KH_NKH.jpg','02/04/2016',30,1,2)
	Insert into Sach values (N' Quy luật vũ trụ',75000,N' Từ trước đến nay chưa từng có cuốn sách chiêm tinh nào giống cuốn sách này. Chỉ trong vài giờ đồng hồ đọc sách bạn đã có thể hiểu rõ về chính mình và vận dụng những hướng dẫn của sách vào từng năm trong cuộc sống để tiên đoán được những sự kiện trong tương lai: được mất, bại thành trong mọi sự việc, đồng thời hiểu rõ ta sinh ra từ đâu và ta từ đâu đến, nhiệm vụ của ta là gì...','KH_QLVT.jpg','03/04/2016',5,1,3)
	Insert into Sach values (N' Vũ Trụ',45000,N' Vũ Trụ - Sự Tiến Hóa Của Vũ Trụ, Sự Sống Và Nền Văn Minh Mười ba câu chuyện tuyệt đẹp về Vũ trụ. Qua lời kể trữ tình của Carl Sagan, người đọc sẽ có dịp du hành trong vũ trụ, khám phá thế giới từ vĩ mô của những thiên hà to lớn...','KH_VT.jpg','04/04/2016',9,1,4)
select *from Sach
---Tác giả
	Insert into TacGia values (N' Matthieu richard Trịnh Xuân Thuận','','','')
	Insert into TacGia values (N' Robert B. Laughlin','','','')
	Insert into TacGia values (N' John Simmons','','','')
	Insert into TacGia values (N' Joanna Martine Woolfolk','','','')
	Insert into TacGia values (N' Carl Sagan','','','')
select *from TacGia
---Viết sách
	Insert into VietSach values (1,'','')
	Insert into VietSach values (2,'','')
	Insert into VietSach values (3,'','')
	Insert into VietSach values (4,'','')
	Insert into VietSach values (5,'','')
select *from VietSach
--Dữ liệu cập nhật: Tài khoản quản trị
Create table Admin
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nVarchar(50)
)
Insert into Admin values('admin','123456',N'Triệu Văn Phin')
Insert into Admin values('user','654321','Mr Phin')
select *from Admin
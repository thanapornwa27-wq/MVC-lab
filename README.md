# 7-Elephant MVC Lab

โปรเจกต์ ASP.NET Core MVC .NET 8 ตาม Lab 1: Phone CRUD, Order, รูปสินค้า และแสดง My Order เฉพาะผู้ใช้ที่ Login อยู่

## วิธีรัน
1. ติดตั้ง .NET 8 SDK และ SQL Server LocalDB หรือแก้ DefaultConnection ใน appsettings.json.
2. รัน dotnet restore.
3. วิธีที่แนะนำ: รัน dotnet ef migrations add InitialCreate แล้วรัน dotnet ef database update เพื่อสร้างทั้ง Identity และตารางของแอปแบบครบชุด.
4. ถ้าต้องการทำตาม SQL ในเอกสาร ให้รัน database/schema.sql ก่อน และตรวจสอบ migration ไม่ให้สร้างตาราง Phone และ Order ซ้ำ.
5. รัน dotnet run แล้วเปิด /Phone.
6. Register ผู้ใช้, เพิ่มข้อมูลโทรศัพท์พร้อมชื่อไฟล์รูปใน wwwroot/images, ทดลอง CRUD, เข้า /Phone/Order เพื่อสั่งซื้อ และเปิด /Order เพื่อดูเฉพาะรายการของผู้ใช้ที่ Login อยู่.

## คำสั่ง Scaffold ตามเอกสาร
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 8.0.23
dotnet tool install --global dotnet-aspnet-codegenerator --version 8.0.23

โปรเจกต์นี้มีผลลัพธ์ที่ Scaffold แล้วอยู่ใน Controllers, Models และ Views; จึงสามารถรันต่อได้โดยไม่ต้อง Scaffold ซ้ำ.
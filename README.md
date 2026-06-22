# School Management

C# console ilovasi — o'qituvchilar va o'quvchilarni boshqarish uchun CRUD dastur. OOP prinsiplari, Repository pattern, Generic Service pattern, LINQ va JSON persistence asosida yozilgan.

## Imkoniyatlar

### O'qituvchilar

- ➕ Yangi o'qituvchi qo'shish
- 📋 Barcha o'qituvchilar ro'yxatini ko'rish
- 🔍 ID bo'yicha o'qituvchini topish
- 🔎 Ism bo'yicha qidirish
- 📄 Sahifalab ko'rish (pagination)
- ✏️ O'qituvchi ma'lumotlarini yangilash
- 🗑️ O'qituvchini o'chirish

### O'quvchilar

- ➕ Yangi o'quvchi qo'shish
- 📋 Barcha o'quvchilar ro'yxatini ko'rish
- 🔍 ID bo'yicha o'quvchini topish
- 🔎 Ism bo'yicha qidirish
- 📄 Sahifalab ko'rish (pagination)
- ✏️ O'quvchi ma'lumotlarini yangilash
- 🗑️ O'quvchini o'chirish

## Texnologiyalar va yondashuvlar

- **OOP** — inheritance, abstraction, polymorphism
- **Repository Pattern** — `BaseRepository<T>` orqali ma'lumot saqlash logikasi ajratilgan
- **Generic Service Pattern** — `BaseService<T>` orqali biznes logika umumlashtirilgan
- **LINQ** — qidiruv, filtrlash va pagination
- **JSON persistence** — `System.Text.Json` orqali ma'lumotlarni saqlash

## Loyiha tuzilishi

```
School-Management/
├── Models/
│ ├── Common/
│ │ └── IModel.cs # Umumiy model interface
│ ├── Teachers/
│ │ └── Teacher.cs # Teacher modeli
│ └── Students/
│ └── Student.cs # Student modeli
├── Repositories/
│ ├── Common/
│ │ ├── IRepository.cs # Umumiy repository interface
│ │ └── BaseRepository.cs # Umumiy repository implementatsiyasi
│ ├── Teachers/
│ │ ├── ITeacherRepository.cs # Teacher repository interface
│ │ └── TeacherRepository.cs # Teacher repository implementatsiyasi
│ └── Students/
│ ├── IStudentRepository.cs # Student repository interface
│ └── StudentRepository.cs # Student repository implementatsiyasi
├── Services/
│ ├── Common/
│ │ ├── IGenericService.cs # Umumiy service interface
│ │ └── BaseService.cs # Umumiy service implementatsiyasi
│ ├── Teachers/
│ │ ├── ITeacherService.cs # Teacher service interface
│ │ └── TeacherService.cs # Teacher service implementatsiyasi
│ └── Students/
│ ├── IStudentService.cs # Student service interface
│ └── StudentService.cs # Student service implementatsiyasi
├── Apps/
│ ├── TeacherApp.cs # Teacher UI va menu logikasi
│ └── StudentApp.cs # Student UI va menu logikasi
├── Extensions/
│ └── StudentsExtension.cs # Student extension metodlari
├── Helpers/
│ └── ConsoleHelper.cs # Console yordamchi metodlari
├── App.cs # Asosiy menu logikasi
└── Program.cs # Entry point
```

## DEMO

![Demo](./assets/demo.gif)

## Muallif

Izzatjon Qodirov

---

_Bu loyiha o'qish va amaliyot maqsadida yozilgan._

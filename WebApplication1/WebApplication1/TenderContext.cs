using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1
{
    public class TenderContext : DbContext
    {
        public TenderContext()
            :base("DbConnection")
        { }

        public DbSet<Tender> Tenders { get; set; }
    }

    public class TenderDbInitializer : DropCreateDatabaseAlways<TenderContext>
    {
        protected override void Seed(TenderContext db)
        {
            db.Tenders.Add(new Tender
            {
                Subject_tender = "Нефть",
                Description_tender = "сырье",
                Organizer_tender = "Укренерго",
                Kind_tender = "открытые торги",
                Category_tender = "Нефтепродукты",
                Budget_tender = 1000000,
                Currency_tender = "Доллар",
                Data_tender = new DateTime(2001, 3, 1, 7, 0, 0),
                AcceptFromThe = new DateTime(2001, 3, 1, 7, 0, 0),
                AcceptTo = new DateTime(2004, 1, 2, 3, 4, 5)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Одежда",
                Description_tender = "последняя колекция",
                Organizer_tender = "Киевский метрополитен",
                Kind_tender = "анализ цен",
                Category_tender = "Одежда",
                Budget_tender = 400000,
                Currency_tender = "Доллар",
                Data_tender = new DateTime(2011, 4, 5, 6, 0, 0),
                AcceptFromThe = new DateTime(2011, 4, 5, 6, 0, 0),
                AcceptTo = new DateTime(2014, 1, 2, 3, 4, 5)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Химия",
                Description_tender = "бытовая химия",
                Organizer_tender = "Киевский метрополитен",
                Kind_tender = "Открытые торги",
                Category_tender = "Химическая продукция",
                Budget_tender = 700000,
                Currency_tender = "Доллар",
                Data_tender = new DateTime(2010, 2, 7, 2, 0, 0),
                AcceptFromThe = new DateTime(2010, 2, 7, 2, 0, 0),
                AcceptTo = new DateTime(2014, 2, 6, 2, 3, 4)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Электроэнергия",
                Description_tender = "энергия",
                Organizer_tender = "Энергоатом",
                Kind_tender = "Закрытые торги",
                Category_tender = "Электроэнергия",
                Budget_tender = 900000,
                Currency_tender = "Доллар",
                Data_tender = new DateTime(2009, 1, 3, 5, 0, 0),
                AcceptFromThe = new DateTime(2009, 1, 3, 5, 0, 0),
                AcceptTo = new DateTime(2015, 7, 1, 2, 4, 3)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Топливо",
                Description_tender = "Топливо",
                Organizer_tender = "Укрэнерго",
                Kind_tender = "Закрытые торги",
                Category_tender = "Топливо",
                Budget_tender = 1100000,
                Currency_tender = "Доллар",
                Data_tender = new DateTime(2011, 2, 4, 4, 0, 0),
                AcceptFromThe = new DateTime(2011, 2, 4, 4, 0, 0),
                AcceptTo = new DateTime(2016, 6, 6, 6, 6, 6)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Нефть",
                Description_tender = "сырье",
                Organizer_tender = "Укренерго",
                Kind_tender = "закрытые торги",
                Category_tender = "Нефтепродукты",
                Budget_tender = 400000,
                Currency_tender = "Евро",
                Data_tender = new DateTime(2002, 1, 2, 4, 1, 1),
                AcceptFromThe = new DateTime(2002, 3, 1, 7, 0, 0),
                AcceptTo = new DateTime(2004, 1, 2, 3, 4, 5)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Одежда",
                Description_tender = "последняя колекция",
                Organizer_tender = "Киевский метрополитен",
                Kind_tender = "открытые торги",
                Category_tender = "Одежда",
                Budget_tender = 3300000,
                Currency_tender = "Евро",
                Data_tender = new DateTime(2012, 4, 5, 6, 0, 0),
                AcceptFromThe = new DateTime(2012, 4, 5, 6, 0, 0),
                AcceptTo = new DateTime(2014, 1, 2, 3, 4, 5)
            });

            db.Tenders.Add(new Tender
            {
                Subject_tender = "Химия",
                Description_tender = "бытовая химия",
                Organizer_tender = "Киевский метрополитен",
                Kind_tender = "закрытые торги",
                Category_tender = "Химическая продукция",
                Budget_tender = 230000,
                Currency_tender = "Доллар",
                Data_tender = new DateTime(2010, 2, 7, 2, 0, 0),
                AcceptFromThe = new DateTime(2010, 2, 7, 2, 0, 0),
                AcceptTo = new DateTime(2014, 2, 6, 2, 3, 4)
            });
            base.Seed(db);
        }
    }
}
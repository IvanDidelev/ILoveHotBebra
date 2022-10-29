namespace Bebra
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataGridViewDbLoad();
        }

        private void DataGridViewDbLoad()
        {
            using (AutoModelContext db = new AutoModelContext())
            {
                //Если нет ни одного элемента в бд
                if (!db.Games.Any())
                {
                    //Добавляем и сохраняем данные
                    db.Games.Add( new GameModel { Company = "Supercel", Name = "Brawl Stars", Version="5.2.2.8", Cost = 0} );
                    db.SaveChanges();
                }

                //Отображаем текущее состояние бд 
                dataGridView1.DataSource = db.Games.ToList<GameModel>();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string name = nameTb.Text;
                string company = companyTb.Text;
                string version = versionTb.Text;
                int cost = int.Parse(costTb.Text);

                using (AutoModelContext db = new AutoModelContext())
                {
                    //Создаем модель для добавления в бд
                    GameModel gameToAdd = new GameModel { Company = company, Name = name, Version = version, Cost = cost };

                    //Добавляем и сохраняем данные
                    db.Games.Add(gameToAdd);
                    db.SaveChanges();

                    //Отображаем текущее состояние бд 
                    dataGridView1.DataSource = db.Games.ToList<GameModel>();
                }
            }
            catch {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Попытка привести к нужному типу
                int id = int.Parse(idTb.Text);
                string name = nameTb.Text;
                string company = companyTb.Text;
                string version = versionTb.Text;
                int cost = int.Parse(costTb.Text);

                using (AutoModelContext db = new AutoModelContext())
                {
                    GameModel gameToAdd = new GameModel { Id = id, Company = company, Name = name, Version = version, Cost = cost };
                    try
                    {
                        //Пытаемся обновить данные
                        db.Games.Update(gameToAdd);
                        db.SaveChanges();

                        //Отображаем текущее состояние бд 
                        dataGridView1.DataSource = db.Games.ToList<GameModel>();
                    }
                    catch
                    {
                        //Отображаем сообщение об ошибке
                        MessageBox.Show("Невозможно обновить данных которых еще нет в БД");
                    }
                }
            }
            catch 
            {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(idTb.Text);

                using (AutoModelContext db = new AutoModelContext())
                {
                    //Пытаемся найти категорию на удаление из бд
                    GameModel? gameToRemove = db.Games.FirstOrDefault<GameModel?>(game => game.Id == id);
                    if (gameToRemove != null)
                    {
                        //Удаляем и сохраняем
                        db.Games.Remove(gameToRemove);
                        db.SaveChanges();

                        //Отображаем текущее состояние бд 
                        dataGridView1.DataSource = db.Games.ToList<GameModel>();
                    }
                }
            }
            catch {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }
    }
}

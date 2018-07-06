using HomeFinanceManager.DataModels;
using System.Collections.Generic;

namespace HomeFinanceManager.Database
{
    interface IDatabaseClient
    {
        List<User> SelectAllUsers();

        List<Bill> SelectAllBills();

        List<string> SelectAllLogins();

        User SelectUserByLogin(string login);

        User SelectUserByPassword(string password);

        User SelectUserByIDBalace(int idBalance);

        Bill SelectBill(string login);

        double SelectSumByIdFromBills(int idBalance);

        Bill UpdateOrInsertSumAtBills(string login, double sum);

        User UpdateIdBalanceAtUsers(string login, int idbalance);

        double UpdateSumBySummingAtBills(string login, double sum);

        double UpdateSumBySubtractionAtBills(string login, double sum);

        int DeleteBill(int idbalance);

        User InsertLoginPassword(string login, string password);

    }
}
